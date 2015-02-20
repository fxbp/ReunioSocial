using ReunioSocial;
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
using System.Windows.Shapes;

namespace Principal
{
    /// <summary>
    /// Lógica de interacción para Graella.xaml
    /// </summary>
    public partial class Graella : Window
    {

        Dictionary<string, int> files;
        Dictionary<string, int> columnes;
        Escenari escenari;
        Convidat[] taulaConvidats;



        public Graella(Escenari escenari)
        {
            InitializeComponent();
            files = new Dictionary<string, int>();
            columnes = new Dictionary<string, int>();
            this.escenari = escenari;
            taulaConvidats = new Convidat[escenari.Homes + escenari.Dones];



            //CrearGraella(escenari);
            //MostrarSimpaties();
        }

        

        private void MostrarSimpaties()
        {
            int fila, columna;
            TextBlock lbl;
            TextBlock actual;
            Grid provisional = new Grid();
            List<KeyValuePair<string, int>> simpaties;
            foreach (UIElement ui in grdSimpaties.Children)
            {
                //per a cada element del grid receptor mira si conte una label
                if (ui is TextBlock)
                {
                    lbl = ui as TextBlock;
                    //si es una label mira si el tag conte un convidat
                    //Nomes s'han afegit els convidats al tag de les files, les columnes no tenen res al tag
                    if (lbl.Tag != null && lbl.Tag is Convidat)
                    {
                        //extreu la simpatia del convidat
                        simpaties = ((Convidat)lbl.Tag).ExtreuSimpaties();
                        //obte la fila del convidat actual al diccionari de files
                        fila = files[((Convidat)lbl.Tag).Nom];
                        foreach (KeyValuePair<string, int> kvp in simpaties)
                        {
                            //per a cada elmeent dins les simpeties obte la columna on anira el convidat
                            //no es conte a si mateix
                            columna = columnes[kvp.Key];
                            actual = new TextBlock();
                            actual.Text = kvp.Value.ToString();
                            provisional.Children.Add(actual);
                            actual.SetValue(Grid.RowProperty, fila);
                            actual.SetValue(Grid.ColumnProperty, columna);
                            //cada element es posa dins un grid prosivional. 
                            //si es fiques directament al grid desti el foreach acabaria ja que cada cop canvia el nombre de fills que te
                        }
                    }
                }
            }

            //es fa el traspas del grid provisional al grid de desti.
            foreach (UIElement ui in provisional.Children)
            {

                lbl = ui as TextBlock;
                actual = new TextBlock();
                actual.Text = lbl.Text;
                grdSimpaties.Children.Add(actual);
                actual.SetValue(Grid.RowProperty, lbl.GetValue(Grid.RowProperty));
                actual.SetValue(Grid.ColumnProperty, lbl.GetValue(Grid.ColumnProperty));

            }
            
        }

        private void CrearGraella(Escenari e)
        {
            int actual = 1;
            TextBlock lblPersonaActual;
            foreach (Persona p in e.TaulaPersones)
            {
                if (p.EsConvidat())
                {



                    
                    //afageix una fila i una columna mes
                    grdSimpaties.RowDefinitions.Add(new RowDefinition());
                    grdSimpaties.ColumnDefinitions.Add(new ColumnDefinition());
                    //per la fila
                    //crea un nou label amb el nom de la persona i la posa al tag
                    //posa la label dins la graella, la fila sempre sera 0 la columna anira canviant
                    lblPersonaActual = new TextBlock();
                    lblPersonaActual.Text = p.Nom;
                    //lblPersonaActual.Name = "lbl" + p.Nom + "fila";
                    lblPersonaActual.Tag = p;
                    grdSimpaties.Children.Add(lblPersonaActual);
                    lblPersonaActual.SetValue(Grid.RowProperty, 0);
                    lblPersonaActual.SetValue(Grid.ColumnProperty, actual);
                    //diccionari de files que conte el nom de la persona i la fila que li toca
                    files.Add(p.Nom, actual);
                    //per la columna
                    //crea igualment una nova label amb el nom de la persona
                    //posa la label dins la graella, la columna sempre sera 0 i la fila anira canviant
                    lblPersonaActual = new TextBlock();
                    lblPersonaActual.Text = p.Nom;
                    grdSimpaties.Children.Add(lblPersonaActual);
                    //lblPersonaActual.Name = "lbl" + p.Nom + "columna";
                    lblPersonaActual.SetValue(Grid.RowProperty, actual);
                    lblPersonaActual.SetValue(Grid.ColumnProperty, 0);
                    //diccionari de columnes que conte el nom de la persona i la columna que li toca
                    columnes.Add(p.Nom, actual);
                    actual++;
                    
                }
            }
        }

        /*
        private void wndGraella_Loaded(object sender, RoutedEventArgs e)
        {
            //Random r = new Random();

            //            Escenari esc = CrearEscenari(r);


            //CrearGraella(escenari);
            //MostrarSimpaties();

        }
        */
       
    }
}
