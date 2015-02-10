using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class TaulaPersones
    {
<<<<<<< HEAD
        private Dictionary<string, Persona> gent;

        public Dictionary<string, Persona> Gent
        {
            get { return gent; }
            set { gent = value; }
        }
=======
        int numPersones;
>>>>>>> 634d037c00a2eaff2e283ba022cdab392d18bcd0



        /// <summary>
        /// Crea una taula de referències a persones
        /// </summary>
        public TaulaPersones()
<<<<<<< HEAD
        {
            gent = new Dictionary<string, Persona>();
        }

=======
        { }
>>>>>>> 634d037c00a2eaff2e283ba022cdab392d18bcd0
        /// <summary>
        /// Assigna o obté una persona de la taula
        /// </summary>
        public Persona this[string nom]
<<<<<<< HEAD
        { 
            get { return gent[nom]; } 
            set { gent[nom] = value;} 
        }

=======
        { get ;
            set; }
>>>>>>> 634d037c00a2eaff2e283ba022cdab392d18bcd0
        /// <summary>
        /// Obtè el número total de persones
        /// </summary>
        public int NumPersones
<<<<<<< HEAD
        {
            get { return gent.Count; }
        }

=======
        { get { return numPersones; } }
>>>>>>> 634d037c00a2eaff2e283ba022cdab392d18bcd0
        /// <summary>
        /// Afegeix una persona a la taula
        /// </summary>
        /// <param name="conv">Convidat a afegir</param>
        public void Afegir(Persona pers)
<<<<<<< HEAD
        {
            gent.Add(pers.Nom, pers);
        }
=======
        { }
>>>>>>> 634d037c00a2eaff2e283ba022cdab392d18bcd0
        /// <summary>
        /// Eliminar una persona de la taula
        /// </summary>
        /// <param name="conv">Convidat a eliminar</param>
        public void Eliminar(Persona pers)
<<<<<<< HEAD
        {
            Eliminar(pers.Nom);
        }
=======
        { }
>>>>>>> 634d037c00a2eaff2e283ba022cdab392d18bcd0
        /// <summary>
        /// Elimina la persona donat el seu nom
        /// </summary>
        /// <param name="posicio">Posició a eliminar</param>
        public void Eliminar(string nom)
<<<<<<< HEAD
        {
            gent.Remove(nom);
        }
=======
        { }
>>>>>>> 634d037c00a2eaff2e283ba022cdab392d18bcd0
    }
}
