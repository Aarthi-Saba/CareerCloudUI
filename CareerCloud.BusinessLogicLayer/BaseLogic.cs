using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CareerCloud.BusinessLogicLayer
{
    public abstract class BaseLogic<TPoco> where TPoco : IPoco
    {
        protected IDataRepository<TPoco> _repository;
        protected enum ErrorCodes : int
        {
            Invalid_App_MajorLen = 107,
            Invalid_StartDate = 108,
            ComDate_Less_SrtDate = 109,
            AppDate_Grt_Today = 110,
            Negative_Salary = 111,
            Negative_Rate = 112,
            Empty_Resume = 113,
            Invalid_StartMonth = 101,
            Invalid_EndMonth = 102,
            Invalid_StartYear = 103,
            Invalid_EndYear = 104,
            Invalid_App_CompName = 105,
            Invalid_CompDesc_CompName = 106,
            Invalid_Company_Description = 107,
            Invalid_JobName = 300,
            Invalid_JobDescriptions = 301,
            Invalid_Comp_MajorLen = 200,
            Negative_Education_Importance = 201,
            Negative_Skill_Importance = 400,
            Empty_Country_Code = 500,
            Empty_Province = 501,
            Empty_Street_Address = 502,
            Empty_City = 503,
            Empty_Postal_Code = 504,
            Invalid_Company_Website = 600,
            Invalid_Company_ContactPhone = 601,
            Invalid_Password_Length = 700,
            Invalid_Password = 701,
            Blank_Phone_Number = 702,
            Invalid_Phone_Pattern = 703,
            Invalid_Email_Address = 704,
            Empty_FullName = 705,
            Empty_Role = 800
        }
        public BaseLogic(IDataRepository<TPoco> repository)
        {
            _repository = repository;
        }

        protected virtual void Verify(TPoco[] pocos)
        {
            return;
        }

        public virtual TPoco Get(Guid id)
        {
            return _repository.GetSingle(c => c.Id == id);
        }

        public virtual List<TPoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public virtual void Add(TPoco[] pocos)
        {
            foreach (TPoco poco in pocos)
            {
                if (poco.Id == Guid.Empty)
                {
                    poco.Id = Guid.NewGuid();
                }
            }

            _repository.Add(pocos);
        }

        public virtual void Update(TPoco[] pocos)
        {
            _repository.Update(pocos);
        }

        public void Delete(TPoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}