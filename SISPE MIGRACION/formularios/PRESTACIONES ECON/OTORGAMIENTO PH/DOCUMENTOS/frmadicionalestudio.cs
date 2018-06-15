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
    public partial class frmadicionalestudio : Form


    {
        public string opc { get; set; }

        public frmadicionalestudio(string folio="")
        {
            InitializeComponent();
            this.txtexpediente.Text = folio;
        }

        private void frmadicionalestudio_Load(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
        }

        private void llenacampos(Dictionary<string, object> datos)
        {
            this.txtexpediente.Text = Convert.ToString(datos["folio"]);
        }

        private void frmadicionalestudio_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmadicionalestudio_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        public void opciones()


        {

            switch (listBox1.SelectedIndex)
            {
                case 0:
                    opc = "0";

                    break;
                case 1:
                    opc = "1";
                    break;
                case 2:
                    opc = "2";
                    break;
                case 3:
                    opc = "3";
                    break;

                default:
                    break;

            }
            validar();


        }


        private void validar()
        {
            string ampliacion = opc;
            string query = "SELECT * FROM datos.h_sdepec where expediente='{0}'and sec='{1}'";
            string valida = string.Format(query, txtexpediente.Text, opc);
            List<Dictionary<string, object>> resultado = globales.consulta(valida);
            if (resultado.Count != 0)
            {
                MessageBox.Show("SE MOSTRARÁ EL DETALLE DEL EXPEDIENTE SELECCIONADO " + txtexpediente.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("FAVOR DE VALIDAR LA INFORMACIÓN, NO SE ENCUENTRA EL EXPEDIENTE " + txtexpediente.Text);
                Close();
            }
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) opciones();
        }
    }
}
