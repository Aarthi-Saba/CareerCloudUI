using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginLogic : BaseLogic<SecurityLoginPoco>
    {
        private const int saltLengthLimit = 10;

        public SecurityLoginLogic(IDataRepository<SecurityLoginPoco> repository) : base(repository)
        {
        }

        public bool Authenticate(string userName, string password)
        {
            SecurityLoginPoco poco = base.GetAll().Where(s => s.Login == userName).FirstOrDefault();
            if (null == poco)
            {
                return false;
            }
            return VerifyHash(password, poco.Password);
        }

        public override void Add(SecurityLoginPoco[] pocos)
        {
            Verify(pocos);
            foreach (SecurityLoginPoco poco in pocos)
            {
                poco.Password = ComputeHash(poco.Password, new byte[saltLengthLimit]);
                poco.Created = DateTime.Now.ToUniversalTime();
                poco.IsLocked = false;
                poco.IsInactive = false;
                poco.ForceChangePassword = true;
                poco.PasswordUpdate = poco.Created.AddDays(30);
            }
            base.Add(pocos);
        }

        public override void Update(SecurityLoginPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(SecurityLoginPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            string[] requiredExtendedPasswordChars = new string[] { "$", "*", "#", "_", "@" };

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Password) || poco.Password.Length < 10)
                {
                    exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Password_Length, 
                        $"Password for SecurityLogin {poco.Id} cannot be null"));
                }
                else if (!requiredExtendedPasswordChars.Any(t => poco.Password.Contains(t)))
                {
                    exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Password, 
                        $"Password for SecurityLogin {poco.Id} must contain an extended character of '$', '*', '#', '_' or '@' ."));
                }

                if (string.IsNullOrEmpty(poco.PhoneNumber))
                {
                    exceptions.Add(new ValidationException((int)ErrorCodes.Blank_Phone_Number,
                        $"PhoneNumber for SecurityLogin {poco.Id} is required"));
                }
                else
                {
                    string[] phoneComponents = poco.PhoneNumber.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Phone_Pattern,
                            $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                    }
                    else
                    {
                        if (phoneComponents[0].Length != 3)
                        {
                            exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Phone_Pattern,
                                $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                        }
                        else if (phoneComponents[1].Length != 3)
                        {
                            exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Phone_Pattern,
                                $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                        }
                        else if (phoneComponents[2].Length != 4)
                        {
                            exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Phone_Pattern, 
                                $"PhoneNumber for SecurityLogin {poco.Id} is not in the required format."));
                        }
                    }
                }

                if (string.IsNullOrEmpty(poco.EmailAddress))
                {
                    exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Email_Address, 
                        $"EmailAddress for SecurityLogin {poco.Id} is not a valid email address format."));
                }
                else if (!Regex.IsMatch(poco.EmailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    exceptions.Add(new ValidationException((int)ErrorCodes.Invalid_Email_Address,
                        $"EmailAddress for SecurityLogin {poco.Id} is not a valid email address format."));
                }

                if (string.IsNullOrEmpty(poco.FullName))
                {
                    exceptions.Add(new ValidationException((int)ErrorCodes.Empty_FullName, 
                        $"FullName for SecurityLogin {poco.Id} is required."));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        private static byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }

        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }

        private string ComputeHash(string plainText, byte[] saltBytes)
        {
            if (saltBytes == null)
            {
                saltBytes = GetSalt();
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
            {
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
            }

            HashAlgorithm hash = new SHA512Managed();
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];
            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashWithSaltBytes[i] = hashBytes[i];
            }
            for (int i = 0; i < saltBytes.Length; i++)
            {
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
            }


            return Convert.ToBase64String(hashWithSaltBytes);
        }
        private bool VerifyHash(string plainText, string hashValue)
        {
            const int hashSizeInBytes = 64;

            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
            string expectedHashString = ComputeHash(plainText, saltBytes);
            return (hashValue == expectedHashString);
        }

    }
}
