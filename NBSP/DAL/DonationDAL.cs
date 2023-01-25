using Microsoft.Extensions.Configuration;
using NBSP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.DAL
{
    public class DonationDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public DonationDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "NBSPConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        public int Add(Donation donation)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Donation (DonationDate, Name, Amount, Note)
                                OUTPUT INSERTED.DonationID
                                VALUES(@date, @name, @amount, @note)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            if (donation.Name == null)
            {
                cmd.Parameters.AddWithValue("@name", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@name", donation.Name);
            }
            cmd.Parameters.AddWithValue("@amount", donation.Money);
            if (donation.Description == null)
            {
                cmd.Parameters.AddWithValue("@note", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@note", donation.Description);
            }
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            donation.DonationID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return donation.DonationID;
        }
    }
}
