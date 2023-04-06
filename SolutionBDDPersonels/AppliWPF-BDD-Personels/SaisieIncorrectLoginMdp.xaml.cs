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
    /// Logique d'interaction pour SaisieIncorrectLoginMdp.xaml
    /// </summary>
    public partial class SaisieIncorrectLoginMdp : Window
    {
        public SaisieIncorrectLoginMdp()
        {
            InitializeComponent();
        }

        private void okerreur(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
