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

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    public partial class frmaportaciones : Form
    {
        private bool insertar;
        public frmaportaciones()
        {
            InitializeComponent();
        }


        private void busqueda()
        {
            frmEmpleados c_aporta = new frmEmpleados();
            c_aporta.enviar = llenacampos;
            c_aporta.ShowDialog();
        }

        private void frmaportaciones_Load(object sender, EventArgs e)
        {
            busqueda();
        }

        private void llenacampos(Dictionary<string, object> datos, bool externo = false)
        {
            this.txtrfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtfechaing.Text = Convert.ToString(datos["fecha_ing"]);
            this.txtnap.Text = Convert.ToString(datos["nap"]);
            this.txtnue.Text = Convert.ToString(datos["nue"]);
            this.txtsueldob.Text = Convert.ToString(datos["sueldo_base"]);
            this.txttiporel.Text = Convert.ToString(datos["tipo_rel"]);
            this.txtdependencia.Text = Convert.ToString(datos["depe"]);


            string quey = ("SELECT ID,inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento,fecharegistro,rfc,idusuario,status,fum" +
                 " FROM datos.aportaciones WHERE rfc = '{0}'AND COALESCE (TRIM(status) <> 'e')AND COALESCE(TRIM(status) <> 'P ')" +
                 "ORDER BY inicio,FINAL,new_tipo; ");
            string rfc = txtrfc.Text;

            string convierte = string.Format(quey, rfc);
            List<Dictionary<string, object>> resultado = globales.consulta(convierte);

            foreach (var item in resultado)
            {
                string inicio = Convert.ToString(item["inicio"]).Replace("12:00:00 a. m.", "");
                string final = Convert.ToString(item["final"]).Replace("12:00:00 a. m.", "");
                string new_tipo = Convert.ToString(item["new_tipo"]);
                string entrada = Convert.ToString(item["entrada"]);
                string salida = Convert.ToString(item["salida"]);
                string cuenta = Convert.ToString(item["cuenta"]);
                string movimiento = Convert.ToString(item["movimiento"]);
                string id = Convert.ToString(item["id"]);


                dtggrid.Rows.Add(inicio, final, new_tipo, entrada, salida, cuenta, movimiento,id);

                dtggrid.ReadOnly = true;
            }


        }

        private void LABEL01_Click(object sender, EventArgs e)
        {

        }

        private void txtAdscripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmaportaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
            if (e.KeyCode == Keys.F12)
            {
                limpia();
                busqueda();
            }
            if (e.KeyCode == Keys.F7)  // habilita el grid para modificar
            {
                dtggrid.ReadOnly = false;
                MessageBox.Show("SE PUEDEN MODIFICAR REGISTROS");

            }
        }




        private void limpia()
        {
            txtrfc.Clear();
            txtnombre.Clear();
            txtproyecto.Clear();
            txtfechaing.Clear();
            txtdependencia.Clear();
            txtnap.Clear();
            txtnue.Clear();
            txtsueldob.Clear();
            txttiporel.Clear();

            dtggrid.Rows.Clear();
        }

        private void frmaportaciones_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dtggrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = dtggrid.Rows[c];
                string inicio = Convert.ToString(row.Cells[0].Value);
                string final = Convert.ToString(row.Cells[1].Value);
                string new_tipo = Convert.ToString(row.Cells[2].Value);
                string entrada = Convert.ToString(row.Cells[3].Value);
                string salida = Convert.ToString(row.Cells[4].Value);
                string cuenta = Convert.ToString(row.Cells[5].Value);
                string movimiento = Convert.ToString(row.Cells[6].Value);
               string id = Convert.ToString(row.Cells[7].Value);

                string query = "update datos.aportaciones set inicio='{0}', final='{1}',new_tipo='{2}',entrada='{3}',salida='{4}', cuenta='{5}',movimiento='{6}' where id='{7}'";
                query = string.Format(query, inicio, final,new_tipo, entrada, salida, cuenta, movimiento,id);

                if (globales.consulta(query,true))
                {
                    MessageBox.Show("Registros modificados de DATOS ADICIONALES");
                  
                }
                else
                    MessageBox.Show("Existe un error en tipo de datos contacte a sistemas");
            }



        
    
            

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
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
