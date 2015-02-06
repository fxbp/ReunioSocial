using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class Posicio
    {
        int columna;
        int fila;
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
            return 0;
        }
    }
}
