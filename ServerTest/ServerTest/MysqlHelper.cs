using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ServerTest
{
 
    public static class MysqlHelper
    {
        private const string connect ="data source=localhost;database=userdb;user id=root;password=root;pooling=false;charset=utf8";
        
        public static int Insert(string sql,params MySqlParameter[] ps)
        {
            using (MySqlConnection conn=new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
