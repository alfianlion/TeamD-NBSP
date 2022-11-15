using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NBSP.Models;

namespace NBSP.DAL
{
    public class VolunteerDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public VolunteerDAL()
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
        public List<Volunteer> GetAllVolunteer()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Volunteer ORDER BY VolunteerID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Volunteer> vList = new List<Volunteer>();
            while (reader.Read())
            {
                vList.Add(
                new Volunteer
                {
                    VolunteerID = reader.GetInt32(0), 
                    Name = reader.GetString(1), 
                    EmailAddr = reader.GetString(2), 
                    ContactNo = reader.GetInt32(3), 
                    Pwd = reader.GetString(4), 
                    DOB = reader.GetDateTime(5), 
                    Gender = reader.GetChar(6), 
                    Mon = !reader.IsDBNull(7) ?
                reader.GetBoolean(7) : (bool?)null,
                    Tue = !reader.IsDBNull(8) ?
                reader.GetBoolean(7) : (bool?)null,
                    Wed = !reader.IsDBNull(9) ?
                reader.GetBoolean(7) : (bool?)null,
                    Thur = !reader.IsDBNull(10) ?
                reader.GetBoolean(7) : (bool?)null,
                    Fri = !reader.IsDBNull(11) ?
                reader.GetBoolean(7) : (bool?)null,
                    Sat = !reader.IsDBNull(12) ?
                reader.GetBoolean(7) : (bool?)null,
                    Sun = !reader.IsDBNull(13) ?
                reader.GetBoolean(7) : (bool?)null,
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return vList;
        }
        public bool LoginCheck(string loginId, string password)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Customer 
            WHERE MemberID = @selectedMemberID AND MPassword = @mPassword";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedMemberID", loginId);
            cmd.Parameters.AddWithValue("@mPassword", password);

            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Close();
                conn.Close();
                return true;
            }
            else
            {
                reader.Close();
                conn.Close();
                return false;
            }
        }

        public int Add(Volunteer volunteer)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Volunteer (Name, EmailAddr, ContactNo, Pwd) 
        OUTPUT INSERTED.VolunteerID 
        VALUES(@name, @email, @contact, @pwd)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@name", volunteer.Name);
            cmd.Parameters.AddWithValue("@email", volunteer.EmailAddr);
            cmd.Parameters.AddWithValue("@contact", volunteer.ContactNo);
            cmd.Parameters.AddWithValue("@pwd", volunteer.Pwd);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            volunteer.VolunteerID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return volunteer.VolunteerID;
        }
    }
}
