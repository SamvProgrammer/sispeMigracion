﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.VALIDACIONES
{
    public partial class frmSalida : Form
    {
        private object[] arreglo;
        private bool tipoPrestamo;
        private string r3f1;
        private string r3f2;
        private List<Dictionary<string, object>> resultado;
        public frmSalida(bool tipoPrestamo, string t1, string t2, object[] arreglo, List<Dictionary<string, object>> resultado)
        {
            InitializeComponent();
            this.arreglo = arreglo;
            this.tipoPrestamo = tipoPrestamo;
            this.r3f1 = t1;
            this.r3f2 = t2;
            this.resultado = resultado;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult p = MessageBox.Show("¿Seguro que desea realizar la operación?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (p == DialogResult.No) return;

            this.Cursor = Cursors.WaitCursor;
            if (rd1.Checked)
            {
                string tipoPrestamo = this.tipoPrestamo ? "QUIROGRAFARIOS" : "HIPOTECARIOS";
                string f1 = string.Format("{0}/{1}/{2}", r3f1.Split('-')[2], r3f1.Split('-')[1], r3f1.Split('-')[0]);
                string f2 = string.Format("{0}/{1}/{2}", r3f2.Split('-')[2], r3f2.Split('-')[1], r3f2.Split('-')[0]);
                string fechaActual = string.Format("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);

                object[][] parametros = new object[2][];
                object[] headers = { "tipoPrestamo", "R3F2", "R3F1", "fechaActual", "total" };
                object[] body = { tipoPrestamo, f2, f1, fechaActual, this.arreglo.Length.ToString() };

                parametros[0] = headers;
                parametros[1] = body;

                globales.reportes("reporteSinPagos", "p_quirog", this.arreglo, "", false, parametros);

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

                    DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("FOLIO", DotNetDBF.NativeDbType.Numeric, 20,2);
                    DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("RFC", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("NOMBRE_EM", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("PROYECTO", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("IMPORTE", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("UBIC_PAGAR", DotNetDBF.NativeDbType.Char,10);
                    DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("NUMDESC", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("TOTDESC", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("PAGADO", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c10 = new DotNetDBF.DBFField("ULTIMOP", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c11 = new DotNetDBF.DBFField("TIPO_MOV", DotNetDBF.NativeDbType.Char, 10);
                    DotNetDBF.DBFField c12 = new DotNetDBF.DBFField("F_DESCUENT", DotNetDBF.NativeDbType.Char, 20);

                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12 };
                    escribir.Fields = campos;

                    foreach (Dictionary<string, object> item in resultado)
                    {
                        List<object> record = new List<object> {
                        Convert.ToDouble(item["folio"]),
                        Convert.ToString(item["rfc"]),
                        Convert.ToString(item["nombre_em"]),
                        Convert.ToString(item["proyecto"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])))?0:Convert.ToDouble(item["importe"]),
                        Convert.ToString(item["ubic_pagare"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["numdesc"])))?0:Convert.ToDouble(item["numdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["totdesc"])))?0:Convert.ToDouble(item["totdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"])))?0:Convert.ToDouble(item["pagado"]),
                        Convert.ToString(item["ultimop"]),
                        Convert.ToString(item["tipo_mov"]),
                        Convert.ToString(item["f_descuento"]).Replace(" 12:00:00 a. m.","")
                    };

                        escribir.AddRecord(record.ToArray());
                    }

                    escribir.Write(ops);
                    escribir.Close();
                    ops.Close();

                    MessageBox.Show("Archivo .DBF generado exitosamente", "Archivo generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            this.Cursor = Cursors.Default;
        }
    }
}