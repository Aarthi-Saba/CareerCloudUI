using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
        {
        }
        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.Major) || poco.Major.Length < 2)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_Comp_MajorLen,
                        $"Major {poco.Major} should be atleast 2 charcters long"));
                }
                if(poco.Importance < 0)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Negative_Education_Importance,
                        $"Value of Importance {poco.Importance} cannot be less than 0"));
                }
            }
            if (exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
