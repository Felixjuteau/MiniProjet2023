using biblioBDDPersonels1;
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
using BddpersonnelContext;
using System.Security.Cryptography.X509Certificates;

namespace AppliWPF_BDD_Personels
{
    /// <summary>
    /// Logique d'interaction pour GestionBDD.xaml
    /// </summary>
    public partial class GestionBDD : Window
    {
        private CBDDPersonels1 bddPersonels = null;
        public GestionBDD()
        {
            InitializeComponent();
            TxtBadIPV4.Text = Properties.Settings.Default.IpServeur;
            TxtBportecoute.Text = Properties.Settings.Default.PortEcouteTCP;
            TxtBnomBDD.Text = Properties.Settings.Default.NomBDD;
            TxtBlogin.Text = Properties.Settings.Default.UserNom;
        }


        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            //enregistre les modifications apportées

            //if (TxtBadIPV4.Text)
            {

            }
            Properties.Settings.Default.IpServeur = TxtBadIPV4.Text;
          //  if(TxtBportecoute.Text)
            Properties.Settings.Default.PortEcouteTCP = TxtBportecoute.Text;
            Properties.Settings.Default.NomBDD = TxtBnomBDD.Text;
            Properties.Settings.Default.UserNom = TxtBlogin.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Paramètres sauvegardés");
            this.Close();  

            //if (username == "userBDDW" && password == "Password1234@il")
            //{
            //    //bdd gestionnaire
            //    bddPersonels = new CBDDPersonels1(username, password, host, database);
            //    //A REVOIR
            //    //this.Close();
            //}                    
            //if (username == "userBDDR" && password == "Password1234@il")
            //{
            //        //bdd simple utilisateur
            //        bddPersonels = new CBDDPersonels1();
            //        this.Close();
            //}
            //else
            //{
            //    //fenêtre d'erreur
            //    SaisieIncorrectLoginMdp recommence = new SaisieIncorrectLoginMdp();
            //    recommence.ShowDialog();
            //}


        }
        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
                    //ferme la fenêtre sans enregistrer
                    this.Close();
        }
        
    }
}
