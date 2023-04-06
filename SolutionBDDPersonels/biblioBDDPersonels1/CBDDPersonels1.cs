using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BddpersonnelContext;
using Devart.Common;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;

namespace biblioBDDPersonels1

{
    public class CBDDPersonels1
    {
        private BddpersonnelDataContext dc=null;

        //constructeur de base pour accéder à la bdd
        public CBDDPersonels1()
        {
            dc = new BddpersonnelDataContext();
        }

        //constructeur pour accéder à la base en gestionnaire(mais pas uniquement)
        public CBDDPersonels1( string UserId, string Password, string Host, string Database)
        {
            
            string connectionString = "User Id="+UserId+";Password="+Password+";Host="+Host+";Database="+Database+";Persist Security Info=True";
            dc = new BddpersonnelDataContext(connectionString);
           
        }

        public  User GetUser()
        {
            try
            {
                
                return dc.Users.FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        //tableau de services à revoir
        public List<Service> GetAllServices()
        {
            try
            {
                return dc.Services.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //tableau des fonctions

        public List<Fonction> GetAllFonctions()
        {
            try
            {
                return dc.Fonctions.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //tableau du personnels
        public List<Personnel> GetAllPersonnels()
        {
            try
            {
                return dc.Personnels.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    //Felix
        public List<Service> GetServicestrier()//retourne une liste de tout les services trié par l'intitule 
        {
            return dc.Services.OrderBy(y => y.Intitule).ToList();
        }
        public Service GetServicebyintitule(string intitule)//retourne un service selon l'intitule mis en paramettre
        {
            return dc.Services.Where(x => x.Intitule == intitule).FirstOrDefault();
        }
        public Fonction GetFonctionbyintitule(string intitule)//retourne une fonctio selon l'intituler mis en paramettre
        {
            return dc.Fonctions.Where(x => x.Intitule == intitule).FirstOrDefault();
        }
        public Personnel GetPersonnel(string _Prenom, string _Nom, int _IdService, int _IdFonction, string _Telephone, byte[] _Photo)//retourne un personnel donc les attribues sont ce mis en paramettre
        {
            Personnel personnel = new Personnel();
            personnel.Prenom = _Prenom;
            personnel.Nom = _Nom;
            personnel.IdService = _IdService;
            personnel.IdFonction = _IdFonction;
            personnel.Telephone = _Telephone;
            personnel.Photo = _Photo;
            return personnel;
        }
        public void ModifPersonel(Personnel personnel)//permet de modifier un personnel
        {
            dc.Connection.Open();//ouvre la connection avec la bdd
            using (System.Data.Common.DbTransaction transaction
            = dc.Connection.BeginTransaction())//on utilise le système de transaction, pour eviter que si il y a une mauvaise valeur ou autre que le changement ne ce fasse pas
            {
                try
                {
                    int id = personnel.Id;
                    Personnel personnel2 = dc.Personnels.Single(x => x.Id == id);//recupere un personnel dans la bdd dont l'id est celui mis en paramettre
                    dc.SubmitChanges();
                    dc.Personnels.InsertOnSubmit(personnel);//insert un personnel dans les starts in block de la bdd
                    dc.Personnels.DeleteOnSubmit(personnel2);//supprime un personnel dans les start in block de la bdd
                    dc.SubmitChanges();//"sauvgarde" les changements
                    transaction.Commit();//si tout s'est bien passer confirmer les changement des start in block dans la bdd 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();//si probleme revenir en arriere et supprimer les changement dans les start in block
                    throw ex;
                };
            }
        }
        public void SuppPersonnel(int Id)//supprimer un personnel
        {
            try
            { 
                Personnel personnel2 = dc.Personnels.Single(x => x.Id == Id);//recupere un personnel dans la bdd dont l'id est celui mis en paramettre
                    dc.Personnels.DeleteOnSubmit(personnel2);//supprimer le personnel recuperer avec l'id
                dc.SubmitChanges();//sauvegarder les changements
            }
            catch (Exception ex) { throw ex; };
        }
        public void AjouterPersonnel(Personnel personnel)//ajoute un personnel
        {
            try
            {
                dc.Personnels.InsertOnSubmit(personnel);//envoyer le personnel dans la bdd
                dc.SubmitChanges();//sauvgarder les changements
            }
            catch (Exception ex) { throw ex; };
        }
    }
}
//return bdd.Personnels.Where(xx => x.Service.Id == service Id.OrderBy(y=> y.nom).then(y=>u.Prenom).ToList();



