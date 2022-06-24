using System;
using System.Collections.Generic;
namespace MCA.DBClass
{
    public static class Constant
    {
        //public static string[] APPRV_STATUS = { "created", "checked", "approved", "synced", "updated" };

        public const string CREATED = "created";
        public const string CHECKED = "checked";
        public const string APPROVED = "approved";
        public const string SYNCED = "synced";
        public const string UPDATE = "updated";

        public const string SYNC_VERSION = "220";
        public const string RULE = "rule";

        public static string getNewVersion(string version)
        {
            string newVersion = version;
            int length = newVersion.Length;
            int num = length - 3;
            int length2 = length - num;
            string str2 = newVersion.Substring(num, length2);
            str2 = (Int32.Parse(str2) + 1).ToString();
            str2 = str2.PadLeft(4 - (str2.ToString().Length), '0');
            newVersion = SYNC_VERSION + "." + str2;
            return newVersion;
        }

    }
}
