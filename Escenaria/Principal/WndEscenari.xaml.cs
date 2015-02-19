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

        private List<string> homes;
        private List<string> dones;
        private Escenari escenari;
        private Random r;
        Window gr;

        public WndEscenari(int nFiles, int nColumnes, int nDones, int nHomes, int nCambrers)
        {
            r = new Random();
            escenari = new Escenari(nFiles, nColumnes);

            

            EmplenaNomsDones();
            EmplenaNomsHomes();

            GeneraHomes(nHomes);
            GeneraDones(nDones);
            GeneraCambrers(nCambrers);
            InitializeComponent();

            ugPista.Rows = nFiles;
            ugPista.Columns = nColumnes;
            
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
                else
                {
                    r.Fill = db;
                    
                    /*
                    TextBlock tb = new TextBlock();
                    tb.Foreground = Brushes.Yellow;
                    tb.Background = Brushes.Red;
                     */
                    if (!p.Buida)
                    {
                        if (!((Persona)p).EsConvidat()) r.Fill = (Brush)FindResource("cambrer");
                        else
                        {
                            if(((Convidat)p).EsHome())
                                r.Fill = (Brush)FindResource("home");
                            else r.Fill = (Brush)FindResource("dona");
                        }
                       
                        // = ((Persona)p).Nom;
                    }
                }
                /*else
                {
                    tb.Text = "Buida";
                }*/
                ugPista.Children.Add(r);

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
            ActualitzaEscenari();
        }


        

    }
}
