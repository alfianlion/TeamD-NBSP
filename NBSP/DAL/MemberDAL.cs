using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NBSP.Models;

namespace NBSP.DAL
{
    public class MemberDAL
    {
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;

    }
}
