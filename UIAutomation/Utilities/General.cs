using System;
using System.Globalization;
using System.Linq;

namespace UIAutomation.Utilities
{
    public class General
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static decimal BalanceDiff(string beforeTrans, string afterTrans) 
        {
            decimal diff = decimal.Parse(beforeTrans, NumberStyles.Number) - decimal.Parse(afterTrans, NumberStyles.Number);
            return diff;
        }

    }
}
