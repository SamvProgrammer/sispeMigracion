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
    public partial class frmestudiotec : Form
    {
        public frmestudiotec()
        {
            InitializeComponent();
        }

        private void frmestudiotec_Load(object sender, EventArgs e)
        {

        }

        public void primeretapa()
        {  // manda a llamar el catalogo
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();

          

        }



        private void llenacampos(Dictionary<string, object> datos)
        {


            string folio = Convert.ToString(datos["folio"]);
            frmadicionaldocu document = new frmadicionaldocu(folio);
            document.ShowDialog();

            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtnombre.Text = Convert.ToString(datos["rfc"]);
            this.txtdomicilio.Text = Convert.ToString(datos["direccion"]);
            this.txtlabora.Text = Convert.ToString(datos["descripcion"]);
            this.txtubicacion.Text = Convert.ToString(datos["direc_inmu"]);
            this.txttel.Text = Convert.ToString(datos["tel_partic"]);
            this.txtproy.Text = Convert.ToString(datos["proyecto"]);
            this.txtexpediente.Text = Convert.ToString(datos["folio"]);

            txtampliacion.Text = document.opc;
        }
    }
}
