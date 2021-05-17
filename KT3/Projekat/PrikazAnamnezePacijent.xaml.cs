﻿using Model;
using Projekat.Model;
using Projekat.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat
{
    public partial class PrikazAnamnezePacijent : Page
    {

        private Anamneza anamneza;
        private static int idPacijent;
        public PrikazAnamnezePacijent(Pacijent izabraniPacijent, Anamneza izabranaAnamneza)
        {
            InitializeComponent();
            this.DataContext = this;
            idPacijent = izabraniPacijent.IdPacijenta;
            anamneza = izabranaAnamneza;

            this.datumTermina.Text = anamneza.Datum;
            this.podaciLekar.Text = anamneza.ImePrezimeLekara;
            this.opisBolesti.Text = anamneza.OpisBolesti;
            this.terpaija.Text = anamneza.Terapija;
            this.beleska.Text = anamneza.Beleska;
            if (!anamneza.Beleska.Equals(""))
            {
                this.DodajBelesku.Content = "Izmeni";
            }
            Pacijent prijavljeniPacijent = PacijentiServis.PronadjiPoId(idPacijent);
            this.ime.Text = prijavljeniPacijent.ImePacijenta;
            this.prezime.Text = prijavljeniPacijent.PrezimePacijenta;
            this.jmbg.Text = prijavljeniPacijent.Jmbg.ToString();
            Termin termin = TerminMenadzer.NadjiTerminPoId(anamneza.IdTermina);
            this.sala.Text = termin.Prostorija.brojSale.ToString();
            this.vremeTermina.Text = termin.VremePocetka + "-" + termin.VremeKraja;
            this.podaci.Header = prijavljeniPacijent.ImePacijenta.Substring(0, 1) + ". " + prijavljeniPacijent.PrezimePacijenta;
            PrikaziTermin.AktivnaTema(this.zaglavlje, this.svetlaTema);
        }
        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            Page odjava = new PrijavaPacijent();
            this.NavigationService.Navigate(odjava);
        }
        public void karton_Click(object sender, RoutedEventArgs e)
        {
            Page karton = new ZdravstveniKartonPacijent(idPacijent);
            this.NavigationService.Navigate(karton);
        }
        public void zakazi_Click(object sender, RoutedEventArgs e)
        {
            if (MalicioznoPonasanjeMenadzer.DetektujMalicioznoPonasanje(idPacijent))
            {
                MessageBox.Show("Nije Vam omoguceno zakazivanje termina jer ste prekoracili dnevni limit modifikacije termina.", "Upozorenje", MessageBoxButton.OK);
                return;
            }
            Page zakaziTermin = new ZakaziTermin(idPacijent);
            this.NavigationService.Navigate(zakaziTermin);
        }
        public void uvid_Click(object sender, RoutedEventArgs e)
        {
            Page uvid = new ZakazaniTerminiPacijent(idPacijent);
            this.NavigationService.Navigate(uvid);
        }
        private void pocetna_Click(object sender, RoutedEventArgs e)
        {
            Page pocetna = new PrikaziTermin(idPacijent);
            this.NavigationService.Navigate(pocetna);
        }
        private void anketa_Click(object sender, RoutedEventArgs e)
        {
            Page prikaziAnkete = new PrikaziAnkete(idPacijent);
            this.NavigationService.Navigate(prikaziAnkete);
        }
        private void Korisnik_Click(object sender, RoutedEventArgs e)
        {
            Page podaci = new LicniPodaciPacijenta(idPacijent);
            this.NavigationService.Navigate(podaci);
        }
        private void PromeniTemu(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            MenuItem mi = (MenuItem)sender;
            if (mi.Header.Equals("Svetla"))
            {
                mi.Header = "Tamna";
                app.ChangeTheme(new Uri("Teme/Svetla.xaml", UriKind.Relative));
            }
            else
            {
                mi.Header = "Svetla";
                app.ChangeTheme(new Uri("Teme/Tamna.xaml", UriKind.Relative));
            }
        }

        private void SacuvajBelesku_Click(object sender, RoutedEventArgs e)
        {
            anamneza.Beleska = this.beleska.Text;
            this.beleska.IsEnabled = false;
            this.SacuvajBelesku.IsEnabled = false;
            PacijentiServis.SacuvajIzmenePacijenta(); // ?
        }

        private void DodajBelesku_Click(object sender, RoutedEventArgs e)
        {
            this.DodajBelesku.Content = "Izmeni";
            this.beleska.IsEnabled = true;
            this.SacuvajBelesku.IsEnabled = true;
        }
    }
}
