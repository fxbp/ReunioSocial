using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class Cambrer : Persona
    {
        private static int nCambrer;

        /// <summary>
        /// Crea un cambrer (Persona de la que no importa el nom, i es dirà "Cambrer 1",
        /// "Cambrer 2", "Cambrer 3", "Cambrer 4" ... "CambrerN"/// </summary>
        public Cambrer(int fil, int col): base("Cambrer" + NCambrer,fil,col)
        {
            NCambrer++;
          
        }

        public Cambrer():base()
        {

        }

        public static int NCambrer
        {
            get { return Cambrer.nCambrer; }
            set { Cambrer.nCambrer = value; }
        }

        
        /// <summary>
        /// Interès del cambrer per una posició
        /// </summary>
        /// <param name="pos">posició per la que s'interessa</param>/// <returns>Retorna 0 si no hi ha ningú, 1 si hi ha un convidat i -1 si un cambrer</returns>
        public override int Interes(Posicio pos)
        {
            int resultat=0;
            Persona p;
            if(!pos.Buida)
            {
                if (pos is Persona)
                {
                    p = (Persona)pos;

                    if (p.EsConvidat())
                        resultat = 1;
                    else
                        resultat = -1;
                }
            }
            return resultat;
        }
        /// <summary>
        /// Retorna que el Cambrer no és un convidat
        /// </summary>
        /// <returns>false</returns>
        public override bool EsConvidat()
        {
            return false;
        }
    }
}
