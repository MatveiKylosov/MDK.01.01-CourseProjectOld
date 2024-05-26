using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;

namespace MDK._01._01_CourseProject.Common.Database
{
    public class Config
    {
        public static readonly string connection = "server=127.0.0.1; uid=root; pwd=; database=CarDealership";
        public static readonly MySqlServerVersion version = new MySqlServerVersion(new Version(8, 0, 11));
    }
}
