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
    
    public partial class frmMenuOpciones : Form
    {
        
        public frmMenuOpciones()
        {
            InitializeComponent();
        }

        private void frmMenuOpciones_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime tiempo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                tiempo = tiempo.AddDays(30);
                tiempo = new DateTime(tiempo.Year, tiempo.Month, 15);
                txtFecha.Value = tiempo;

            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                DateTime dFecha = txtFecha.Value;
                string qtipoRelacion = string.Empty;
                string sFecha = string.Format("{0}-{1}-{2}", dFecha.Year,dFecha.Month,dFecha.Day);

                string query = "select DISTINCT tipo_rel from ";

                if (rdQuiro.Checked)
                    query += " datos.d_ecqdep ";
                else
                    query += " datos.d_echdep ";

                string altas = string.Empty;
                if (rdAltas.Checked)
                    altas = "A";
                else if (rdCambios.Checked)
                    altas = "C";
                else
                    altas = "B";


                if (chkAval.Checked && chkNormal.Checked)
                {
                    query += string.Format(" where (tipo_mov = '{0}N' OR tipo_mov = '{0}A') and f_descuento = '{1}'", altas, sFecha);
                    qtipoRelacion = "SUSCRIPTOR Y AVAL";
                }
                else if (chkAval.Checked)
                {
                    query += string.Format(" where tipo_mov = '{0}A' and f_descuento = '{1}'", altas, sFecha);
                    qtipoRelacion = "AVAL";
                 }
                else if (chkNormal.Checked)
                {
                    query += string.Format(" where tipo_mov = '{0}N' and f_descuento = '{1}'", altas, sFecha);
                    qtipoRelacion = "SUSCRIPTOR";
                }
                else {
                    MessageBox.Show("Error en selección","Aviso!!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
                query += "  order by tipo_rel desc";
                string queryGlobal = query;
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                List<Dictionary<string,string>> listaDiskets = new List< Dictionary < string,string>> ();
                foreach (Dictionary<string,object> item in resultado) {
                    string tipoRelacion = Convert.ToString(item["tipo_rel"]);
                    if (!string.IsNullOrWhiteSpace(tipoRelacion)) {
                        query = string.Format("select * from catalogos.disket where cuenta = '{0}'", item["tipo_rel"]);
                        List<Dictionary<string, object>> tmpDisket = globales.consulta(query);
                        if (tmpDisket.Count > 0) {
                            Dictionary<string, string> diccionario = new Dictionary<string, string>();
                            diccionario.Add("cuenta",Convert.ToString(tmpDisket[0]["cuenta"]));
                            diccionario.Add("descripcion", Convert.ToString(tmpDisket[0]["descripcion"]));
                            listaDiskets.Add(diccionario);
                        }
                    }                    
                }

                frmTiporelacion tr = new frmTiporelacion();
                tr.setLista(listaDiskets,queryGlobal,dFecha,qtipoRelacion);
                tr.ShowDialog();
            
            
        }
      
    }
}
