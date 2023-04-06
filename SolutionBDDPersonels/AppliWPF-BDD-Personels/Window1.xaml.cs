using BddpersonnelContext;
using biblioBDDPersonels1;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AppliWPF_BDD_Personels
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>

    
    public partial class Window1 : Window
    {
        private CBDDPersonels1 bddPersonels = null;

        public Window1()
        {
            InitializeComponent();//lance l'application
            bddPersonels = new CBDDPersonels1();
            List<Personnel> personnels = bddPersonels.GetAllPersonnels();
            Trombinoscope(personnels);
            ComboBox();
            BackgroundTask();
            }

        private void Trombinoscope(List<Personnel> personnels)//permet d'afficher le trombinoscope
        {
            try
            {
                ListBoxTrom.Items.Clear();
                foreach (Personnel personnel in personnels)
                {
                    ListBoxTrom.Items.Add(stack(personnel));//pour chaques personnel, ajouté dans la listbox par une methode stack
                }
            }
            catch (Exception ex) { throw ex; }
        }
        public StackPanel stack(Personnel personnel)//permet d'ajouter un personnel dans une stackPanel dans la listbox 
        {
            try
            {
                StackPanel stackPanel = new StackPanel();
                Image image = new Image();
                TextBlock textBlock = new TextBlock();
                image.Name = "I" + personnel.Id.ToString();//donnée comme nom I+l'id du personnel, exemple: I32 ou I403
                textBlock.Name = "TB" + personnel.Id.ToString();////donnée comme nom TB+l'id du personnel, exemple: TB32 ou TB403
                LoadImage(personnel.Photo, image);//convertire une image byte[] en objet image 
                textBlock.Text = "Nom: " + personnel.Nom + "\nPrénom: " + personnel.Prenom;//exemple Nom: Juteau Prénom: Félix
                textBlock.TextWrapping = TextWrapping.Wrap;//permet de rendre le texte an plusieur ligne plutot que une grande ligne 
                textBlock.TextAlignment = TextAlignment.Center;//permet de centrer le texte dans le blocktexte
                image.Width = image.Height = 150;//image min de 150 sur 150
                textBlock.Width = 150;//largeur de 150 pixel
                stackPanel.Width = 150;//largeur de 150 pixel
                stackPanel.Children.Add(image);//ajoute l'image en enfant du stackpanel
                stackPanel.Children.Add(textBlock);//ajoute le textebox en enfant du stackPanel
                stackPanel.Name = "SP" + personnel.Id;//donnée comme nom SP+l'id du personnel, exemple: SP32 ou SP403
                return stackPanel;
            }
            catch (Exception ex) { throw ex; }
        }
        public void LoadImage(byte[] imageData, Image icon)//methode qui change le type d'une image
        {
            BitmapImage bitmapImage = new BitmapImage(); //crée une image utilisable par l'interface

            try
            {
                using (MemoryStream memoryStream = new MemoryStream(imageData))//
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                }

                icon.Source = bitmapImage;//ajoute l'image dans l'ovjet image
            }
            catch(Exception ex) {throw ex;}
        }
        private void BtPhoto_Click(object sender, RoutedEventArgs e)//permet de mettre une image dans le pc vers l'application
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();//ouvre un dialogue avec l'explorateur de fichier
                openDialog.Filter = "Image files |* .bmp;*.jpg;*.png";//filtre les fichiers part type de .bmp .jpg .png
                openDialog.FilterIndex = 1;
                if (openDialog.ShowDialog() == true)//ouvre l'explorateur de fichier
                {
                    imagePicture.Source = new BitmapImage(new Uri(openDialog.FileName));//ajoute la photo choisie dansl 'explorateur de fichier dans l'imagePicture
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListBoxTrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CheckInputs();
                ListBox listBox = sender as ListBox;
                StackPanel selectedStackPanel = listBox.SelectedItem as StackPanel;
                if (selectedStackPanel != null)
                {
                    //je recupere l'image du stackpanel et je l'implante dans mon image de modif
                    Image selectedImage = selectedStackPanel.Children.OfType<Image>().FirstOrDefault();
                    imagePicture.Source = selectedImage.Source;

                    //je recupere le texte du textBlock
                    TextBlock selectedTextBlock = selectedStackPanel.Children.OfType<TextBlock>().FirstOrDefault();

                    //je recupere le nom
                    int found = selectedTextBlock.Text.IndexOf("\n");
                    string PP = selectedTextBlock.Text.Substring(found + 8).Trim();

                    String searchString = ":";
                    int startIndex = selectedTextBlock.Text.IndexOf(searchString) + 1;
                    searchString = "Prénom";
                    int endIndex = selectedTextBlock.Text.IndexOf(searchString) - 7;
                    String NP = selectedTextBlock.Text.Substring(startIndex, endIndex + searchString.Length - startIndex).Trim();
                    //MessageBox.Show(substring);
                    //MessageBox.Show(PP);
                    List<Personnel> personnels = bddPersonels.GetAllPersonnels();

                    BtModif.IsEnabled = true;
                    BtSupprimer.IsEnabled = true;
                    if (personnels != null)
                    {
                        foreach (Personnel personnel in personnels)
                        {
                            if (NP == personnel.Nom && PP == personnel.Prenom)
                            {

                                TBNom.Text = personnel.Nom.ToString();
                                TBPrenom.Text = personnel.Prenom;
                                TBTelephone.Text = personnel.Telephone;
                                CBService.Text = personnel.Service.Intitule;
                                CBFonction.Text = personnel.Fonction.Intitule;
                                TBId.Text = personnel.Id.ToString();
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("listPersonnel vide");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ComboBox()
        {
            try
            {
                List<Service> services = bddPersonels.GetAllServices();
                List<Fonction> fonctions = bddPersonels.GetAllFonctions();
                foreach (Service service in services)
                {
                    CBService.Items.Add(service.Intitule);

                }
                foreach (Fonction fonction in fonctions)
                {
                    CBFonction.Items.Add(fonction.Intitule);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Personnel personnel = new Personnel();
                personnel.Nom = TBNom.Text.ToString();
                personnel.Prenom = TBPrenom.Text.ToString();
                personnel.Photo = ReverseImage(imagePicture);
                List<Service> services = bddPersonels.GetAllServices();
                List<Fonction> fonctions = bddPersonels.GetAllFonctions();
                foreach (Service service in services)
                {
                    if (CBService.Text == service.Intitule)
                    {
                        personnel.IdService = service.Id;
                    }
                }
                foreach (Fonction fonction in fonctions)
                {
                    if (CBFonction.Text == fonction.Intitule)
                    {
                        personnel.IdFonction = fonction.Id;
                    }
                }
                bddPersonels.AjouterPersonnel(personnel);
                
                ListBoxTrom.Items.Clear();
                List<Personnel> personnels = bddPersonels.GetAllPersonnels();
                Trombinoscope(personnels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtModif_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TBNom.Text != "" || TBPrenom.Text != "")
                {
                    Personnel personnel = new Personnel();

                    personnel.Nom = TBNom.Text;
                    personnel.Prenom = TBPrenom.Text;
                    personnel.Telephone = TBTelephone.Text;
                    personnel.IdService = bddPersonels.GetServicebyintitule(CBService.SelectedItem.ToString()).Id;
                    personnel.IdFonction = bddPersonels.GetFonctionbyintitule(CBFonction.SelectedItem.ToString()).Id;
                    personnel.Photo = ReverseImage(imagePicture);
                    personnel.Id = Convert.ToInt32(TBId.Text);
                    bddPersonels.ModifPersonel(personnel);
                    ListBoxTrom.Items.Clear();
                    List<Personnel> personnels = bddPersonels.GetAllPersonnels();
                    Trombinoscope(personnels);
                }
                else
                {
                    throw new Exception("erreur Modification");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        public byte[] ReverseImage(Image image)//
        {
            byte[] imageData = null;

            try
            {
                BitmapSource bitmapSource = (BitmapSource)image.Source;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    imageData = ms.ToArray();
                }
            }
            catch { }

            return imageData;
        }
        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TBNom.Text != "" || TBPrenom.Text != "")
                {

                    int Id = Convert.ToInt32(TBId.Text);
                    bddPersonels.SuppPersonnel(Id);
                    ListBoxTrom.Items.Clear();
                    List<Personnel> personnels = bddPersonels.GetAllPersonnels();
                    Trombinoscope(personnels);
                }
            }
            catch (Exception ex) { throw ex; }
        }



        private void BackgroundTask()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // intervalle de 1 seconde
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckInputs();
        }
        private void TBNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInputs();
        }

        private void TBPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInputs();
        }

        private void CheckInputs()
        {
            if (ListBoxTrom.SelectedItem == null && !string.IsNullOrEmpty(TBNom.Text) && !string.IsNullOrEmpty(TBPrenom.Text))
            {
                BtAjouter.IsEnabled = true;
            }
            else
            {
                BtAjouter.IsEnabled = false;
            }
        }
    }
}
