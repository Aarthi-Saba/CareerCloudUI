using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic : BaseLogic<SecurityRolePoco>
    {
        public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository) : base(repository)
        {
        }
        protected override void Verify(SecurityRolePoco[] pocos)
        {
            List<ValidationException> exceptionslist = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Role))
                {
                    exceptionslist.Add(new ValidationException((int)ErrorCodes.Empty_Role,
                        $"Security Role {poco.Role} cannot be empty"));
                }
            }
            if (exceptionslist.Count > 0)
            {
                throw new AggregateException(exceptionslist);
            }
        }
        public override void Add(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
