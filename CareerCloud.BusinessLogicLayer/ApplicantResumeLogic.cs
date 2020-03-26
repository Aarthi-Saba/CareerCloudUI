using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantResumeLogic : BaseLogic<ApplicantResumePoco>
    {
        public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repository) : base(repository)
        {
        }
        protected override void Verify(ApplicantResumePoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Resume))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Empty_Resume,
                        $"Mandatory to upload your Resume.{poco.Resume} cannot be empty!"));
                }
            }
            if(exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
