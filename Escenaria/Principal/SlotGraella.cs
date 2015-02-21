using ReunioSocial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principal
{
    class SlotGraella
    {


        /********************************
         * Atributs
         */
        private Convidat convidatFila;
        private Convidat convidatColumna;

        /********************************
         * Propietats
         */
        /// <summary>
        /// Propietat Cambrer fila
        /// </summary>
        public Convidat ConvidatFila
        {
            get { return convidatFila; }
            set { convidatFila = value; }
        }
        /// <summary>
        /// Propietat Cambrer columna
        /// </summary>
        public Convidat ConvidatColumna
        {
            get { return convidatColumna; }
            set { convidatColumna = value; }
        }


        /// <summary>
        /// Retorna true si hi ha un convidat fila i false si no.
        /// </summary>
        public bool teConvidatFila
        {
            get { return ConvidatFila != null; }
        }
        /// <summary>
        /// Retorna true si hi ha un convidat columna i false si no.
        /// </summary>
        public bool teConvidatColumna
        {
            get { return ConvidatColumna != null; }
        }
        /// <summary>
        /// Retorna true si els convidats estan a null.
        /// </summary>
        public bool esBuida
        {
            get { return !teConvidatColumna && !teConvidatFila; }
        }



        /********************************
         * Constructors
         */
        public SlotGraella(Convidat fila, Convidat columna)
        {
            this.ConvidatFila = fila;
            this.ConvidatColumna = columna;
            
        }

        public SlotGraella(Convidat conv, bool esFila)
        {

            if (esFila)
            {
                this.ConvidatFila = conv;
                this.ConvidatColumna = null;
            }
            else
            {
                this.ConvidatColumna = conv;
                this.ConvidatFila = null;
            }
        }



        /********************************
         * Mètodes
         */
        /// <summary>
        /// Retorna un valor resultant a sumar la simpatia de la persona amb el seu plus de sexe.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="agrada"></param>
        /// <returns></returns>
        private static int CalculaSimpatia(Convidat actual, Convidat agrada)
        {

            int total = actual[agrada.Nom];
            if ((actual.EsHome() && !agrada.EsHome()) || (!actual.EsHome() && agrada.EsHome()))
                total += actual.PlusSexe;
            return total;

        }

        /// <summary>
        /// Actualitza la simpatia envers el valor que s'ha actualitzat a la taula.
        /// </summary>
        /// <param name="nova"></param>
        public void ActualitzaSimpatia(int nova)
        {
            int valor = nova - convidatFila.PlusSexe;
            ConvidatFila[ConvidatColumna.Nom] = valor;

        }

        /// <summary>
        /// Retorna el plus de sexe d'un convidat en concret.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string ExtreuPlusSexe(Convidat c)
        {
            return c.PlusSexe.ToString();
        }
        


        /// <summary>
        /// Mètode que retorna els valors per cada slot.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if ((ConvidatColumna != null || ConvidatFila != null))
            {
                if (ConvidatFila != null && ConvidatColumna != null) 
                   return CalculaSimpatia(ConvidatFila, ConvidatColumna).ToString();
                else
                {
                    if (ConvidatFila == null)
                        return ConvidatColumna.ToString();
                    else if (ConvidatColumna == null)
                        return ConvidatFila.ToString();
                }
            }
            return "";
        }



    }
}
