using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using EF.Validation.Model;


namespace EF.Validation
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyValidationTestDb")
        {
        }


        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Person> People { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                        .HasMany(c => c.People)
                        .WithRequired(p => p.Company)
                        .HasForeignKey(p => p.CompanyId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Person>()
                        .HasOptional(x => x.Manager)
                        .WithMany()
                        .HasForeignKey(x => x.ManagerId)
                        .WillCascadeOnDelete(false);
        }


        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            // create our customized result to add a possible DbValidationError to it
            var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

            // We need to check for duplication just in case of adding new entities or modifing existed ones
            if (entityEntry.State == EntityState.Added)
                checkDuplication(entityEntry, result);

            if (entityEntry.State == EntityState.Modified)
                checkDuplication(entityEntry, result);


            // After we did our check to the entity, if we found any duplication, we don't want to continue
            // so we just return our DbEntityValidationResult
            if (!result.IsValid)
                return result;

            // If we didn't find and duplications, then let DbContext do its ordinary checks
            return base.ValidateEntity(entityEntry, items);
        }


        private void checkDuplication(DbEntityEntry entityEntry, DbEntityValidationResult result)
        {
            //we'll get the entity that we want to check from the passed DbEntityEntry
            var entity = entityEntry.Entity;

            // Using reflection to retrive all properties that implement IndexAttribute
            // We'll have with each property the IndexAttribute(s) that it has
            var propertiesDictionary = (from p in entity.GetType().GetProperties()
                                        let attrs = p.GetCustomAttributes(typeof(IndexAttribute), false).Cast<IndexAttribute>()
                                        where attrs.Any(a => a.IsUnique)
                                        select new
                                        {
                                            Property = p,
                                            Attributes = attrs.Where(a => a.IsUnique)
                                        }).ToList();

            // Get Distinct list of all unique indexes that we have
            var indexNames = propertiesDictionary.SelectMany
                (x => x.Attributes).Select(x => x.Name).Distinct();

            // iterate through indexes to check db values
            foreach (var indexName in indexNames)
            {
                // We'll get all the properties that related to this unique index
                // because one index may have combined properties involved in it
                Dictionary<string, PropertyInfo> involvedProperties = propertiesDictionary
                    .Where(p => p.Attributes.Any(a => a.Name == indexName))
                    .ToDictionary(p => p.Property.Name, p => p.Property);

                // Get the DbSet that is representing the Entity table
                DbSet set = Set(entity.GetType());

                //Using dynamic linq to query database for existed rows
                //with the values of the properties that we passed
                var whereClause = "";
                var whereParams = new List<object>();
                var i = 0;
                foreach (var involvedProperty in involvedProperties)
                {
                    if (whereClause.Length > 0)
                        whereClause += " AND ";

                    if (Nullable.GetUnderlyingType(involvedProperty.Value.PropertyType) != null)
                        whereClause += "it." + involvedProperty.Key + ".Value.Equals(@" + i + ")";
                    else
                        whereClause += "it." + involvedProperty.Key + ".Equals(@" + i + ")";

                    whereParams.Add(involvedProperty.Value.GetValue(entity));
                    i += 1;
                }
                // If this is an update, then we should exclude the same record from our query,
                // then we just need to add new condition to tell search for all records but not this one
                if (entityEntry.State == EntityState.Modified)
                {
                    var key = this.GetEntityKey(entity);
                    for (var j = i; j < key.EntityKeyValues.Count() + i; j++)
                    {
                        if (whereClause.Length > 0)
                            whereClause += " AND ";
                        whereClause += "it." + key.EntityKeyValues[j - i].Key + " <> @" + j;
                        whereParams.Add(key.EntityKeyValues[j - i].Value);
                    }
                }

                //If we find any record, we should add DbValidationError with suitable error message
                if (set.Where(whereClause, whereParams.ToArray()).Any())
                {
                    // this logic is just to create the error message
                    var errorMessageBuilder = new StringBuilder()
                        .Append("The ")
                        .Append(involvedProperties.Count > 1 ? "values " : "value ")
                        .Append("of '")
                        .Append(string.Join(", ", involvedProperties.Keys))
                        .Append("' ")
                        .Append(involvedProperties.Count > 1 ? "are " : "is ")
                        .Append("already exist!.");

                    //Add the error to the result and return it
                    result.ValidationErrors.Add(new DbValidationError(indexName, errorMessageBuilder.ToString()));
                }
            }
        }
    }
}