using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TUP_PI_EF_F1.Datos
{
    public class AccesoDatos
    {
        private string cadenaConexion;
        private SqlConnection conexion;
        private SqlCommand comando;
        public AccesoDatos()
        {
            
            cadenaConexion = @"Data Source=DESKTOP-KMG9VHE\SQLEXPRESS;Initial Catalog=FORMULA_UNO_2023;Integrated Security=True";   
            conexion = new SqlConnection(cadenaConexion);
            comando = new SqlCommand();
            
            
            

        }
        private void Conectar()
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
        }

        private void Desconectar()
        {
            conexion.Close();
        }

        public DataTable ConsultarBD(string consultaSQL)
        {
            DataTable tabla = new DataTable();
            Conectar();
            comando.CommandText = consultaSQL;
            tabla.Load(comando.ExecuteReader());
            Desconectar();
            return tabla;
        }

        public int ActualizarBD(string consultaSQL, List<Parametro> lista)
        {
            int filasafectadas = 0;
            Conectar();
            comando.CommandText = consultaSQL;
            comando.Parameters.Clear();
            foreach (Parametro param in lista)
            {
                comando.Parameters.AddWithValue(param.Nombre, param.Valor);
            }
            filasafectadas = comando.ExecuteNonQuery();
            Desconectar();
            return filasafectadas;
        }

    }
}
