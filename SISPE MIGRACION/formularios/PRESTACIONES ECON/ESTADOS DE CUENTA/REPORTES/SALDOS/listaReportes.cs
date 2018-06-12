using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.SALDOS
{
    public partial class listaReportes : Form
    {
        private List<Dictionary<string, object>> lista;
        private string fecha { get; set; }
        private bool opcion { get; set; }

        public listaReportes(List<Dictionary<string, object>> lista, bool opcion, string fecha)
        {
            InitializeComponent();
            this.lista = lista;
            this.fecha = fecha;
            this.opcion = opcion;
        }

        private void listaReportes_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object[] obj = new object[lista.Count];

            int contador = 0;

            foreach (Dictionary<string, object> item in this.lista) {
                double folio = Convert.ToDouble(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string ubicPagare = Convert.ToString(item["ubic_pagare"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string numdesc = Convert.ToString(item["numdesc"]);
                string totdesc = Convert.ToString(item["totdesc"]);
                double importe = (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])))?0: Math.Round(Convert.ToDouble(item["importe"]),2);
                double imp_unit = (string.IsNullOrWhiteSpace(Convert.ToString(item["imp_unit"]))) ? 0 : Math.Round(Convert.ToDouble(item["imp_unit"]), 2); ;
                double pagado = (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"]))) ? 0 : Math.Round(Convert.ToDouble(item["pagado"]), 2);
                double saldo = (string.IsNullOrWhiteSpace(Convert.ToString(item["saldo"]))) ? 0 : Math.Round(Convert.ToDouble(item["saldo"]), 2);
                string cta = Convert.ToString(item["cta"]); 
                string cta_descripcion = Convert.ToString(item["cta_descripcion"]);

                object[] aux = { folio,rfc,nombre,ubicPagare,proyecto,numdesc+"/"+totdesc,importe,imp_unit,pagado,saldo,cta,cta_descripcion};
                obj[contador] = aux;
                contador++;
        }

            

            if (listBox1.SelectedIndex == 0) {

                string opcion1 = (opcion)?"QUIROGRAFARIOS":"HIPOTECARIOS";
                string fecha = this.fecha;

                object[][] parametros = new object[2][];
                object[] headers = { "p1","p2","fecha" };
                object[] body = { opcion1,fecha,string.Format("{0}/{1}/{2}",DateTime.Now.Day,DateTime.Now.Month,DateTime.Now.Year) };
                parametros[0] = headers;
                parametros[1] = body;

                globales.reportes("reporteRsaldopSaldosPrestamo", "reporteSinSaldo", obj,"",false,parametros);
            }
        }
    }
}
