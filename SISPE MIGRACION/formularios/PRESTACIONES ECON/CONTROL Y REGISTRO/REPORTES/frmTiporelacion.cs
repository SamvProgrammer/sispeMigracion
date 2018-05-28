using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES
{
    public partial class frmTiporelacion : Form
    {
        private List<Dictionary<string, string>> tmp;
        private string query { get; set; }
        private DateTime fecha { get; set; }
        private string tipoReporte { get; set; }

        public frmTiporelacion()
        {
            InitializeComponent();
        }

        private void frmTiporelacion_Load(object sender, EventArgs e)
        {
            try
            {
                if (tmp.Count == 0) {
                    MessageBox.Show("Sin registros, consultar otro","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                foreach (Dictionary<string, string> item in tmp)
                {
                    this.list.Items.Add(item["cuenta"] + " | " + item["descripcion"]);
                }
                this.list.SelectedIndex = 0;
            }
            catch {
                MessageBox.Show("Error en el sistema..","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        internal void setLista(List<Dictionary<string, string>> resultado, string cadena, DateTime fecha,string tipoReporte) {
            this.tmp = resultado;
            this.query = cadena.Replace("DISTINCT tipo_rel", " * ").Replace("order by tipo_rel desc", "order by folio desc").Replace("order by", "and tipo_rel = '{0}'  order by");
            this.fecha = fecha;
            this.tipoReporte = tipoReporte;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texto = list.Text;
            texto = texto.Split('|')[0].Trim();
            string temporal = string.Format(this.query, texto);
            List<Dictionary<string, object>> resultado = globales.consulta(temporal);
            object[] objeto = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado) {
                string folio = Convert.ToString(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string serie = Convert.ToString(item["numdesc"]) + "/" + Convert.ToString(item["totdesc"]);
                string imp_unit = globales.checarDecimales(Convert.ToString(item["imp_unit"]));
                object[] tmp = { folio, rfc, nombre, proyecto, serie, "", imp_unit };
                objeto[contador] = tmp;
                contador++;
            }
            string[] meses = { "", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            string fecha = string.Format("OAXACA DE JUÁREZ, OAX., A {0} DE {1} DEL {2}", DateTime.Now.Day, meses[DateTime.Now.Month], DateTime.Now.Year);
            string descripcion1 = string.Format("RELACIÓN DE DESCUENTOS {0} DE PRESTAMOS QUIROGRAFARIOS PARA APLICAR AL PERSONAL {1}", (texto == "J" || texto == "P" || texto == "T" || texto == "JF" || texto == "PF" || texto == "TF")?"MENSUALES":"QUINCENALES", (texto == "J" || texto == "P" || texto == "T" || texto == "JF" || texto == "PF" || texto == "TF") ? "" : "DE");
            string descripcion2 = texto.Replace("|",":");
            string descripcion3 = string.Format("A PARTIR DEL {0} DE {1} DEL {2}",this.fecha.Day,meses[this.fecha.Month],this.fecha.Year);


            object[][] parametros = new object[2][];
            object[] headers = { "fecha","descripcion1", "descripcion2", "descripcion3","tipoReporte","total"};
            object[] body = { fecha, descripcion1, descripcion2, descripcion3, this.tipoReporte, contador.ToString() };
            parametros[0] = headers;
            parametros[1] = body;

            globales.reportes("reporteImpAltasQ", "impAltaQ",objeto,"",false,parametros);
        }

      
    }
}
