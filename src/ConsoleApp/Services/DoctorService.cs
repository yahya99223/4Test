using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.DataAccess;
using ConsoleApp.Model;
using Infrastructure;

namespace ConsoleApp.Services
{
    public class DoctorService
    {
        private readonly bool isSubService;
        private readonly UnitOfWork unitOfWork;


        public DoctorService(bool isSubService, UnitOfWork unitOfWork = null)
        {
            this.isSubService = isSubService;
            this.unitOfWork = unitOfWork ?? new UnitOfWork();
        }


        public IList<Doctor> GetAll()
        {
            return unitOfWork.DoctorRepository.GetAll();
        }


        public Doctor GetById(int id)
        {
            return unitOfWork.DoctorRepository.GetById(id);
        }


        public void Add(Doctor doctor)
        {
            unitOfWork.DoctorRepository.Add(doctor);
            var userService = new UserService(true, unitOfWork);
            userService.Add(doctor);
            if (!isSubService)
                unitOfWork.Commit();
        }


        public void Update(Doctor doctor)
        {
            unitOfWork.DoctorRepository.Update(doctor);

            var userService = new UserService(true, unitOfWork);
            userService.Update(doctor);

            if (!isSubService)
                unitOfWork.Commit();
        }


        public void Delete(int id)
        {
            unitOfWork.DoctorRepository.Delete(id);

            var userService = new UserService(true, unitOfWork);
            userService.Delete(id);

            if (!isSubService)
                unitOfWork.Commit();
        }
    }
}