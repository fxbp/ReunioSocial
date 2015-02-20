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

        private Convidat fila;
        private Convidat columna;
        

        public Convidat Fila
        {
            get { return fila; }
            set { fila = value; }
        }
        public Convidat Columna
        {
            get { return columna; }
            set { columna = value; }
        }

        public SlotGraella(Convidat fila, Convidat columna)
        {
            this.Fila = fila;
            this.Columna = columna;
        }


        public SlotGraella(Convidat fila)
        {

        }





    }
}
