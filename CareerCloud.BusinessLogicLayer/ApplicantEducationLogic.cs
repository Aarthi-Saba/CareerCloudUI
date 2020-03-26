using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            
            foreach(ApplicantEducationPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.Major) || (poco.Major.Length) < 3)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_App_MajorLen,
                         $"Major of Applicant {poco.Major} cannot be blank or less than 3 characters"));
                }
                if(poco.StartDate > DateTime.Now)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_StartDate,
                        $"Start Date {poco.StartDate} should be greater than Current Date"));
                }
                if(poco.CompletionDate < poco.StartDate)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.ComDate_Less_SrtDate,
                        $"Completion Date {poco.CompletionDate} cannot be earlier than Start Date {poco.StartDate}"));
                }
            }
            if(exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
