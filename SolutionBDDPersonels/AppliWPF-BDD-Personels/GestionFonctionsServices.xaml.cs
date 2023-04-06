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
    /// Logique d'interaction pour GestionFonctionsServices.xaml
    /// </summary>
    public partial class GestionFonctionsServices : Window
    {
        public GestionFonctionsServices()
        {
            InitializeComponent();
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        

        private void ouvrirAjservices(object sender, RoutedEventArgs e)
        {
            //ouvre la fenêtre d'ajout

            AjouterServices AjouterS = new AjouterServices();
            AjouterS.ShowDialog();

        }

        private void ouvrirAjfonctions(object sender, RoutedEventArgs e)
        {
            //ouvre la fenêtre d'ajout

            AjouterFonctions AjouterF= new AjouterFonctions();
            AjouterF.ShowDialog();
        }
    }
}
