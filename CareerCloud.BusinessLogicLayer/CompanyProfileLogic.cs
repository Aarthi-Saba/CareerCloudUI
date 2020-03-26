using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            string[] WebExtensions = new string[] { ".ca", ".com", ".biz" };
            bool flag = false;
            foreach(var poco in pocos)
            {
                foreach(string extn in WebExtensions)
                {
                    if (string.IsNullOrEmpty(poco.CompanyWebsite))
                    {
                        break;
                    }
                    else if (poco.CompanyWebsite.Length > 3 && poco.CompanyWebsite.EndsWith(extn))
                    {
                        flag = true;
                    }
                }

                if (!flag)
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_Company_Website,
                        $"Company website {poco.CompanyWebsite} should end with any of these domain : .ca , .com , .biz"));
                }
                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_Company_ContactPhone,
                        $"Company Phone number {poco.ContactPhone} cannot be empty"));
                }
                else if(!Regex.IsMatch(poco.ContactPhone, @"^([0-9]{3})\-([0-9]{3})\-([0-9]{4})"))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Invalid_Company_ContactPhone,
                        $"Company Phone number {poco.ContactPhone}  pattern should be : xxx-xxx-xxxx"));
                }
            }
            if(exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
