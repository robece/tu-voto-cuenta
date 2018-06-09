using System.Text.RegularExpressions;

namespace TuVotoCuenta.Functions.Logic.Classes
{
    public class RegexValidation
    {
        private static string USERNAME_PATTERN = @"^[A-Za-z\d]{6,20}$";

        public static bool IsValidUsername(string username)
        {
            Regex regex = new Regex(USERNAME_PATTERN);
            Match match = regex.Match(username);
            return match.Success;
        }
    }
}