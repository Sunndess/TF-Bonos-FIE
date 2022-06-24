using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MPeruano : Form
    {   
        public MPeruano()
        {
            InitializeComponent();
        }
        private double Calculo_D_Periodos(double CUOTITAS)
        {
            double PERIODOS = 0;
            switch (cboPer_Tasa_Per.SelectedIndex)
            {
                case 0: PERIODOS = CUOTITAS * 360; break; //Diario
                case 1: PERIODOS = CUOTITAS * 24; break; //Quincenal
                case 2: PERIODOS = CUOTITAS * 12; break; //Mensual
                case 3: PERIODOS = CUOTITAS * 6; break; //Bimestral
                case 4: PERIODOS = CUOTITAS * 4; break; //Trimestral
                case 5: PERIODOS = CUOTITAS * 3; break; //Cuatrimestral
                case 6: PERIODOS = CUOTITAS * 2; break; //Semestral
                case 7: PERIODOS = CUOTITAS; break; //Anual
            }
            return PERIODOS;
        }
        private double Calculo_TEA(double i)
        {
            double tasa = 0.00;
            int n = 1, m = 1;
            string tipo_tasa = cboTipo_Tasa.Text;
            switch (cboPer_Tasa_Per.SelectedIndex)
            {
                case 0: n = 360; break; //Diaria
                case 1: n = 24; break; //Quincenal
                case 2: n = 12; break; //Mensual
                case 3: n = 6; break; //Bimestral
                case 4: n = 4; break; //Trimestral
                case 5: n = 3; break; //Cuatrimestral
                case 6: n = 2; break; //Semestral
                case 7: n = 1; break; //Anual
            }
            switch (cboCap_Per.SelectedIndex)
            {
                case 0: m = 360; break; //Diaria
                case 1: m = 24; break; //Quincenal
                case 2: m = 12; break; //Mensual
                case 3: m = 6; break; //Bimestral
                case 4: m = 4; break; //Trimestral
                case 5: m = 3; break; //Cuatrimestral
                case 6: m = 2; break; //Semestral
                case 7: m = 1; break; //Anual
            }
            //DE TASA EFECTIVA A TASA EFECTIVA
            if (cboTipo_Tasa.Text == "Efectiva")
            {
                tasa = (100 * (Math.Pow(1 + i / 100, n / 1) - 1)); // Tasa Efectiva Anual (TEA)
            }

            //DE TASA NOMINAL A TASA EFECTIVA
            else if (tipo_tasa == "Nominal")
            {
                tasa = 100 * (Math.Pow(1 + (i * n) / (100 * m), m / 1) - 1); // Tasa Efectiva Anual (TEA)               
            }

            return tasa;
        }
        private double Calculo_Tasas_Periodos(double i)
        {
            double tasa = 0.00;
            int n = 1, m = 1;
            string tipo_tasa = cboTipo_Tasa.Text;
            switch (cboPer_Tasa_Per.SelectedIndex)
            {
                case 0: n = 360; break; //Diaria
                case 1: n = 24; break; //Quincenal
                case 2: n = 12; break; //Mensual
                case 3: n = 6; break; //Bimestral
                case 4: n = 4; break; //Trimestral
                case 5: n = 3; break; //Cuatrimestral
                case 6: n = 2; break; //Semestral
                case 7: n = 1; break; //Anual
            }
            switch (cboCap_Per.SelectedIndex)
            {
                case 0: m = 360; break; //Diaria
                case 1: m = 24; break; //Quincenal
                case 2: m = 12; break; //Mensual
                case 3: m = 6; break; //Bimestral
                case 4: m = 4; break; //Trimestral
                case 5: m = 3; break; //Cuatrimestral
                case 6: m = 2; break; //Semestral
                case 7: m = 1; break; //Anual
            }
            //DE TASA EFECTIVA A TASA EFECTIVA
            if (cboTipo_Tasa.Text == "Efectiva") tasa = i;

            //DE TASA NOMINAL A TASA EFECTIVA
            else if (tipo_tasa == "Nominal")
                tasa = (Math.Pow(1 + (i * n) / (100 * m), m / n) - 1) * 100;

            return tasa;
        }
        private double Calculo_COK(double tasa)
        {
            int pertasa = Convert.ToInt32(txtNumPerTasa.Text);
            double COK = Math.Pow(1.0 + tasa/100, (pertasa*1.0) / 360) - 1;
            return COK;
        }
        private double Calculo_VAN(double cuota, double tasa, int per)
        {
            double van = cuota / Math.Pow(1 + tasa / 100, per * 1.0);

            return van;
        }
        private void Calcular_Amortizacion()
        {
            string tip_entrada = "", tip_salida = "", comb, simbolo = "";

            switch (cbomonentra.SelectedIndex)
            {
                case 0: tip_entrada = "1"; break; //Dólares
                case 1: tip_entrada = "2"; break; //Euros
                case 2: tip_entrada = "3"; break; //Soles
            }
            switch (cbomonsalida.SelectedIndex)
            {
                case 0:
                    tip_salida = "1";
                    simbolo = "$ "; break; //Dólares
                case 1:
                    tip_salida = "2";
                    simbolo = "€ "; break; //Euro
                case 2:
                    tip_salida = "3";
                    simbolo = "S/ "; break; //Nuevos soles
            }

            // DATOS DE ENTRADA
            double PRECIOVENTA = Convert.ToDouble(nudPrecioF.Value);
            double CUOTAINICIAL = Convert.ToDouble(nudCuotaF.Value) / 100;
            double PRESTAMO = PRECIOVENTA - (PRECIOVENTA * CUOTAINICIAL); 
            DateTime FECHAEMISION = dtFecha_Emision.Value.Date;
            double ANIOS = Convert.ToDouble(nudN_Anios.Value);
            double TEA = Calculo_TEA(Convert.ToDouble(nudTasa.Value)) / 100;            
            double PERIODOS = Calculo_D_Periodos(ANIOS);
            double TEP = Calculo_Tasas_Periodos(Convert.ToDouble(nudTasa.Value)) / 100;
            double TASADESC = Convert.ToDouble(nudescuento.Value);

            // RESULTADOS
            int DIAS = Convert.ToInt32(txtNumPerTasa.Text);
            DateTime FECHA_PROGRAMADA = FECHAEMISION;
            double INTERES, FACTOR, FACTOR_TOTAL = 0, AMORTIZACION, CUOTA, SALDOINICIAL, SALDOFINAL = PRESTAMO;
            double COK = Calculo_COK(TASADESC);
            double VAN = 0.0;
            double C_P = 0;  // cuota durante el periodo
            double FA =0.0;     // Flujo Activo
            double FAXP = 0.0;   // Flujo Activo x Plazo
            double FC = 0.0;     // Factor por Convexidad
            double sumaFA = 0.0, sumaFAXP = 0.0, sumaFC = 0.0;

            switch (cboCuotaPeriodo.SelectedIndex)
            {
                case 0: C_P = 2; break; // Doble
                case 1: C_P = 3; break; // Triple
            }
            if (PRECIOVENTA == 0 || cbomonentra.Text == "" || cboTipo_Tasa.Text == "" || TEA == 0 || cboPer_Tasa_Per.Text == "" || ANIOS == 0)
            {
                MessageBox.Show("Es necesario completar los \ncampos de DATOS DE ENTRADA, \npor favor completelos");
            }
            else
            {
                for (int i = 1; i <= PERIODOS; i++)
                {
                    if (chlbperiodos.GetItemChecked(i - 1) == true)
                    {
                        FACTOR_TOTAL += C_P / Math.Pow(1 + TEP, i);
                    }
                    else
                    {
                        FACTOR_TOTAL += 1 / Math.Pow(1 + TEP, i);
                    }
                }

                for (int i = 0; i < PERIODOS + 1; i++)
                {
                    if (i == 0)
                    {
                        FACTOR = FACTOR_TOTAL;
                        SALDOINICIAL = 0;
                        INTERES = 0;
                        CUOTA = 0;
                        AMORTIZACION = 0;
                        SALDOFINAL = PRESTAMO;
                    }
                    else
                    {
                        FECHA_PROGRAMADA = FECHAEMISION.AddDays(DIAS * i);
                        SALDOINICIAL = SALDOFINAL;
                        INTERES = SALDOINICIAL * TEP;

                        if (chlbperiodos.GetItemChecked(i - 1) == true)
                        {
                            FACTOR = C_P / Math.Pow(1 + TEP, i);
                            CUOTA = C_P * PRESTAMO / FACTOR_TOTAL;
                        }
                        else
                        {
                            FACTOR = 1 / Math.Pow(1 + TEP, i);
                            CUOTA = PRESTAMO / FACTOR_TOTAL;
                        }
                        AMORTIZACION = CUOTA - INTERES;
                        SALDOFINAL = SALDOINICIAL - AMORTIZACION;

                        VAN += Calculo_VAN(CUOTA, COK * 100, i);
                        FA = CUOTA / Math.Pow(1 + COK, i);
                        FAXP = (FA * i * DIAS) / 360;
                        FC = FA * i * (1 + i);
                        sumaFA += FA;
                        sumaFAXP += FAXP;
                        sumaFC += FC;
                    }
                    if (tip_entrada == tip_salida)
                    {
                        dgvMetPer.Rows.Add(i, FECHA_PROGRAMADA.ToShortDateString(), Math.Round(TEA * 100, 7) + " %", Math.Round(TEP * 100, 7) + " %", Math.Round(FACTOR, 7), "s",
                            simbolo + Math.Round(SALDOINICIAL, 2), simbolo + Math.Round(INTERES, 2), simbolo + Math.Round(CUOTA, 2), simbolo + Math.Round(AMORTIZACION, 2),
                            simbolo + Math.Round(SALDOFINAL, 2), simbolo + Math.Round(FA, 2), simbolo + Math.Round(FAXP, 2), simbolo + Math.Round(FC, 2));
                    }
                    else
                    {
                        //hallando los tipos de cambio
                        double cambio = 0;
                        comb = string.Concat(tip_entrada, tip_salida);

                        switch (comb)
                        {
                            case "12": cambio = 0.93; break; //de dolar a euro
                            case "13": cambio = 3.74; break; //de dolar a soles
                            case "21": cambio = 1.08; break; //de euro a dolar
                            case "23": cambio = 4.05; break; //de euro a soles
                            case "31": cambio = 0.27; break; //de soles a dolar
                            case "32": cambio = 0.25; break; //de soles a euro
                        }
                        dgvMetPer.Rows.Add(i, FECHA_PROGRAMADA.ToShortDateString(), Math.Round(TEA * 100, 7) + " %", Math.Round(TEP * 100, 7) + " %", Math.Round(FACTOR, 7), "s",
                            simbolo + Math.Round(SALDOINICIAL * cambio, 2), simbolo + Math.Round(INTERES * cambio, 2), simbolo + Math.Round(CUOTA * cambio, 2), 
                            simbolo + Math.Round(AMORTIZACION * cambio, 2), simbolo + Math.Round(SALDOFINAL * cambio, 2), simbolo + Math.Round(FA * cambio, 2),
                            simbolo + Math.Round(FAXP * cambio, 2), simbolo + Math.Round(FC * cambio, 2));
                    }
                }
            }
            double UTPER = VAN - PRESTAMO;
            double duracion = sumaFAXP / sumaFA, convexidad = sumaFC / (Math.Pow(1 + COK, 2) * sumaFA * Math.Pow(360 / DIAS, 2));
            double duracion_modificada = duracion / (1 + COK), total = duracion + convexidad;

            lblPrestamo.Text = PRESTAMO.ToString();
            lblPeriodos.Text = PERIODOS.ToString();
            lblPrecioActual.Text = Math.Round(VAN, 2).ToString();
            lblCOK.Text = Math.Round(COK * 100, 7).ToString() + " %";
            lblUTPER.Text = Math.Round(UTPER, 2).ToString();
            lblDuracion.Text = Math.Round(duracion, 2).ToString();
            lblConvexidad.Text = Math.Round(convexidad, 2).ToString();
            lblTotalDC.Text = Math.Round(total, 2).ToString();
            lblDurMod.Text = Math.Round(duracion_modificada, 2).ToString();
            lblTIR.Text = Math.Round(TEA*100, 7).ToString() + " %";
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            dgvMetPer.Rows.Clear();
            Calcular_Amortizacion();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // LIMPIAR DATOS DE ENTRADA
            nudPrecioF.Value = 0;
            nudCuotaF.Value = 0;
            dtFecha_Emision.Value = DateTime.Today;
            nudTasa.Value = 0;            
            cboPer_Tasa_Per.SelectedItem = null;
            cboCuotaPeriodo.SelectedItem = null;
            cbomonentra.SelectedItem = null;
            cbomonsalida.SelectedItem = null;
            cboTipo_Tasa.SelectedItem = null;
            cboCap_Per.SelectedItem = null;
            txtNumCap.Text = null;
            txtNumPerTasa.Text = null;        
            nudN_Anios.Value = 0;

            // LIMPIAR RESULTADOS
            lblPrestamo.Text = null;
            lblPeriodos.Text = null;
            lblPrecioActual.Text= null;
            lblCOK.Text = null;
            lblUTPER.Text = null;
            lblTIR.Text = null;
            lblDuracion.Text = null;
            lblConvexidad.Text = null;
            lblTotalDC.Text = null;
            lblDurMod.Text = null;

            chlbperiodos.Items.Clear();
            dgvMetPer.Rows.Clear();
        }
        public void ExportarExcel(DataGridView dataperuano)
        {
            Microsoft.Office.Interop.Excel.Application exporta = new Microsoft.Office.Interop.Excel.Application();
            exporta.Application.Workbooks.Add(true);
            int indicecolumna = 0;
            foreach (DataGridViewColumn columna in dataperuano.Columns)
            {
                indicecolumna++;
                exporta.Cells[1, indicecolumna] = columna.Name;
            }
            int indicefila = 0;
            foreach (DataGridViewRow fila in dataperuano.Rows)
            {
                indicefila++;
                indicecolumna = 0;
                foreach (DataGridViewColumn columna in dataperuano.Columns)
                {
                    indicecolumna++;
                    exporta.Cells[indicefila + 1, indicecolumna] = fila.Cells[columna.Name].Value;
                }
            }
            exporta.Visible = true;
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarExcel(dgvMetPer);
        }
        private void nudNacuotaF_ValueChanged(object sender, EventArgs e)
        {
            double CUOTITAS = Convert.ToDouble(nudN_Anios.Value);
            double PERIODOS = Calculo_D_Periodos(CUOTITAS);
            if (nudN_Anios.Value != 0 && cboPer_Tasa_Per.Text != " ")
            {      
                chlbperiodos.Items.Clear();

                for (int i = 1; i <= PERIODOS; i++)
                {
                    chlbperiodos.Items.Add(i);
                }
            }
        }
        private void cbomonentra_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbomonentra.SelectedIndex)
            {
                case 0: cbomonsalida.SelectedIndex = 0; break;
                case 1: cbomonsalida.SelectedIndex = 1; break;
                case 2: cbomonsalida.SelectedIndex = 2; break;
            }
        }
        private void cboTipo_Tasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipo_Tasa.Text == "Efectiva")
            {
                lblFrec_Cap.Visible = false;
                cboCap_Per.Visible = false;
                txtNumCap.Visible = false;
            }
            if (cboTipo_Tasa.Text == "Nominal")
            {
                lblFrec_Cap.Visible = true;
                cboCap_Per.Visible = true;
                txtNumCap.Visible = true;
            }
        }

        private void cboPer_Tasa_Per_SelectedIndexChanged(object sender, EventArgs e)
        {
            double CUOTITAS = Convert.ToDouble(nudN_Anios.Value);
            double PERIODOS = Calculo_D_Periodos(CUOTITAS);
            if (nudN_Anios.Value != 0 && cboPer_Tasa_Per.Text != " ")
            {
                chlbperiodos.Items.Clear();

                for (int i = 1; i <= PERIODOS; i++)
                {
                    chlbperiodos.Items.Add(i);
                }
            }
            switch (cboPer_Tasa_Per.SelectedIndex)
            {
                case 0: txtNumPerTasa.Text = "1"; break; //Diaria
                case 1: txtNumPerTasa.Text = "15"; break; //Quincenal
                case 2: txtNumPerTasa.Text = "30"; break; //Mensual
                case 3: txtNumPerTasa.Text = "60"; break; //Bimestral
                case 4: txtNumPerTasa.Text = "90"; break; //Trimestral
                case 5: txtNumPerTasa.Text = "120"; break; //Cuatrimestral
                case 6: txtNumPerTasa.Text = "180"; break; //Semestral
                case 7: txtNumPerTasa.Text = "360"; break; //Anual
            }
            lblCokPeriodo.Text = "COK " + cboPer_Tasa_Per.Text + " (%):";
        }
        private void cboCap_Per_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboCap_Per.SelectedIndex)
            {
                case 0: txtNumCap.Text = "1"; break; //Diaria
                case 1: txtNumCap.Text = "15"; break; //Quincenal
                case 2: txtNumCap.Text = "30"; break; //Mensual
                case 3: txtNumCap.Text = "60"; break; //Bimestral
                case 4: txtNumCap.Text = "90"; break; //Trimestral
                case 5: txtNumCap.Text = "120"; break; //Cuatrimestral
                case 6: txtNumCap.Text = "180"; break; //Semestral
                case 7: txtNumCap.Text = "360"; break; //Anual
            }
        }
    }
}
