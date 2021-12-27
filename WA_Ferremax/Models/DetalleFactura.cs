using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WA_Ferremax.Models
{
    public class DetalleFactura
    {
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal SubTotal { get; set; }

        readonly Conexion con = new Conexion();


        public void Create(DetalleFactura bean)
        {
            string query = "SP_RegistrarDetalleFactura";

            using (SqlConnection conn = new SqlConnection(con.connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@Idfactura", System.Data.SqlDbType.Int).Value = bean.IdFactura;
                command.Parameters.Add("@Idproducto", System.Data.SqlDbType.Int).Value = bean.IdProducto;
                command.Parameters.Add("@Cantidad", System.Data.SqlDbType.Int).Value = bean.Cantidad;
                command.Parameters.Add("@Precio_unitario", System.Data.SqlDbType.Decimal).Value = bean.Precio_Unitario;
                command.Parameters.Add("@SubTotal", System.Data.SqlDbType.Decimal).Value = bean.SubTotal;

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
    }
}
