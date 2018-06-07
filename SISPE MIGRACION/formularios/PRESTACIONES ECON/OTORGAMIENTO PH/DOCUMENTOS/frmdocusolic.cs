using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
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
    public partial class frmdocusolic : Form
    {
        public string opc { get; set; }
        public frmdocusolic()
        {
            InitializeComponent();

            primeretapa();
      
            
        }

        public void primeretapa()
        {  // manda a llamar el catalogo
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();

            cargagrid();

        }

    

        private void llenacampos(Dictionary<string, object> datos)
        {
          

            string folio = Convert.ToString(datos["folio"]);
            frmadicionaldocu document = new frmadicionaldocu(folio);
            document.ShowDialog();

            this.txtsolicitante.Text = Convert.ToString(datos["nombre_em"]);
            this.txtexpediente.Text = Convert.ToString(datos["folio"]);
            this.txtdomi.Text = Convert.ToString(datos["direccion"]);
            this.txtlabora.Text = Convert.ToString(datos["descripcion"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtubicacion.Text = Convert.ToString(datos["direc_inmu"]);
            this.txttel.Text = Convert.ToString(datos["tel_partic"]);

            txtamplia.Text = document.opc;
        }


       

        private void cargagrid()
        {
            string query = "select cve_docum,documento,original,copia  from datos.h_sdocum where expediente='{0}' and sec='{1}'";
            string convert = string.Format(query, txtexpediente.Text, txtamplia.Text);
            globales.consulta(convert);
            var elemento = baseDatos.consulta(convert);
            foreach (var item in elemento)
            {

                string cve_docum = Convert.ToString(item["cve_docum"]);
                string documento = Convert.ToString(item["documento"]);
                bool original = Convert.ToString(item["original"]) != "*" ? false : true; 
                bool copia = Convert.ToString(item["copia"]) != "*" ? false : true; 

                griddocument.Rows.Add(cve_docum, documento, original, copia);

            }
        }

        private void frmdocusolic_Load(object sender, EventArgs e)
        {

        }
    }
}
