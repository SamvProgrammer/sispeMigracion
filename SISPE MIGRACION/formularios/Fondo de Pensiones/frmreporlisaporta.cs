using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    public partial class frmreporlisaporta : Form
    {
        public frmreporlisaporta()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmreporlisaporta_KeyDown(object sender, KeyEventArgs e)
        {
          if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmreporlisaporta_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea cerrar el módulo?",
          "Cerrar el módulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT a1.rfc, a1.proyecto, a1.cve_categ, a1.sueldo_base, a1.tipo_rel, a1.depe, a1.rpe, a2.FINAL FROM empleados a1 INNER JOIN aportaciones a2 ON a2.rfc = a1.rfc WHERE a1.rfc = 'GUGD760215TGA' ORDER BY FINAL DESC LIMIT 1";

        }
    }
}
