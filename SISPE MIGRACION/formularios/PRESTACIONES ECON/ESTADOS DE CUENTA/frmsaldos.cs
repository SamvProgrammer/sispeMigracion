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
            DateTime fec2 = fecha2.Value;
            string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
            string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);
            string tipoprestamo = (RBquiro.Checked)?"from datos.p_quirog": "from datos.p_hípote";
            string query = "";
            MessageBox.Show("estas seleccionando" + tipoprestamo);
            

        }
    }
}
