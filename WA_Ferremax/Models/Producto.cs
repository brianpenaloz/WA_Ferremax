using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WA_Ferremax.Models
{
    public class Producto
    {
        public int Idproducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio_unitario { get; set; }

        readonly Conexion con = new Conexion();


        public void Create(Producto bean)
        {
            string query = "SP_RegistrarProducto";

            using (SqlConnection conn = new SqlConnection(con.connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = bean.Nombre;
                command.Parameters.Add("@Precio_unitario", System.Data.SqlDbType.Decimal).Value = bean.Precio_unitario;

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e.Message);
                }
            }
        }


        public List<Producto> Read()
        {
            List<Producto> lstBean = new List<Producto>();
            string query = "EXEC ListarProducto";

            using (SqlConnection conn = new SqlConnection(con.connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);

                try
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto objBean = new Producto
                        {
                            Idproducto = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Precio_unitario = reader.GetDecimal(2)
                        };
                        lstBean.Add(objBean);
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e.Message);
                }
            }

            return lstBean;
        }
    }
}
