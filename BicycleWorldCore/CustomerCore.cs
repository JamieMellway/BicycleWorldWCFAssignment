using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;

namespace BicycleWorldCore
{
    public static class CustomerCore
    {
        public static bool IsUserAdmin(string username)
        {
            bool result = false;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    Customer customer = database.Customers.FirstOrDefault(c => c.Username == username);

                    if (customer != null && customer.IsAdmin)
                            result = true;
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Is Admin",
                        SystemReason = "Exception authorizing user",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return result;
        }

        public static string[] GetUserRoles(string username)
        {
            List<string> result = new List<string>();

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    Customer customer = database.Customers.FirstOrDefault(c => c.Username == username);

                    if (customer != null) 
                    {
                        result.Add(Role.User.ToString());

                        if (customer.IsAdmin)
                            result.Add(Role.Admin.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Authorizing user",
                        SystemReason = "Exception authorizing user",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return result.ToArray<string>();
        }


        public static bool Authenicate(string username, string password)
        {
            bool result = false;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    Customer customer = database.Customers.FirstOrDefault(c => c.Username == username);
                    if (customer == null) return false;

                    byte[] salt = Convert.FromBase64String(customer.PasswordSalt);
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hash = Convert.FromBase64String(customer.PasswordHash);

                    if (CompareByteArrays(hash, GeneratedSaltedHash(passwordBytes, salt)))
                    { return true; }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Authenicating user",
                        SystemReason = "Exception authenicating user",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }
            return result;
        }

        private static byte[] CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return buff;
        }

        private static byte[] GeneratedSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        private static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length) return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i]) return false;
            }
            return true;
        }

        //public static void CreateInitialUsers()
        //{
        //    CreateUser(new CustomerData()
        //    {
        //        Username = "Fred",
        //        FirstName = "Fred",
        //        LastName = "Flintstone"
        //    }, "Pa$$w0rd");

        //    CreateUser(new CustomerData()
        //    {
        //        Username = "Bert",
        //        FirstName = "Bert",
        //        LastName = "Andernie"
        //    }, "Pa$$w0rd");

        //    Authenicate("Fred", "Pa$$w0rd");

        //}

        private static void CreateUser(CustomerData customer, string password)
        {
            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    byte[] salt = CreateSalt(32);
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                    Customer customerToAdd = new Customer()
                    {
                        Username = customer.Username,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        IsAdmin = false,
                        PasswordSalt = Convert.ToBase64String(salt),
                        PasswordHash = Convert.ToBase64String(GeneratedSaltedHash(passwordBytes, salt)),
                    };

                    database.Customers.Add(customerToAdd);
                    database.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Authenicating user",
                        SystemReason = "Exception authenicating user",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }
        }
    }
}