using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUP_PI_EF_F1.Negocio
{
    public class Competidor
    {
        private int numero;
        private string nombre;
        private int pais;
        private int escuderia;
        private DateTime fechaNacimiento;

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        public int Escuderia
        {
            get { return escuderia; }
            set { escuderia = value; }
        }

        public int Pais
        {
            get { return pais; }
            set { pais = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public override string ToString()
        {
            return numero + " - " + nombre;
        }
    }
}
