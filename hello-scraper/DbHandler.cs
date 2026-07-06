using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace hello_scraper
{
    internal class DbHandler
    {
        private readonly string _connectionString;
        public DbHandler (string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SendOffer (IOffer offer)
        {
            string query = @" INSERT INTO offers (major, location, date, salary) VALUES (@Major, @Location, @Date, @Salary);";
            using IDbConnection db = new NpgsqlConnection(_connectionString);
            db.Execute(query, new
            {
                Major = offer.major,
                Location = offer.location,
                Date = offer.date,
                Salary = offer.salary
            });
        }


    }
}
