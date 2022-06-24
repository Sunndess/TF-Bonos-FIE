using System;
using System.Data.SqlClient;

namespace Datos.Datos
{
   public class DataBase
    {
        SqlConnection connection;
        public SqlConnection Conectar()
        {
            try
            {
                string server = "LAPTOP-SFM\\SQLEXPRESS";//AQUI TU EQUIPO
                string database = "dbfinanzas";//AQUI EL NOMBRE DE LA BD

                string connection_string = String.Format("Data Source={0};Initial Catalog={1};Integrated Security =True", server, database);

                connection = new SqlConnection(connection_string);

                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }
        public void Desconectar()
        {
            connection.Close();
        }
    }
}
