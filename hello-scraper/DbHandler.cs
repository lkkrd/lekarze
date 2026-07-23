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

        public void SendOffer (Offer offer)
        {
            string query = @" INSERT INTO offers (id, major, location, salary, website) VALUES (@id, @Major, @Location, @Salary, @Website);";
            using IDbConnection db = new NpgsqlConnection(_connectionString);
            try
            {
                db.Execute(query, new
            {
                id = offer.id,
                Major = offer.major,
                Location = offer.location,
                Date = offer.date,
                Salary = offer.salary,
                Website = offer.website
            });

            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.ToString ());
            }
        }


    }
}
