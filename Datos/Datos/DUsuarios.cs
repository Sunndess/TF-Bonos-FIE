using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Datos
{
    public class DUsuarios
    {
        DataBase db = new DataBase();
        public string Registrar(string Nombres, string Apellidos, string N_Usuario, string Contraseña, string Telefono, string Correo)
        {
            try
            {
                SqlConnection connection = db.Conectar();

                string sql = String.Format("INSERT INTO Usuarios (Nombres, Apellidos, N_Usuario, Contraseña, Telefono, Correo) VALUES ('{0}', '{1}', '{2}', '{3}','{4}','{5}');", Nombres, Apellidos, N_Usuario, Contraseña, Telefono, Correo);
                SqlCommand command = new SqlCommand(sql, connection);

                command.ExecuteNonQuery();

                return "Se registró el usuario: " + N_Usuario;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            finally
            {
                db.Desconectar();
            }
        }

        public bool Validar_Cuenta(string N_Usuario, string Contraseña)
        {
            bool resultado = false;
            try
            {
                SqlConnection connection = db.Conectar();
                string sql = string.Format("SELECT N_Usuario, Contraseña FROM Usuarios WHERE ( N_Usuario = '" + N_Usuario + "' AND Contraseña = '" + Contraseña + "')");
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resultado = true;
                }
                return resultado;
            }
            catch
            {
                return resultado;
            }
            finally
            {
                db.Desconectar();
            }
        }
        public bool Validar_Usuario(string usuario)
        {
            bool resultado = false;
            try
            {
                SqlConnection connection = db.Conectar();
                string sql = string.Format("SELECT N_Usuario FROM Usuarios WHERE ( N_Usuario = '" + usuario + "')");
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resultado = true;
                }
                return resultado;
            }
            catch
            {
                return resultado;
            }
            finally
            {
                db.Desconectar();
            }
        }
        public bool Validar_Telefono(string telefono)
        {
            bool resultado = false;
            try
            {
                SqlConnection connection = db.Conectar();
                string sql = string.Format("SELECT Telefono FROM Usuarios WHERE ( Telefono = '" + telefono + "')");
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resultado = true;
                }
                return resultado;
            }
            catch
            {
                return resultado;
            }
            finally
            {
                db.Desconectar();
            }
        }
        public bool Validar_Correo(string correo)
        {
            bool resultado = false;
            try
            {
                SqlConnection connection = db.Conectar();
                string sql = string.Format("SELECT Correo FROM Usuarios WHERE ( Correo = '" + correo + "')");
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resultado = true;
                }
                return resultado;
            }
            catch
            {
                return resultado;
            }
            finally
            {
                db.Desconectar();
            }
        }
    }
}