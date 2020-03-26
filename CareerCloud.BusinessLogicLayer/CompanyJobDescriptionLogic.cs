using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.JobName))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_JobName,
                        $"Job Name {poco.JobName} cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.JobDescriptions))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_JobDescriptions,
                        $"Job Description {poco.JobDescriptions}  cannot be empty"));
                }
            }
            if (exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }
        public override void Add(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
