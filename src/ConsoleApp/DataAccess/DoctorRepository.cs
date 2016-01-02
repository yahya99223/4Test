using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using Infrastructure;

namespace ConsoleApp.DataAccess
{
    public class DoctorRepository
    {
        private static IList<Doctor> inMemoryList = new EnlistmentNotificationList<Doctor>();


        public DoctorRepository()
        {
        }


        public IList<Doctor> GetAll()
        {
            return inMemoryList;
        }


        public Doctor GetById(int id)
        {
            return inMemoryList.FirstOrDefault(x => x.Id == id);
        }


        public void Add(Doctor doctor)
        {
            if (inMemoryList.Any(u => u.Id == doctor.Id || u.Name == doctor.Name))
                throw new Exception("Duplicated");
            inMemoryList.Add(doctor);
        }


        public void Update(Doctor doctor)
        {
            var oldItem = GetById(doctor.Id);
            if (oldItem != null)
            {
                inMemoryList.Remove(oldItem);
                Add(doctor);
            }
            else
                throw new Exception("Not Found");
        }


        public void Delete(int id)
        {
            var oldItem = GetById(id);
            if (oldItem != null)
                inMemoryList.Remove(oldItem);
            else
                throw new Exception("Not Found");
        }
    }
}