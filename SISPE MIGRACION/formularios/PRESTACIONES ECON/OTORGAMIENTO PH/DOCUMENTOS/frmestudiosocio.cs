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
    public partial class frmestudiosocio : Form
    {
        public frmestudiosocio()
        {
            InitializeComponent();
        }

        private void frmestudiosocio_Load(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();
        
        }

        private void llenampos(Dictionary<string,object> resultado)
        {
            this.txtrfc.Text = Convert.ToString(resultado["rfc"]);

        }
    }
}
