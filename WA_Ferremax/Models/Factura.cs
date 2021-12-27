using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WA_Ferremax.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public string Nombre_sucursal { get; set; }
        public string Nombre_cliente { get; set; }
        public string Numero_Factura{ get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IGV { get; set; }
        public decimal Total { get; set; }

        readonly Conexion con = new Conexion();


        public int Create(Factura bean)
        {
            string query = "SP_RegistrarFactura";

            using (SqlConnection conn = new SqlConnection(con.connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@Sucursal", System.Data.SqlDbType.VarChar).Value = bean.Nombre_sucursal;
                command.Parameters.Add("@Cliente", System.Data.SqlDbType.VarChar).Value = bean.Nombre_cliente;
                command.Parameters.Add("@Factura", System.Data.SqlDbType.VarChar).Value = bean.Numero_Factura;
                command.Parameters.Add("@Fecha", System.Data.SqlDbType.DateTime).Value = bean.Fecha;
                command.Parameters.Add("@Subtotal", System.Data.SqlDbType.Decimal).Value = bean.Subtotal;
                command.Parameters.Add("@IGV", System.Data.SqlDbType.Decimal).Value = bean.IGV;
                command.Parameters.Add("@Total", System.Data.SqlDbType.Decimal).Value = bean.Total;
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    int TheId = Int32.Parse(command.Parameters["@id"].Value.ToString());
                    conn.Close();

                    return TheId;
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e.Message);
                }
            }
        }
    }
}
