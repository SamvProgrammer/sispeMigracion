using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH
{
    public partial class hipotecarios : Form
    {
        public hipotecarios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from datos.hip";
            List<Dictionary<string, object>> r = globales.consulta(query);
            object[] arreglo = new object[r.Count];
            barra.Maximum = r.Count;
            int barrax = 0;
            foreach (Dictionary<string, object> item in r) {
                barrax++;
                barra.Value = barrax;
                query = string.Format("select * from datos.edoctahip where folio = {0} order by numdesc desc limit 1;",item["folio"]);
                List<Dictionary<string, object>> auxItem = globales.consulta(query);
                int numDescPendiente = Convert.ToInt32(auxItem[0]["totdes"]) - Convert.ToInt32(auxItem[0]["numdesc"]);
                double pagoPendiente = Convert.ToDouble(auxItem[0]["imp_unit"])*numDescPendiente;
                double importe = Convert.ToDouble(item["importe"]);

                double saldo = importe - pagoPendiente;


            }

         //   globales.reportes("reportePaty", "datosPaty",lista);
        }
    }
}
