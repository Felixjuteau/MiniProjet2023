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

namespace AppliWPF_BDD_Personels
{
    /// <summary>
    /// Logique d'interaction pour AjouterFonctions.xaml
    /// </summary>
    public partial class AjouterFonctions : Window
    {
        public AjouterFonctions()
        {
            InitializeComponent();
        }

        private void AnnulerAjoutF(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AjouterFonction(object sender, RoutedEventArgs e)
        {
            //enregistre dans la bdd et l'ajoute
        }
    }
}
