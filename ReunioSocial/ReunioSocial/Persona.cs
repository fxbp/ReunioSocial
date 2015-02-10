using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public abstract class Persona : Posicio
    {

        protected string nom;
        


        /// <summary>
        /// Crea una persona
        /// </summary>
        /// <param name="nom">Strng que identifica la persona</param>
        /// <param name="fil">Fila on està localitzada</param>
        /// <param name="col">Columna on està localitzada</param>
        public Persona(string nom, int fil, int col): base(fil, col)
        {
            this.nom = nom;
        }


        /// <summary>
        /// Crea una persona
        /// </summary>
        /// <param name="nom">nom de la persona</param>
        public Persona(string nom)
        {
            this.nom = nom;
        }


        /// <summary>
        /// Crea una persona
        /// </summary>
        public Persona(): base()
        { }


        /// <summary>
        /// Obté el nom de la persona
        /// </summary>
        public string Nom
        {
            get { return nom; }
        }


        /// <summary>
        /// Retorna que la posició ocupada per aquesta persona no està buida
        /// </summary>
        public override bool Buida
        {
            get { return false; }
        }


        /// <summary>
        /// Atraccio de persona sobre una determinada posicio
        /// </summary>
        /// <param name="fil">Fila de la posició</param>
        /// <param name="col">Columan de la posició</param>
        /// <param name="esc">Escenari on estan situats</param>
        /// <returns>Atracció quantificada</returns>
        private double Atraccio(int fil, int col, Escenari esc)
        {
            double resultat = 0;
            double distancia = 0;
            Posicio referencia = new Posicio(fil, col);
            Posicio actual;
            int interes;
            for (int i = 0; i < esc.Files; i++)
            {
                for(int j=0;j<esc.Columnes;j++)
                {
                    actual = esc[i, j];
                    if (!actual.Buida)
                    {
                        distancia = Posicio.Distancia(referencia, actual);
                        interes = Interes(actual);
                        resultat += interes / distancia;
                    }
                }
            }

            return resultat;
        }


        /// <summary>
        /// Decideix quin serà el següent moviment que farà la persona
        /// </summary>
        /// <param name="esc">Escenari on esta situada la persona</param>
        /// <returns>Una de les 5 possibles direccions (Quiet, Amunt, Avall, Dreta, Esquerra</returns>
        public Direccio OnVaig(Escenari esc)
        {
            return Direccio.Amunt;
        }


        /// <summary>
        /// Interès de la persona sobre una determinada posició
        /// </summary>
        /// <param name="pos">Posició</param>
        /// <returns>Interès quantificat</returns>
        public abstract int Interes(Posicio pos);


        /// <summary>
        /// Determina si la persona es un convidat (home o dona) o un cambrer
        /// </summary>
        /// <returns>Retorna si és convidat</returns>
        public abstract bool EsConvidat();
    }
}
