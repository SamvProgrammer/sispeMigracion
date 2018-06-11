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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS
{
    public partial class frmavaluo : Form
    {
        public frmavaluo()
        {
            InitializeComponent();
        }

        private void frmavaluo_Load(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();

          

        }



        private void llenacampos(Dictionary<string, object> datos)
        {


            this.txtexpediente.Text = Convert.ToString(datos["folio"]);
            this.txtrfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtdomicilio.Text = Convert.ToString(datos["direccion"]);
            this.txtdependencia.Text = Convert.ToString(datos["secretaria"]);
            this.txttel.Text = Convert.ToString(datos["tel_ofici"]);
            this.txtcvecateg.Text = Convert.ToString(datos["cve_categ"]);
            this.txtubicacion.Text = Convert.ToString(datos["direc_inmu"]);


            string query = "SELECT * FROM datos.h_evalua where expediente='{0}' ";
            string con = string.Format(query, txtexpediente.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(con);
            this.txtfecha.Text = Convert.ToString(resultado[0]["f_solic"]).Replace("12:00:00 a. m.", "");
            this.txtdestinario.Text = Convert.ToString(resultado[0]["nombre"]);
            this.txtvalor.Text = Convert.ToString(resultado[0]["valor_bien"]);

            // joelk d




        }
    }
    
}
