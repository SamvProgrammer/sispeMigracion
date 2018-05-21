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
            int contador = 0;
            int barrax = 0;
            foreach (Dictionary<string, object> item in r) {
                barrax++;
                double saldo1 = Convert.ToDouble(item["saldo"]);
                double importeUnitario = Convert.ToDouble(item["imp_unit"]);
                double importe = Convert.ToDouble(item["importe"]);
                int totalDescuento = Convert.ToInt32(item["totdesc"]);
                query = string.Format("select proyecto from datos.edoctahip where folio = {0} order by numdesc desc limit 1", item["folio"]);
                List<Dictionary<string, object>> tmp = globales.consulta(query);
                string proyecto = tmp[0]["proyecto"].ToString();
                importeUnitario = (proyecto.Contains("JUB")) ? importeUnitario * 2 : importeUnitario;
                query = string.Format("select ({0}-numdesc) as operacion,numdesc,totdes from datos.edoctahip where folio = {1} order by numdesc desc limit 1", totalDescuento, item["folio"]);
                tmp = globales.consulta(query);
                double operacion = Convert.ToDouble(tmp[0]["operacion"]);
                double faltante = operacion * importeUnitario;
                double saldo2 =  faltante;
                barra.Value = barrax;
                if (saldo1 != saldo2) {
                    double diferencia = (saldo1 - saldo2);
                    object[] arreaux = { item["folio"],item["rfc"], globales.checarDecimales(saldo1), globales.checarDecimales(saldo2), globales.checarDecimales(diferencia),tmp[0]["numdesc"],operacion, totalDescuento, globales.checarDecimales(item["imp_unit"]), globales.checarDecimales(item["importe"]),item["nombre"] };
                    arreglo[contador] = arreaux;
                    contador++;
                }
            }

            object[] lista = new object[contador];

            for (int x = 0; x < arreglo.Length; x++) {
                if (arreglo[x] == null) 
                    break;

                lista[x] = arreglo[x];
            }
            globales.reportes("reportePaty", "datosPaty",lista);
        }
    }
}
