using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public abstract class Convidat : Persona
    {

        /****
         * Atributs
         */
        private Dictionary<string, int> simpaties = new Dictionary<string,int>();
        protected int plusSexe;

        public Convidat() :base()
        { 
        }

        /// <summary>
        /// Crea un convidat
        /// </summary>
        /// <param name="nom">Caràcter que l'identificarà</param>
        /// <param name="sex">Plus de simpatia sobre el sexe contrari</param>
        public Convidat(string nom, int sexe, int fil, int col)
            : base(nom, fil, col)
        {
            this.plusSexe = sexe;
        }

        /*******
         * Propietats
         */ 
        protected Dictionary<string, int> Simpaties
        {
            get { return simpaties; }
            set { simpaties = value; }
        }

        /// <summary>
        /// Afegeix un nivell de simpatia al diccionari de la persona segons el nom que li pasem
        /// </summary>
        /// <param name="nom">nom de la persona que afegim al diccionari</param>
        /// <param name="simpatia">nivell simpatia de -5 a 5</param>
        public void AfegirSimpatia(string nom, int simpatia)
        {
                if(!simpaties.Keys.Contains(nom))
                    simpaties.Add(nom, simpatia);
        }

        /// <summary>
        /// Retorna una llista de tuples clau valor per poder utiliutzr amb la graella.
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<string,int>> ExtreuSimpaties()
        {
            List<KeyValuePair<string,int>> llista = new List<KeyValuePair<string,int>>(); 
            foreach (KeyValuePair<string,int> pers in Simpaties)
            {
                llista.Add(pers);
            }
            return llista;
        }

        /// <summary>
        /// Retorna o estableix la simpaties envers a algú
        /// </summary>
        public int this[string nom]
        {
            get { return Simpaties[nom]; }
            set { Simpaties[nom] = value; }
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
