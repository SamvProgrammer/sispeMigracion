﻿using SISPE_MIGRACION.codigo.baseDatos.repositorios;
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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.CAJA
{
    public partial class frmImpresion : Form
    {
        public frmImpresion()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar == 8) {
                textBox1.Text = "";
                fecha2.Value = DateTime.Now;
                return;
            }
            frmCatalogoP_quirog q = new frmCatalogoP_quirog();
            q.tablaConsultar = "p_cajaq";
            q.enviarBool = true;
            q.enviar2 = rellenar;
            q.ShowDialog();
        }

        private void rellenar(Dictionary<string,object> obj) {
            textBox1.Text = Convert.ToString(obj["folio"]);
            string[] arreglo = Convert.ToString(obj["f_descuento"]).Replace(" 12:00:00 a. m.", "").Split('/');
            DateTime fecha = new DateTime(Convert.ToInt32(arreglo[2]), Convert.ToInt32(arreglo[1]), Convert.ToInt32(arreglo[0]));
            fecha2.Value = fecha;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) {
                MessageBox.Show("Favor de seleccionar un folio para generar el reporte", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
                return;
            }

            DialogResult p = MessageBox.Show("¿Desea generar el reporte?","Reporte",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (p == DialogResult.No) return;
            p_cajaQ obj = new p_cajaQ();
            string query = string.Format("select * from  datos.p_cajaq where folio = {0}",textBox1.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            Dictionary<string, object> item = resultado[0];

            obj.total = Convert.ToString(item["total"]);
            obj.nombre_em = Convert.ToString(item["nombre_em"]);
            obj.folio = Convert.ToString(item["folio"]);
            obj.rfc = Convert.ToString(item["rfc"]);
            obj.secretaria = Convert.ToString(item["secretaria"]);
            obj.descripcion = Convert.ToString(item["descripcion"]);
            obj.imp_unit_cap = Convert.ToString(item["imp_unit_cap"]);
            obj.imp_unit_capl = Convert.ToString(item["imp_unit_capl"]);
            obj.descuentos = Convert.ToInt32(item["descuentos"]);
            obj.deldescuentos = Convert.ToInt32(item["deldesc"]);
            obj.numdesc = Convert.ToInt32(item["numdesc"]);
            obj.plazo = Convert.ToInt32(item["plazo"]);
            obj.imp_unit_int = Convert.ToString(item["imp_unit_int"]);
            obj.imp_unit_intl = Convert.ToString(item["imp_unit_intl"]);
            obj.f_descuento = Convert.ToString(item["f_descuento"]);
            string[] meses = { "", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

            string total = globales.checarDecimales(obj.total);
            string nombre = string.Format("{0} ({1})", obj.nombre_em, obj.rfc);
            string descripcion = obj.descripcion;
            string imp_unit_cap = string.Format("${0} ({1})", globales.checarDecimales(obj.imp_unit_cap), obj.imp_unit_capl);
            string texto1 = string.Format("POR CONCEPTO {0} DEL{1} NÚMERO{1} {2} AL {3}/{4} DE SU PRESTAMO QUIROGRAFARIO DE FOLIO {5} {6}", (obj.descuentos == 1) ? "DEL" : "DE LOS", (obj.descuentos == 1) ? "" : "S", obj.deldescuentos, obj.numdesc, obj.plazo, obj.folio, (obj.descuentos < 0) ? "MÁS" : "MENOS -");
            string imp_unit_int = string.Format("${0} ({1})", obj.imp_unit_int, obj.imp_unit_intl);
            string moratorios = string.Format("{0}", (obj.descuentos < 0) ? "BONIFICADOS" : "MORATORIOS");
            string[] arreglo = new string[3];
            if (!string.IsNullOrWhiteSpace(obj.f_descuento))
            {
                obj.f_descuento = obj.f_descuento.Replace(" 12:00:00 a. m.", "");
                if (obj.f_descuento.Contains("/"))
                {
                    arreglo = obj.f_descuento.Split('/');
                }
                else
                {
                    arreglo = obj.f_descuento.Split('-');
                    string aux = arreglo[2];
                    arreglo[2] = arreglo[0];
                    arreglo[0] = aux;
                }
            }
            string fecha = string.Format("OAXACA DE JUAREZ, OAX., {0} DE {1} DE {2}", arreglo[0], meses[Convert.ToInt32(arreglo[1])], arreglo[2]);
            string firma = "L.A.E. PATRICIA CRUZ GOMEZ";
            string cargo = "JEFE DE DEPTO. DE PRESTACIONES ECONOMICAS";
            object[][] parametros = new object[2][];
            object[] header = { "total", "nombre", "descripcion", "impCap", "texto1", "imp_unit_int", "moratorios", "fecha", "firma", "cargo" };
            object[] values = { total, nombre, descripcion, imp_unit_cap, texto1, imp_unit_int, moratorios, fecha, firma, cargo };

            parametros[0] = header;
            parametros[1] = values;

            globales.reportes("reportePagoCajaQ", "p_marcha", new object[] { }, "", false, parametros);
        }
    }
}
