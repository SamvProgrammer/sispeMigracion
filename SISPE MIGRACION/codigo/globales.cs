﻿using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.herramientas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class globales
{
    public static bool alfaNumerico(char caracter)
    {
        return validaciones.alfaNumericos(caracter);
    }

    public static bool numerico(char caracter)
    {
        return validaciones.numerico(caracter);
    }

    public static DateTime sacarFechaHabil(int dias,string fechaEspecifica = "") {
        return herramientas.sacarFechaHabil(dias,fechaEspecifica);
    }

    public static object seleccionaTasaDeInteres() {
       return herramientas.SeleccionaTasaInteres();
    }

    public static dynamic consulta(string consulta,bool tipoSelect = false) {
        return baseDatos.consulta(consulta,tipoSelect);
    }
    public static string ShowDialog(string text,string caption) {
        return Prompt.ShowDialog(text,caption);
    }
    public static void imprimiendo() {
        herramientas.imprimirDocumento();
    }

    public static void reportes(string nombreReporte,string tablaSetNombre,object[] objeto,string mensaje="",bool imprimir = false,object[] parametros = null) {
       herramientas.reportes(nombreReporte,tablaSetNombre,objeto,mensaje,imprimir,parametros);
    }
    public static string numerosLetras(int numero) {
        return herramientas.numerosALetras(numero);
    }

    public static string convertirNumerosLetras(String numero, bool mayusculas) {
        return NumLetra.Convertir(numero, mayusculas);
    }

    public static string checarDecimales(object texto) {
        return herramientas.checarDecimales(texto);
    }
    public static string[] getMeses() {
        return herramientas.meses;
    }

    public static string formatoFecha(string fecha,char formato = '-') {
        return herramientas.formatoFecha(fecha,formato);
    }

    public static dynamic leerDbf(string ruta,bool verTipo = false) {
        return herramientas.leerDbf(ruta,verTipo);
    }
}

