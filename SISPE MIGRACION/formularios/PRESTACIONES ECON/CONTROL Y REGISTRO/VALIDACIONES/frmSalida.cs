using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.VALIDACIONES
{
    public partial class frmSalida : Form
    {
        private object[] arreglo; 
        private bool tipoPrestamo;
        private string r3f1;
        private string r3f2;
        public frmSalida(bool tipoPrestamo,string t1,string t2,params object[] arreglo)
        {
            InitializeComponent();
            this.arreglo = arreglo;
            this.tipoPrestamo = tipoPrestamo;
            this.r3f1 = t1;
            this.r3f2 = t2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult p = MessageBox.Show("¿Seguro que desea realizar la operación?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (p == DialogResult.No) return;
            if (rd1.Checked)
            {
              string tipoPrestamo = this.tipoPrestamo ? "QUIROGRAFARIOS" : "HIPOTECARIOS";
              string f1 = string.Format("{0}/{1}/{2}",r3f1.Split('-')[2], r3f1.Split('-')[1], r3f1.Split('-')[0]);
              string f2 = string.Format("{0}/{1}/{2}", r3f2.Split('-')[2], r3f2.Split('-')[1], r3f2.Split('-')[0]);
              string fechaActual = string.Format("{0}/{1}/{2}",DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);

              object[][] parametros = new object[2][];
              object[] headers = { "tipoPrestamo","R3F2","R3F1","fechaActual","total" };
              object[] body = { tipoPrestamo,f2,f1,fechaActual,this.arreglo.Length.ToString()};

              parametros[0] = headers;
              parametros[1] = body;

              globales.reportes("reporteSinPagos", "p_quirog", this.arreglo, "", false, parametros);
                
            }
            else {

            }
        }
    }
}
