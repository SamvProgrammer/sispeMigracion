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
        private List<Dictionary<string, object>> resultado;
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

            
            resultado = globales.leerDbf(ruta);
            string claveDependencia = Convert.ToString(resultado[0]["proyecto"]).Substring(0,3);
            string desde = Convert.ToString(resultado[0]["desde"]);
            string hasta = Convert.ToString(resultado[0]["hasta"]);

            string query = string.Format("select descripcion from catalogos.cuenta where proy = '{0}'",claveDependencia);
            
            List<Dictionary<string,object>> tmp1 = globales.consulta(query);

            txtDesde.Text = desde;
            txtHasta.Text = hasta;
            txtDependencia.Text = (tmp1.Count == 0)?"":Convert.ToString(tmp1[0]["descripcion"]);

            DialogResult p = MessageBox.Show("¿Desea visualizar los datos del archivo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (p == DialogResult.Yes) {
                this.Cursor = Cursors.WaitCursor;
                datos1.Rows.Clear();
                resultado.ForEach(o => {
                    datos1.Rows.Add(o["rfc"], o["nombre"], o["categoria"], o["sueldo"],
                        o["aportacion"], o["patronal"], o["tipoaporta"], o["curp"]);
                });
                this.Cursor = Cursors.Default;
            }

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArchivo.Text)) {
                MessageBox.Show("Favor de elegir un archivo dbf", "Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            string fecha = string.Format("{0}-{1}-{2}",dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);

            string query = string.Format("select count(archivo) as cantidad from datos.aportaciones where archivo = '{0}'",txtArchivo.Text);
            if (globales.consulta(query).Count > 0) {
                MessageBox.Show("Registros ya se encuentran registrador","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            foreach (Dictionary<string,object> item in resultado) {
                string rfc = Convert.ToString(item["rfc"]).Split('|')[0];
                string desde = Convert.ToString(item["desde"]);
                string hasta = Convert.ToString(item["hasta"]);
                string aportacion = Convert.ToString(item["aportacion"]);
                string proyecto = Convert.ToString(item["proyecto"]).Substring(0, 3);
                string curp = Convert.ToString(item["curp"]);

                query = "insert into datos.aportaciones (rfc,inicio,final,new_tipo,movimiento,entrada,status,cuenta,fecharegistro,curp,archivo) values('{0}','{1}','{2}','{3}','APORTACION',{4},'n','{5}','{6}','{7}','{8}')";
                query = string.Format(query,rfc,desde,hasta,"AN",aportacion, proyecto, fecha,curp,txtArchivo.Text);
                globales.consulta(query,true);
            }
           
        }
    }
}
