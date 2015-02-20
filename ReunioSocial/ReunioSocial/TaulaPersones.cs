using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class TaulaPersones:IEnumerable<Persona>
    {


        private Dictionary<string, Persona> gent;
        private Escenari escenari;

        public Dictionary<string, Persona> Gent
        {
            get { return gent; }
            set { gent = value; }
        }
        public TaulaPersones(Escenari e)
        {
            gent = new Dictionary<string, Persona>();
            escenari = e;
        }


        /// <summary>
        /// Assigna o obté una persona de la taula
        /// </summary>
        public Persona this[string nom]
        { 
            get { return gent[nom]; } 
            set { gent[nom] = value;} 
        }

        public bool Conte(string nom)
        {
            return gent.Keys.Contains(nom);
        }

        /// <summary>
        /// Retorna la matriu de posicions de en forma de matriu de persones, totes les posicions de la taula que estan buides estan inicialitzades a null.
        /// </summary>
        public Persona[,] ToMatriu
        {
            get
            {
                Persona[,] matriu = new Persona[escenari.Files,escenari.Columnes];
                foreach (Persona p in Gent.Values)
                {
                    matriu[p.Fila,p.Columna] = p;
                }
                return matriu;
            }
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




        public IEnumerator<Persona> GetEnumerator()
        {
            return gent.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
