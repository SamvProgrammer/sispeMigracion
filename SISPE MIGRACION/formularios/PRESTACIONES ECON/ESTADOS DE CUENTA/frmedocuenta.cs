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
    public partial class frmedocuenta : Form
    {
        public frmedocuenta()
        {
            InitializeComponent();
        }

        private void frmedocuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void btngenerar_Click(object sender, EventArgs e)
        {
            DateTime fec1 = fecha.Value;
            string f1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);




        }

        private void frmedocuenta_Load(object sender, EventArgs e)
        {
            pnlfolio.Visible = false;

           

        }

        private void rbfolio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbfolio.Checked)
                pnlfolio.Visible = true;
        }

        private void rbdependencia_CheckedChanged(object sender, EventArgs e)
        {
            if (rbdependencia.Checked)
                pnlfolio.Visible = false;
        }
    }
}
