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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES
{

    public partial class frmMenuOpciones : Form
    {
        private bool esReporte;
        public frmMenuOpciones(bool esReporte = false)
        {
            InitializeComponent();
            this.esReporte = esReporte;
        }

        private void frmMenuOpciones_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime tiempo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                tiempo = tiempo.AddDays(30);
                tiempo = new DateTime(tiempo.Year, tiempo.Month, 15);
                txtFecha.Value = tiempo;

            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombreDbf = string.Empty;
            DateTime dFecha = txtFecha.Value;
            string qtipoRelacion = string.Empty;
            string sFecha = string.Format("{0}-{1}-{2}", dFecha.Year, dFecha.Month, dFecha.Day);

            string query = (this.esReporte) ? "select * from " : "select DISTINCT tipo_rel from ";

            if (rdQuiro.Checked)
            {
                query += " datos.d_ecqdep ";
                nombreDbf = "PQ_";
            }
            else
            {
                query += " datos.d_echdep ";
                nombreDbf = "PH_";
            }

            string altas = string.Empty;
            if (rdAltas.Checked)
                altas = "A";
            else if (rdCambios.Checked)
                altas = "C";
            else
                altas = "B";


            if (chkAval.Checked && chkNormal.Checked)
            {
                query += string.Format(" where (tipo_mov = '{0}N' OR tipo_mov = '{0}A') and f_descuento = '{1}'", altas, sFecha);
                qtipoRelacion = "SUSCRIPTOR Y AVAL";
                nombreDbf += string.Format("{0}N_{0}A", altas);
            }
            else if (chkAval.Checked)
            {
                query += string.Format(" where tipo_mov = '{0}A' and f_descuento = '{1}'", altas, sFecha);
                qtipoRelacion = "AVAL";
                nombreDbf += string.Format("{0}A", altas);
            }
            else if (chkNormal.Checked)
            {
                query += string.Format(" where tipo_mov = '{0}N' and f_descuento = '{1}'", altas, sFecha);
                qtipoRelacion = "SUSCRIPTOR";
                nombreDbf += string.Format("{0}N", altas);
            }
            else
            {
                MessageBox.Show("Error en selección", "Aviso!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            query += "  order by tipo_rel desc";
            string queryGlobal = query;

            if (this.esReporte)
            {
                MessageBox.Show(string.Format("Seleccionando altas del {0} de sector central", sFecha), "Seleccionando", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            if (this.esReporte) goto seccionReporte;

            List<Dictionary<string, string>> listaDiskets = new List<Dictionary<string, string>>();
            foreach (Dictionary<string, object> item in resultado)
            {
                string tipoRelacion = Convert.ToString(item["tipo_rel"]);
                if (!string.IsNullOrWhiteSpace(tipoRelacion))
                {
                    query = string.Format("select * from catalogos.disket where cuenta = '{0}'", item["tipo_rel"]);
                    List<Dictionary<string, object>> tmpDisket = globales.consulta(query);
                    if (tmpDisket.Count > 0)
                    {
                        Dictionary<string, string> diccionario = new Dictionary<string, string>();
                        diccionario.Add("cuenta", Convert.ToString(tmpDisket[0]["cuenta"]));
                        diccionario.Add("descripcion", Convert.ToString(tmpDisket[0]["descripcion"]));
                        listaDiskets.Add(diccionario);
                    }
                }
            }

            frmTiporelacion tr = new frmTiporelacion();
            tr.setLista(listaDiskets, queryGlobal, dFecha, qtipoRelacion);
            tr.ShowDialog();
            return;

        seccionReporte:

            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.DefaultExt = ".dbf";
            if (dialogoGuardar.ShowDialog() == DialogResult.OK)
            {

                string ruta = dialogoGuardar.FileName;

                Stream ops = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                DotNetDBF.DBFWriter escribir = new DotNetDBF.DBFWriter();
                escribir.DataMemoLoc = ruta.Replace("dbf", "dbt");

                DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("Inrfc", DotNetDBF.NativeDbType.Char, 100);
                DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("Innom", DotNetDBF.NativeDbType.Char, 100);
                DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("Inpro", DotNetDBF.NativeDbType.Char, 100);
                DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("Innomina", DotNetDBF.NativeDbType.Char, 100);
                DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("Inimp", DotNetDBF.NativeDbType.Numeric,10,2);
                DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("Infolio", DotNetDBF.NativeDbType.Numeric,10,2);
                DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("Inpag", DotNetDBF.NativeDbType.Numeric,10,2);
                DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("Intpag", DotNetDBF.NativeDbType.Numeric,10,2);

                DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8 };
                escribir.Fields = campos;

                foreach (Dictionary<string, object> item in resultado)
                {
                    List<object> record = new List<object> {
                        item["rfc"],
                        item["nombre_em"],
                        item["proyecto"],
                        item["tipo_rel"],
                        item["imp_unit"],
                        item["folio"],
                        item["numdesc"],
                        item["totdesc"]
                    };
                    
                    escribir.AddRecord(record.ToArray());
                }

                escribir.Write(ops);
                escribir.Close();
                ops.Close();

                MessageBox.Show("Archivo .DBF generado exitosamente","Archivo generado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                MessageBox.Show(string.Format("El archivo generado ( {0}.DBF ) tiene {1} registros.",nombreDbf,resultado.Count), "Archivo generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

    }

}

