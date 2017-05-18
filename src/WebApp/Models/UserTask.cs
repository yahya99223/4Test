using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Documents.Client;

namespace WebApp.Models
{
    public class UserTask
    {
        public UserTask()
        {
            Notes = new List<Note>();
        }

        public Guid id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DonePercent { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Note> Notes { get; set; }
    }

    public class Note
    {
        public string Text { get; set; }
        public DateTime AddingDate { get; set; }
    }

    public class UserTaskStore
    {
        private readonly DocumentClient client;
        private readonly Uri tasksLink;
        private readonly string databaseId = ConfigurationManager.AppSettings["DocumentDbDatabaseId"];
        private const string collectionId = "Tasks";

        public UserTaskStore()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["DocumentDbEndpointUrl"]), ConfigurationManager.AppSettings["DocumentDbAuthKey"]);
            tasksLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
        }

        public IEnumerable<UserTask> GetAll()
        {
            var query = client.CreateDocumentQuery<UserTask>(tasksLink);
            return query.OrderBy(x => x.CreateDate);
        }

        public async Task<UserTask> GetById(Guid id)
        {
            var documentLink = UriFactory.CreateDocumentUri(databaseId, collectionId, id.ToString());
            return await client.ReadDocumentAsync<UserTask>(documentLink);
        }

        public async Task Add(UserTask task)
        {
            await client.CreateDocumentAsync(tasksLink, task);
        }

        public async Task Update(UserTask task)
        {
            var document = await GetById(task.id);

            document.Title = task.Title;
            document.Description = task.Description;
            document.DonePercent = task.DonePercent;
            document.Notes = task.Notes;

            var documentLink = UriFactory.CreateDocumentUri(databaseId, collectionId, document.id.ToString());
            await client.ReplaceDocumentAsync(documentLink, document);
        }

        public async Task Delete(Guid taskId)
        {
            var documentLink = UriFactory.CreateDocumentUri(databaseId, collectionId, taskId.ToString());
            await client.DeleteDocumentAsync(documentLink);
        }
    }
}