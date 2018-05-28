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
    public partial class frmsolicitudes : Form
    {
        public frmsolicitudes()
        {
            InitializeComponent();
            btnGuardar.Visible = false;
            btnguardanuevo.Visible = false;

        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
  
            btnGuardar.Visible = true;
            btnNuevo.Visible = false;
            btnModifica.Visible = false;
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();

        }

        private void llenacampos(Dictionary<string, object> datos)
        {

            limpiacampos();
            this.txtexpediente.Text = Convert.ToString(datos["folio"]);
            this.txtRfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre_em.Text = Convert.ToString(datos["nombre_em"]);
            this.txtdomi.Text = Convert.ToString(datos["direccion"]);
            this.txtfechanac.Text = Convert.ToString(datos["fecha_nac"]).Replace("12:00:00 a. m.", "");
            this.txtconyuge.Text = Convert.ToString(datos["nombre_cony"]);
            this.txttel.Text = Convert.ToString(datos["tel_partic"]);
            this.txtedad.Text = Convert.ToString(datos["edad"]);
            this.txtedocivil.Text = Convert.ToString(datos["edo_civil"]);

            ///primer panel
            /// 
            this.txtdependencia.Text = Convert.ToString(datos["secretaria"]);
            this.txtproy.Text = Convert.ToString(datos["proyecto"]);
            this.txtfechadenom.Text = Convert.ToString(datos["f_nombran"]).Replace("12:00:00 a. m.", "");
            this.txtsueldob.Text = Convert.ToString(datos["sueldo_base"]);
            this.txtcvecateg.Text = Convert.ToString(datos["cve_categ"]);
            this.txtteléfono.Text = Convert.ToString(datos["tel_ofici"]);
            this.txtantiguedad.Text = Convert.ToString(datos["ant_a"]);
            this.txtnomina.Text = Convert.ToString(datos["nomina"]);
            this.txtTrl.Text = Convert.ToString(datos["tipo_rel"]);
            this.txtsexo.Text = Convert.ToString(datos["sexo"]);
            this.txtccatdes.Text = Convert.ToString(datos["ccatdes"]);
            txtdescripcion.Text = Convert.ToString(datos["descripcion"]);
            // panel 2
            this.txtubicacion.Text = Convert.ToString(datos["direc_inmu"]);



        }

        private void limpiacampos()
        {

            txtantiguedad.Clear();
            txtconyuge.Clear();
            txtcvecateg.Clear();
            txtdependencia.Clear();
            txtdomi.Clear();
            txtedad.Clear();
            txtedocivil.Clear();
            txtexpediente.Clear();
            txtfechadenom.Clear();
            txtfechanac.Clear();
            txtnombre_em.Clear();
            txtnomina.Clear();
            txtproy.Clear();
            txtRfc.Clear();
            txtsueldob.Clear();
            txttel.Clear();
            txtteléfono.Clear();
            txtTrl.Clear();
            txtubicacion.Clear();
            txtsexo.Clear();
            txtdescripcion.Clear();
            txtccatdes.Clear();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmexpediente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }

            if (e.KeyCode == Keys.F4)
            {
                
            }
        }

        private void frmexpediente_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmexpediente_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiacampos();
            emplea();
            btnguardanuevo.Visible = true;
            btnModifica.Visible = false;
            txtexpediente.Text = "AUTOGENERADO";





        }

        private void emplea()
        {
            frmEmpleados empleados = new frmEmpleados();
            empleados.enviar = rellenarConsulta;
            empleados.ShowDialog();

        }

        private void rellenarConsulta(Dictionary<string, object> resultado, bool externo = false)
        {
            this.txtRfc.Text = Convert.ToString(resultado["rfc"]);
            this.txtnombre_em.Text = Convert.ToString(resultado["nombre_em"]);
            this.txtdomi.Text = Convert.ToString(resultado["direccion"]);
            this.txtfechanac.Text = Convert.ToString(resultado["fecha_nac"]).Replace("12:00:00 a. m.", "");
            this.txtproy.Text = Convert.ToString(resultado["proyecto"]);
            this.txtsueldob.Text = Convert.ToString(resultado["sueldo_base"]);
            this.txtcvecateg.Text = Convert.ToString(resultado["cve_categ"]);
            this.txtTrl.Text = Convert.ToString(resultado["tipo_rel"]);
            this.txtsexo.Text = Convert.ToString(resultado["sexo"]);
            this.txtdependencia.Text = Convert.ToString(resultado["depe"]);

            string dato = txtdependencia.Text;
            string depe = "SELECT dependencias FROM catalogos.dependencias where proy = '{0}'";
            string convierte = string.Format(depe, dato);
            globales.consulta(depe, true);




        }

        private void nuevo()
        {

            string maximo = "SELECT MAX(folio+1) as tmp FROM datos.p_hipotecarios";

            List<Dictionary<string, object>> resultado = globales.consulta(maximo);

            string folio = Convert.ToString(resultado[0]["tmp"]);
            string nuevo = "INSERT INTO datos.p_hipotecarios (rfc,nombre_em,sexo,fecha_nac,direccion,proyecto,nombre_cony,cve_categ,sueldo_base,tipo_rel,nomina,secretaria,descripcion,edad,edo_civil,tel_ofici,tel_partic,direc_inmu,f_nombran,ant_a,ccatdes,folio)VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')";
            string query = string.Format(nuevo, txtRfc.Text, txtnombre_em.Text, txtsexo.Text, txtfechanac.Text, txtdomi.Text, txtproy.Text, txtconyuge.Text, txtcvecateg.Text, txtsueldob.Text, txtTrl.Text, txtnomina.Text, txtdependencia.Text, txtdescripcion.Text, txtedad.Text, txtedocivil.Text, txttel.Text, txtteléfono.Text, txtubicacion.Text, txtfechadenom.Text, txtantiguedad.Text, txtccatdes.Text, folio);
            if (globales.consulta(query, true))
            {
                MessageBox.Show("Nuevo Folio Insertado");

                MessageBox.Show("EL FOLIO GENERADO ES:" + folio);
            }
            else
            {
                MessageBox.Show("ERROR , CONTACTE A SISTEMAS");
            }

        }

        private void actualiza()
        {
            try
            {
                string llena = "UPDATE datos.p_hipotecarios set rfc= '{0}',nombre_em='{1}',sexo='{2}',fecha_nac='{3}',direccion='{4}',proyecto='{5}',nombre_cony='{6}',cve_categ='{7}',sueldo_base='{8}',tipo_rel='{9}',nomina='{10}',secretaria='{11}',descripcion='{12}',edad='{13}',edo_civil= '{14}',tel_ofici='{15}',tel_partic='{16}',direc_inmu='{17}',f_nombran='{18}',ant_a='{19}',ccatdes='{20}' where folio={21}";
                string query = string.Format(llena, txtRfc.Text, txtnombre_em.Text, txtsexo.Text, txtfechanac.Text, txtdomi.Text, txtproy.Text, txtconyuge.Text, txtcvecateg.Text, txtsueldob.Text, txtTrl.Text, txtnomina.Text, txtdependencia.Text, txtdescripcion.Text, txtedad.Text, txtedocivil.Text, txttel.Text, txtteléfono.Text, txtubicacion.Text, txtfechadenom.Text, txtantiguedad.Text, txtccatdes.Text, txtexpediente.Text);
                if (globales.consulta(query, true))
                    MessageBox.Show("Registros modificados");
                else
                    MessageBox.Show("Existe un error contacte a sistemas");
            }

            catch
            {


            }
           
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtantiguedad.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtconyuge.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtcvecateg.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtdependencia.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtdomi.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtedad.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtedocivil.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtfechadenom.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtfechanac.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtnombre_em.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtnomina.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtproy.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtRfc.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtsueldob.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txttel.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtteléfono.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTrl.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtubicacion.Text))
            {
                MessageBox.Show("NO PUEDES DEJAR DATOS SIN RELLENAR");
                txtdependencia.Focus();
                return;
            }

            actualiza();
            limpiacampos();
            btnNuevo.Visible = true;
            btnguardanuevo.Visible = false;
            btnGuardar.Visible = false;
            btnModifica.Visible = true;

        }

        private void btnguardanuevo_Click(object sender, EventArgs e)
        {

            nuevo();
            limpiacampos();
            btnguardanuevo.Visible = false;
            btnModifica.Visible = true;
        }

        private void txtexpediente_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

