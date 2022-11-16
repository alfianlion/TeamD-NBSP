using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NBSP.Models;
using System.IO;
using System.Collections;


namespace NBSP.DAL
{
    public class MemberDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public MemberDAL()
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
        public int Add(Member member)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Member (Name, EmailAddr, PhoneNo, Pwd)
                                OUTPUT INSERTED.MemberID
                                VALUES(@name, @email, @contact, @pwd)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@name", member.Name);
            cmd.Parameters.AddWithValue("@email", member.EmailAddr);
            cmd.Parameters.AddWithValue("@contact", member.PhoneNo);
            cmd.Parameters.AddWithValue("@pwd", member.Pwd);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            member.MemberID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return member.MemberID;
        }

        public bool MemberLoginCheck(string LoginId, string pwd)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Member 
            WHERE Name = @selectedName AND Pwd = @pwd";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedName", LoginId);
            cmd.Parameters.AddWithValue("@pwd", pwd);

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
        public Member GetMemberDetail(string memberID)
        {
            Member member = new Member();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Member
                                WHERE Name = @selectedName";
            cmd.Parameters.AddWithValue("@selectedName", memberID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    member.MemberID = reader.GetInt32(0);
                    member.Name = memberID;
                    member.EmailAddr = reader.GetString(2);
                    member.PhoneNo = reader.GetInt32(3);
                    member.Pwd = reader.GetString(4);
                    member.PasswordConfirm = reader.GetString(4);
                }
            }
            reader.Close();
            conn.Close();
            return member;

        }
        public void Update(Member member,int id)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE Member SET Name=@name,
                                EmailAddr=@email, PhoneNo = @telno, Pwd = @password
                                WHERE MemberID = @selectedMemberID";

            cmd.Parameters.AddWithValue("@name", member.Name);
            cmd.Parameters.AddWithValue("@email", member.EmailAddr);
            cmd.Parameters.AddWithValue("@telno", member.PhoneNo);
            cmd.Parameters.AddWithValue("@password", member.Pwd);
            cmd.Parameters.AddWithValue("@selectedMemberID", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
