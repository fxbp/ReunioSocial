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
        /// </summary>
        /// <param name="nom">String que l'identificarà</param>
        /// <param name="simpa">Taula de simpaties</param>
        /// <param name="sexe">Plus de simpatia envers del sexe contrari</param>
        public Home(string nom, int[] simpa, int sexe)
        {
            this.nom = nom;
            //Hem de reinicialitzar el diccionari de persones.
            this.plusSexe = sexe;
        }


        /// <summary>
        /// Crea un Home
        /// </summary
        /// <param name="nom">String que l'identificarà</param>
        /// <param name="sexe">Plus de simpatia envers del sexe contrari</param>
        public Home(string nom, int sexe)
        {
            this.nom = nom;
            this.plusSexe = sexe;
        }


        /// <summary>
        /// Interès d'aquest home per una posició
        /// </summary>
        /// <param name="pos">Posició per la qual s'interessa</param>
        /// <returns>Interès quantificat</returns>
        public override int Interes(Posicio pos)
        {
            int resultat = 0;
            if (pos.Buida) resultat = 0;
            else
            {
                Persona p = (Persona)pos;
                if (p.EsConvidat())
                {
                    if (p is Dona)
                    {
                        resultat += ((Dona)p).PlusSexe;

                    }

                    if (Simpaties.Keys.Contains(p.Nom))
                        resultat += Simpaties[p.Nom];
                   
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
