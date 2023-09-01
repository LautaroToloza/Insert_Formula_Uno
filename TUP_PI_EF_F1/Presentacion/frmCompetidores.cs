using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TUP_PI_EF_F1.Datos;
using TUP_PI_EF_F1.Negocio;


namespace TUP_PI_EF_F1
{
    public partial class frmCompetidores : Form
    {
        AccesoDatos oBD;
        List<Competidor> listaCompetidores;
        public frmCompetidores()
        {
            InitializeComponent();
            oBD = new AccesoDatos();
            listaCompetidores = new List<Competidor>();
        }

        private void frmCompetidores_Load(object sender, EventArgs e)
        {
            Habilitar(false);
            CargarCombo(cboEscuderia, "Escuderias", "idEscuderia", "nombreEscuderia");
            CargarCombo(cboPais, "Paises", "idPais", "nombrePais");
            CargarLista();
            LimpiarCombo();
        }

        private void CargarLista()
        {
            listaCompetidores.Clear();
            lstCompetidores.Items.Clear();
            

            DataTable tabla = oBD.ConsultarBD("SELECT * FROM Competidores");
            foreach (DataRow fila in tabla.Rows)
            {
                Competidor c= new Competidor();
                c.Numero = Convert.ToInt32(fila[0]);
                c.Nombre = Convert.ToString(fila[1]);
                c.Pais = Convert.ToInt32(fila[2]);
                c.Escuderia = Convert.ToInt32(fila[3]);
                c.FechaNacimiento = Convert.ToDateTime(fila[4]);
                listaCompetidores.Add(c);
                lstCompetidores.Items.Add(c);
            }
        }
        private void CargarCombo(ComboBox cbo,string nombreTabla,string idTabla,string nombreCampo)
        {

            DataTable tabla = oBD.ConsultarBD($"SELECT * FROM {nombreTabla} ORDER BY {nombreCampo}");
            cbo.DataSource = tabla;
            cbo.ValueMember = idTabla;
            cbo.DisplayMember = nombreCampo;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea salir?", "SALIR",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Valido())
            {
                Competidor c = new Competidor();
                
                c.Numero = Convert.ToInt32(txtNumero.Text);
                c.Nombre = txtNombre.Text;
                c.Pais = Convert.ToInt32(cboPais.SelectedValue);
                c.Escuderia = Convert.ToInt32(cboEscuderia.SelectedValue);
                c.FechaNacimiento = dtpFecNac.Value;
                string query = "INSERT INTO Competidores VALUES(@Numero, @Nombre, @Pais, @Escuderia, @Fecha)";
                List<Parametro> listaParametro = new List<Parametro>();
                listaParametro.Add(new Parametro("@Numero", c.Numero));
                listaParametro.Add(new Parametro("@Nombre", c.Nombre));
                listaParametro.Add(new Parametro("@Pais", c.Pais));
                listaParametro.Add(new Parametro("@Escuderia", c.Escuderia));
                listaParametro.Add(new Parametro("@Fecha", c.FechaNacimiento));
                int filasAfectadas = oBD.ActualizarBD(query, listaParametro);
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Se inserto el competidor con éxito!!");
                    CargarLista();
                    Limpiar();
                    Habilitar(false);
                }
            }
        }

        private void LimpiarCombo()
        {
            cboPais.SelectedIndex = -1;
            cboEscuderia.SelectedIndex = -1;
        }
        private void Limpiar()
        {
            txtNumero.Text = "";
            txtNombre.Text = "";
            dtpFecNac.Value = DateTime.Today;
            LimpiarCombo();
        }

        private void Habilitar(bool x)
        {
            
            btnGrabar.Enabled = x;
            btnNuevo.Enabled = !x;
            txtNombre.Enabled = x;
            txtNumero.Enabled = x;
            cboEscuderia.Enabled = x;
            cboPais.Enabled = x;
            dtpFecNac.Enabled = x;
            lstCompetidores.Enabled = x;
            
        }

        private bool Valido()
        {
            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show("Debe ingresar un Numero!!");
                txtNumero.Focus();
                return false;
            }
            
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre!!");
                txtNombre.Focus();
                return false;
            }
            if (Convert.ToInt32(txtNombre.Text.Length) > 50)
            {
                MessageBox.Show("El nombre ingresado tiene que tener menor de 50 caracteres!!");
                txtNombre.Focus();
                return false;
            }
            if (cboPais.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un país!!");
                cboPais.Focus();
                return false;
            }
            if (cboEscuderia.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una escuderia!!");
                cboEscuderia.Focus();
                return false;
            }
            if (dtpFecNac.Value > DateTime.Today)
            {
                MessageBox.Show("La fecha no puede ser mayor a la actual!!");
                dtpFecNac.Focus();
                return false;
            }
            if ( (DateTime.Today.Year - dtpFecNac.Value.Year ) < 18 )
            {
                MessageBox.Show("El competidor tiene que ser mayor de 18 años!!");
                dtpFecNac.Focus();
                return false;
            }
            if(listaCompetidores.Count > 0)
            {
                foreach (Competidor lc in listaCompetidores)
                {
                    if (lc.Numero == Convert.ToInt32(txtNumero.Text))
                    {
                        MessageBox.Show($"Ya existe un competidor con el número '{txtNumero.Text}', cambie de número!!");
                        txtNumero.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar(true);
        }
    }
}
