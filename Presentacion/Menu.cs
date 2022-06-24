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
    public partial class formMenu : Form
    {
        public formMenu()
        {
            InitializeComponent();
            customizeDesing();
        }
        //DISEÑO
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        //
        //DISEÑOS
        private void customizeDesing()
        {
            panelCalculoSubmenu.Visible = false;
            panelInfoSubmenu.Visible = false;

        }
        private void hideSubMenu()
        {
            if (panelCalculoSubmenu.Visible == true)
                panelCalculoSubmenu.Visible = false;
            if (panelInfoSubmenu.Visible == true)
                panelInfoSubmenu.Visible = false;
        }
        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        //CÁLCULOS
        private void btnCalculo_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelCalculoSubmenu);
        }
        private void btnPeruano_Click(object sender, EventArgs e)
        {
            //CODE FORM
            OpenChildForm(new MPeruano());
            hideSubMenu();
        }
        private void btnAmericano_Click(object sender, EventArgs e)
        {
            //CODE FORM
            OpenChildForm(new MAmericano());
            hideSubMenu();

        }
        private void btnFrances_Click(object sender, EventArgs e)
        {
            //CODE FORM
            OpenChildForm(new MFrances());
            hideSubMenu();
        }
        private void btnAleman_Click(object sender, EventArgs e)
        {
            //CODE FORM
            OpenChildForm(new MAleman());
            hideSubMenu();
        }
        //INFORMACIÓN GENERAL
        private void btnInfoGeneral_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelInfoSubmenu);
        }
        private void btnDesarrolladores_Click(object sender, EventArgs e)
        {
            //CODE FORM
            OpenChildForm(new FDesarrolladores());
            hideSubMenu();
        }
        private void btnTutorialUso_Click(object sender, EventArgs e)
        {
            //CODE FORM
            OpenChildForm(new Tutorial());
            hideSubMenu();
        }
        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMenu.Controls.Add(childForm);
            panelMenu.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void pbCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbReducir_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void formMenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelSlideMenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelMenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void formMenu_Load(object sender, EventArgs e)
        {

        }

        //Fin del diseño
    }
}
