using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using biblioBDDPersonels1;
using BddpersonnelContext;
using System.Net.Configuration;

namespace AppliWPF_BDD_Personels
{
    /// <summary>
    /// Logique d'interaction pour WinConnexion.xaml
    /// </summary>
    
    public partial class WinConnexion : Window
    {
        private CBDDPersonels1 bddPersonels = null;

        public WinConnexion()
        {
            InitializeComponent();
        }

        private void Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SeConnecter(object sender, RoutedEventArgs e)
        {
            string username = TxtBLogin.Text;
            string passwordG = PwdBoxMdp.Password;
            //se connecter en gestionnaire
            // ouvrir une fenêtre d'erreur si c'est incorect

            //List<User> users = bddPersonels.GetUser();
            //foreach (User user in users)
            //{
            //    user.
            // }

            if (username ==  bddPersonels.GetUser().Login && passwordG == bddPersonels.GetUser().Password) //ne plus le mettre en dur en cas de modification du login
            {

                //se connecte en gestionnaire -> ce qui montre le menu du gestionnaire et enlève le menu du simple utilisateur
                string Username = "userBDDW"; // -> faire en sorte de ne plus avoir en dur
                string Password = "Password1234@il";
                string Ipserveur = Properties.Settings.Default.IpServeur; ;
                string NomBase = Properties.Settings.Default.NomBDD;

                bddPersonels = new CBDDPersonels1(Username, Password, Ipserveur, NomBase);// A revoir
                //Test pour voir si ça fonctionne MessageBox.Show("Connexion établie");
                //fermer la fenêtre
                this.Close();
             }
             else
            {
                //fenêtre d'erreur
                SaisieIncorrectLoginMdp recommence = new SaisieIncorrectLoginMdp();
                recommence.ShowDialog();
            } 
            }
        }


    }
