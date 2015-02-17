using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class Escenari
    {
        Posicio[,] escenari;
        TaulaPersones persones;
        int files;
        int columnes;
        
        int nDones;
        int nHomes;
        int nCambrers;
        
        /// <summary>
        /// Crea un escenari donades unes mides
        /// </summary>
        /// <param name="files">Número de files de l'escenari</param>
        /// <param name="columnes">Número de columnes de l'escenari</param>
        public Escenari(int f, int c)
        {
            files = f;
            columnes = c;
            escenari=new Posicio[files,columnes];
            EmplenarEscenari();
            persones = new TaulaPersones(this);
            nDones = 0;
            nHomes = 0;
            nCambrers = 0;
             

        }


        /// <summary>
        /// Retorna una matriu de posicions.
        /// </summary>
        public Posicio[,] EscenariToMatriusPosicions
        {
            get { return escenari; }
        }
        

        /// <summary>
        /// Retorna el número de files de l'escenari
        /// </summary>
        public int Files
        { 
            get { return files; } 
        }


        /// <summary>
        /// Retorna el número de columnes de l'escenari
        /// </summary>
        public int Columnes
        {
            get { return columnes; }
        }

        /// <summary>
        /// Retorna la taula persones
        /// </summary>
        public TaulaPersones TaulaPersones
        {
            get { return persones; }
        }


        /// <summary>
        /// Retorna el número de homes que hi ha dins de l'escenari
        /// </summary>
        public int Homes
        {
            get { return nHomes; }
        }


        /// <summary>
        /// Retorna el número de dones que hi ha dins de l'escenari
        /// </summary>
        public int Dones
        {
            // Falta implementar
            get { return nDones; } 
        }


        /// <summary>
        /// Retorna el número de Cambrers que hi ha dins de l'escenari
        /// </summary>
        public int Cambrers
        {
            get { return nCambrers; }
        }


        /// <summary>
        /// Mou una persona de (filOrig, colOrig) fins a la posicio adjacent (filDesti,colDesti)
        /// Es suposa que les coordenades són vàlides, que hi ha una persona a l'origen i que
        /// el destí està buit.
        /// </summary>
        /// <param name="filOrig">Fila de la coordenada d'origen</param>
        /// <param name="colOrig">Columna de la coordenada d'origen</param>
        /// <param name="filDesti">Fila de la coordenada de destí</param>
        /// <param name="colDesti">Columna de la coordenada de destí</param>
        private void Moure(int filOrig, int colOrig, int filDesti, int colDesti)
        {
            
            Posicio aux = this[filDesti, colDesti];
            if (DestiValid(filDesti, colDesti))
            {
                escenari[filDesti, colDesti] = escenari[filOrig, colOrig];
                escenari[filOrig, colOrig] = aux;

                escenari[filDesti, colDesti].Fila = filOrig;
                escenari[filDesti, colDesti].Columna = colOrig;

                escenari[filOrig, colOrig].Fila = filDesti;
                escenari[filOrig, colOrig].Columna = colDesti;
            }

        }

        /// <summary>
        /// Retorna la Posició que hi ha en una coordenada donada
        /// </summary>
        public Posicio this[int fil, int col]
        {
            get { return escenari[fil, col]; }
        }


        /// <summary>
        /// Mira si una coordenada es correcte per ser destí d'una persona
        /// </summary>
        /// <param name="fil">fila de la coordenada</param>
        /// <param name="col">columna de la coordenada</param>
        /// <returns>retorna si la coordenada és vàlida i està buida</returns>
        public bool DestiValid(int fil, int col)
        {
            return (fil >= 0 && fil < Files && col >= 0 && col < Columnes) && escenari[fil, col].Buida;       
        }


        /// <summary>
        /// Retorna el contingut del escenari en forma de matriu d'strings,
        /// representant cada persona amb el seu nom
        /// </summary>
        /// <returns>Matriu de caràcters</returns>
        public String[,] ContingutNoms()
        {
            string[,] contingut = new string[files, columnes];
            for (int i = 0; i < files; i++)
            {
                for (int j = 0; j < columnes; j++)
                {
                    if (escenari[i, j] is Persona)
                    {
                        Persona p = (Persona)(escenari[i, j]);
                        contingut[i, j] = p.Nom;
                    }
                    else
                    {
                        contingut[i, j] = "0";
                    }
                }
            }
            return contingut;
        }


        /// <summary>
        /// Elimina una persona de l'escenari i de la taula de persones
        /// </summary>
        /// <param name="fil">Fila on està la persona</param>
        /// <param name="col">Columna on està la persona</param>
        public void buidar(int fil, int col)
        {
            if (escenari[fil, col] is Persona)
            {
                Persona p = (Persona)escenari[fil, col];
                escenari[fil, col] = new Posicio();
                persones.Eliminar(p.Nom);
            }
        }


        /// <summary>
        /// Posa una Persona dins de l'escenari i a la taula de persones
        /// Si la posició de la persona ja està ocupada, genera una excepció
        /// </summary>
        /// <param name="pers">Persona a afegir</param>
        public void Posar(Persona pers)
        {
            if (!escenari[pers.Fila, pers.Columna].Buida) throw new Exception("La posicio ja està ocupada!!");
            
            escenari[pers.Fila, pers.Columna] = pers;
            persones.Afegir(pers);
            if (pers.EsConvidat())
            {
                if (!((Convidat)pers).EsHome())
                    nDones++;
                else
                    nHomes++;
            }
            else
                nCambrers++;
        }


        /// <summary>
        /// Mira si en el tauler hi ha alguna persona amb aquest nom
        /// </summary>
        /// <param name="nom">Nom a cercar</param>
        /// <returns>Si hi ha coincidència</returns>
        public bool NomRepetit(string nom)
        {
            return TaulaPersones[nom] != null;
        }

        /// <summary>
        /// Fa que totes les persones facin un moviment
        /// </summary>
        public void Cicle()
        {
            Posicio posNova;
            
            foreach(Persona p in persones.Gent.Values)
            {
                posNova = CalculaPosicioNova(p.Fila,p.Columna,p.OnVaig(this));
                Moure(p.Fila, p.Columna, posNova.Fila, posNova.Columna);
            }

        }

        private Posicio CalculaPosicioNova(int fil, int col, Direccio direccio)
        {
            Posicio pos = new Posicio();

            switch (direccio)
            {
                case Direccio.Amunt:
                    pos.Fila = fil - 1;
                    pos.Columna = col;
                    break;
                case Direccio.Avall:
                    pos.Fila = fil + 1;
                    pos.Columna=col;
                    break;
                case Direccio.Dreta:
                    pos.Fila = fil;
                    pos.Columna = col+1;
                    break;
                case Direccio.Esquerra:
                    pos.Fila = fil;
                    pos.Columna = col - 1;
                    break;
                default:
                    pos.Fila = fil;
                    pos.Columna = col;
                    break;
            }

            return pos;
        }

        /// <summary>
        /// Funció que ens emplena l'escenari de posicions buides, 
        /// </summary>
        private void  EmplenarEscenari()
        {
            for (int i = 0; i < files; i++)
            {
                for (int j = 0; j < columnes; j++)
                {
                    escenari[i, j] = new Posicio(i,j);
                }
            }
        }

        /// <summary>
        /// Mètode Tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < files; i++)
            {
                for (int j = 0; j < columnes; j++)
                {
                    if (!escenari[i, j].Buida)
                    {
                        Persona p = (Persona)(escenari[i, j]);
                        s += p.ToString().PadLeft(10, ' ');
                    }
                    else
                    {
                        string cadena = "0";
                        s +=  cadena.PadLeft(10, ' ');
                    }
                }
                s += "\n";
            }
            return s;

        }


    }
}
