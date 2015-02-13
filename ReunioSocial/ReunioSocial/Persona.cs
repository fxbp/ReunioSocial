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
        // Aleatori que s'utilitza per la direcció.
        private static Random r=new Random();

    
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
     { 
     }
 

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
        /// [DEPRECATED]
        /// Atraccio de persona sobre una determinada posicio
        /// </summary>
        /// <param name="fil">Fila de la posició</param>
        /// <param name="col">Columan de la posició</param>
        /// <param name="esc">Escenari on estan situats</param>
        /// <returns>Atracció quantificada</returns>
        private double Atraccio(int fil, int col, Escenari esc)
        {
            /**************************************
             * DEPRECATED
             */
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
                    if (!actual.Buida&&actual!=this)
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
        /// Mètode atracció sobre taula de persones. Calcula l'atracció per cada persona decidint la que té més possibilitats d'acostar-se.
        /// </summary>
        /// <param name="fil"></param>
        /// <param name="col"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        private double Atracció(int fil, int col, TaulaPersones tp)
        {
            double resultat = 0;
            double distancia = 0;
            Posicio referencia = new Posicio(fil, col);
            int interes;
            foreach (Persona p in tp.Gent.Values)
            {
                if (p != this)
                {
                    distancia = Posicio.Distancia(referencia, p);
                    interes = Interes(p);
                    resultat += interes / distancia;
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
            //Genero un graudirecció nou que m'ajudarà a calcular i a fer el codi del on vaig més senzill 
            // Aquesta clase per defecte s'instancia amb els valors a 0.
            GrauDireccio gd = new GrauDireccio(r);
            // Assigno les posicions sempre que siguin assignables. 
            gd.Assigna(Direccio.Quiet,Atraccio(fila,columna,esc));
            if (esc.DestiValid(fila + 1, columna)) gd.Assigna(Direccio.Avall, Atraccio(fila + 1, columna, esc));
            if (esc.DestiValid(fila - 1, columna)) gd.Assigna(Direccio.Amunt, Atraccio(fila - 1, columna, esc));
            if (esc.DestiValid(fila, columna - 1)) gd.Assigna(Direccio.Esquerra, Atraccio(fila, columna - 1, esc));
            if (esc.DestiValid(fila, columna + 1)) gd.Assigna(Direccio.Dreta, Atraccio(fila, columna + 1, esc));
            // Retorno la que té preferència. En cas d'empat ja sap calcular-se el random ella sola gràcies a que li he passat anteriorment.
            return gd.DireccióResultat();
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
    /// <summary>
    /// Classe interna de cada persona la qual sab desar i Retornar les direccions prioritàries. 
    /// </summary>
    class GrauDireccio
    {
        Dictionary<string, double> Graus;
        Random r;

        public GrauDireccio(Random r)
        {
            this.r = r;
            Graus = new Dictionary<string, double>();
            Graus.Add("Amunt", 0);
            Graus.Add("Avall", 0);
            Graus.Add("Esquerre", 0);
            Graus.Add("Dreta", 0);
            Graus.Add("Quiet", 0);
        }

        /// <summary>
        /// Switch que ens permet definir la direcció.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="grau"></param>
        public void Assigna(Direccio d, double grau)
        {
            string dir = getDireccio(d);
            Graus[dir] = grau;
        }
        /// <summary>
        /// Retorna un nombre referent a la càrrega de la direcció.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public double Extreu(Direccio d)
        {
            string dir = getDireccio(d);
            return Graus[dir];
        }

        /// <summary>
        /// Evalua el valor més alt cap on ha d'anar la direcció. 
        /// en cas que els valors siguin 
        /// </summary>
        /// <returns></returns>
        public Direccio DireccióResultat()
        {

            List<KeyValuePair<string, double>> valors = new List<KeyValuePair<string, double>>();
            KeyValuePair<string,double>[] kvp = Graus.ToArray();
            KeyValuePair<string, double> temp = kvp[0];
            // Em quedo el valor més alt.
            for (int i = 1; i < kvp.Length; i++)
            {
                if (temp.Value < kvp[i].Value) temp = kvp[i];
            }
            // Recullo repeticions. 
            for (int i = 0; i < kvp.Length; i++)
            {
                if (temp.Value == kvp[i].Value) valors.Add(kvp[i]);
            }
            // Valorem si hi ha repetits i es llença un random en cas que n'hi hagi.
            if (valors.Count > 1)
            {
                KeyValuePair<string, double> res = valors[r.Next(0, valors.Count)];
                return getDireccioEnum(res.Key);
            }
            else return getDireccioEnum(temp.Key);
        }

        /// <summary>
        /// Retorna el valor de la clau del diccionari referent a la direcció que reb.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string getDireccio(Direccio d){
            string s = "";
            switch (d)
            {
                case Direccio.Amunt:
                    s = "Amunt";
                    break;
                case Direccio.Avall:
                    s = "Avall";
                    break;
                case Direccio.Dreta:
                    s = "Esquerra";
                    break;
                case Direccio.Esquerra:
                    s = "Dreta";
                    break;
                case Direccio.Quiet:
                    s = "Quiet";
                    break;
            }
            return s; 
        }
        /// <summary>
        /// Se li passa un string i retorna l'enumeració.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private Direccio getDireccioEnum(string dir)
        {
            Direccio res = Direccio.Amunt;
            switch (dir)
            {
                case "Amunt":
                    res = Direccio.Amunt;
                    break;
                case "Avall":
                    res = Direccio.Avall;
                    break;
                case "Esquerra":
                    res = Direccio.Esquerra;
                    break;
                case "Dreta":
                    res = Direccio.Dreta;
                    break;
                case "Quiet":
                    res = Direccio.Quiet;
                    break;
            }
            return res; 
        }
        
    }
   
}
