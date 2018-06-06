using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.VALIDACIONES
{
    public partial class frmSinPagos : Form
    {
        private bool terminanDePagar { get; set; }
        public frmSinPagos(bool terminanDePagar = false)
        {
            InitializeComponent();
            this.terminanDePagar = terminanDePagar;
        }

        private void rdHipotecario_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmSinPagos_Load(object sender, EventArgs e)
        {
            int minimo = 1997;
            int maximo = DateTime.Now.Year + 1;
            for (int x = maximo; x >= minimo; x--)
                cmbAño.Items.Add(x);

            string[] meses = globales.getMeses();
            for (int x = 1; x < meses.Length; x++)
                cmbMes.Items.Add(meses[x]);

            cmbAño.SelectedIndex = 1;
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;

            this.Text = (this.terminanDePagar) ? "Saldo pagado.." : "Sin saldo..";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string R3F1 = string.Empty;
            string R3F2 = string.Empty;
            string R3EDOCTA = string.Empty;
            string R3EC = string.Empty;
            string R3ECD = string.Empty;
            string R3NCA = string.Empty;
            string R3JPT = string.Empty;
            string sFechaEmision = string.Empty;
            DateTime tiempo;

            if (rdQuincena.Checked)
            {
                R3F1 = string.Format("{0}-{1}-01", cmbAño.Text, cmbMes.SelectedIndex + 1);
                R3F2 = string.Format("{0}-{1}-15", cmbAño.Text, cmbMes.SelectedIndex + 1);
                R3JPT = " AND c1.tipo_pago <> 'M' ";
            }
            else if (rdQuincena2.Checked)
            {
                R3F1 = string.Format("{0}-{1}-16", cmbAño.Text, cmbMes.SelectedIndex + 1);
                tiempo = new DateTime(Convert.ToInt32(cmbAño.Text), cmbMes.SelectedIndex + 1, 16);
                tiempo = tiempo.AddDays(30);
                tiempo = new DateTime(tiempo.Year, tiempo.Month, 01);
                tiempo = tiempo.AddDays(-1);
                R3F2 = string.Format("{0}-{1}-{2}", tiempo.Year, tiempo.Month, tiempo.Day);
            }
            else
            {
                R3F1 = string.Format("{0}-{1}-01", cmbAño.Text, cmbMes.SelectedIndex + 1);
                tiempo = new DateTime(Convert.ToInt32(cmbAño.Text), cmbMes.SelectedIndex + 1, 16);
                tiempo = tiempo.AddDays(30);
                tiempo = new DateTime(tiempo.Year, tiempo.Month, 01);
                tiempo = tiempo.AddDays(-1);
                R3F2 = string.Format("{0}-{1}-{2}", tiempo.Year, tiempo.Month, tiempo.Day);
            }

            if (rdQuiro.Checked)
            {
                R3EDOCTA = "p_edocta";
                R3EC = "d_ecquir";
                R3ECD = "d_ecqdep";
            }
            else
            {
                R3EDOCTA = "p_edocth";
                R3EC = "descuentos";
                R3ECD = "d_echdep";
            }

            if (rdNormal.Checked)
                R3NCA = " and c1.ubic_pagare = '' ";
            else if (rdCobranzas.Checked)
                R3NCA = "  and c1.ubic_pagare = 'C' ";
            else
                R3NCA = "  and c1.ubic_pagare <> 'X' ";

            this.Cursor = Cursors.WaitCursor;

            List<Dictionary<string, object>> resultado;

            #region obteniendo saldos

            MessageBox.Show("Se va a obtener los saldos...", "Saldos",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
            string query = "SELECT	folio,rfc,nombre_em,proyecto,	importe,	ubic_pagare,'' as numdesc,'' as totdesc,'' as pagado,'' as ultimop,'' as tipo_mov,'' as f_descuento FROM	datos.p_edocta WHERE 	(f_emischeq <= '2018-04-30' or f_emischeq is null) AND ubic_pagare = '' AND tipo_pago <> 'M' order by folio asc";
            List<Dictionary<string, object>> r1 = globales.consulta(query);

            query = "select folio, max(numdesc) as numdesc, max(totdesc) totdesc,sum(imp_unit) as pagado,max(f_descuento) as ultimop from datos.d_ecquir where f_descuento <= '2018-05-15' group by folio order by folio asc";

            List<Dictionary<string, object>> r2 = globales.consulta(query);

            int contador = 0;
            for (int x = 0; x < r2.Count; x++) {
                if (r1.Count == contador) break;
                double folio = Convert.ToDouble(r2[x]["folio"]);
                double folio2 = Convert.ToDouble(r1[contador]["folio"]);

                if (folio > folio2) {
                    for (int y = contador; y < r1.Count; y++ ) {
                        double folioAux = Convert.ToDouble(r1[contador]["folio"]);
                        if (folioAux >= folio)
                        {
                            folio2 = Convert.ToDouble(r1[contador]["folio"]);
                            break;
                        }
                            
                        contador++;
                    }
                }

                if (folio == folio2) {
                    r1[contador]["numdesc"] = r2[x]["numdesc"];
                    r1[contador]["totdesc"] = r2[x]["totdesc"];
                    r1[contador]["pagado"] = r2[x]["pagado"];
                    r1[contador]["ultimop"] = r2[x]["ultimop"].ToString().Replace(" 12:00:00 a. m.","");
                    contador++;
                }

               
            }

            query = "select folio,max(f_descuento) as ultimop from datos.d_ecquir  group by folio order by folio asc";
            contador = 0;
            r2 = globales.consulta(query);
            for (int x = 0; x < r2.Count; x++) {
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
                    r1[contador]["ultimop"] = r2[x]["ultimop"].ToString().Replace(" 12:00:00 a. m.", "");
                    contador++;
                }

                var xss = r2[x]["ultimop"];

            }


            #endregion

            resultado = r1;
            r1 = null;

            #region seleccionando folios con saldo

            MessageBox.Show("Se seleccionara folios con saldo...", "Con saldo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            List<Dictionary<string, object>> tmp = new List<Dictionary<string, object>>();

            foreach (Dictionary<string, object> item in resultado)
            {
                string sImporte = (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"]))) ? "0" : Convert.ToString(item["importe"]);
                string sPagado = (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"]))) ? "0" : Convert.ToString(item["pagado"]);

                double dImporte = Math.Round(Convert.ToDouble(sImporte), 2);
                double dPagado = Math.Round(Convert.ToDouble(sPagado), 2);
            
                if (dPagado >= dImporte || dImporte == 0) {
                    continue;
                }

                tmp.Add(item);
            }

            resultado = tmp;
            tmp = null;

            #endregion

            MessageBox.Show("Se seleccionara folios no pagados en el período...", "Saldos en el periodo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            query = string.Format("select folio from datos.d_ecquir where f_descuento >= '{0}' and f_descuento <= '{1}'", R3F1, R3F2);
            List<Dictionary<string, object>> folios = globales.consulta(query);
            foreach (Dictionary<string, object> item in folios)
            {
                double folio1 = Convert.ToDouble(item["folio"]);
                foreach (Dictionary<string, object> item2 in resultado)
                {
                    double folio2 = Convert.ToDouble(item2["folio"]);
                    if (folio1 == folio2)
                    {
                        resultado.Remove(item2);
                        break;
                    }
                }
            }

            MessageBox.Show("Se buscara altas a las dependencias...", "Altas dependencias", MessageBoxButtons.OK, MessageBoxIcon.Information);

            query = "select folio,tipo_mov,f_descuento from datos.d_ecqdep  order by folio asc,f_descuento desc";

            tmp = globales.consulta(query);

            contador = 0;

            List<Dictionary<string, object>> lista_ecqdep = new List<Dictionary<string, object>>();

            int x1 = 0;
            foreach (Dictionary<string,object> item in tmp) {
                if (contador == resultado.Count) break;
                double folio1 = Convert.ToDouble(item["folio"]);
                double folio2 = Convert.ToDouble(resultado[contador]["folio"]);
               
                if (folio1 > folio2)
                {
                    for (int y = contador; y < resultado.Count; y++)
                    {
                        double folioAux = Convert.ToDouble(resultado[contador]["folio"]);
                        if (folioAux >= folio1)
                        {
                            folio2 = Convert.ToDouble(resultado[contador]["folio"]);
                            break;
                        }

                        contador++;
                    }
                }

                if (folio1 == folio2) {
                    string fecha1 = Convert.ToString(item["f_descuento"]).Replace(" 12:00:00 a. m.", "");
                    if (!string.IsNullOrWhiteSpace(fecha1)) {
                        string[] tmpArreglo = fecha1.Split('/');
                        DateTime tiempo1 = new DateTime(Convert.ToInt32(tmpArreglo[2]), Convert.ToInt32(tmpArreglo[1]), Convert.ToInt32(tmpArreglo[0]));
                        string fecha2 = Convert.ToString(resultado[contador]["ultimop"]);
                        if (!string.IsNullOrWhiteSpace(fecha2))
                        {
                            string[] tmpArreglo2 = fecha2.Split('/');
                            DateTime tiempo2 = new DateTime(Convert.ToInt32(tmpArreglo2[2]), Convert.ToInt32(tmpArreglo2[1]), Convert.ToInt32(tmpArreglo2[0]));
                            if (tiempo1 > tiempo2)
                            {
                                lista_ecqdep.Add(item);
                            }
                        }
                        else {
                            lista_ecqdep.Add(item);
                        }
                    }
                    if (folio2 !=  Convert.ToDouble(tmp[x1+1]["folio"]))
                        contador++;
                }
                x1++;
            }


            contador = 0;

            List<Dictionary<string, object>> listaFinal = new List<Dictionary<string, object>>();
            for(int x = 0; x < resultado.Count; x++) {
                double folio = Convert.ToDouble(resultado[x]["folio"]);
                double folio2 = Convert.ToDouble(lista_ecqdep[contador]["folio"]);
                if (folio == folio2)
                {
                    

                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    foreach (string llave in resultado[x].Keys)
                        obj.Add(llave, resultado[x][llave]);

                    obj["tipo_mov"] = lista_ecqdep[contador]["tipo_mov"];
                    obj["f_descuento"] = lista_ecqdep[contador]["f_descuento"];

                    if (!(contador + 1 == lista_ecqdep.Count))
                    {
                        if (Convert.ToDouble(lista_ecqdep[contador + 1]["folio"]) != folio)
                            contador++;
                        else
                        {
                            x--;
                            contador++;

                        }
                    }
                    listaFinal.Add(obj);
                }
                else
                {
                    listaFinal.Add(resultado[x]);
                }
            }

            resultado = listaFinal;
            listaFinal = null;

            this.Cursor = Cursors.Default;
            object[] objetos = new object[resultado.Count];
            contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                string[] fecha = Convert.ToString(item["ultimop"]).Replace(" 12:00:00 a. m.", "").Split('/');
                string[] fecha2 = Convert.ToString(item["f_descuento"]).Replace(" 12:00:00 a. m.", "").Split('/');
                string folio = Convert.ToString(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string ubicpagare = Convert.ToString(item["ubic_pagare"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string numdesc = Convert.ToString(item["numdesc"]) + "/" + Convert.ToString(item["totdesc"]);
                string importe = globales.checarDecimales(string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])) ? "0.00" : Convert.ToString(item["importe"]));
                string pagado = string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"])) ? "0.00" : Convert.ToString(item["pagado"]);
                string saldo = globales.checarDecimales((Convert.ToDouble(importe) - Convert.ToDouble(pagado)));
                string ultimop = (fecha.Length == 3) ? string.Format("{0}/{1}/{2}", fecha[2], fecha[1], fecha[0]) : "";
                string tipomov = Convert.ToString(item["tipo_mov"]);
                string f_descuento = (fecha2.Length == 3) ? string.Format("{0}/{1}/{2}", fecha2[2], fecha2[1], fecha2[0]) : "";

                

                objetos[contador] = new object[] {
                    folio,rfc,nombre,ubicpagare,proyecto,numdesc,importe,saldo,
                    ultimop,tipomov,f_descuento
                };

                contador++;
            }

            this.Cursor = Cursors.Default;

            new frmSalida(rdQuiro.Checked, R3F1, R3F2, objetos).ShowDialog();

        }


    }
}
