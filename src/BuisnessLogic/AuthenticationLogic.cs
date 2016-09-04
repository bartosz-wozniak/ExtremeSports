using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shared.DtoObjects;

namespace BuisnessLogic
{
    /// <summary>
    ///     Class containing Business Logic for Authentication
    /// </summary>
    public static class AuthenticationLogic
    {
        /// <summary>
        ///     Hashes password
        /// </summary>
        /// <param name="password">Unhashed password</param>
        /// <param name="c">Customer whose password it is</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password, DtoCustomer c)
        {
            SHA256 sha256 = new SHA256Managed();
            var saltedPassword = GetSaltedPassword(password, c);
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            var hash = Convert.ToBase64String(bytes);
            return hash;
        }

        private static string GetSaltedPassword(string password, DtoCustomer c)
        {
            return c.Email + password;
        }

        /// <summary>
        ///     Hashes password
        /// </summary>
        /// <param name="password">Unhashed password</param>
        /// <param name="emp">Employeee whose password it is</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password, DtoEmployee emp)
        {
            SHA256 sha256 = new SHA256Managed();
            var saltedPassword = GetSaltedPassword(password, emp);
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            var hash = Convert.ToBase64String(bytes);
            return hash;
        }

        private static string GetSaltedPassword(string password, DtoEmployee emp)
        {
            return emp.Email + password;
        }

        /// <summary>
        ///     Is the token correct
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="login">Login</param>
        /// <returns>Is correct</returns>
        public static bool IsTokenCorrect(string token, string login)
        {
            return token == GetToken(login);
        }

        /// <summary>
        ///     Gets token
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>Token</returns>
        public static string GetToken(string login)
        {
            SHA256 sha256 = new SHA256Managed();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(login));
            var hash = Convert.ToBase64String(bytes);
            return hash;
        }

        /// <summary>
        ///     Authenticates user which attempts to sign in
        /// </summary>
        /// <param name="signInData">Data to autenticate</param>
        /// <returns>String containing information about user's permissions</returns>
        public static async Task<string> AuthenticateCustomer(DtoSignIn signInData)
        {
            var c = await new CustomerLogic().GetCustomer(signInData.EMail);
            if (c == null)
                return "Unauthorized";
            var hash = HashPassword(signInData.Password, c);
            return hash != c.Password ? "Unauthorized" : c.Name + " " + c.Surname;
        }

        /// <summary>
        ///     Authenticates user which attempts to sign in
        /// </summary>
        /// <param name="signInData">Data to autenticate</param>
        /// <returns>String containing information about user's permissions</returns>
        public static async Task<string> Authenticate(DtoSignIn signInData)
        {
            var emp = await new EmployeeLogic().GetEmployee(signInData.EMail);
            if (emp == null)
                return "Unauthorized";
            var hash = HashPassword(signInData.Password, emp);
            if (hash != emp.Password)
                return "Unauthorized";
            return emp.Position.Name == "Admin" ? "Administrator" : "Employee";
        }
    }
}