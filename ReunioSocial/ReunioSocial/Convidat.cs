﻿using System;
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
            
        }






        public Convidat();

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
            get { return 0; }
            set { }
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
