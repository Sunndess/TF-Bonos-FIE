using Datos.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class NUsuarios
    {
        DUsuarios dusuarios = new DUsuarios();
        public string Crear(string Nombres, string Apellidos, string N_Usuario, string Contraseña, string Telefono, string Correo)
        {
            return dusuarios.Registrar(Nombres, Apellidos, N_Usuario, Contraseña, Telefono, Correo);
        }
        public bool Validar_Cuenta(string N_Usuario, string Contraseña)
        {
            return dusuarios.Validar_Cuenta(N_Usuario, Contraseña);
        }
        public bool Validar_Usuario(string usuario)
        {
            return dusuarios.Validar_Usuario(usuario);
        }
        public bool Validar_Telefono(string telefono)
        {
            return dusuarios.Validar_Telefono(telefono);
        }
        public bool Validar_Correo(string correo)
        {
            return dusuarios.Validar_Correo(correo);
        }
    }
}
