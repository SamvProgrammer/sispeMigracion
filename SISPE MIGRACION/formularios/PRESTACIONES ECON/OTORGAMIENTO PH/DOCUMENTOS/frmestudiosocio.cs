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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS
{
    public partial class frmestudiosocio : Form
    {
        private bool bInsertargrid { get; set; }
        private int iInsetarGrid { get; set; }
        public string idseleccionado;
        public int Rowselect; 
        public frmestudiosocio()
        {
            InitializeComponent();
            primeretapa();
        }

        private void frmestudiosocio_Load(object sender, EventArgs e)
        {
            griddocument.Visible = true;
        }


        private void primeretapa()
        {
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();
            cargagrid();
        }

        private void llenampos(Dictionary<string,object> datos)
        {
           
            string folio = Convert.ToString(datos["folio"]);
            frmadicionalestudio document = new frmadicionalestudio(folio);
            document.ShowDialog();

            txtetapasol.Text = document.opc;
            this.txtrfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtdomicilio.Text = Convert.ToString(datos["direccion"]);
            this.txtproy.Text = Convert.ToString(datos["proyecto"]);
            this.txttel.Text = Convert.ToString(datos["tel_partic"]);
            this.txtlabora.Text = Convert.ToString(datos["descripcion"]);
            this.txtubicacion.Text = Convert.ToString(datos["direc_inmu"]);
            this.txtexpediente.Text = Convert.ToString(datos["folio"]);
        }

        private void cargagrid()
        {
            string query = "SELECT * FROM datos.h_sdepec where expediente='{0}' and sec='{1}'";
            string expediente = txtexpediente.Text;
            string convierte = string.Format(query,expediente,txtetapasol.Text);
            var elemento = globales.consulta(convierte);
            foreach (var item in elemento)
            {

                string nombre_depec = Convert.ToString(item["nom_depec"]);
                string edad = Convert.ToString(item["edad"]);
                string parentesco = Convert.ToString(item["parentesco"]);
                string escolaridad = Convert.ToString(item["escolaridad"]);
                string ocupacion = Convert.ToString(item["ocupacion"]);
           


                griddocument.Rows.Add(nombre_depec, edad, parentesco, escolaridad, ocupacion);

            }


        }

        private void frmestudiosocio_FormClosing(object sender, FormClosingEventArgs e)
        {
            griddocument.Rows.Clear();
        }
        /// <summary>
        /// /
        /// </summary>
        private void actualiza()
        {
            int c = this.iInsetarGrid;
            if (c == -1) return;
            {
                this.iInsetarGrid = c;

                if (this.bInsertargrid) return;

                DataGridViewRow row = griddocument.Rows[c];
                string nombre_depec = Convert.ToString(row.Cells[0].Value);
                string edad = Convert.ToString(row.Cells[1].Value);
                string parentesco = Convert.ToString(row.Cells[2].Value);
                string escolaridad = Convert.ToString(row.Cells[3].Value);
                string ocupacion = Convert.ToString(row.Cells[4].Value);
                string expediente = txtexpediente.Text;
                string sec = txtetapasol.Text;
             
               
                try
                {
                    string query = "update datos.h_sdepec set nom_depec='{0}',edad='{1}',parentesco='{2}',escolaridad='{3}',ocupacion='{4}' where expediente='{5}' and sec='{6}'";

                    string convierte = string.Format(query, nombre_depec, edad, parentesco, escolaridad, ocupacion, expediente, sec);

                    if (baseDatos.consulta(convierte, true))
                        MessageBox.Show("Registros modificados");
                    else
                        MessageBox.Show("Existe un error contacte a sistemas");
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// ///
        /// </summary>


        private void nuevo_registro()
        {

            int c = this.iInsetarGrid;
            if (c == -1) return;



            DataGridViewRow row = griddocument.Rows[c];
            string nombre_depec = Convert.ToString(row.Cells[0].Value);
            string edad = Convert.ToString(row.Cells[1].Value);
            string parentesco = Convert.ToString(row.Cells[2].Value);
            string escolaridad = Convert.ToString(row.Cells[3].Value);
            string ocupacion = Convert.ToString(row.Cells[4].Value);
            string expediente = txtexpediente.Text;
            string sec = txtetapasol.Text;
           
                string query = "INSERT INTO datos.h_sdepec(nombre_depec, edad, parentesco, escolaridad, ocupacion,expediente,sec)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                string convierte = string.Format(query, nombre_depec, edad, parentesco, escolaridad, ocupacion, expediente, sec);

                if (baseDatos.consulta(convierte, true))
                {
                    MessageBox.Show("REGISTROS INSERTADOS CORRECTAMENTE AL FOLIO:" + expediente);
                }
                else
                {
                    MessageBox.Show("Existe un error contacte a sistemas");
               

            }
        }


        /// <summary>
        /// /
        /// </summary>
        
        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }

            if (e.KeyCode == Keys.F7)
            {
                griddocument.ReadOnly = false;
                this.bInsertargrid = true;
                MessageBox.Show("HABILITADO MÓDULO PARA INSERCIONES");
                lblinsertar.Visible = true;
                lbl1.Visible = false;
                lblcambios.Visible = false;
                lbl2.Visible = false;
            }
            if (e.KeyCode == Keys.F5)
            {
                griddocument.ReadOnly = false;
                this.bInsertargrid = false;
                MessageBox.Show("HABILITADO MÓDULO PARA ACTUALIZACIONES");
                lblcambios.Visible = true;
                lbl2.Visible = false;
                lblinsertar.Visible = false;
                lbl1.Visible = false;
            }

            if (e.KeyCode == Keys.F11)
            {
                actualiza();
                lblcambios.Visible = false;
                lbl1.Visible = true;
                lbl2.Visible = true;

            }

            if (e.KeyCode == Keys.F12)
            {
                nuevo_registro();
                lbl1.Visible = true;
                lbl2.Visible = true;
                lblinsertar.Visible = false;
                    
            }
        }

        private void griddocument_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.iInsetarGrid = e.RowIndex;
        }
    }



}
