﻿using Model;
using Projekat.Model;
using Projekat.Servis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for PodsetnikPacijent.xaml
    /// </summary>
    public partial class PodsetnikPacijent : Page, INotifyPropertyChanged
    {
        private static int idPacijent;
        private static Pacijent prijavljeniPacijent; 
        //*
        public static bool aktivan;
        public int validacija;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public int Validacija
        {
            get
            {
                return validacija;
            }
            set
            {
                if (value != validacija)
                {
                    validacija = value;
                    OnPropertyChanged("Validacija");
                }
            }
        }

        public PodsetnikPacijent(int idPrijavljenogPacijenta)
        {
            InitializeComponent();
            this.DataContext = this;
            idPacijent = idPrijavljenogPacijenta;
            PacijentPagesServis.AktivnaTema(this.zaglavlje, this.SvetlaTema, this.tamnaTema);
            prijavljeniPacijent = PacijentiMenadzer.PronadjiPoId(idPacijent);
            this.podaci.Header = prijavljeniPacijent.ImePacijenta.Substring(0, 1) + ". " + prijavljeniPacijent.PrezimePacijenta;

        }

        private void DodajPodsetnik_Click(object sender, RoutedEventArgs e)
        {
            valVreme.Visibility = Visibility.Hidden;
            valDatum.Visibility = Visibility.Hidden;
            valSadrzaj.Visibility = Visibility.Hidden;
            try
            {
                
                bool potvrdi = false;
                potvrdi = ProveriIspravnostZaVreme(this.Vreme);
                potvrdi = ProveriIspravnostZaDatum(this.Datum);

                string vremePodsetnika = Vreme.Text;
                string datumPodsetnika = Datum.SelectedDate.Value.ToString("MM/dd/yyyy") + " " + vremePodsetnika;
                string sadrzajPodsetnika = SadrzajPodsetnika.Text;

                List<int> pacijenti = new List<int>();
                pacijenti.Add(idPacijent);
                if (SadrzajPodsetnika.Text == "")
                {
                    valSadrzaj.Visibility = Visibility.Visible;
                    return;
                }
                Obavestenja obavestenjeZaPodsetnik = new Obavestenja(ObavestenjaServis.GenerisanjeIdObavestenja(), datumPodsetnika, "Podsetnik", sadrzajPodsetnika, pacijenti, true);
                ObavestenjaServis.PronadjiSvaObavestenja().Add(obavestenjeZaPodsetnik);
                ObavestenjaServis.sacuvajIzmene();

                Vreme.Text = null;
                Datum.Text = null;
                SadrzajPodsetnika.Text = null;

                Page pocetna = new PrikaziTermin(idPacijent);
                this.NavigationService.Navigate(pocetna);
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    valVreme.Visibility = Visibility.Visible;
                }
                if (ex is InvalidOperationException)
                {
                    valDatum.Visibility = Visibility.Visible;
                }
                if(SadrzajPodsetnika.Text == "")
                {
                    valSadrzaj.Visibility = Visibility.Visible;
                }
            }
        }

        private bool ProveriIspravnostZaDatum(DatePicker datum)
        {
            if(datum.SelectedDate.Value == null)
            {
                return false;
            }
            else
            {
                return false;
            }
            
        }

        private bool ProveriIspravnostZaVreme(TextBox vreme)
        {
            try
            {
                TimeSpan vremee = TimeSpan.Parse(Vreme.Text);
                // HH:mm
                if (vreme.Text.Length == 5)
                {
                    return true;
                }
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            /*Page odjava = new PrijavaPacijent();
            this.NavigationService.Navigate(odjava);*/
            PacijentPagesServis.odjava_Click(this);
        }
        public void karton_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.karton_Click(this, idPacijent);
        }
        public void zakazi_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.zakazi_Click(this, idPacijent);
        }
        public void uvid_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.uvid_Click(this, idPacijent);
        }
        private void pocetna_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.pocetna_Click(this, idPacijent);
        }
        private void PromeniTemu(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.PromeniTemu(SvetlaTema, tamnaTema);
        }
        private void Korisnik_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.Korisnik_Click(this, idPacijent);
        }
        private void anketa_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.anketa_Click(this, idPacijent);
        }
        private void Jezik_Click(object sender, RoutedEventArgs e)
        {
            /* var app = (App)Application.Current;
             // TODO: proveriti
             string eng = "en-US";
             string srb = "sr-LATN";
             MenuItem mi = (MenuItem)sender;
             if (mi.Header.Equals("en-US"))
             {
                 mi.Header = "sr-LATN";
                 app.ChangeLanguage(eng);
             }
             else
             {
                 mi.Header = "en-US";
                 app.ChangeLanguage(srb);
             }*/
            PacijentPagesServis.Jezik_Click(Jezik);

        }

    }
}
