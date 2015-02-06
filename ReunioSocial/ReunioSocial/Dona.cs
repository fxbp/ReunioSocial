using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class Dona : Convidat
    {


        /// <summary>
        /// Crea una Dona
        /// </summary>
        /// <param name="nom">String que identifica aquesta Dona</param>
        /// <param name="simpa">Taula de simpaties envers els altres convidats</param>
        /// <param name="sexe">Plus de simpatia envers convidats del sexe contrari</param>
        public Dona(string nom, int[] simpa, int sexe)
            : base(nom, simpa, sexe)
        {
            this.nom = nom;
            this.simpaties = simpa;
            this.plusSexe = sexe;
        }


        /// <summary>
        /// Crea una Dona
        /// </summary>
        /// <param name="nom"> String que identifica aquesta Dona</param>
        /// <param name="sexe">Plus de simpatia envers convidats del sexe contrari</param>
        public Dona(string nom, int sexe)
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
            return 0;
        }
    }
}
