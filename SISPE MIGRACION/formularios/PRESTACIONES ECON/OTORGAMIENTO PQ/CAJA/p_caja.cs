using SISPE_MIGRACION.codigo.baseDatos.repositorios;
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
    public partial class p_caja : Form
    {
        private bool esInsertar = false;
        private double numdesc = 0;
        private double descuentos = 0;
        private double delDescuento = 0;
        private double imp_unit_cap = 0;
        private double imp_unit = 0;
        private string imp_unit_capl = string.Empty;
        private string fum = string.Empty;
        private string hum = string.Empty;
        private string folioImprimir = string.Empty;
        private string fechaImprimir = string.Empty;
        private string imp_unit_intl = string.Empty;
        private double imp_unit_int = 0;

        private double total = 0;

        public p_caja()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void p_caja_Load(object sender, EventArgs e)
        {
            deshabilitar();
            txtTotal.Enabled = false;
            txtdescripcion.Enabled = false;
            txtNombre_em.Enabled = false;
            txtRfc.Enabled = false;

            txtF_descuento.Value = DateTime.Now;
            btnGuardar.Enabled = false;
        }

        private void deshabilitar(bool habilitar = false) {
            deshabilitarElemento(txtFolio, habilitar);
            deshabilitarElemento(txtF_descuento, habilitar);


            deshabilitarElemento(txtSecretaria, habilitar);

            deshabilitarElemento(txtDescuentos, habilitar);
            deshabilitarElemento(txtImp_unit, habilitar);
            deshabilitarElemento(txtDelDescuento, habilitar);
            deshabilitarElemento(txtPlazo, habilitar);
            deshabilitarElemento(txtImp_unitCap, habilitar);
            deshabilitarElemento(txtImp_unitIntereses, habilitar);
        }

        private void deshabilitarElemento(Control x, bool aux) {
            x.Enabled = aux;
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            try
            {
                llamarForma();
                this.ActiveControl = txtF_descuento;
                btnGuardar.Enabled = true;
                btnModifica.Enabled = false;
                btnNuevo.Enabled = false;
                this.esInsertar = true;
            }
            catch {

            }
        }

        public void rellenarcampos(Dictionary<string, object> datos) {
            limpiarCampos(true);

            string rfc = Convert.ToString(datos["rfc"]);
            string secretaria = Convert.ToString(datos["secretaria"]);
            string descripcion = Convert.ToString(datos["descripcion"]);
            string nombre = Convert.ToString(datos["nombre_em"]);
            string folio = Convert.ToString(datos["folio"]);
            string plazo = Convert.ToString(datos["plazo"]);
            string imp_unit = Convert.ToString(datos["imp_unit"]);

            txtFolio.Text = folio;
            txtRfc.Text = rfc;
            txtSecretaria.Text = secretaria;
            txtdescripcion.Text = descripcion;
            txtNombre_em.Text = nombre;
            txtPlazo.Text = plazo;
            txtImp_unit.Text = imp_unit;
        }

        public void rellenarcampos2(Dictionary<string, object> datos)
        {
            limpiarCampos(true);

            string rfc = Convert.ToString(datos["rfc"]);
            string secretaria = Convert.ToString(datos["secretaria"]);
            string descripcion = Convert.ToString(datos["descripcion"]);
            string nombre = Convert.ToString(datos["nombre_em"]);
            string folio = Convert.ToString(datos["folio"]);
            string plazo = Convert.ToString(datos["plazo"]);
            string imp_unit = Convert.ToString(datos["imp_unit"]);
            this.fum = Convert.ToString(datos["fum"]);
            this.hum = Convert.ToString(datos["hum"]);

            txtFolio.Text = folio;
            txtRfc.Text = rfc;
            txtSecretaria.Text = secretaria;
            txtdescripcion.Text = descripcion;
            txtNombre_em.Text = nombre;
            txtPlazo.Text = plazo;
            txtImp_unit.Text = globales.checarDecimales(imp_unit);
            txtTotal.Text = globales.checarDecimales(Convert.ToString(datos["total"]));
            txtLetra1.Text = Convert.ToString(datos["imp_unit_capl"]);
            txtLetra2.Text = Convert.ToString(datos["imp_unit_intl"]);
            txtDescuentos.Text = Convert.ToString(datos["descuentos"]);
            txtDelDescuento.Text = Convert.ToString(datos["deldesc"]);
            txtNumDesc.Text = Convert.ToString(datos["numdesc"]);
            txtImp_unitCap.Text = globales.checarDecimales(Convert.ToString(datos["imp_unit_cap"]));
            txtImp_unitIntereses.Text = globales.checarDecimales(Convert.ToString(datos["imp_unit_int"]));
        }

        private void limpiarCampos(bool limpiar)
        {
            deshabilitar(limpiar);
            txtFolio.Text = "";
            txtF_descuento.Text = "";
            txtTotal.Text = "";
            txtRfc.Text = "";
            txtNombre_em.Text = "";
            txtSecretaria.Text = "";
            txtdescripcion.Text = "";

            txtDescuentos.Text = "";
            txtImp_unit.Text = "";
            txtDelDescuento.Text = "";
            txtNumDesc.Text = "";
            txtPlazo.Text = "";

            txtImp_unitCap.Text = "";
            txtLetra1.Text = "";
            txtLetra2.Text = "";

            txtImp_unitIntereses.Text = "";
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar == 8) {
                limpiarCampos(false);
                txtFolio.Enabled = true;
                btnGuardar.Enabled = false;
                btnModifica.Enabled = true;
                this.ActiveControl = txtFolio;
                return;
            }
            llamarForma();
            btnGuardar.Enabled = true;
            btnModifica.Enabled = false;
            this.ActiveControl = txtF_descuento;
        }

        private void llamarForma()
        {
            frmCatalogoP_quirog cuenta = new frmCatalogoP_quirog();
            cuenta.enviar2 = rellenarcampos;
            cuenta.enviarBool = true;
            cuenta.tablaConsultar = "p_edocta";
            cuenta.ShowDialog();
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            try
            {
                btnGuardar.Enabled = true;
                this.esInsertar = false;

                this.ActiveControl = txtF_descuento;
                btnGuardar.Enabled = true;
                btnModifica.Enabled = false;
                btnNuevo.Enabled = false;

                frmCatalogoP_quirog cuenta = new frmCatalogoP_quirog();
                cuenta.enviar2 = rellenarcampos2;
                cuenta.enviarBool = true;
                cuenta.tablaConsultar = "p_cajaq";
                cuenta.ShowDialog();

            }
            catch {

            }
        }

        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaciones()) return;
            p_cajaQ p = obtenerObjeto();
            if (this.esInsertar) {
                guardar(p);
            }
            else{
                modificar(p);
            }
        }

        private p_cajaQ obtenerObjeto() {
            p_cajaQ obj = new p_cajaQ();
            DateTime fecha = txtF_descuento.Value;
            string auxFecha = string.Format("{0}-{1}-{2}",fecha.Year,fecha.Month,fecha.Day);

            obj.folio = txtFolio.Text;
            obj.f_descuento = auxFecha;
            obj.rfc = txtRfc.Text;
            obj.nombre_em = txtNombre_em.Text;
            obj.secretaria = txtSecretaria.Text;
            obj.descripcion = txtdescripcion.Text;
            obj.descuentos = Convert.ToInt32(txtDescuentos.Text);
            obj.total = txtTotal.Text;
            obj.deldescuentos = Convert.ToInt32(txtDelDescuento.Text);
            obj.numdesc = Convert.ToInt32(txtNumDesc.Text);
            obj.plazo = Convert.ToInt32(txtPlazo.Text);
            obj.imp_unit = txtImp_unit.Text;
            obj.imp_unit_cap = txtImp_unitCap.Text;
            obj.imp_unit_int = txtImp_unitIntereses.Text;
            obj.imp_unit_intl = txtLetra2.Text;
            obj.imp_unit_capl = txtLetra1.Text;
            obj.fum = this.fum;
            obj.hum = this.hum;
            return obj;
        }


        private void modificar(p_cajaQ obj) {
            //Proceso para guardar datos.....
            try { 
            DialogResult p = MessageBox.Show("¿Desea actualizar los cambios?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (p == DialogResult.No) return;


                string query = string.Format("update datos.p_cajaq set f_descuento = '{0}', rfc = '{1}',nombre_em = '{2}',secretaria = '{3}', descripcion = '{4}',descuentos = {5}, deldesc = {6},numdesc = {7}, plazo = {8}, imp_unit = {9}, imp_unit_cap = {10}, imp_unit_int = {11}, imp_unit_capl ='{12}', imp_unit_intl = '{13}', total = {14}, status = '{15}', fum = '{16}', hum = '{17}' where folio = {18}",
                        obj.f_descuento, obj.rfc, obj.nombre_em, obj.secretaria, obj.descripcion, obj.descuentos, obj.deldescuentos, obj.numdesc, obj.plazo, obj.imp_unit, obj.imp_unit_cap, obj.imp_unit_int, obj.imp_unit_capl, obj.imp_unit_intl, obj.total, "T", obj.fum, obj.hum,obj.folio);
                if (globales.consulta(query, true))
            {
                MessageBox.Show("Registro actualizado existosamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarCampos(false);
                    btnGuardar.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnModifica.Enabled = true;
                }

        }
            catch {

            }
        }

        //        ;*** PAGOS POR CAJA***
        //;PROCESO QUE PERMITE EN LA EDICION DE RECIBOS QUIROGRAFARIOS
        //;O HIPOTECARIOS CALCULAR EL TOTAL Y EL IMPORTE EN LETRAS
        //; ADEMAS DEL PAGO FINAL DEL RECIBO.
        private void letr_recib(string nombre) {
            switch (nombre) {
                case "txtPlazo":
                    //this.numdesc = Convert.ToDouble(txtNumDesc.Text);
                    this.delDescuento = Convert.ToDouble(txtDelDescuento.Text);
                    this.descuentos = Convert.ToDouble(txtDescuentos.Text);
                    this.imp_unit = Convert.ToDouble(txtImp_unit.Text);
                    if (this.numdesc != (this.delDescuento + (this.descuentos - 1))) {
                        this.numdesc = this.delDescuento + (this.descuentos - 1);
                        this.imp_unit_cap = this.imp_unit * this.descuentos;
                        this.imp_unit_capl = globales.convertirNumerosLetras(this.imp_unit_cap.ToString(), true);
                        this.fum = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        this.hum = string.Format("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);

                        txtImp_unitCap.Text = this.imp_unit_cap.ToString();
                        txtNumDesc.Text = this.numdesc.ToString();
                        txtLetra1.Text = this.imp_unit_capl;
                        txtImp_unitIntereses.Text = "0.00";
                    }
                    break;
                case "txtImp_unitIntereses":
                    this.imp_unit_int = (string.IsNullOrWhiteSpace(txtImp_unitCap.Text) ? 0 : Convert.ToDouble(txtImp_unitIntereses.Text));
                    //this.total = Convert.ToDouble(txtTotal.Text);
                    this.imp_unit_intl = globales.convertirNumerosLetras(this.imp_unit_int.ToString(), true);
                    this.imp_unit_capl = globales.convertirNumerosLetras(txtImp_unitCap.Text, true);
                    this.total = this.imp_unit_cap + this.imp_unit_int;


                    txtTotal.Text = total.ToString();
                    break;
            }
        }


        private void guardar(p_cajaQ obj) {
            try
            {
                 
                //Proceso para guardar datos.....
                DialogResult p = MessageBox.Show("¿Desea guardar cambios?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (p == DialogResult.No) return;

               

                string query = string.Format("select * from datos.p_cajaq where folio = {0}",txtFolio.Text);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count > 0) {
                    MessageBox.Show("El registro ya esta insertado, dar clic en \"ACTUALIZAR INFORMACIÓN\" si requiere hacer cambios","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }

                query = string.Format("insert into datos.p_cajaq values ({0},'{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},'{13}','{14}',{15},'{16}','{17}','{18}',null)",
                    obj.folio,obj.f_descuento,obj.rfc,obj.nombre_em,obj.secretaria,obj.descripcion,obj.descuentos,obj.deldescuentos,obj.numdesc,obj.plazo,obj.imp_unit,obj.imp_unit_cap,obj.imp_unit_int,obj.imp_unit_capl,obj.imp_unit_intl,obj.total,"T",obj.fum,obj.hum);
                if (globales.consulta(query, true)) {
                    MessageBox.Show("Registro insertado existosamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarCampos(false);
                    btnGuardar.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnModifica.Enabled = true;
                }
                
            }
            catch {

            }
        }

        private bool validaciones()
        {
            bool aux = false;
            if (string.IsNullOrWhiteSpace(txtDescuentos.Text))
            {
                MessageBox.Show("Se debe ingresar la cantidad de pagos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescuentos.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtImp_unit.Text))
            {
                MessageBox.Show("Se debe ingresar el importe unitario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtImp_unit.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtDelDescuento.Text))
            {
                MessageBox.Show("Se debe ingresar el primer pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDelDescuento.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtNumDesc.Text))
            {
                MessageBox.Show("Se debe ingresar el número de descuento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNumDesc.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtPlazo.Text))
            {
                MessageBox.Show("Se debe ingresar los plazos del prestamo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPlazo.Focus();
                return true;

            }

            if (string.IsNullOrWhiteSpace(txtImp_unitCap.Text))
            {
                MessageBox.Show("Se debe ingresar el pago capital", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtImp_unitCap.Focus();
                return true;
            }
            return aux;
        }

        private void eventoEntrar(object sender, EventArgs e)
        {
            string nombre = ((TextBox)sender).Name;
            letr_recib(nombre);
        }

        private void txtImp_unitCap_Leave(object sender, EventArgs e)
        {
            letr_recib("txtImp_unitIntereses");
        }
    }
}
