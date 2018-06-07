using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    public partial class frmsaldos : Form
    {
        public frmsaldos()
        {
            InitializeComponent();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
     


        }

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime fec1 = fecha1.Value;
            string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
            
            // Validación de tipo de prestmo 
            if (RBquiro.Checked)
            {
                string R2_EDOCTA = "P_edocta";
                string R2_EC = "D_ecquir";
            }
            else 
            {
                string R2_EDOCTA = "P_edocth";
                string R2_EC = "D_echipo";
            }
            

            if (rbnormal.Checked)
            {
                string R2_NCA = " ";
            }
            else if (rbambos.Checked)
            {
                string R2_NCA = "and ubic_pagare <> 'X'";

            }
            else 
            {
                string R2_NCA = " and ubic_pagare = 'C'";
            }

            MessageBox.Show("Seleccionando ultima CTA. de cada folio al " +fec1 );

            string qry = string.Format("select folio from  {0}  ");









        }
    }
}
