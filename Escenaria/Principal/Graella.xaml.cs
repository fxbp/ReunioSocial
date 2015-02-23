using ReunioSocial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        /**************************************
         * Atributs per el control de la graella
         */
        int valor;


        public Graella(Escenari escenari)
        {
            InitializeComponent();
            files = new Dictionary<string, int>();
            columnes = new Dictionary<string, int>();
            this.escenari = escenari;
            matriuSlots = new SlotGraella[escenari.Homes+escenari.Dones + 1, escenari.Homes+ escenari.Dones + 1];
            CrearGraella(escenari);
        }

        

        

        
        /// <summary>
        /// Genera el procés de creació d'una graell nova sencera. Es crida al constructor.
        /// </summary>
        /// <param name="e"></param>
        private void CrearGraella(Escenari e)
        {
            // Empleno els noms de les files i de les columnes per els convidats amb un sol convidat a cada fila o a cada columna.
            EmplenaNomsFiles();
            EmplenaNomsColumnes();
            EmplenaSimpaties();
            MostraGraella();

            ConstrueixVisual();


        }

        /// <summary>
        /// Construeix la graella visual, el que fa és anar a 
        /// </summary>
        private void ConstrueixVisual()
        {
            GeneraColumnes();
            GeneraFiles();
            UIElement element;
            for (int i = 0; i < matriuSlots.GetLength(0); i++)
            {
                for (int j = 0; j < matriuSlots.GetLength(1); j++)
                {
                    //Recopero l'element que s'ha d'inserir                 
                    element = EvaluaElement(i, j);
                    //Finalment afegeixo l'element a la graella.
                    grdSimpaties.Children.Add(element);
                }
            }


        }

        /// <summary>
        /// Mètode que retorna l'element que s'ha d'inserir a la graella en forma de uielement. 
        /// FALTARÀ!!!! -> Canviar els estils i posar-los en un diccionari i assignar l'estil aquí
        /// </summary>
        /// <param name="fila"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private UIElement EvaluaElement(int fila, int col)
        {
            UIElement retorn = new UIElement();
            if (!(fila == 0 && col == 0))
            {
                if (fila == 0 || col == 0)
                {
                    TextBlock tb = new TextBlock();
tb.Background = Brushes.Yellow;                   
                    if (matriuSlots[fila, col].teConvidatFila)
                        tb.Text = matriuSlots[fila, col].ConvidatFila.ToString();                        
                    else
                        tb.Text = matriuSlots[fila, col].ConvidatColumna.ToString();
                    retorn = (UIElement)tb;
                }
                else
                {
                    if (fila == col)
                    {
                        TextBox sexe = new TextBox();
                        sexe.Text = matriuSlots[fila, col].ExtreuPlusSexe(matriuSlots[fila, 0].ConvidatFila);
                        sexe.TextChanged += tb_TextChanged;
                        sexe.LostFocus += tb_LostFocus;
sexe.Background = Brushes.BlueViolet;
                        retorn = (UIElement)sexe;
                    }
                    else
                    {
                        TextBox tx = new TextBox();
                        tx.Text = matriuSlots[fila, col].ToString();
                        tx.TextChanged += tb_TextChanged;
                        tx.LostFocus += tb_LostFocus;
                        retorn = (UIElement)tx;

                    }
                }
            }
            else
            {
                TextBox sexe = new TextBox();
                sexe.Text = "Plus Sexe";
                sexe.TextChanged += tb_TextChanged;
                sexe.LostFocus += tb_LostFocus;
sexe.Background = Brushes.BlueViolet;
                retorn = (UIElement)sexe;

            }
            
            retorn.SetValue(Grid.RowProperty, fila);
            retorn.SetValue(Grid.ColumnProperty, col);
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
                        if (i != j)
                            aMostrar += matriuSlots[i, j].ToString().PadLeft(10, ' ');
                        else
                            aMostrar += matriuSlots[i, 0].ConvidatFila.PlusSexe.ToString().PadLeft(10, ' ');
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
                    //Si es una casella amb convidats diferents s'afegeixen els dos. 
                    if (i != j)
                        matriuSlots[i, j] = new SlotGraella(matriuSlots[i, 0].ConvidatFila, matriuSlots[0, j].ConvidatColumna);
                    //Si és una casella del mateix convidat, s'emplena amb el mateix convidat tant a fila com a columna
                    else
                        matriuSlots[i, j] = new SlotGraella(matriuSlots[i, 0].ConvidatFila, matriuSlots[i, 0].ConvidatFila);
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

        /// <summary>
        /// Cada cop que s'assigna un valor l'evalua. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)e.Source;
            string val = (string)tb.Text;
            if (Regex.IsMatch(val, @"^[\-\+]?\s*\d+\s*$"))
            {
                valor = Convert.ToInt32(val);
            }
            else
            {
                tb.Text = Convert.ToString(valor);
                
            }
        }
        /// <summary>
        /// Event que actualitza el valor quan es perd el focus, evalua si el valor ha canviat i si és vàlid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_LostFocus(object sender, RoutedEventArgs e)
        {
            // REcopero el textbox
            TextBox tb = (TextBox)e.Source;
            //Si s'ha actualitzat el valor algun cop i els valors son diferents.
            if (valor != default(int))
            {
                //Recopero la fila i la columna on està
                int fila = (int)tb.GetValue(Grid.RowProperty);
                int columna = (int)tb.GetValue(Grid.ColumnProperty);
                // Recopero els tius dins de la fila i la columna.
                Convidat cFila = matriuSlots[fila, 0].ConvidatFila;
                Convidat cColumna = matriuSlots[0, columna].ConvidatColumna;
                //Actualitzo els valors dels convidats utilitzant el valor nou.
                if (cFila == cColumna)
                {
                    cFila.PlusSexe = valor;
                }
                else
                {
                    cFila[cColumna.Nom] = valor;
                }
            }

            MostraGraella();



        }

       
        
       
    }
}
