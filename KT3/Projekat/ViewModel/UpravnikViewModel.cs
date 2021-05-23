﻿using Projekat.Model;
using Projekat.Servis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Projekat.ViewModel
{
    public class UpravnikViewModel : BindableBase
    {
        #region UpravnikViewModel
        private ObservableCollection<Obavestenja> obavestenja;
        private Obavestenja izabranoObavjestenje;
        private string obavjestenje;
        public static Window UpravnikProzor { get; set; }
        public static Window UpravnikRegistracijaProzor { get; set; }
        public static Window UspjesnaRegistracijaProzor { get; set; }
        public string Obavjestenje { get { return obavjestenje; } set { obavjestenje = value; OnPropertyChanged("Obavjestenje"); } }
        public Obavestenja IzabranoObavjestenje { get { return izabranoObavjestenje; } set { izabranoObavjestenje = value; OnPropertyChanged("IzabranoObavjestenj"); } }
        public ObservableCollection<Obavestenja> Obavestenja { get { return obavestenja; } set { obavestenja = value; OnPropertyChanged("Obavestenja"); } }
        public UpravnikViewModel()
        {
            OdjavaKomanda = new MyICommand(ZatvoriAplikaciju);
            ProstorijeProzor = new MyICommand(OtvoriProstorije);
            ZahtjeviProzor = new MyICommand(OtvoriZahtjeve);
            KomunikacijProzor = new MyICommand(OtvoriKomunikaciju);
            PrikazObavjestenja = new MyICommand(ObavjestenjeDetaljnije);
            ZatvoriObavjestenje = new MyICommand(ZatvoriObavjestenja);
            PrijaviSeKomanda = new MyICommand(Prijava);
            Registracija = new MyICommand(RegistrujSe, ValidnaRegistracija);
            RegistracijaKlik = new MyICommand(OtvoriRegistraciju);
            Logovanje = new MyICommand(UlogujSe);
            Upravnik = new MyICommand(OtvoriUpravnika);
            dodajObavjestenja();
        }
        public MyICommand RegistracijaKlik { get; set; }
        public Window ObavjestenjeProzor { get; set; }
        public MyICommand Logovanje { get; set; }
        public static Window PrijavaProzor { get; set; }
        private void ZatvoriObavjestenja()
        {
            ObavjestenjeProzor.Close();
        }
        private void ObavjestenjeDetaljnije()
        {
            if(izabranoObavjestenje != null)
            {
                ObavjestenjeProzor = new PrikazObavjestenja();
                ObavjestenjeProzor.Show();
                Obavjestenje = izabranoObavjestenje.SadrzajObavestenja;
                ObavjestenjeProzor.DataContext = this;
            }
            else
            {
                MessageBox.Show("Morate izabrati obavjestenje!");
            }
        }

        private void dodajObavjestenja()
        {
            Obavestenja = new ObservableCollection<Obavestenja>();
            foreach (Obavestenja obavjestenja in ObavestenjaMenadzer.obavestenja)
            {
                if (obavjestenja.Oznaka.Equals("svi") || obavjestenja.Oznaka.Equals("upravnici"))
                {
                    Obavestenja.Add(obavjestenja);
                }
            }
        }
        public MyICommand OdjavaKomanda { get; set; }
        public MyICommand PrikazObavjestenja { get; set; }
        public MyICommand ProstorijeProzor { get; set; }
        public MyICommand ZahtjeviProzor { get; set; }
        public MyICommand KomunikacijProzor { get; set; }
        public MyICommand ZatvoriObavjestenje { get; set; }
        public MyICommand PrijaviSeKomanda { get; set; }
        public MyICommand Registracija { get; set; }
        public MyICommand Upravnik { get; set; }

        private void OtvoriKomunikaciju()
        {
            KomunikacijaViewModel.KomunikacijaProzor = new Komunikacija();
            KomunikacijaViewModel.KomunikacijaProzor.Show();
            KomunikacijaViewModel.KomunikacijaProzor.DataContext = new KomunikacijaViewModel();
            UpravnikProzor.Close();
        }
        private void OtvoriZahtjeve()
        {
            ZahtjeviViewModel.ZahtjeviProzor = new Zahtjevi();
            ZahtjeviViewModel.ZahtjeviProzor.Show();
            ZahtjeviViewModel.ZahtjeviProzor.DataContext = new ZahtjeviViewModel();
            UpravnikProzor.Close();
        }
        private void OtvoriProstorije()
        {
            SaleViewModel.SaleProzor = new PrikaziSalu();
            SaleViewModel.SaleProzor.Show();
            SaleViewModel.SaleProzor.DataContext = new SaleViewModel();
            UpravnikProzor.Close();
        }
        private void ZatvoriAplikaciju()
        {
            PrijavaProzor = new UpravnikPrijava();
            PrijavaProzor.Show();
            korisnickoIme = "";
            lozinka = "";
            PrijavaProzor.DataContext = this;
            UpravnikProzor.Close();
        }
        #endregion
        #region PrijavaUpravnika
        private string korisnickoIme;
        private string lozinka;
        public string KorisnickoIme { get { return korisnickoIme; } set { korisnickoIme = value; OnPropertyChanged("KorisnickoIme"); } }
        public string Lozinka { get { return lozinka; } set { lozinka = value; OnPropertyChanged("Lozinka"); } }
        private void Prijava()
        {
            foreach(UpravnikModel upravnik in UpravnikServis.NadjiSveUpravnike())
            {
                if(upravnik.KorisnickoIme.Equals(KorisnickoIme) && upravnik.Lozinka.Equals(Lozinka))
                {
                    UpravnikProzor = new Upravnik();
                    UpravnikProzor.Show();
                    UpravnikProzor.DataContext = this;
                    PrijavaProzor.Close();
                    return;
                }
            }
            Console.WriteLine("DSadasdasdas");
            MessageBox.Show("Neisptavno korisnicko ime i / ili lozinka");
        }
        #endregion
        #region RegistracijaUpravnika
        private string ime;
        private string prezime;
        private string korisnickoImeRegistracija;
        private string lozinkaRegistracija;
        public string Ime { get { return ime; } set { ime = value; OnPropertyChanged("Ime"); Registracija.RaiseCanExecuteChanged(); } }
        public string Prezime { get { return prezime; } set { prezime = value; OnPropertyChanged("Prezime"); Registracija.RaiseCanExecuteChanged(); } }
        public string KorisnickoImeRegistracija { get { return korisnickoImeRegistracija; } set { korisnickoImeRegistracija = value; OnPropertyChanged("KorisnickoImeRegistracija"); Registracija.RaiseCanExecuteChanged(); } }
        public string LozinkaRegistracija { get { return lozinkaRegistracija; } set { lozinkaRegistracija = value; OnPropertyChanged("LozinkaRegistracija"); Registracija.RaiseCanExecuteChanged(); } }
        private void RegistrujSe()
        {
            UpravnikModel upravnik = new UpravnikModel(korisnickoImeRegistracija, lozinkaRegistracija);
            UpravnikServis.DodajUpravnika(upravnik);
            UpravnikRegistracijaProzor.Close();
            UspjesnaRegistracijaProzor = new UspjesnaRegistracijaUpravnik();
            UspjesnaRegistracijaProzor.Show();
            UspjesnaRegistracijaProzor.DataContext = this;
        }
        private void UlogujSe()
        {
            PrijavaProzor = new UpravnikPrijava();
            PrijavaProzor.Show();
            PrijavaProzor.DataContext = this;
            UspjesnaRegistracijaProzor.Close();
        }
        private void OtvoriUpravnika()
        {
            UpravnikProzor = new Upravnik();
            UpravnikProzor.Show();
            UpravnikProzor.DataContext = this;
            UspjesnaRegistracijaProzor.Close();
        }
        private bool ValidnaRegistracija()
        {
            if(ime != null && prezime != null && korisnickoImeRegistracija != null && lozinkaRegistracija != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OtvoriRegistraciju()
        {
            UpravnikRegistracijaProzor = new UpravnikRegistracija();
            UpravnikRegistracijaProzor.Show();
            korisnickoImeRegistracija = "";
            lozinkaRegistracija = "";
            ime = "";
            prezime = "";
            UpravnikRegistracijaProzor.DataContext = this;
            PrijavaProzor.Close();
        }

        #endregion
    }
}