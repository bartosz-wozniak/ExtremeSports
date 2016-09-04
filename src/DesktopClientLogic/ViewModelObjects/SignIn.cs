namespace DesktopClientLogic.ViewModelObjects
{
    /// <summary>
    ///     Business Class Sign In
    /// </summary>
    public class SignIn
    {
        /// <summary>
        ///     Sign In Email
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        ///     Sign In Password
        /// </summary>
        public string Password { get; set; }

        public override string ToString()
        {
            return EMail;
        }
    }
}