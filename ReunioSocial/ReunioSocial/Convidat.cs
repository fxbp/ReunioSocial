using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public abstract class Convidat : Persona
    {
        protected Dictionary<string, int> simpaties;
        protected int plusSexe;
        /// <summary>
        /// Crea un convidat
        /// </summary>
        /// <param name="nom">string que l'identificarà</param>
        /// <param name="simp">Taula de simpaties</param>
        /// <param name="sex">Plus de simpatia sobre el sexe contrari</param>
        public Convidat(string nom, int[] simp, int sexe)
        {
            simpaties = new Dictionary<string, int>();
            plusSexe = sexe;
            
        }

        //Cada persona que es creii nova harua d'afegirse a la taula de persones i llavors un cop tinguem totes les persones crear un metoder afegirSimpaties i 
        //passar-li la taula persona i una llista d¡interesos. si no per a cada convidat que creem l'hi hauriem de afegir les noves simpaties per a cada convidat nou.

        //parlem si es canvia en comptes de llista
        public void AfegirSimpaties(List<KeyValuePair<string,int>> llistaSimpaties)
        {
            foreach (KeyValuePair<string,int> kvp in llistaSimpaties)
            {
                if(!simpaties.Keys.Contains(kvp.Key))//&&kvp.Key!=this.nom)
                    simpaties.Add(kvp.Key, kvp.Value);
            }
        }

        public Convidat(){}

        /// <summary>
        /// Crea un convidat
        /// </summary>
        /// <param name="nom">Caràcter que l'identificarà</param>
        /// <param name="sex">Plus de simpatia sobre el sexe contrari</param>
        public Convidat(string nom, int sexe)
        {
            this.nom = nom;
            this.plusSexe = sexe; 
        }


        public Dictionary<string,int> Simpaties
        {
            get { return simpaties; }
        }
        /// <summary>
        /// Retorna o estableix la simpaties envers a algú
        /// </summary>
        public int this[string nom]
        {
            get { return 0; }
            set { this.nom = nom; }
        }

        /// <summary>
        /// Retorna o estableix el plus de simpatia envers del sexe contrari
        /// </summary>
        public int PlusSexe
        {
            get { return plusSexe; }
            set { plusSexe = value; }
        }

        /// <summary>
        /// Retorna que si és un convidat
        /// </summary>
        /// <returns>Cert</returns>
        public override bool EsConvidat()
        {
            return true;
        }
    }
}
