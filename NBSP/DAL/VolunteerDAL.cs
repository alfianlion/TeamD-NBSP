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
            cmd.CommandText = @"SELECT * FROM Volunteer";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Volunteer> vList = new List<Volunteer>();
            while (reader.Read())
            {
                DateTime nulldatetime = new DateTime(0001, 01, 01); 
                vList.Add(
                new Volunteer
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
                   
                    VolunteerID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    EmailAddr = reader.GetString(2),
                    ContactNo = reader.GetInt32(3),
                    Pwd = reader.GetString(4),
                    DOB = !reader.IsDBNull(5) ? reader.GetDateTime(5) : (DateTime?)nulldatetime,
                    Gender = !reader.IsDBNull(6) ? reader.GetString(6) : (string?)"-",
                    Mon = !reader.IsDBNull(7) ? reader.GetBoolean(7) : (bool?)false,
                    Tue = !reader.IsDBNull(8) ? reader.GetBoolean(8) : (bool?)false,
                    Wed = !reader.IsDBNull(9) ? reader.GetBoolean(9) : (bool?)false,
                    Thur = !reader.IsDBNull(10) ? reader.GetBoolean(10) : (bool?)false,
                    Fri = !reader.IsDBNull(11) ? reader.GetBoolean(11) : (bool?)false,
                    Sat = !reader.IsDBNull(12) ? reader.GetBoolean(12) : (bool?)false,
                    Sun = !reader.IsDBNull(13) ? reader.GetBoolean(13) : (bool?)false,
                    
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return vList;
        }
        public bool LoginCheck(string LoginId, string password)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Volunteer 
            WHERE Name = @selectedName AND Pwd = @Pwd";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedName", LoginId);
            cmd.Parameters.AddWithValue("@Pwd", password);

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
        public void Update(Volunteer volunteer, int id)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE Volunteer SET Name=@name,
                                EmailAddr=@email, ContactNo = @telno,Pwd = @password
                                WHERE VolunteerID = @selectedVolunteerID";

            cmd.Parameters.AddWithValue("@name", volunteer.Name);
            cmd.Parameters.AddWithValue("@email", volunteer.EmailAddr);
            cmd.Parameters.AddWithValue("@telno", volunteer.ContactNo);
            cmd.Parameters.AddWithValue("@password", volunteer.Pwd);
            cmd.Parameters.AddWithValue("@selectedVolunteerID", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public void UpdateAfter(Volunteer volunteer, int id)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE Volunteer SET DOB = @dob,Gender=@gender,
                                Mon=@mon,Tue=@tues,Wed=@wed,Thur=@thurs,Fri=@fri,Sat=@sat,Sun=@sun
                                WHERE VolunteerID = @selectedVolunteerID";

            cmd.Parameters.AddWithValue("@dob", volunteer.DOB);
            cmd.Parameters.AddWithValue("@gender", volunteer.Gender);
            cmd.Parameters.AddWithValue("@mon", volunteer.Mon);
            cmd.Parameters.AddWithValue("@tues", volunteer.Tue);
            cmd.Parameters.AddWithValue("@wed", volunteer.Wed);
            cmd.Parameters.AddWithValue("@thurs", volunteer.Thur);
            cmd.Parameters.AddWithValue("@fri", volunteer.Fri);
            cmd.Parameters.AddWithValue("@sat", volunteer.Sat);
            cmd.Parameters.AddWithValue("@sun", volunteer.Sun);
            cmd.Parameters.AddWithValue("@selectedVolunteerID", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public Volunteer CheckAvailable(Volunteer v)
        {
            foreach(string a in v.A)
            {
                if(a == "Mon")
                {
                    v.Mon = true;
                }
                if (a == "Tue")
                {
                    v.Tue = true;
                }
                if (a == "Wed")
                {
                    v.Wed = true;
                }
                if (a == "Thur")
                {
                    v.Thur = true;
                }
                if (a == "Fri")
                {
                    v.Fri = true;
                }
                if (a == "Sat")
                {
                    v.Sat = true;
                }
                if (a == "Sun")
                {
                    v.Sun = true;
                }

            }
            return v;
        }
        public Volunteer GetVolunteerDetail(string volunteerID)
        {
            Volunteer volunteer = new Volunteer();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Volunteer
                                WHERE Name = @selectedName";
            cmd.Parameters.AddWithValue("@selectedName", volunteerID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    volunteer.VolunteerID = reader.GetInt32(0);
                    volunteer.Name = volunteerID;
                    volunteer.EmailAddr = reader.GetString(2);
                    volunteer.ContactNo = reader.GetInt32(3);
                    volunteer.Pwd = reader.GetString(4);
                    volunteer.PasswordConfirm = reader.GetString(4);
                    volunteer.DOB = !reader.IsDBNull(5) ? reader.GetDateTime(5) : DateTime.MinValue;
                    volunteer.Gender = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                    volunteer.Mon = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    volunteer.Tue = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    volunteer.Wed = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    volunteer.Thur = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    volunteer.Fri = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    volunteer.Sat = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    volunteer.Sun = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                }
            }
            reader.Close();
            conn.Close();
            return volunteer;
        }
    }
}
