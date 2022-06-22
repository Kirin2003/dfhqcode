﻿using Achieve.Common.Helper;
using System;
using System.IO;

namespace Achieve.Common.DB
{
    public class BaseDBConfig
    {
        private static string sqliteConnection = Appsettings.app(new string[] { "AppSettings", "Sqlite", "SqliteConnection" });
        private static bool isSqliteEnabled = Appsettings.app(new string[] { "AppSettings", "Sqlite", "Enabled" }).ObjToBool();

        private static string sqlServerConnection = Appsettings.app(new string[] { "AppSettings", "SqlServer", "SqlServerConnection" });
        private static bool isSqlServerEnabled = Appsettings.app(new string[] { "AppSettings", "SqlServer", "Enabled" }).ObjToBool();

        private static string mySqlConnection = Appsettings.app(new string[] { "AppSettings", "MySql", "MySqlConnection" });
        private static bool isMySqlEnabled = Appsettings.app(new string[] { "AppSettings", "MySql", "Enabled" }).ObjToBool();

        private static string oracleConnection = Appsettings.app(new string[] { "AppSettings", "Oracle", "OracleConnection" });
        private static bool IsOracleEnabled = Appsettings.app(new string[] { "AppSettings", "Oracle", "Enabled" }).ObjToBool();


        public static string ConnectionString => InitConn();
        public static DataBaseType DbType = DataBaseType.SqlServer;


        private static string InitConn()
        {
            if (isSqliteEnabled)
            {
                DbType = DataBaseType.Sqlite;
                return $"DataSource=" + Path.Combine(Environment.CurrentDirectory, sqliteConnection);
            }
            else if (isSqlServerEnabled)
            {
                DbType = DataBaseType.SqlServer;
                return DifDBConnOfSecurity(@"D:\my-file\dbStudentAchieve.txt", @"c:\my-file\dbStudentAchieve.txt", sqlServerConnection);
            }
            else if (isMySqlEnabled)
            {
                DbType = DataBaseType.MySql;
                return DifDBConnOfSecurity(@"D:\my-file\dbStudentAchieve_MySqlConn.txt", @"c:\my-file\dbStudentAchieve_MySqlConn.txt", mySqlConnection);
            }
            else if (IsOracleEnabled)
            {
                DbType = DataBaseType.Oracle;
                return DifDBConnOfSecurity(@"D:\my-file\dbStudentAchieve_OracleConn.txt", @"c:\my-file\dbStudentAchieve_OracleConn.txt", oracleConnection);
            }
            else
            {
                return "server=.;uid=sa;pwd=sa;database=WMBlogDB";
            }

        }
        private static string DifDBConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (Exception) { }
            }

            return conn[conn.Length - 1];
        }

    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
}
