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
    public class JobDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public JobDAL()
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
        public List<Job> GetAllJob()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Job";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Job> jList = new List<Job>();
            while (reader.Read())
            {
                DateTime nulldatetime = new DateTime(0001, 01, 01);
                jList.Add(
                new Job
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

                    JobID = reader.GetInt32(0),
                    JobName = reader.GetString(1),
                    Summary = !reader.IsDBNull(2) ? reader.GetString(2) : "Unknown",
                 Description= !reader.IsDBNull(3) ? reader.GetString(3) : "Unknown",
                Company = reader.GetString(4),
                    Salary = reader.GetDecimal(5),
                    PhoneNo = !reader.IsDBNull(6) ? reader.GetString(6) : "Unknown",
                EmailAddr= !reader.IsDBNull(7) ? reader.GetString(7) : "Unknown",
            }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return jList;
        }
        public Job GetDetail(int jobID)
        {
            Job job = new Job();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Job
                                WHERE JobID = @selectedName";
            cmd.Parameters.AddWithValue("@selectedName", jobID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    job.JobID = jobID;
                    job.JobName = reader.GetString(1);
                    job.Summary = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    job.Description = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    job.Company = reader.GetString(4);
                    job.Salary = reader.GetDecimal(5);
                    job.PhoneNo = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                    job.EmailAddr = !reader.IsDBNull(7) ? reader.GetString(7) : null;
                }
            }
            reader.Close();
            conn.Close();
            return job;
        }
        public int Add(Job job)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Job (JobTitle, Summary, JobDescription, Company,Salary,PhoneNo,EmailAddr) 
        OUTPUT INSERTED.JobID 
        VALUES(@name, @summary, @desc, @company,@money,@phone,@email)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@name", job.JobName);
            if (job.Summary == null)
            {
                cmd.Parameters.AddWithValue("@summary", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@summary", job.Summary);
            }
            if (job.Description == null)
            {
                cmd.Parameters.AddWithValue("@desc", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@desc", job.Description);
            }
            cmd.Parameters.AddWithValue("@company",job.Company);
            cmd.Parameters.AddWithValue("@money", job.Salary);
            if (job.PhoneNo == null)
            {
                cmd.Parameters.AddWithValue("@phone", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@phone", job.PhoneNo);
            }
            if (job.EmailAddr == null)
            {
                cmd.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@email", job.EmailAddr);
            }
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            job.JobID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return job.JobID;
        }
        public int Delete(int jobid)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Job
 WHERE JobID = @selectID";
            cmd.Parameters.AddWithValue("@selectID", jobid);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of staff record updated or deleted
            return rowAffected;
        }
    }
}
