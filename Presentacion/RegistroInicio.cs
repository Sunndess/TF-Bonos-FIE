using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class RegistroInicio : Form
    {
        NUsuarios nUsuarios = null;
        public RegistroInicio(NUsuarios nUsuarios)
        {
            InitializeComponent();
            this.nUsuarios = nUsuarios;
        }
        //DISEÑO
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        //
        private void btnRegistrarR_Click(object sender, EventArgs e)
        {
            error_campos.Clear();
            error_registro.Clear();

            if (!ValidarCampos())
            {
                MessageBox.Show("Se encontraron ERRORES durante el registro, por favor corrigalos");
            }
            else if (!ValidarRegistros())
            {
                MessageBox.Show("Se han querido registrar datos existentes, por favor ingrese un nuevo dato para el/los campo(s) correspondiente(s)");
            }
            else if (txtContraseña.Text != txtConfirmarContraseña.Text)
            {
                MessageBox.Show("Las contraseñas NO coinciden, confirme su contraseña");
            }
            else
            {
                string resultado = nUsuarios.Crear(txtNombres.Text, txtApellidos.Text, txtUsuario.Text, txtContraseña.Text, txtTelefono.Text, txtCorreo.Text);
                MessageBox.Show(resultado, "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombres.Clear();
                txtApellidos.Clear();
                txtUsuario.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtContraseña.Clear();
                txtConfirmarContraseña.Clear();
                label8.Text = " ";
            }
        }
        private bool ValidarCampos()
        {
            bool no_error = true;
            if (txtNombres.Text == string.Empty)
            {
                error_campos.SetError(txtNombres, "Ingrese sus nombres");
                no_error = false;
            }
            if (txtApellidos.Text == string.Empty)
            {
                error_campos.SetError(txtApellidos, "Ingrese sus apellidos");
                no_error = false;
            }
            if (txtUsuario.Text == string.Empty)
            {
                error_campos.SetError(txtUsuario, "Ingrese su nombre de usuario");
                no_error = false;
            }
            if (txtCorreo.Text == string.Empty)
            {
                error_campos.SetError(txtCorreo, "Ingrese su correo electronico");
                no_error = false;
            }
            if (txtTelefono.Text == string.Empty)
            {
                error_campos.SetError(txtTelefono, "Ingrese su numero de telefono");
                no_error = false;
            }
            if (txtContraseña.Text == string.Empty)
            {
                error_campos.SetError(txtContraseña, "Ingrese su contraseña");
                no_error = false;
            }
            if (txtConfirmarContraseña.Text == string.Empty)
            {
                error_campos.SetError(txtConfirmarContraseña, "Confirme su contraseña");
                no_error = false;
            }
            return no_error;
        }
        private bool ValidarRegistros()
        {
            string N_Usuario = txtUsuario.Text;
            string Telefono = txtTelefono.Text;
            string Correo = txtCorreo.Text;
            bool no_error = true;
            if (nUsuarios.Validar_Usuario(N_Usuario))
            {
                error_registro.SetError(txtUsuario, N_Usuario + " ya existe");
                no_error = false;
            }
            if (nUsuarios.Validar_Telefono(Telefono))
            {
                error_registro.SetError(txtTelefono, Telefono + " ya esta registrado a un usuario");
                no_error = false;
            }
            if (nUsuarios.Validar_Correo(Correo))
            {
                error_registro.SetError(txtCorreo, Correo + " ya esta registrado a un usuario");
                no_error = false;
            }
            return no_error;
        }

        private void btnCancelarR_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            if (txtContraseña.Text == string.Empty)
            {
                label8.Text = " ";
            }
            else if (txtConfirmarContraseña.Text == string.Empty)
            {
                label8.Text = " ";
            }
            else
            if (txtContraseña.Text == txtConfirmarContraseña.Text)
            {
                label8.Text = "Las contraseñas coinciden";
                label8.ForeColor = Color.Blue;
            }
            else
            {
                label8.Text = "Las contraseñas NO coinciden";
                label8.ForeColor = Color.Red;
            }
        }

        private void txtConfirmarContraseña_TextChanged(object sender, EventArgs e)
        {
            if (txtConfirmarContraseña.Text == string.Empty)
            {
                label8.Text = " ";
            }
            else if (txtContraseña.Text == string.Empty)
            {
                label8.Text = " ";
            }
            else
            if (txtConfirmarContraseña.Text == txtContraseña.Text)
            {
                label8.Text = "Las contraseñas coinciden";
                label8.ForeColor = Color.Blue;
            }
            else
            {
                label8.Text = "Las contraseñas NO coinciden";
                label8.ForeColor = Color.Red;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            error_campos.Clear();
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                error_campos.SetError(txtTelefono, "No se permiten valores de texto");
                e.Handled = true;
                return;
            }
        }

        private void pbCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbReducir_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void RegistroInicio_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
