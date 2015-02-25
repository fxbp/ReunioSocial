﻿using System;
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

namespace Principal
{
    /// <summary>
    /// Lógica de interacción para Escenari.xaml
    /// </summary>
    public partial class WndEscenari : Window
    {
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
            ActualitzaEscenari();


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
                ugPista.RowDefinitions.Add(new RowDefinition());
            }  
            for(int j=0;j<columnes;j++)
            {
                ugPista.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private void ActualitzaEscenari()
        {
            ugPista.Children.Clear();
            foreach (Posicio p in escenari.EscenariToMatriusPosicions)
            {
                
                Rectangle r = new Rectangle();
                r.Height = 50;
                r.Width = 80;
                DrawingBrush db = (DrawingBrush)FindResource("buida");
                if (db == null) r.Fill = Brushes.Black;
                StackPanel sp = new StackPanel();
                
                Label lbNom = new Label();
                //Rectangle rTerra = new Rectangle();
                Rectangle rCara = new Rectangle();
                //rTerra.Height = 50;
                //rTerra.Width = 80;
                rCara.Height = 80;
                rCara.Width = 110;
                db = (DrawingBrush)FindResource("terra");
                if (db == null)sp.Background = Brushes.Black; //rTerra.Fill = Brushes.Black;
                else
                {
                   
                    
                    
                    TextBlock tb = new TextBlock();
                    tb.Foreground = Brushes.Yellow;
                    tb.Background = Brushes.Red;
                     
                    //r.Fill = db;
                    sp.Background = db;
                    

                    //rTerra.Fill = db;

                    if (!p.Buida)
                    {
                        if (!((Persona)p).EsConvidat()) r.Fill = (Brush)FindResource("cambrer");
                        Persona person = p as Persona;
                        if (!person.EsConvidat()) rCara.Fill = (Brush)FindResource("cambrer");
                        else
                        {
                            if(((Convidat)p).EsHome())
                                r.Fill = (Brush)FindResource("home");
                            else r.Fill = (Brush)FindResource("dona");
                            if (((Convidat)person).EsHome())
                                rCara.Fill = (Brush)FindResource("home");
                            else rCara.Fill = (Brush)FindResource("dona");
                        }
                       
                        // = ((Persona)p).Nom;
                        lbNom.Content = person.Nom.ToString();
                        lbNom.FontSize = 28;
                        lbNom.FontWeight = FontWeights.Bold;
                        lbNom.HorizontalAlignment = HorizontalAlignment.Center;
                        lbNom.Margin=new Thickness(15,0,15,0);
                        lbNom.Foreground = Brushes.Black;

                        sp.Tag = person;
                       
                        
                    }
                    
                    sp.Children.Add(rCara);
                    sp.Children.Add(lbNom);
                }
                /*else
                {
                    tb.Text = "Buida";
                }*/
                //ugPista.Children.Add(r);

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
            StackPanel sp = diccionariStacks[anterior.Fila.ToString() + "," + anterior.Columna.ToString()];
            StackPanel sp2 = diccionariStacks[actual.Fila.ToString() + "," + actual.Columna.ToString()];
            int col2 = (int)sp2.GetValue(Grid.ColumnProperty);
            int col1 = (int)sp.GetValue(Grid.ColumnProperty);
            int fil2 = (int)sp2.GetValue(Grid.RowProperty);
            int fil1 = (int)sp.GetValue(Grid.RowProperty);
           
            sp.SetValue(Grid.RowProperty, actual.Fila);
            sp.SetValue(Grid.RowProperty, actual.Columna);
            sp2.SetValue(Grid.RowProperty, anterior.Fila);
            sp2.SetValue(Grid.ColumnProperty, anterior.Columna);
            sp2.SetValue(Grid.ZIndexProperty, 100);
            sp.SetValue(Grid.ZIndexProperty, 100);
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
