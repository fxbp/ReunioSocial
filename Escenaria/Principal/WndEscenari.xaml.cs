using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ReunioSocial;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace Principal
{
    /// <summary>
    /// Lógica de interacción para Escenari.xaml
    /// </summary>
    public partial class WndEscenari : Window
    {
        public const double SEGONSPAUSA = 2;
        private const string FITXERHOMES = @"../../DataSource/Homes.txt";
        private const string FITXERDONES = @"../../DataSource/dones.txt";
        private Dictionary<string, StackPanel> diccionariStacks;
        private List<string> homes;
        private List<string> dones;
        private Escenari escenari;
        private Random r;
        Window gr;
    

        public WndEscenari(int nFiles, int nColumnes, int nDones, int nHomes, int nCambrers)
        {
            r = new Random();
            escenari = new Escenari(nFiles, nColumnes);
            diccionariStacks = new Dictionary<string, StackPanel>();
            
            escenari.Moguda += escenari_Moguda;
            EmplenaNomsDones();
            EmplenaNomsHomes();

            GeneraHomes(nHomes);
            GeneraDones(nDones);
            GeneraCambrers(nCambrers);
            InitializeComponent();

            //ugPista.Rows = nFiles;
           // ugPista.Columns = nColumnes;
            GenerarPista(nFiles, nColumnes);
            GenerarSimpaties();
           

            gr = new Graella(escenari);
            
          
            gr.Show();
            gr.Focusable = true;
            CrearEscenariGrafic();


        }

             

        /**********************************************************************
         * GENERACIONS AUTOMÀTIQUES
         */
        /// <summary>
        /// Genera tots els homes. 
        /// </summary>
        /// <param name="nHomes"></param>
        private void GeneraHomes(int nHomes)
        {
            for (int i = 0; i < nHomes; i++)
            {
                Posicio p = GeneraPosicioBuida(r);
                p = new Home(homes[r.Next(0, homes.Count)], GeneraSexe(r), p.Fila, p.Columna);
                escenari.Posar((Persona)p);
                
            }
        }
        /// <summary>
        /// Genera totes les dones.
        /// </summary>
        /// <param name="nDones"></param>
        private void GeneraDones(int nDones)
        {
            for (int i = 0; i < nDones; i++)
            {
                Posicio p = GeneraPosicioBuida(r);
                p = new Dona(dones[r.Next(0,dones.Count)],GeneraSexe(r),p.Fila, p.Columna);
                escenari.Posar((Persona)p);
            }
        }
        /// <summary>
        /// GeneraCambrers
        /// </summary>
        /// <param name="nCambrers"></param>
        private void GeneraCambrers(int nCambrers)
        {
            for (int i = 0; i < nCambrers; i++)
            {
                Posicio p = GeneraPosicioBuida(r);                 
                p = new Cambrer(p.Fila, p.Columna);
                escenari.Posar((Persona)p);
            }
        }
        /// <summary>
        /// Afegeix totes les simpaties
        /// 
        /// </summary>
        private void GenerarSimpaties()
        {
            foreach (Persona p in escenari.TaulaPersones)
            {
                if (p.EsConvidat())
                {
                    Convidat actual = (Convidat)p;
                    foreach (Persona convidat in escenari.TaulaPersones)
                    {
                        if (convidat.EsConvidat() && p != convidat)
                        {
                            Convidat llistat = (Convidat)convidat;
                            actual.AfegirSimpatia(llistat.Nom, r.Next(-5, 6));
                        }
                    }
                }
            }
        }

        /**********************************************************************
         * UTILITATS
         */

        /// <summary>
        /// Retorna un nombre per la qualificació del sexe vàlid.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private int GeneraSexe(Random r)
        {
            return r.Next(0, 3);
        }
        /// <summary>
        /// Genera una posició buida per ser assignada.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private Posicio GeneraPosicioBuida(Random r)
        {
            Posicio p = escenari[r.Next(0, escenari.Files), r.Next(0, escenari.Columnes)];
            while (!escenari[p.Fila,p.Columna].Buida)
            {
                p = escenari[r.Next(0, escenari.Files), r.Next(0, escenari.Columnes)];
            }
            return p;
            
        }
        /// <summary>
        /// Emplena una llista de homes per poder generar els randoms per els noms. 
        /// </summary>
        private void EmplenaNomsHomes()
        {
            homes = new List<string>();
            FileStream fs = new FileStream(FITXERHOMES, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            while (!sr.EndOfStream)
            {
                homes.Add(sr.ReadLine());
            }
            sr.Close();
            fs.Close();
        }
        /// <summary>
        /// Emplena una llista de dones per poder generar els randoms dels noms.
        /// </summary>
        private void EmplenaNomsDones()
        {
            dones = new List<string>();
            FileStream fs = new FileStream(FITXERDONES, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            while (!sr.EndOfStream)
            {
                dones.Add(sr.ReadLine());
            }
            sr.Close();
            fs.Close();
        }
        /// <summary>
        /// S'ha canviat el ugrid per un grid ara s'han de generar les files i columnes
        /// </summary>
        private void GenerarPista(int files, int columnes)
        {
            for(int i=0;i<files;i++)
            {
                RowDefinition ro = new RowDefinition();
                ro.Height = new GridLength(130);
                ugPista.RowDefinitions.Add(ro);
            }  
            for(int j=0;j<columnes;j++)
            {
                ColumnDefinition co = new ColumnDefinition();
                co.Width = new GridLength(170);
                ugPista.ColumnDefinitions.Add(co);
            }
        }
        private void CrearEscenariGrafic()
        {
            ugPista.Children.Clear();
            foreach (Posicio p in escenari.EscenariToMatriusPosicions)
            {
                StackPanel sp = new StackPanel();      
                Label lbNom = new Label();
                Rectangle rCara = new Rectangle();
                DrawingBrush db = (DrawingBrush)FindResource("terra");
                lbNom.Style = (Style)FindResource("nomsStyle");                
                rCara.Style = (Style)FindResource("rCaraStyle");           
                if (db == null)sp.Background = Brushes.Black; 
                else
                {               
                    //sp.Background = db;

                    if (!p.Buida)
                    {
                        Persona person = p as Persona;
                        if (!person.EsConvidat()) rCara.Fill = (Brush)FindResource("cambrer");
                        else
                        {
                            if (((Convidat)person).EsHome())
                                rCara.Fill = (Brush)FindResource("home");
                            else rCara.Fill = (Brush)FindResource("dona");
                        }
                       
                        lbNom.Content = person.Nom.ToString();

                        sp.Tag = person; 
                    }
                    
                    sp.Children.Add(rCara);
                    sp.Children.Add(lbNom);
                }

                ugPista.Children.Add(sp);
                sp.SetValue(Grid.RowProperty, p.Fila);
                sp.SetValue(Grid.ColumnProperty, p.Columna);
                diccionariStacks.Add((p.Fila.ToString() + "," + p.Columna.ToString()), sp);
      
            }
        }

        /// <summary>
        /// Esdeveniment del cicle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            escenari.Cicle();
          //mirar events.
            
           // ActualitzaEscenari();
        }

        /// <summary>
        /// Esdeveniment per acutalitzar les posicions
        /// </summary>
        /// <param name="anterior"></param>
        /// <param name="actual"></param>
        void escenari_Moguda(Posicio anterior, Posicio actual)
        {

            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() => CanviarPosicions(anterior, actual)));
       
          
        }



        private void CanviarPosicions(Posicio anterior, Posicio actual)
        {
            StackPanel sp = diccionariStacks[anterior.Fila.ToString() + "," + anterior.Columna.ToString()];
            StackPanel sp2 = diccionariStacks[actual.Fila.ToString() + "," + actual.Columna.ToString()];

            Direccio dir;
            if (!anterior.Buida)
            {
                dir = ((Persona)anterior).DireccioActual;
                Transicio(sp, dir);
            }
            else
            {
                dir = ((Persona)actual).OnVaig(escenari);
                Transicio(sp2, dir);
            }

            ugPista.Children.Remove(sp);
            ugPista.Children.Remove(sp2);

            ugPista.Children.Add(sp);
            sp.SetValue(Grid.RowProperty, actual.Fila);
            sp.SetValue(Grid.ColumnProperty, actual.Columna);

            ugPista.Children.Add(sp2);
            sp2.SetValue(Grid.RowProperty, anterior.Fila);
            sp2.SetValue(Grid.ColumnProperty, anterior.Columna);

            //Modifica el diccionari perque apunti al stackpanel correcte
            diccionariStacks[anterior.Fila.ToString() + "," + anterior.Columna.ToString()] = sp2;
            diccionariStacks[actual.Fila.ToString() + "," + actual.Columna.ToString()] = sp;

        }

        private Thickness RetornaDireccioMargin(Direccio dir)
        {
            Thickness th = new Thickness();
            switch (dir)
            {
                case Direccio.Amunt:
                    th.Top = -130;
                    break;
                case Direccio.Avall:
                    th.Bottom = -130;
                    break;
                case Direccio.Dreta:
                    th.Right = -170 * 2;
                    break;
                case Direccio.Esquerra:
                    th.Left = -170 * 2;
                    break;

            }
            return th;
        }

        private void Transicio(StackPanel sp, Direccio dir)
        {
            Storyboard story = new Storyboard();
            ThicknessAnimation animacioStack1 = new ThicknessAnimation();

            Thickness margin = RetornaDireccioMargin(dir);

            story.Children.Add(animacioStack1);

            sp.SetValue(Grid.ZIndexProperty, 100);
            animacioStack1.To = margin;
            animacioStack1.Duration = TimeSpan.FromSeconds(SEGONSPAUSA / 2);

            Storyboard.SetTarget(animacioStack1, sp);
            Storyboard.SetTargetProperty(animacioStack1, new PropertyPath(StackPanel.MarginProperty));

            story.Begin();
            Pausa(SEGONSPAUSA);

            sp.BeginAnimation(StackPanel.MarginProperty, null);
        }

        private void Pausa(double segons)
        {
            var frame = new DispatcherFrame();
            new Thread((ThreadStart)(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(segons));
                frame.Continue = false;
            })).Start();
            Dispatcher.PushFrame(frame);
        }
        

        /// <summary>
        /// Esdevenimen al tencar l'escenari. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gr.Close();
        }


        

    }
}
