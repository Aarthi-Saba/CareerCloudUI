using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CountryCode))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Empty_Country_Code,
                        $"Country Code {poco.CountryCode} cannot be blank"));
                }
                if (string.IsNullOrEmpty(poco.Province))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Empty_Province,
                        $"Province Code {poco.Province} cannot be blank"));
                }
                if (string.IsNullOrEmpty(poco.Street))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Empty_Street_Address,
                        $"Street name {poco.Street} cannot be blank"));
                }
                if (string.IsNullOrEmpty(poco.City))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Empty_City,
                        $"City name {poco.City} Code cannot be blank"));
                }
                if (string.IsNullOrEmpty(poco.PostalCode))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Empty_Postal_Code,
                        $"Postal code {poco.PostalCode} cannot be blank"));
                }
            }
            if (exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
