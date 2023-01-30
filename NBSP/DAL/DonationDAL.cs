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
            cmd.CommandText = @"INSERT INTO Donation (DonationDate, DonationName, Amount, Note,PhoneNo)
                                OUTPUT INSERTED.DonationID
                                VALUES(@date, @name, @amount, @note,@phone)";
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
            if (donation.PhoneNo == null)
            {
                cmd.Parameters.AddWithValue("@phone", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@phone", donation.PhoneNo);
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
        public List<Donation> GetAllDonation()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Donation";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Donation> dList = new List<Donation>();
            while (reader.Read())
            {
                DateTime nulldatetime = new DateTime(0001, 01, 01);
                dList.Add(
                new Donation
                {
                    /*
                    VolunteerID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    EmailAddr = reader.GetString(2),
                    ContactNo = reader.GetInt32(3),
                    Pwd = reader.GetString(4),
                    DOB = !reader.IsDBNull(5) ? reader.GetDateTime(5) : (DateTime?)null,
                    Gender = !reader.IsDBNull(6) ? reader.GetChar(6) : (char?)null,
                    Mon = !reader.IsDBNull(7) ? reader.GetBoolean(7) : (bool?)null,
                    Tue = !reader.IsDBNull(8) ? reader.GetBoolean(8) : (bool?)null,
                    Wed = !reader.IsDBNull(9) ? reader.GetBoolean(9) : (bool?)null,
                    Thur = !reader.IsDBNull(10) ? reader.GetBoolean(10) : (bool?)null,
                    Fri = !reader.IsDBNull(11) ? reader.GetBoolean(11) : (bool?)null,
                    Sat = !reader.IsDBNull(12) ? reader.GetBoolean(12) : (bool?)null,
                    Sun = !reader.IsDBNull(13) ? reader.GetBoolean(13) : (bool?)null,
                    */

                    DonationID = reader.GetInt32(0),
                    DonationDate = reader.GetDateTime(1),
                    Name = !reader.IsDBNull(2) ? reader.GetString(2) : "Unknown",
                    Money = reader.GetDecimal(3),
                    Description = !reader.IsDBNull(4) ? reader.GetString(4) : "Unknown",
                    PhoneNo = !reader.IsDBNull(5) ? reader.GetString(5) : "Unknown",
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return dList;
        }
    }
}
