using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas
{
    class herramientas
    {
        public static string[] meses = { "","Enero","Febrero","Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public static DateTime sacarFechaHabil(int dias, string fechaEspecifica = "")
        {
            DateTime tiempo = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fechaEspecifica))
            {
                string[] aux = fechaEspecifica.Split('/');
                tiempo = new DateTime(Convert.ToInt32(aux[2]), Convert.ToInt32(aux[1]), Convert.ToInt32(aux[0]));
            }
            int contador = dias;
            for (int x = 1; x <= contador; x++)
            {
                DayOfWeek nombreDia = tiempo.DayOfWeek;
                string nombre = nombreDia.ToString();
                if (nombre == "Saturday" || nombre == "Sunday")
                {
                    contador++;
                }
                tiempo = tiempo.AddDays(1);
            }


            return tiempo;
        }

        internal static void reportes(string nombreReporte, string tablaDataSet, object[] objeto,string mensaje, bool imprimir = false,object[] parametros=null)
        {
            frmReporte reporte = new frmReporte(nombreReporte,tablaDataSet);
            reporte.cargarDatos(tablaDataSet, objeto,mensaje, imprimir,parametros);
            reporte.ShowDialog();
        }

        

      

        public static dynamic SeleccionaTasaInteres()
        {
            object tasaDeInteres = null;
           
                frmTasaDeInteresescs tasa = new frmTasaDeInteresescs();
                tasa.ShowDialog();
                tasaDeInteres = tasa.resultado;
            
            return tasaDeInteres;
        }
       
        public static void imprimirDocumento(){
            

            }
        public static string numerosALetras(int number)
        {
            if (number == 0)
                return "Cero";

            if (number < 0)
                return "Menos " + numerosALetras(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += numerosALetras(number / 1000000) + " Millones ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += numerosALetras(number / 1000) + " Mil ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += numerosALetras(number / 100) + " Cien ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "Y ";

                var unitsMap = new[] { "cero", "Uno", "Dos", "Tres", "Cuatro", "Cinco", "Seis", "Siete", "Ocho", "Nueve", "Diez", "Once", "Doce", "Trece", "Catorce", "Quince", "Dieciséis", "diecisiete", "dieciocho", "diecinueve" };
                var tensMap = new[] { "Cero", "Diez", "Veinte", "Treinta", "Cuarenta", "Cincuenta", "Sesenta", "Setenta", "Ochenta", "Noventa" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static string checarDecimales(object cadena) {
            string aux = "";

            aux = Convert.ToDouble(cadena).ToString("#.##");


            if (aux.Contains("."))
            {
                string[] tmp = aux.Split('.');
                if (tmp[1].Length == 1)
                {
                    aux += "0";
                }
            }
            else {
                aux += ".00";
            }

            return aux;
        }

        public static string formatoFecha(string fecha,char formato)
        {
            string f1 = string.Empty;
            if (string.IsNullOrWhiteSpace(fecha)) return "";

            string[] arreglo;
            if ('-' == formato)
            {
                arreglo = fecha.Split('/');
                if (arreglo.Length != 3) return "";

                f1 = string.Format("{0}-{1}-{2}",arreglo[2],arreglo[1],arreglo[0]);
            }
            else {
                arreglo = fecha.Split('-');
                if (arreglo.Length != 3) return "";

                f1 = string.Format("{0}/{1}/{2}", arreglo[2], arreglo[1], arreglo[0]);
            }
            return f1;
        }

        public static List<Dictionary<string, object>> leerDbf(string ruta) {
            DbfDataReader.DbfTable tabla = new DbfDataReader.DbfTable(ruta);
            DbfDataReader.DbfRecord filas = new DbfDataReader.DbfRecord(tabla);
            List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();
            string[] tipos = new string[tabla.Columns.Count];
            string[] nombreColumnas = new string[tabla.Columns.Count];
            int contador = 0;
            foreach (var dbfColumn in tabla.Columns)
            {
                string tipo = Convert.ToString(dbfColumn.ColumnType);
                string nombreColumna = Convert.ToString(dbfColumn.Name).ToLower();
                tipos[contador] = tipo;
                nombreColumnas[contador] = nombreColumna;
                contador++;
            }
            while (tabla.Read(filas))
            {
                contador = 0;
                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                foreach (var dbfValue in filas.Values)
                {
                    string obj = Convert.ToString(dbfValue.GetValue()).Replace(" 12:00:00 a. m.", "");
                    obj = (string.IsNullOrWhiteSpace(Convert.ToString(obj)) && tipos[contador].Equals("Number") ? "0" : obj);
                    obj = obj + "|" + ((tipos[contador].Equals("Number")) ? "N" : "C");
                    diccionario.Add(nombreColumnas[contador], obj);
                    contador++;
                }
                lista.Add(diccionario);
            }
            tabla.Close();
            return lista;
        }

    }
}
