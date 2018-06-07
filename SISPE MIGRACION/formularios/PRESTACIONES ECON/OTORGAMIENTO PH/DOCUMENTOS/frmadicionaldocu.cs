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
    public partial class frmadicionaldocu : Form
    {
        public string opc { get; set;}
        

        public frmadicionaldocu(string folio = "")
        {

         
            InitializeComponent();
            this.txtexpediente.Text = folio;
            

        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) opciones();
            


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
                    opc= "2" ;
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
            string query = "select cve_docum,documento,original,copia  from datos.h_sdocum where expediente='{0}' and sec='{1}'";
            string valida = string.Format(query, txtexpediente.Text,opc);
            List<Dictionary<string,object>>  resultado =  globales.consulta(valida);
            if (resultado.Count != 0)
            {
                MessageBox.Show("SE MOSTRARÁ EL DETALLE DEL EXPEDIENTE SELECCIONADO " + txtexpediente.Text) ;
                this.Close();
            }
            else
            {
                MessageBox.Show("FAVOR DE VALIDAR LA INFORMACIÓN, NO SE ENCUENTRA EL EXPEDIENTE " + txtexpediente.Text)  ;
                DialogResult result = MessageBox.Show("DESEAS GENERAR UNA NUEVA AMPLIACIÓN", "Salir", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("SELECCIONA LA AMPLIACIÓN");
                }
                else
                {
                    this.Close();
                }




            }
           

        }



        private void frmadicionaldocu_Load(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
            p_hipo.enviar2 = llenacampos;
        }

        private void llenacampos(Dictionary<string, object> datos)
        {
            this.txtexpediente.Text = Convert.ToString(datos["folio"]);
        }

        private void frmadicionaldocu_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }
    }
