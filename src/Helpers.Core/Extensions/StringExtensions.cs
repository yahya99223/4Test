using System;
using System.Text.RegularExpressions;

namespace Helpers.Core
{
    public static class StringExtensions
    {
        public static bool IsValidEmail(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static bool IsValidUrl(this string urlString)
        {
            if (string.IsNullOrEmpty(urlString) || string.IsNullOrWhiteSpace(urlString))
                return false;

            if (!urlString.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
                return false;

            return Uri.IsWellFormedUriString(urlString, UriKind.Absolute);
        }

        public static bool IsValidInternationalPhoneNumber(this string number)
        {
            return !(string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number)) && Regex.IsMatch(number, @"^(0{2}|\+)[1-9][0-9]{3,13}$", RegexOptions.IgnoreCase);
        }
    }
}
