using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if(poco.CurrentSalary < 0)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Negative_Salary,
                        $"Salary value {poco.CurrentSalary} should not be negative"));
                }
                if(poco.CurrentRate < 0)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Negative_Rate,
                         $"Value of Rate {poco.CurrentRate} should not be negative"));
                }
            }
            if(exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
