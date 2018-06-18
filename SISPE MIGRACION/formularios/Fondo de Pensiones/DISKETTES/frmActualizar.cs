using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES
{
    public partial class frmActualizar : Form
    {
        private DbfDataReader.DbfTable tabla;
        public frmActualizar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult p =  open1.ShowDialog();
            if (p == DialogResult.OK) {
                string ruta = open1.FileName;
                string[] arreglo = ruta.Split('\\');
                string nombreArchivo = arreglo[arreglo.Length-1];
                string letra = nombreArchivo.First().ToString().ToUpper();
                if (letra == "A" || letra == "D")
                {
                    bool aportacion = letra == "A";
                    realizarOperacion(aportacion,nombreArchivo,ruta);
                }
                else {
                    MessageBox.Show("Archivo seleccionado invalido, asegurase que cumpla con el nombre establecido empezando con A o D","Error archivo",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

        private void realizarOperacion(bool aportacion,string nombre,string ruta)
        {
            string queEs = aportacion ? "APORTACIÓN" : "DESCUENTOS";
            txtArchivo.Text = nombre.Replace(".dbf","");
            txtConcepto.Text = queEs;
            txtRuta.Text = ruta;

            
            List<Dictionary<string, object>> resultado = globales.leerDbf(ruta);
            string claveDependencia = Convert.ToString(resultado[0]["proyecto"]).Substring(0,3);
            string desde = Convert.ToString(resultado[0]["desde"]).Split('|')[0];
            string hasta = Convert.ToString(resultado[0]["hasta"]).Split('|')[0];

            string query = string.Format("select descripcion from catalogos.cuenta where proy = '{0}'",claveDependencia);
            
            List<Dictionary<string,object>> tmp1 = globales.consulta(query);

            txtDesde.Text = desde;
            txtHasta.Text = hasta;
            txtDependencia.Text = (tmp1.Count == 0)?"":Convert.ToString(tmp1[0]["descripcion"]);

            DialogResult p = MessageBox.Show("¿Desea visualizar los datos del archivo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (p == DialogResult.Yes) {
                datos1.Rows.Clear();
                resultado.ForEach(o => {
                    datos1.Rows.Add(o["rfc"], o["nombre"], o["categoria"], o["sueldo"],
                        o["aportacion"], o["patronal"], o["tipoaporta"], o["curp"]);
                });
            }

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }
    }
}
