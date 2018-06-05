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
        public frmSinPagos()
        {
            InitializeComponent();
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


            MessageBox.Show("Se va a obtener los saldos", "Saldos", MessageBoxButtons.OK, MessageBoxIcon.Information);



            string[] auxsplit = R3F1.Split('-');
            tiempo = new DateTime(Convert.ToInt32(auxsplit[0]), Convert.ToInt32(auxsplit[1]), 1);
            tiempo = tiempo.AddDays(-1);
            sFechaEmision = string.Format("{0}-{1}-{2}", tiempo.Year, tiempo.Month, tiempo.Day);

            string query = "SELECT c1.folio,c1.rfc,c1.nombre_em,c1.proyecto,c1.importe,c1.ubic_pagare,max(d.numdesc) as numdesc,max(d.totdesc) as totdesc,sum(d.imp_unit) as pagado, max(f_descuento) as ultimop  " +
                " FROM datos.{0} c1 LEFT JOIN datos.{1} d ON d.folio = c1.folio " +
                " WHERE	(	(c1.f_emischeq <= '{2}'OR c1.f_emischeq IS NULL)) " +
                " {3} " +
                " {4} " +
                " GROUP BY c1.folio,c1.importe,c1.ubic_pagare,c1.rfc,c1.nombre_em,c1.proyecto " +
                " ORDER BY	folio asc ";
            query = string.Format(query, R3EDOCTA, R3EC, sFechaEmision, R3NCA, R3JPT);


            this.Cursor = Cursors.WaitCursor;


            List<Dictionary<string, object>> resultado = globales.consulta(query);
            List<Dictionary<string, object>> tmp = new List<Dictionary<string, object>>();
            MessageBox.Show("Se seleccionara folios con saldo...", "Saldos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (Dictionary<string, object> item in resultado)
            {
                string sImporte = (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"]))) ? "0" : Convert.ToString(item["importe"]);
                string sPagado = (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"]))) ? "0" : Convert.ToString(item["pagado"]);
               
                double dImporte = Math.Round(Convert.ToDouble(sImporte));
                double dPagado = Math.Round(Convert.ToDouble(sPagado));






                if (dPagado >= dImporte - 0.0001)
                {
                    string sUltimop = (string.IsNullOrWhiteSpace(Convert.ToString(item["ultimop"])) ? "" : Convert.ToString(item["ultimop"]).Replace(" 12:00:00 a. m.", ""));

                    string[] auxArreglo = R3F2.Split('-');

                    bool valido = string.IsNullOrWhiteSpace(sUltimop);
                    bool va2 = false;
                    DateTime auxTiempo1 = new DateTime(Convert.ToInt32(auxArreglo[0]), Convert.ToInt32(auxArreglo[1]), Convert.ToInt32(auxArreglo[2]));
                    if (!valido)
                    {
                        string[] auxArreglo2 = sUltimop.Replace(" 12:00:00 a. m.", "").Split('/');
                        DateTime auxTiempo2 = new DateTime(Convert.ToInt32(auxArreglo2[2]), Convert.ToInt32(auxArreglo2[1]), Convert.ToInt32(auxArreglo2[0]));
                        va2 = auxTiempo2 > auxTiempo1;
                    }
                    if (Convert.ToDouble(item["folio"]) == 54573)
                    {
                        var xxx = 3;
                    }

                    bool nullImporte = string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])) && string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"]));

                    if (!valido && !va2 || nullImporte) {
                        continue;
                    }
                    
                    
                }
                tmp.Add(item);
            }

            resultado = tmp;
            tmp = null;

            MessageBox.Show("Se seleccionara folios no pagados en el período...", "Saldos", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            //Es necesario este proceso ya que igual se puede sacar con pura sententencia sql pero 
            //se tarda demasiado, ahora con una sentencia for se saca todos los movimiento de d_ecqdep y la cantidad que sale se 
            //agrega al resultado final :) .... by Santiago...

            query = "select folio,tipo_mov,f_descuento from datos.d_ecqdep order by folio asc";
            tmp = globales.consulta(query);
            List<Dictionary<string, object>> listaEcq_dep = new List<Dictionary<string, object>>();
            int contador = 0;
            int auxContador = 0;
            foreach (Dictionary<string, object> item in tmp)
            {

                double folio = Convert.ToDouble(item["folio"]);


                if (contador == resultado.Count) break;


                double folio2 = Convert.ToDouble(resultado[contador]["folio"]);
                if (folio != folio2)
                {
                    auxContador++;
                    continue;
                }

                bool bFechas = string.IsNullOrWhiteSpace(Convert.ToString(item["f_descuento"]));


                if (bFechas) goto continuar;

                if (string.IsNullOrWhiteSpace(Convert.ToString(resultado[contador]["ultimop"])))
                {
                    listaEcq_dep.Add(item);
                    goto continuar;
                }

                string[] saFecha1 = Convert.ToString(item["f_descuento"]).Replace(" 12:00:00 a. m.", "").Split('/');
                string[] saFecha2 = Convert.ToString(resultado[contador]["ultimop"]).Replace(" 12:00:00 a. m.", "").Split('/');

                DateTime fecha1 = new DateTime(Convert.ToInt32(saFecha1[2]), Convert.ToInt32(saFecha1[1]), Convert.ToInt32(saFecha1[0]));
                DateTime fecha2 = new DateTime(Convert.ToInt32(saFecha2[2]), Convert.ToInt32(saFecha2[1]), Convert.ToInt32(saFecha2[0]));

                if (fecha1 > fecha2)
                {
                    listaEcq_dep.Add(item);
                }

                continuar:

                double folioAux = Convert.ToInt32(tmp[auxContador + 1]["folio"]);

                if (folio != folioAux)
                {
                    contador++;
                }
                auxContador++;
            }
            contador = 0;
            List<Dictionary<string,object>> listaFinal = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> item in resultado) {
                double folio = Convert.ToDouble(item["folio"]);
                double folio2 = Convert.ToDouble(listaEcq_dep[contador]["folio"]);
                
                Dictionary<string, object> obj = new Dictionary<string, object>();
                if (folio == folio2)
                {
                    for(int y = contador; y < listaEcq_dep.Count; y++) {
                        double folioAux = Convert.ToDouble(listaEcq_dep[y]["folio"]);
                        if (folioAux != folio2) {
                            contador = y;
                            break;
                        }
                        obj = new Dictionary<string, object>();
                        foreach (string llave in item.Keys)
                            obj.Add(llave, item[llave]);

                        obj.Add("tipo_mov", listaEcq_dep[y]["tipo_mov"]);
                        obj.Add("f_descuento", listaEcq_dep[y]["f_descuento"]);

                        listaFinal.Add(obj);
                        
                    }
                    
                }
                else {
                    foreach (string llave in item.Keys)
                        obj.Add(llave, item[llave]);

                    obj.Add("tipo_mov", "");
                    obj.Add("f_descuento", "");

                    listaFinal.Add(obj);
                }
           
            }

            resultado = listaFinal;

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
                string importe = globales.checarDecimales(string.IsNullOrWhiteSpace(Convert.ToString(item["importe"]))?"0.00": Convert.ToString(item["importe"]));
                string pagado = string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"])) ? "0.00" : Convert.ToString(item["pagado"]);
                string saldo = globales.checarDecimales((Convert.ToDouble(importe) - Convert.ToDouble(pagado)));
                string ultimop = (fecha.Length == 3) ? string.Format("{0}/{1}/{2}", fecha[2], fecha[1], fecha[0]):"";
                string tipomov = Convert.ToString(item["tipo_mov"]);
                string f_descuento = (fecha2.Length == 3)? string.Format("{0}/{1}/{2}", fecha2[2], fecha2[1], fecha2[0]):"";
               
                

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
