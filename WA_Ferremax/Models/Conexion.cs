using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using System.Data.SqlClient;


namespace WA_Ferremax.Models
{
    public class Conexion
    {
        public string connectionString = "data source=DESKTOP-NVGBAVV;initial catalog=DB_FERREMAX;user id=sa; password=sql;";

        public bool MyConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
            }
            catch (Exception)
            {

                //throw;
                return false;
            }
            finally
            {
                //conn.Close();
            }

            return true;
        }
    }
}
