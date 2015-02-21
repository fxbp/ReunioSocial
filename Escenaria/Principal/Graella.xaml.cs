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
        SlotGraella[,] matriuSlots;



        public Graella(Escenari escenari)
        {
            InitializeComponent();
            files = new Dictionary<string, int>();
            columnes = new Dictionary<string, int>();
            this.escenari = escenari;
            matriuSlots = new SlotGraella[escenari.Homes+escenari.Dones + 1, escenari.Homes+ escenari.Dones + 1];



            CrearGraella(escenari);
            MostrarSimpaties();
        }

        

        private void MostrarSimpaties()
        {
            int fila, columna;
            TextBlock lbl;
            TextBox txb;
            TextBox actual;
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
                            actual = new TextBox();
                            Binding b = new Binding();
                            b.Source = ((Convidat)lbl.Tag)[kvp.Key];
                           // actual.SetBinding(TextBox.TextProperty, b);
                            actual.Text = kvp.Value.ToString();
                            actual.Tag = kvp.Key;
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

                txb= ui as TextBox;
                actual =  new TextBox();
                actual.Tag = txb.Tag;
                actual.Text = txb.Text;
                actual.TextChanged+=actual_TextChanged;
                grdSimpaties.Children.Add(actual);
                actual.SetValue(Grid.RowProperty, txb.GetValue(Grid.RowProperty));
                actual.SetValue(Grid.ColumnProperty, txb.GetValue(Grid.ColumnProperty));

            }
            
        }

        private void actual_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            int fila = 0;
            int columna = (int)txb.GetValue(Grid.ColumnProperty);
            
            
        }

        private void CrearGraella(Escenari e)
        {
            // Empleno els noms de les files i de les columnes per els convidats amb un sol convidat a cada fila o a cada columna.
            EmplenaNomsFiles();
            EmplenaNomsColumnes();
            EmplenaSimpaties();
            MostraGraella();

            ConstrueixVisual();


                //TextBlock lblPersonaActual;
                foreach (Persona p in e.TaulaPersones)
                {
                    if (p.EsConvidat())
                    {



                        /*
                    
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
                        */
                    }
                }
        }


        private void ConstrueixVisual()
        {
            GeneraColumnes();
            GeneraFiles();
            

            UIElement element;
            for (int i = 0; i < matriuSlots.GetLength(0); i++)
            {
                for (int j = 0; j < matriuSlots.GetLength(1); j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        if (i == j)
                        {
                            /*
                            element = new TextBlock();
                            element.Text = matriuSlots[i, j].ExtreuPlusSexe(matriuSlots[i, 0].ConvidatFila);
                            */
                        }
                        else
                        {
                            element = EvaluaElement(i, j);
                            grdSimpaties.Children.Add(element);
                            element.SetValue(Grid.RowProperty, i);
                            element.SetValue(Grid.ColumnProperty, j);
                        }
                    }
                }
            }


        }

        private UIElement EvaluaElement(int fila, int col)
        {
            UIElement retorn = new UIElement();
            if (fila == 0 || col == 0)
            {
                TextBlock tb = new TextBlock();
                tb.Text = matriuSlots[fila, col].ToString();
                retorn = (UIElement)tb;
            }
            else
            {
                TextBox tx = new TextBox();
                tx.Text = matriuSlots[fila, col].ToString();
                retorn = (UIElement)tx;
            }
            return retorn;
        }

        private void GeneraColumnes()
        {
            for(int i = 0; i<matriuSlots.GetLength(1); i++)
            {
                grdSimpaties.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private void GeneraFiles()
        {
            for(int i = 0; i<matriuSlots.GetLength(0); i++)
            {
                grdSimpaties.RowDefinitions.Add(new RowDefinition());
            }
        }


        /***********************************
         * MÈTODES CONSOLA [TEST]
         */ 
        /// <summary>
        /// Mostra la graella per consola.
        /// </summary>
        private void MostraGraella()
        {
            string aMostrar = "";
            for (int i = 0; i < matriuSlots.GetLength(0); i++)
            {
                for (int j = 0; j < matriuSlots.GetLength(1); j++)
                {
                    if (matriuSlots[i, j] != null)
                    {
                        aMostrar += matriuSlots[i, j].ToString().PadLeft(10, ' ');
                    }
                    else
                    {
                        if (i != 0 && j != 0)
                            aMostrar += (matriuSlots[i, 0].ConvidatFila.PlusSexe.ToString().PadLeft(10, ' '));
                        else
                            aMostrar += "          ";
                    }
                }
                aMostrar += "\n";
            }
            Console.WriteLine(aMostrar);
        }



        /***********************************
         * INICIALITZACIÓ DE LA GRAELLA
         */ 

        private void EmplenaSimpaties()
        {
            for (int i = 1; i < matriuSlots.GetLength(0); i++)
            {
                for (int j = 1; j < matriuSlots.GetLength(1); j++)
                {
                    if(i!= j)
                        matriuSlots[i, j] = new SlotGraella(matriuSlots[i,0].ConvidatFila, matriuSlots[0,j].ConvidatColumna);
                }
            }
        }
        private void EmplenaNomsFiles()
        {
            int i = 1;
            foreach(Persona p in escenari.TaulaPersones)
            {
                if (p.EsConvidat())
                {
                    matriuSlots[i, 0] = new SlotGraella((Convidat)p, true);
                    i++;
                }
            }
        }
        private void EmplenaNomsColumnes()
        {
            int i = 1;
            foreach (Persona p in escenari.TaulaPersones)
            {
                if (p.EsConvidat())
                {
                    matriuSlots[0, i] = new SlotGraella((Convidat)p, false);
                    i++;
                }
            }
        }



        /***********************************
         * MÈTODES PER ESDEVENIMENTS.
         */ 

        private void wndGraella_Loaded(object sender, RoutedEventArgs e)
        {
            //Random r = new Random();

            //            Escenari esc = CrearEscenari(r);


            //CrearGraella(escenari);
            //MostrarSimpaties();

        }
        
       
    }
}
