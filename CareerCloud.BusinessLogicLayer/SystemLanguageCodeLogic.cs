using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        private enum Language_Error_Code
        {
            Empty_Lang_ID = 1000,
            Empty_Lang_Name = 1001,
            Empty_Native_Name = 1002
        }
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }

        public void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptionslist.Add(new ValidationException((int)Language_Error_Code.Empty_Lang_ID,
                        $"Language ID {poco.LanguageID} cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptionslist.Add(new ValidationException((int)Language_Error_Code.Empty_Lang_Name,
                        $"Language Name {poco.Name} cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptionslist.Add(new ValidationException((int)Language_Error_Code.Empty_Native_Name,
                        $"Language Native Name {poco.NativeName} cannot be empty"));

                }
            }
            if (exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public SystemLanguageCodePoco Get(string LID)
        {
            return _repository.GetSingle(c => c.LanguageID == LID);
        }

        public List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}
