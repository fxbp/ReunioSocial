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

namespace Principal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Benvingut : Window
    {
        public Benvingut()
        {
            InitializeComponent();
        }


        /// <summary>
        /// A la festa hi ha aforament, Contant que cada persona té una posició han d'haver-hi un 20% de posicions lliures. 
        /// Retornarà si el nombre de persones entrat és vàlid referent al nombre de posicions.
        /// </summary>
        public bool DadesCorrectes
        {
            get 
            {
                // Recoperem les dades. 
                int persones = (int)(sldCambrers.Value + sldDones.Value + sldHomes.Value);
                int posicions = (int)(sldFiles.Value * sldColumnes.Value);
                
                // Calculem els percentatges. 
                if (persones > posicions * 20 / 100) return false;
                else return true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Abans de res hem de comprovar les dades. 
            if (!DadesCorrectes) MessageBox.Show("El nombre de persones supera a la capacitat del local, el màxim de persones és de : \n" + sldColumnes.Value * sldFiles.Value * 80 / 100);
            else
            {
                // generem l'escenari.
                WndEscenari esc = new WndEscenari(
                    (int)sldFiles.Value, 
                    (int)sldColumnes.Value,
                    (int)sldDones.Value,
                    (int)sldHomes.Value,
                    (int)sldCambrers.Value);

                esc.ShowDialog();
            }
            this.Close();
        }

        

    }
}
