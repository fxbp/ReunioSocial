using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class TaulaPersones
    {
        private Dictionary<string, Persona> gent;

        public Dictionary<string, Persona> Gent
        {
            get { return gent; }
            set { gent = value; }
        }



        /// <summary>
        /// Crea una taula de referències a persones
        /// </summary>
        public TaulaPersones()
        {
            gent = new Dictionary<string, Persona>();
        }

        /// <summary>
        /// Assigna o obté una persona de la taula
        /// </summary>
        public Persona this[string nom]
        { 
            get { return gent[nom]; } 
            set { gent[nom] = value;} 
        }

        /// <summary>
        /// Obtè el número total de persones
        /// </summary>
        public int NumPersones
        {
            get { return gent.Count; }
        }

        /// <summary>
        /// Afegeix una persona a la taula
        /// </summary>
        /// <param name="conv">Convidat a afegir</param>
        public void Afegir(Persona pers)
        {
            gent.Add(pers.Nom, pers);
        }
        /// <summary>
        /// Eliminar una persona de la taula
        /// </summary>
        /// <param name="conv">Convidat a eliminar</param>
        public void Eliminar(Persona pers)
        {
            Eliminar(pers.Nom);
        }
        /// <summary>
        /// Elimina la persona donat el seu nom
        /// </summary>
        /// <param name="posicio">Posició a eliminar</param>
        public void Eliminar(string nom)
        {
            gent.Remove(nom);
        }
    }
}
