using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class Home : Convidat
    {
       
        /// <summary>
        /// Crea un Home
        /// </summary
        /// <param name="nom">String que l'identificarà</param>
        /// <param name="sexe">Plus de simpatia envers del sexe contrari</param>
        public Home(string nom, int sexe, int fil, int col):base(nom,sexe,fil,col)
        {

        }

        public Home():base()
        {
        }

        /// <summary>
        /// Retornatrue si és un home.
        /// </summary>
        public override bool EsHome()
        {
            return true; 
        }


        /// <summary>
        /// Interès d'aquest home per una posició
        /// </summary>
        /// <param name="pos">Posició per la qual s'interessa</param>
        /// <returns>Interès quantificat</returns>
        public override int Interes(Posicio pos)
        {
            int resultat = 0;
            if (!pos.Buida)
            {
                if (((Persona)pos).EsConvidat())
                {
                    if (!((Convidat)pos).EsHome())
                    {
                        resultat += ((Dona)pos).PlusSexe;
                    }

                    if (Simpaties.Keys.Contains(((Persona)pos).Nom))
                        resultat += Simpaties[((Persona)pos).Nom];
                }
                else
                    resultat+= 1;
            }
            return resultat;
        }

        public override string ToString()
        {
            return this.Nom;
        }
    }
}
