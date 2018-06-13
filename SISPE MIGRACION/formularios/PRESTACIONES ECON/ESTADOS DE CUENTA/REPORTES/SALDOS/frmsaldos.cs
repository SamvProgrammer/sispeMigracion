using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.SALDOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    public partial class frmsaldos : Form
    {
        public frmsaldos()
        {
            InitializeComponent();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void frmsaldos_Load(object sender, EventArgs e)
        {

        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string R2EDOCTA = string.Empty;
            string R2EC = string.Empty;
            string R3NCA = string.Empty;

            if (rdQuiro.Checked)
            {
                R2EDOCTA = "p_edocta";
                R2EC = "d_ecquir";
            }
            else
            {
                R2EDOCTA = "P_edocth";
                R2EC = "D_echipo";
            }

            if (rdNormal.Checked)
                R3NCA = " and ubic_pagare = '' ";
            else if (rdCobranzas.Checked)
                R3NCA = "  and ubic_pagare = 'C' ";
            else
                R3NCA = "  and ubic_pagare <> 'X' ";

            string fecha = string.Format("{0}/{1}/{2}", dFecha.Value.Day, dFecha.Value.Month, dFecha.Value.Year);
            MessageBox.Show("Se seleccionara ultima CTA. de cada folio al " + fecha, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string query = "select edo.folio,'' as fecha,'' as cta,'' as cta_descripcion,edo.secretaria,cuenta.cuenta as tmp,cuenta.descripcion as tmp2 from datos.p_edocta edo " +
                            " left join catalogos.cuenta cuenta " +
                            " on cuenta.proy = edo.secretaria " +
                            " order by folio ";

            this.Cursor = Cursors.WaitCursor;

            List<Dictionary<string, object>> r1 = globales.consulta(query);

            fecha = string.Format("{0}-{1}-{2}", dFecha.Value.Year, dFecha.Value.Month, dFecha.Value.Day);

            query = string.Format("select folio, max(f_descuento) as fecha, max(cta) as cta from datos.d_ecquir where f_descuento <= '{0}' group by folio order by folio asc ", fecha);
            List<Dictionary<string, object>> r2 = globales.consulta(query);

            query = "select folio,cta from datos.d_ecquir where f_descuento <= '{0}' order by folio asc, f_descuento desc";
            query = string.Format(query, fecha);

            List<Dictionary<string, object>> cta = globales.consulta(query);


            int contador = 0;
            foreach (Dictionary<string,object> item in r2) {
                if (contador == cta.Count) break;
                double folio = Convert.ToDouble(item["folio"]);
                double folio2 = Convert.ToDouble(cta[contador]["folio"]);

                if (folio != folio2) {
                    for (int x = contador; x < cta.Count; x++)
                    {
                        double folio3 = Convert.ToDouble(cta[x]["folio"]);
                        if (folio3 == folio)
                        {
                            folio2 = folio3;
                            break;
                        }
                        contador++;
                    }
                }


                if (folio == folio2)
                {
                    item["cta"] = cta[contador]["cta"];
                    contador++;
                }
                              
            }

            contador = 0;
            for (int x = 0; x < r2.Count; x++)
            {
                if (r1.Count == contador) break;
                double folio = Convert.ToDouble(r2[x]["folio"]);
                double folio2 = Convert.ToDouble(r1[contador]["folio"]);

                if (folio > folio2)
                {
                    for (int y = contador; y < r1.Count; y++)
                    {
                        double folioAux = Convert.ToDouble(r1[contador]["folio"]);
                        if (folioAux >= folio)
                        {
                            folio2 = Convert.ToDouble(r1[contador]["folio"]);
                            break;
                        }

                        contador++;
                    }
                }

                if (folio == folio2)
                {
                    r1[contador]["fecha"] = r2[x]["fecha"].ToString().Replace(" 12:00:00 a. m.", "");
                    r1[contador]["cta"] = r2[x]["cta"].ToString().Replace(" 12:00:00 a. m.", "");
                    contador++;
                }

            }

            List<Dictionary<string, object>> resultado = r1;

            query = "";

            MessageBox.Show("Se actualizara folios sin pagos ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            query = "select cuenta,descripcion from catalogos.cuenta";
            List<Dictionary<string, object>> cuentas = globales.consulta(query);

            foreach (Dictionary<string, object> item in resultado)
            {
                if (string.IsNullOrWhiteSpace(Convert.ToString(item["cta"])))
                {
                    item["cta"] = item["tmp"];
                    item["cta_descripcion"] = item["tmp2"];
                }
                else {

                    if (string.IsNullOrWhiteSpace(Convert.ToString(item["cta_descripcion"]))) {
                        string cta1 = Convert.ToString(item["cta"]);
                        foreach (Dictionary<string, object> item2 in cuentas)
                        {
                            string cta2 = Convert.ToString(item2["cuenta"]);
                            if (cta2 == cta1)
                            {
                                item["cta_descripcion"] = item2["descripcion"];
                                break;
                            }
                        }
                    }

                }
            }



            List<Dictionary<string, object>> crosover = resultado;


            query = "SELECT	folio,rfc,nombre_em,proyecto,	importe,	ubic_pagare,'' as numdesc,'' as totdesc,'' as pagado,'' as ultimop, f_primdesc,imp_unit,'' as saldo,'' as fecha,'' as cta,'' as cta_descripcion FROM	datos.{0} WHERE 	(f_emischeq <= '{1}' or f_emischeq is null) {2} order by folio asc";
            query = string.Format(query, R2EDOCTA, fecha, R3NCA);

            r1 = globales.consulta(query);

            query = "select folio, max(numdesc) as numdesc, max(totdesc) totdesc,sum(imp_unit) as pagado,max(f_descuento) as ultimop from datos.{0} where f_descuento <= '{1}' group by folio order by folio asc";
            query = string.Format(query, R2EC, fecha);

            r2 = globales.consulta(query);

            contador = 0;
            for (int x = 0; x < r2.Count; x++)
            {
                if (r1.Count == contador) break;
                double folio = Convert.ToDouble(r2[x]["folio"]);
                double folio2 = Convert.ToDouble(r1[contador]["folio"]);

                if (folio > folio2)
                {
                    for (int y = contador; y < r1.Count; y++)
                    {
                        double folioAux = Convert.ToDouble(r1[contador]["folio"]);
                        if (folioAux >= folio)
                        {
                            folio2 = Convert.ToDouble(r1[contador]["folio"]);
                            break;
                        }

                        contador++;
                    }
                }

                if (folio == folio2)
                {
                    r1[contador]["numdesc"] = r2[x]["numdesc"];
                    r1[contador]["totdesc"] = r2[x]["totdesc"];
                    r1[contador]["pagado"] = r2[x]["pagado"];
                    contador++;
                }
            }

            MessageBox.Show("Se seleccionara folios con saldo al " + fecha, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            resultado = r1;
            r1 = null;



            MessageBox.Show("Se seleccionara folios con saldo...", "Con saldo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            List<Dictionary<string, object>> tmp = new List<Dictionary<string, object>>();

            foreach (Dictionary<string, object> item in resultado)
            {
                string sImporte = (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"]))) ? "0" : Convert.ToString(item["importe"]);
                string sPagado = (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"]))) ? "0" : Convert.ToString(item["pagado"]);

                double dImporte = Math.Round(Convert.ToDouble(sImporte), 2);
                double dPagado = Math.Round(Convert.ToDouble(sPagado), 2);

                double folio = Convert.ToDouble(item["folio"]);
               
                if (dPagado >= dImporte)
                {
                    continue;
                }

                tmp.Add(item);
            }

            resultado = tmp;
            tmp = null;


            contador = 0;

            foreach (Dictionary<string, object> item in crosover) {
                if (contador == resultado.Count) break;

                double folio = Convert.ToDouble(item["folio"]);
                double folio2 = Convert.ToDouble(resultado[contador]["folio"]);
                if (folio == folio2) {
                    resultado[contador]["fecha"] = item["fecha"];
                    resultado[contador]["cta"] = item["cta"];
                    resultado[contador]["cta_descripcion"] = item["cta_descripcion"];

                    double importe = string.IsNullOrWhiteSpace(Convert.ToString(resultado[contador]["importe"])) ? 0 : (Convert.ToDouble(resultado[contador]["importe"]));
                    double pagado = string.IsNullOrWhiteSpace(Convert.ToString(resultado[contador]["pagado"])) ? 0 : (Convert.ToDouble(resultado[contador]["pagado"]));

                    double saldo = importe - pagado;

                    resultado[contador]["saldo"] = saldo;

                    contador++;
                }
            }
            this.Cursor = Cursors.Default;

            new listaReportes(resultado, rdQuiro.Checked, globales.formatoFecha(fecha,'/')).ShowDialog();

        } 
    }
}
