using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.Fondo_de_Pensiones;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.VALIDACIONES;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.CAJA;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.Edo_cuenta;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.PAGO_DE_MARCHA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//************************************************
//**       SECCIÓN DE CÓDIGO DONDÉ SE DECLARA   **
//**       LOS DELEGADOS...                     **
//************************************************
public delegate void setLista(List<Dictionary<string,object>> obj);
public delegate void setString(string cadena);
namespace SISPE_MIGRACION.formularios
{
    
    public partial class menuPrincipal : Form
    {
        private bool saliendo;

        public menuPrincipal()
        {
            InitializeComponent();
        }

        private void menuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saliendo) return;

            DialogResult dialogo = MessageBox.Show("¿Desea salir de la aplicación?", "Saliendo Sispe", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.Yes)
            {
                saliendo = true;
                Application.Exit();
            }
            e.Cancel = true;
        }

        private void altasDeSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void altasDeSolicitudesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new frmAltas().ShowDialog(); ;
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCatemplea().ShowDialog();
        }

        private void dependenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmdependencias().ShowDialog();
        }

        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmMovimientos().ShowDialog();
        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCategorias().ShowDialog();
        }

        private void proyectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmProyecto().ShowDialog();
        }

        private void firmasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmFirmas().ShowDialog();
        }

        private void prestacionesEconómicasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void actualizacionFechasDeChequesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmprogchq().ShowDialog();
        }

        private void verProgramaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmdiacheque().ShowDialog();
        }

        private void solicEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmSolicitudEntrega().ShowDialog();
        }

        private void menuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void alfabéticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmAlfabet().ShowDialog();
        }

        private void pagaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmPagares().ShowDialog();
        }

        private void generarPorFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmGenerarPorFecha().ShowDialog();
        }

        private void tasasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmTasas().ShowDialog();
        }

        private void montosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmMontos().ShowDialog();
        }

        private void quirografariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmconsulta().ShowDialog();
        }

        private void altasCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAltasCambios().ShowDialog();
        }

        private void otorgamientoPQToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void consultaPdevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmdevol().ShowDialog();
        }

        private void altasCambiosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmpagomarcha().ShowDialog();
        }

        private void actualizarRelLaboralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmActualizarRelLaboral().ShowDialog();
        }

        private void validarSituaciónLaboralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new validarSituacionLaboral().ShowDialog();
        }

        private void pagoPorCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmOpcionesPagoCaja().ShowDialog();
        }

        private void quirografarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmconsulta().ShowDialog();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void saldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmsaldos().ShowDialog();
        }

        private void estadoDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmedocuenta().ShowDialog();
        }

        private void hipotecarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new hipotecarios().ShowDialog();
        }

        private void reportesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmMenuOpciones().ShowDialog();
        }

        private void expedientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmexpediente().ShowDialog();
        }

        private void solicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmsolicitudes().ShowDialog();
        }

        private void expedientesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new frmexpediente().ShowDialog();
        }

        private void solicitudesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new frmsolicitudes().ShowDialog();
        }

        private void diskettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES.frmMenuOpciones(true).ShowDialog();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new frmreporlisaporta().ShowDialog();
        }

        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmDocumentos().ShowDialog();

        }

        private void sinPagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSinPagos().ShowDialog();
        }

        private void terminarDePagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSinPagos(true).ShowDialog();
        }
    }
}
