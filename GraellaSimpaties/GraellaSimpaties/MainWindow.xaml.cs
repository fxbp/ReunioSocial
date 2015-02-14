using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReunioSocial;

namespace GraellaSimpaties
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, int> files;
        Dictionary<string, int> columnes;
        public MainWindow()
        {
            InitializeComponent();
            files = new Dictionary<string, int>();
            columnes = new Dictionary<string, int>();
        }


        private void wndGraella_Loaded(object sender, RoutedEventArgs e)
        {
            Random r = new Random();

            Escenari esc=CrearEscenari(r);
            int homes, dones;

            homes = esc.Homes;
            dones = esc.Dones;

            int totalConviats = homes + dones;

            CrearGraella(totalConviats,esc);
            MostrarSimpaties(esc);
            
        }

        private void MostrarSimpaties(Escenari e)
        {

            int fila, columna;
            Label lbl;
            Label actual;
            Grid provisional = new Grid();
            List<KeyValuePair<string, int>> simpaties;
            foreach(UIElement ui in grdSimpaties.Children)
            {
                if(ui is Label)
                {
                    lbl = ui as Label;
                    if (lbl.Tag!=null&&lbl.Tag is Convidat)
                    {
                        simpaties = ((Convidat)lbl.Tag).ExtreuSimpaties();
                        fila = files[((Convidat)lbl.Tag).Nom];
                        foreach(KeyValuePair<string, int> kvp in simpaties)
                        {
                            columna = columnes[kvp.Key];
                            actual = new Label();
                            actual.Content = kvp.Value;
                            provisional.Children.Add(actual);
                            actual.SetValue(Grid.RowProperty, fila);
                            actual.SetValue(Grid.ColumnProperty, columna);
                        }
                    }
                }
            }
            foreach(UIElement ui in provisional.Children)
            {
                
                lbl = ui as Label;
                actual = new Label();
                actual.Content = lbl.Content;
                grdSimpaties.Children.Add(actual);
                actual.SetValue(Grid.RowProperty,lbl.GetValue(Grid.RowProperty));
                actual.SetValue(Grid.ColumnProperty,lbl.GetValue(Grid.ColumnProperty));
                
            }
        
        }

        private  void CrearGraella(int total,Escenari e)
        {
            int actual = 1;
            Label lblPersonaActual;
            foreach(Persona p in e.TaulaPersones.Gent.Values)
            {
                if (p is Convidat)
                {
                    grdSimpaties.RowDefinitions.Add(new RowDefinition());
                    grdSimpaties.ColumnDefinitions.Add(new ColumnDefinition());
                    //per la fila
                    lblPersonaActual = new Label();
                    lblPersonaActual.Content = p.Nom;
                    lblPersonaActual.Name = "lbl" + p.Nom + "fila";
                    lblPersonaActual.Tag = p;
                    grdSimpaties.Children.Add(lblPersonaActual);
                    lblPersonaActual.SetValue(Grid.RowProperty, 0);
                    lblPersonaActual.SetValue(Grid.ColumnProperty, actual);
                    files.Add(p.Nom, actual);
                    //per la columna
                    lblPersonaActual = new Label();
                    lblPersonaActual.Content = p.Nom;
                    grdSimpaties.Children.Add(lblPersonaActual);
                    lblPersonaActual.Name = "lbl" + p.Nom + "columna";
                    lblPersonaActual.SetValue(Grid.RowProperty, actual);
                    lblPersonaActual.SetValue(Grid.ColumnProperty, 0);
                    columnes.Add(p.Nom, actual);
                    actual++;
                }
            }
        }

        private Escenari CrearEscenari(Random r)
        {
            Escenari esc = new Escenari(50, 50);

            List<Persona> persones = new List<Persona>();
            Convidat conv = null;
            for (int i = 0; i < 30; i++)
            {
                int temp = r.Next(2);
                if (temp == 0)
                {
                    Home h = new Home("Home" + i, r.Next(0, 3), r.Next(0, 8), r.Next(0, 8));
                    persones.Add(h);
                }
                else if (temp == 1)
                {
                    Dona d = new Dona("Dona" + i, r.Next(0, 3), r.Next(0, 8), r.Next(0, 8));
                    persones.Add(d);
                }
             




            }

            foreach (Persona p in persones)
            {
                //Console.WriteLine(p);
                if (esc.DestiValid(p.Fila, p.Columna))
                    esc.Posar(p);
            }



            foreach (Persona p in esc.TaulaPersones.Gent.Values)
            {
                if (p is Convidat)
                {
                    Convidat actual = (Convidat)p;
                    foreach (Persona convidat in esc.TaulaPersones.Gent.Values)
                    {
                        if (convidat is Convidat && p != convidat)
                        {
                            Convidat llistat = (Convidat)convidat;
                            actual.AfegirSimpatia(llistat.Nom, r.Next(-5, 6));
                            conv = actual;
                        }
                    }
                }
            }

            return esc;
        }
    }
}
