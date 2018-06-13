using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            foreach (Dictionary<string, object> item in this.lista)
            {
                double folio = Convert.ToDouble(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string ubicPagare = Convert.ToString(item["ubic_pagare"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string numdesc = Convert.ToString(item["numdesc"]);
                string totdesc = Convert.ToString(item["totdesc"]);
                double importe = (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"]))) ? 0 : Math.Round(Convert.ToDouble(item["importe"]), 2);
                double imp_unit = (string.IsNullOrWhiteSpace(Convert.ToString(item["imp_unit"]))) ? 0 : Math.Round(Convert.ToDouble(item["imp_unit"]), 2); ;
                double pagado = (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"]))) ? 0 : Math.Round(Convert.ToDouble(item["pagado"]), 2);
                double saldo = (string.IsNullOrWhiteSpace(Convert.ToString(item["saldo"]))) ? 0 : Math.Round(Convert.ToDouble(item["saldo"]), 2);
                string cta = Convert.ToString(item["cta"]);
                string cta_descripcion = Convert.ToString(item["cta_descripcion"]);

                object[] aux = { folio, rfc, nombre, ubicPagare, proyecto, numdesc + "/" + totdesc, importe, imp_unit, pagado, saldo, cta, cta_descripcion };
                obj[contador] = aux;
                contador++;
            }
            string opcion1 = (opcion) ? "QUIROGRAFARIOS" : "HIPOTECARIOS";
            string fecha = this.fecha;

            object[][] parametros = new object[2][];
            object[] headers = { "p1", "p2", "fecha" };
            object[] body = { opcion1, fecha, string.Format("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year) };
            parametros[0] = headers;
            parametros[1] = body;


            if (listBox1.SelectedIndex == 0)
            {
                globales.reportes("reporteRsaldopSaldosPrestamo", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 1)
            {

                globales.reportes("reporteSaldopResumenCuenta", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 2)
            {

                globales.reportes("reporteRSaldoPAlfabetico", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 3)
            {


                globales.reportes("reporteRSaldoPAlfabeticoSinSaldordlc", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 4)
            {


                globales.reportes("reporteRSaldoPFolio", "reporteSinSaldo", obj, "", false, parametros);
            }
            else
            {
                SaveFileDialog dialogoGuardar = new SaveFileDialog();
                dialogoGuardar.AddExtension = true;
                dialogoGuardar.DefaultExt = ".dbf";
                if (dialogoGuardar.ShowDialog() == DialogResult.OK)
                {

                    string ruta = dialogoGuardar.FileName;

                    Stream ops = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    DotNetDBF.DBFWriter escribir = new DotNetDBF.DBFWriter();
                    escribir.DataMemoLoc = ruta.Replace("dbf", "dbt");

                    DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("FOLIO", DotNetDBF.NativeDbType.Numeric, 20, 2);
                    DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("RFC", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("NOMBRE_EM", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("PROYECTO", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("IMP_UNIT", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("F_PRIMDESC", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("IMPORTE", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("UBIC_PAGAR", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("NUMDESC", DotNetDBF.NativeDbType.Numeric, 10,2);
                    DotNetDBF.DBFField c10 = new DotNetDBF.DBFField("TOTDESC", DotNetDBF.NativeDbType.Numeric, 10,2);
                    DotNetDBF.DBFField c11 = new DotNetDBF.DBFField("PAGADO", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c12 = new DotNetDBF.DBFField("FECHA", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c13 = new DotNetDBF.DBFField("CTA", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c14 = new DotNetDBF.DBFField("SALDO", DotNetDBF.NativeDbType.Numeric, 10, 2);

                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14 };
                    escribir.Fields = campos;

                    foreach (Dictionary<string, object> item in this.lista)
                    {
                        List<object> record = new List<object> {
                        Convert.ToDouble(item["folio"]),
                        Convert.ToString(item["rfc"]),
                        Convert.ToString(item["nombre_em"]),
                        Convert.ToString(item["proyecto"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["imp_unit"])))?0:Convert.ToDouble(item["imp_unit"]),
                        Convert.ToString(item["f_primdesc"]).Replace(" 12:00:00 a. m.",""),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])))?0:Convert.ToDouble(item["importe"]),
                        Convert.ToString(item["ubic_pagare"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["numdesc"])))?0:Convert.ToDouble(item["numdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["totdesc"])))?0:Convert.ToDouble(item["totdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"])))?0:Convert.ToDouble(item["pagado"]),
                        Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.",""),
                        Convert.ToString(item["cta"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["saldo"])))?0:Convert.ToDouble(item["saldo"])
                    };

                        escribir.AddRecord(record.ToArray());
                    }

                    escribir.Write(ops);
                    escribir.Close();
                    ops.Close();

                    MessageBox.Show("Archivo .DBF generado exitosamente", "Archivo generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
