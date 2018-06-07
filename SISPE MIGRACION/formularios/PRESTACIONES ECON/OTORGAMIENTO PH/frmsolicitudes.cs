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
    public partial class frmsolicitudes : Form
    {
        private List<Dictionary<string, object>> resultado;
        private bool insertar;

        public frmsolicitudes()
        {
            InitializeComponent();
            btnGuardar.Visible = false;
            btnguardanuevo.Visible = false;

        }

     

        private void btnModifica_Click(object sender, EventArgs e)
        {
            txtRfc.Focus();
            btnGuardar.Visible = true;
            btnNuevo.Visible = false;
            btnModifica.Visible = false;
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();
            llenagrid();

            this.insertar = false;
        }


        private void llenagrid ()
        {
            string folio = txtexpediente.Text;
            string querydb= "SELECT sec,f_solicitud,finalidad,descri_finalid,trel ,plazoa,cap_prest,plazo,status,f_autorizacion,id  FROM datos.h_solici where expediente={0}";
            string pasa = string.Format(querydb,txtexpediente.Text);

            resultado  = baseDatos.consulta(pasa);

            foreach (var item in resultado)
            {
                string sec = Convert.ToString(item["sec"]);
                string f_solicitud = Convert.ToString(item["f_solicitud"]).Replace("12:00:00 a. m.", "");
                string finalidad = Convert.ToString(item["finalidad"]);
                string descri_finalid = Convert.ToString(item["descri_finalid"]);
                string trel = Convert.ToString(item["trel"]);
                string plazoa = Convert.ToString(item["plazoa"]);
                string cap_prest = Convert.ToString(item["cap_prest"]);
                string plazo = Convert.ToString(item["plazo"]);
                string status = Convert.ToString(item["status"]);
                string f_autorizacion = Convert.ToString(item["f_autorizacion"]).Replace("12:00:00 a. m.", "");

                data01.Rows.Add(sec, f_solicitud , finalidad , descri_finalid , trel ,plazoa, cap_prest,plazo, status,f_autorizacion);
            }

        }

        
           // string actuagrid = "update from datos.h_solici set f_solicitud='{0}',finalidad={1},descri_finalid='{2}',trel='{3}' ,plazoa={4},cap_prest={5},plazo={6},status='{7}',f_autorizacion='{8}' where expediente= {9} and sec='{10}' ";
          //  string temp = string.Format(actuagrid , txt)

        

     

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
            txtRfc.Focus();
            data01.ReadOnly = false;

            this.insertar = true;
           

            


        }

        private void SeleccionaTasaInteresH()
        {    //procedcimiento para obtener tasa hipotecarios
            double T_INTERESH = 0.0025;
            string query = "SELECT tasa FROM catalogos.tasa  where t_prestamo='H' ORDER BY fmodif desc LIMIT 1";
            globales.consulta(query, true);






        }

        private void Totales_PH()
        { SeleccionaTasaInteresH();   
            // Obtnemos folio
            string maximo = "SELECT MAX(expediente+1) as tmp FROM datos.h_solici";
            List<Dictionary<string, object>> resultado = globales.consulta(maximo);
            string expedient = Convert.ToString(resultado[0]["tmp"]);
            string variables = "select cap_prest , plazo , plazoa , plazo from datos.h_solici where expediente ='{0}'" ;
            string paso = string.Format(variables, expedient);
            List<Dictionary<string, object>> tmpp = globales.consulta(paso);

            string cap_prest= Convert.ToString(tmpp[0]["cap_prest"]);





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

            string dato01 = txtdependencia.Text;
            string depen = "SELECT descripcion FROM catalogos.dependencias where proy = '{0}'";
            string conv = string.Format(depen, dato01);
            List<Dictionary<string, object>> tmp = globales.consulta(conv);
            if (tmp.Count > 0)
            {
                string descripcion = Convert.ToString(tmp[0]["descripcion"]);
                txtccatdes.Text = descripcion;
            }


        }

        

        public  void nuevo()
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
            Totales_PH();
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
            nuevo();
            limpiacampos();
            btnguardanuevo.Visible = false;
            btnModifica.Visible = true;
            Totales_PH();
        }

        private void txtexpediente_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void data01_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            if (this.insertar)
            {

                string maximo = "SELECT MAX(folio+1) as tmp FROM datos.p_hipotecarios";
                List<Dictionary<string, object>> resultado = globales.consulta(maximo);
                string expedi = (string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["tmp"]))) ? "0" : Convert.ToString(resultado[0]["tmp"]);
                DataGridViewRow row = data01.Rows[c];
                string tipo = Convert.ToString(row.Cells[0].Value);
                string f_solicitud = (string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[1].Value)) ? "null" : "'" + Convert.ToString(row.Cells[1].Value) + "'");
                string finalidad = Convert.ToString(row.Cells[2].Value);
                string descri_finalid = Convert.ToString(row.Cells[3].Value);
                string trel = Convert.ToString(row.Cells[4].Value);
                string plazoa = (string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[5].Value)))?"0": Convert.ToString(row.Cells[5].Value);
                string cap_prest = (string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[6].Value)))?"0": Convert.ToString(row.Cells[6].Value);
                string plazo = (string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[7].Value)))?"0": Convert.ToString(row.Cells[7].Value);
                string status = Convert.ToString(row.Cells[8].Value);
                string f_autorizacion = (string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[9].Value)))?"null" : "'"+Convert.ToString(row.Cells[9].Value)+"'";

                string insert = "INSERT INTO datos.h_solici(expediente,sec,f_solicitud,finalidad,descri_finalid,trel,plazoa,cap_prest,plazo,status,f_autorizacion) values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10})";
                insert = string.Format(insert, expedi,tipo,f_solicitud,finalidad,descri_finalid,trel,plazoa,cap_prest,plazo,status,f_autorizacion);
                if (globales.consulta(insert, true))
                {
                    MessageBox.Show("Registros modificados de DATOS ADICIONALES");
                    MessageBox.Show("Para guardar cambios al expediente, seleccione el boton GUARDAR");
                }
                else
                    MessageBox.Show("Existe un error en tipo de datos contacte a sistemas");
            }
            else {
                string id = resultado[c]["id"].ToString();

                DataGridViewRow row = data01.Rows[c];
                string tipo = Convert.ToString(row.Cells[0].Value);
                string f_solicitud = Convert.ToString(row.Cells[1].Value);
                string finalidad = Convert.ToString(row.Cells[2].Value);
                string descri_finalid = Convert.ToString(row.Cells[3].Value);
                string trel = Convert.ToString(row.Cells[4].Value);
                string plazoa = Convert.ToString(row.Cells[5].Value);
                string cap_prest = Convert.ToString(row.Cells[6].Value);
                string plazo = Convert.ToString(row.Cells[7].Value);
                string status = Convert.ToString(row.Cells[8].Value);
                string f_autorizacion = Convert.ToString(row.Cells[9].Value);

                try
                {
                    string query = "update  datos.h_solici set f_solicitud='{0}',finalidad={1},descri_finalid='{2}',trel='{3}' ,plazoa={4},cap_prest={5},plazo='{6}',status='{7}',f_autorizacion='{8}' where id={9}";
                    string qry = string.Format(query, f_solicitud, finalidad, descri_finalid, trel, plazoa, cap_prest, plazo, status, f_autorizacion, id);
                    if (globales.consulta(qry, true))
                    {
                        MessageBox.Show("Registros modificados de DATOS ADICIONALES");
                        MessageBox.Show("Para guardar cambios al expediente, seleccione el boton GUARDAR");
                    }
                    else
                        MessageBox.Show("Existe un error en tipo de datos contacte a sistemas");
                }

                catch
                {


                }
            }


           

        }

        

        private void frmsolicitudes_Load(object sender, EventArgs e)
        {

        }

        private void insertagrid (object sender, DataGridViewCellEventArgs e)
        {
          

        }

        private void data01_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int c = e.RowIndex;
                
            }

            catch
            {


            }
        }

       
    }
}

