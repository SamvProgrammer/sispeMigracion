using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS;
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
    public partial class frmDocumentos : Form
    {
        public frmDocumentos()
        {
            InitializeComponent();

          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) opciones();
        }

        private void opciones() {
            switch (listBox1.SelectedIndex) {
                case 0:
                    new frmdocusolic().ShowDialog();
                    Close();
                    
                    break;
                case 1:
                    new frmavaluo().ShowDialog();
                    break;
                case 2:
                    new frmavaluo().ShowDialog();
                    break;
                case 3:
                    MessageBox.Show("es item 1");
                    break;
                case 4:
                    MessageBox.Show("es item 1");
                    break;
                case 5:
                    MessageBox.Show("es item 1");
                    break;
                case 6:
                    MessageBox.Show("es item 1");
                    break;
                case 7:
                    MessageBox.Show("es item 1");
                    break;
                case 8:
                    MessageBox.Show("es item 1");
                    break;
                case 9:
                    MessageBox.Show("es item 1");
                    break;
                case 10:
                    MessageBox.Show("es item 1");
                    break;
                default:
                    break;

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            opciones();
        }

        private void frmDocumentos_Load(object sender, EventArgs e)
        {

        }
    }
}
