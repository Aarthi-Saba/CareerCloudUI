using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyDescription) || poco.CompanyDescription.Length <= 2)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_Company_Description,
                        $"Company Description {poco.CompanyDescription} cannot be empty or less than 3 characters"));
                }
                if (string.IsNullOrEmpty(poco.CompanyName) || poco.CompanyName.Length < 3)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_CompDesc_CompName,
                        $"Company Name {poco.CompanyName} cannot be less than 3 characters or empty"));
                }
            }
            if(exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

    }
}
