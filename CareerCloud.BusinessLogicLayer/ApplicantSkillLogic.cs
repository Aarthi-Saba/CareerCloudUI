using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if (poco.StartMonth > 12)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_StartMonth,
                        $"Start Month {poco.StartMonth} cannot be greater than 12"));
                }
                if (poco.EndMonth > 12)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_EndMonth,
                        $"End Month {poco.EndMonth} cannot be greater than 12"));
                }
                if (poco.StartYear < 1900)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_StartYear,
                        $"Start Year {poco.StartYear} cannot be less than 1900"));
                }
                if (poco.EndYear < poco.StartYear)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_EndYear,
                        $"End Year{poco.EndYear} cannot be less than start year"));
                }
            }

            if(exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
