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
    public partial class frmdocusolic : Form
    {
        public string opc { get; set; }
        private List<Dictionary<string, object>> resultado;
        private bool bInsertargrid { get; set; }
        private int iInsetarGrid { get; set; }
        
        public frmdocusolic()
        {
            InitializeComponent();

            primeretapa();
            
            
        }

        public void primeretapa()
        {  // manda a llamar el catalogo
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
            p_hipo.tablaConsultar = "p_hipotecarios";
            p_hipo.ShowDialog();

            cargagrid();

        }

    

        private void llenacampos(Dictionary<string, object> datos)
        {
          

            string folio = Convert.ToString(datos["folio"]);
            frmadicionaldocu document = new frmadicionaldocu(folio);
            document.ShowDialog();

            this.txtsolicitante.Text = Convert.ToString(datos["nombre_em"]);
            this.txtexpediente.Text = Convert.ToString(datos["folio"]);
            this.txtdomi.Text = Convert.ToString(datos["direccion"]);
            this.txtlabora.Text = Convert.ToString(datos["descripcion"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtubicacion.Text = Convert.ToString(datos["direc_inmu"]);
            this.txttel.Text = Convert.ToString(datos["tel_partic"]);

            txtamplia.Text = document.opc;
        }


       

        private void cargagrid()
        {
            string query = "select cve_docum,documento,original,copia  from datos.h_sdocum where expediente='{0}' and sec='{1}'";
            string convert = string.Format(query, txtexpediente.Text, txtamplia.Text);
            globales.consulta(convert);
            var elemento = baseDatos.consulta(convert);
            foreach (var item in elemento)
            {

                string cve_docum = Convert.ToString(item["cve_docum"]);
                string documento = Convert.ToString(item["documento"]);
                bool original = string.IsNullOrWhiteSpace(Convert.ToString(item["original"])) ? false : true; 
                bool copia = string.IsNullOrWhiteSpace(Convert.ToString(item["copia"])) ? false : true; 

                griddocument.Rows.Add(cve_docum, documento, original, copia);

            }
        }

     
        private void frmdocusolic_Load(object sender, EventArgs e)
        {
            griddocument.ReadOnly = true;
            lblcambios.Visible = false;
            lblinsertar.Visible = false;

        }

        private void griddocument_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
     
        }


        private void actualiza()
        {
            int c = this.iInsetarGrid;
            if (c == -1) return;
            {
                this.iInsetarGrid = c;

                if (this.bInsertargrid) return;

                DataGridViewRow row = griddocument.Rows[c];
                string clave = Convert.ToString(row.Cells[0].Value);
                string descripcion = Convert.ToString(row.Cells[1].Value);
                bool original = string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[2].Value));
                bool copia = string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[3].Value));
                string ubicacion = Convert.ToString(row.Cells[4].Value);
                string expediente = txtexpediente.Text;
                string sec = txtamplia.Text;
                try
                {
                    string query = "update datos.h_sdocum set cve_docum='{0}',documento='{1}',original='{2}',copia='{3}',ubicacion='{4}' where expediente='{5}' and sec='{6}'";

                    string convierte = string.Format(query, clave, descripcion, original, copia, ubicacion, expediente, sec);

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

        private void nuevo_registro()
        {

            int c = this.iInsetarGrid;
            if (c == -1) return;
            


                DataGridViewRow row = griddocument.Rows[c];
                string clave = Convert.ToString(row.Cells[0].Value);
                string descripcion = Convert.ToString(row.Cells[1].Value);
                bool original = string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[2].Value));
                bool copia = string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[3].Value));
                string ubicacion = Convert.ToString(row.Cells[4].Value);
                string expediente = txtexpediente.Text;
                string sec = txtamplia.Text;
                try
                {
                    string query = "INSERT INTO datos.h_sdocum(expediente,sec,cve_docum,original,copia,documento,ubicacion)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                    string convierte = string.Format(query, expediente, sec, clave, descripcion, original, copia, ubicacion);

                    if(baseDatos.consulta(convierte,true))
                    {
                        MessageBox.Show("REGISTROS INSERTADOS CORRECTAMENTE AL FOLIO:" + expediente);
                    }
                    else
                    {
                        MessageBox.Show("Existe un error contacte a sistemas");
                    }
                }
                catch
                {

                }

                
        }

        private void frmdocusolic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }

            if (e.KeyCode == Keys.F7)
            {
                griddocument.ReadOnly = false;
                this.bInsertargrid = true;
                MessageBox.Show("HABILITADO MÓDULO PARA INSERTAR NUEVOS REGISTROS");
                lblinsertar.Visible = true;
                lbl1.Visible = false;

            }
            if (e.KeyCode ==Keys.F5)
            {
                griddocument.ReadOnly = false;
                this.bInsertargrid = false;
                MessageBox.Show("HABILITADO MÓDULO PARA ACTUALIZACIONES A REGISTROS");
                lblcambios.Visible = true;
                lbl2.Visible = false;
            }

            if(e.KeyCode ==Keys.F11)
            {
                actualiza();
            }

            if(e.KeyCode == Keys.F12)
            {
                nuevo_registro();
            }
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            griddocument.ReadOnly = false;
            MessageBox.Show("HABILITADO MÓDULO PARA ACTUALIZACIONES");
        }

        private void btnactualiza_Click(object sender, EventArgs e)
        {

        }

        private void griddocument_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void griddocument_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.iInsetarGrid = e.RowIndex;
        }
    }
}
