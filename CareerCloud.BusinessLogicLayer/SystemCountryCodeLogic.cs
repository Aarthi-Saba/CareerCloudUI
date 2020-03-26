using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        protected IDataRepository<SystemCountryCodePoco> _repository;
        private enum Country_Code_Error
        {
            Empty_SysCountry_Code = 900,
            Empty_SysCountry_Name = 901
        }
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
            _repository = repository;
        }
        
        public void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach (SystemCountryCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptionslist.Add(new ValidationException((int)Country_Code_Error.Empty_SysCountry_Code,
                        $"Country Code {poco.Code} cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptionslist.Add(new ValidationException((int)Country_Code_Error.Empty_SysCountry_Name,
                        $"Country Name {poco.Name} cannot be empty"));
                }
            }
            if(exceptionslist.Count > 0 )
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public SystemCountryCodePoco Get(string Code)
        {
            return _repository.GetSingle(c => c.Code == Code);
        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}
