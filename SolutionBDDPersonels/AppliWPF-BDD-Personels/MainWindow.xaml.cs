using System;
using System.Collections.Generic;
using System.IO;
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
using BddpersonnelContext;
using biblioBDDPersonels1;


namespace AppliWPF_BDD_Personels
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CBDDPersonels1 bddPersonels=null;
        public MainWindow()
        {
            InitializeComponent();
            string Ipserveur = Properties.Settings.Default.IpServeur;
            string PortEcoute = Properties.Settings.Default.PortEcouteTCP;// essayer de pouvoir l'utiliser 
            string NomBase = Properties.Settings.Default.NomBDD;
            string Username = Properties.Settings.Default.UserNom;
            string Password = Properties.Settings.Default.Password;

            bddPersonels = new CBDDPersonels1(Username,Password,Ipserveur,NomBase);// -> connection en simple utilisateur à l'ouverture de l'application
            
            //faire un if pour éviter les problèmes lors de la connexion si ça ne veut pas se connecter à la base


            List<Service> services=bddPersonels.GetAllServices();
            List<Fonction> fonctions = bddPersonels.GetAllFonctions();
            List<Personnel> personnels = bddPersonels.GetAllPersonnels();
            LbServices.ItemsSource = services;
            CBxTypeRecherche.Items.Add("Type");
            CBxTypeRecherche.Items.Add("Nom");
            CBxTypeRecherche.Items.Add("Prénom");
            CBxTypeRecherche.Items.Add("Fonction");
            CBxTypeRecherche.Items.Add("Service");
            
            //FELIX
            Trombinoscope();
        }



        private void Trier(object sender, RoutedEventArgs e)
        {
            //bouton trier qui ouvre la fenêtre de tri(PAS MOI)
            // WinTrier trier = new WinTrier();
            //trier.ShowDialog();
        }

        private void Connexion(object sender, RoutedEventArgs e)
        {
            //bouton qui ouvre une fenêtre de connexion, après connexion en gestionnaire n'apparait plus
            WinConnexion Login= new WinConnexion();
            Login.ShowDialog();
        }

        private void Rechercher(object sender, RoutedEventArgs e)
        {
            //prend le contenu de la textbox pour faire la recherche (PAS MOI)
        }

        private void FermerApp1_Click(object sender, RoutedEventArgs e)
        {
            //fermer app pour le menu gestionnaire
            Application.Current.Shutdown();
        }

        private void FermerApp_Click(object sender, RoutedEventArgs e)
        {
            //fermer app pour le menu utilisateur
            Application.Current.Shutdown();
        }


        private void GestionBdd_Click(object sender, RoutedEventArgs e)
        {
            //ouvre la fenêtre de gestion bdd
            GestionBDD gestionbasededonnee=new GestionBDD();
            gestionbasededonnee.ShowDialog();
        }

        private void GestionFonctionsServices_Click(object sender, RoutedEventArgs e)
        {
            //ouvre la fenêtre de gestion fonctions et services
            GestionFonctionsServices gestionFS=new GestionFonctionsServices();
            gestionFS.ShowDialog();
        }

        private void GestionPersonnels_Click(object sender, RoutedEventArgs e)
        {
         Window1 window1 = new Window1();
            window1.ShowDialog();
        }

        private void Déconnexion_Click(object sender, RoutedEventArgs e)
        {
            //ouvre la fenêtre de déconnexion
            WinDeconnexion disconnect=new WinDeconnexion(); 
            disconnect.ShowDialog();
        }




        //pour le menu gestionnaire
        // on peut faire la visibilité et définir quand le rendre visible MenuGuest.Visible = true; pour l'afficher uniquement si tu es log en gestionnaire
        //MenuGuest.Visible = false si tu n'es pas connecté pour s'afficher
        //MenuGuest.Visibility = Visibility.Visible; 
        //pour le menu simple utilisateur
        //on peut faire la visibilité et définir quand le rendre visible MenuUser.Visible = true; pour l'afficher uniquement si tu es pas log
        //MenuUser.Visible = false si tu es connecté en gestionnaire;

        
        
        //
        //
        //PARTIE DE FELIX
        private void Trombinoscope()
        {
            List<Personnel> personnels = bddPersonels.GetAllPersonnels();


            foreach (Personnel personnel in personnels)
            {

                ListBoxTrom.Items.Add(stack(personnel));

            }

        }
        public void LoadImage(byte[] imageData, Image icon)
        {
            BitmapImage bitmapImage = new BitmapImage();

            try
            {
                using (MemoryStream memoryStream = new MemoryStream(imageData))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                }

                icon.Source = bitmapImage;
            }
            catch { }
        }
        public StackPanel stack(Personnel personnel)
        {
            StackPanel stackPanel = new StackPanel();
            Image image = new Image();
            TextBlock textBlock = new TextBlock();
            LoadImage(personnel.Photo, image);
            textBlock.Text = personnel.Nom + " " + personnel.Prenom;
            image.Width = image.Height = 150;
            textBlock.Width = 150;
            stackPanel.Width = 150;
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlock);


            return stackPanel;
        }


        //
    }
}

