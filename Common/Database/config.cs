using Microsoft.EntityFrameworkCore;
using System;

namespace MDK._01._01_CourseProject.Common.Database
{
    public class Config
    {
        public static readonly string connection = "server=localhost;" + "uid=root;" + "pwd=;" + "database=CarDealership;";
        public static readonly MySqlServerVersion version = new MySqlServerVersion(new Version(8, 0, 11));
    }
}
