using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class Posicio
    {
        protected enum Direccio { Quiet, Amunt, Dreta, Avall, Esquerra}

       
        protected int columna;
        protected int fila;
        /// <summary> 
        /// Crea una nova posició 
        /// </summary>  
        /// <param name="fil">Fila de la Posició</param>
        /// <param name="col">Columna de la Posició</param> 
        public Posicio(int fil, int col)
        {
            columna = col;
            fila = fil;
            
        }

        /// <summary> 
        /// Crea una nova posició amb fila i columna igual a 0 
        /// </summary> 
        
        public Posicio()
        {

        }

       
        /// <summary> 
        /// Assigna o obté la columna de la posicio 
        /// </summary> 
        public int Columna
        {
            get { return columna; }

            set { columna = value; }
        }
        /// <summary>
        /// Assigna o obté la fila de la posicio
        /// </summary>
        public int Fila
        {
            get { return fila; }
            set { fila = value; }
        }
        /// <summary>
        /// Retorna si la posició està o no buida
        /// </summary>
        public virtual bool Buida
        {
            get { return true; }
        }

        /// <summary>
        /// Retorna la distància entre dues posicions
        /// </summary>
        /// <param name="pos1">Primera posició</</param>
        /// <param name="pos2">Segona posició</param>
        /// <returns>Distància entre les dues posicions</returns>
        public static double Distancia (Posicio pos1, Posicio pos2)
        {
            double distancia = 0;
            double distBase=0;
            double distAlt=0;
            if (!(pos1.fila==pos2.fila&&pos1.columna==pos2.columna))
            {
                distAlt=Math.Abs(pos2.fila - pos1.fila);
                distBase=Math.Abs(pos2.columna - pos1.columna);
                distancia = Pitagoras(distAlt, distBase);
            }

            return distancia;
        }

        private static double Pitagoras(double distancia1, double distancia2)
        {
            double pitagoras = 0;

            pitagoras = Math.Abs((Math.Pow(distancia1, 2)+ Math.Pow(distancia2, 2)));

            return pitagoras;
        }
    }
}
