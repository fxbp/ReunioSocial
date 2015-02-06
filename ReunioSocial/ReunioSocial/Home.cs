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
        { }


        /// <summary>
        /// Crea un Home
        /// </summary
        /// <param name="nom">String que l'identificarà</param>
        /// <param name="sexe">Plus de simpatia envers del sexe contrari</param>
        public Home(string nom, int sexe)
        { }


        /// <summary>
        /// Interès d'aquest home per una posició
        /// </summary>
        /// <param name="pos">Posició per la qual s'interessa</param>
        /// <returns>Interès quantificat</returns>
        public override int Interes(Posicio pos)
        {
            return null;S        
        }
    }
}
