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
            txtfecha.ReadOnly = true;
            txtdestinario.ReadOnly = true;
            txtvalor.ReadOnly = true;
            LBL2.Visible = false;


        }

        private void bloquear ()
        {
            txtfecha.ReadOnly = true;
            txtdestinario.ReadOnly = true;
            txtvalor.ReadOnly = true;
        }


        private void insertar()
        {
            string query = "INSERT INTO datos.h_evalua (f_solic,nombre,valor_bien,expediente)VALUES('{0}','{1}','{2}','{3}')";
            string con = string.Format(query, txtfecha.Text, txtdestinario.Text, txtvalor.Text, txtexpediente.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(con);
            if (globales.consulta(con, true))
            {
                MessageBox.Show("Registros insertados");
            }
            else
            {
                MessageBox.Show("Existe un error en el procedimiento");
            }
       
        }

        private void actualizar()
        {
            string query = "update datos.h_evalua set f_solic='{0}',nombre='{1}',valor_bien='{2}' WHERE expediente='{3}'";
            string con = string.Format(query, txtfecha.Text, txtdestinario.Text, txtvalor.Text, txtexpediente.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(con);
            if (globales.consulta(con, true))
            {
                MessageBox.Show("Registros actualizados");
            }
            else
            {
                MessageBox.Show("Existe un error en el procedimiento");
            }

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

            if (resultado.Count <= 0)
            {
                MessageBox.Show("NO HAY DATOS ADICIONALES DEL EXPEDIENTE SELECCIONADO, FAVOR DE INGRESAR REGISTROS");
            }
            else
            {

                this.txtfecha.Text = Convert.ToString(resultado[0]["f_solic"]).Replace("12:00:00 a. m.", "");
                this.txtdestinario.Text = Convert.ToString(resultado[0]["nombre"]);
                this.txtvalor.Text = Convert.ToString(resultado[0]["valor_bien"]);
            }
            // joelk d




        }

        private void frmavaluo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {

                MessageBox.Show("YA ES POSIBLE INSERTAR O MODIFICAR LOS REGISTROS ");
               
                txtfecha.ReadOnly = false;
                txtdestinario.ReadOnly = false;
                txtvalor.ReadOnly = false;
                LBL2.Visible = true;
                LBL3.Visible = true;

            }

            if (e.KeyCode == Keys.F10)
            {
                insertar();
            }
           
            if (e.KeyCode == Keys.F12)
            {
                actualizar();
            }


        }
    }
}



