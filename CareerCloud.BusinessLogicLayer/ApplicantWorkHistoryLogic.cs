using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.CompanyName) || string.IsNullOrWhiteSpace(poco.CompanyName) ||
                   poco.CompanyName.Length < 3)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_App_CompName,"" +
                        $"Company name {poco.CompanyName} cannot be null/empty/whitespace and must be atleast 3 characters"));
                }
            }
            if(exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
