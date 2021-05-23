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
            isEnabledDugmad();
            Pacijent prijavljeniPacijent = PacijentiServis.PronadjiPoId(idPacijent);
            this.ime.Text = prijavljeniPacijent.ImePacijenta;
            this.prezime.Text = prijavljeniPacijent.PrezimePacijenta;
            this.jmbg.Text = prijavljeniPacijent.Jmbg.ToString();
            Termin termin = TerminServis.NadjiTerminPoId(anamneza.IdTermina);
            this.sala.Text = termin.Prostorija.brojSale.ToString();
            this.vremeTermina.Text = termin.VremePocetka + "-" + termin.VremeKraja;
            this.podaci.Header = prijavljeniPacijent.ImePacijenta.Substring(0, 1) + ". " + prijavljeniPacijent.PrezimePacijenta;
            PrikaziTermin.AktivnaTemaPagea(this.zaglavlje, this.SvetlaTema, this.tamnaTema);
        }

        private void isEnabledDugmad()
        {
            if (anamneza.Beleska.Equals(""))
            {
                this.DodajBelesku.IsEnabled = true;
                this.IzmeniBelesku.IsEnabled = false;
                return;
            }
            else
            {
                this.IzmeniBelesku.IsEnabled = true;
                this.DodajBelesku.IsEnabled = false;
                return;
            }
        }

        private void SacuvajBelesku_Click(object sender, RoutedEventArgs e)
        {
            anamneza.Beleska = this.beleska.Text;
            this.beleska.IsEnabled = false;
            this.SacuvajBelesku.IsEnabled = false;
            isEnabledDugmad();
            PacijentiServis.SacuvajIzmenePacijenta();
        }

        private void DodajBelesku_Click(object sender, RoutedEventArgs e)
        {
            this.beleska.IsEnabled = true;
            this.SacuvajBelesku.IsEnabled = true;
            this.DodajBelesku.IsEnabled = false;
            this.IzmeniBelesku.IsEnabled = false;
        }

        private void IzmeniBelesku_Click(object sender, RoutedEventArgs e)
        {
            this.beleska.IsEnabled = true;
            this.SacuvajBelesku.IsEnabled = true;
            this.DodajBelesku.IsEnabled = false;
            this.IzmeniBelesku.IsEnabled = false;
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
        private void anketa_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.anketa_Click(this, idPacijent);
        }
        private void Korisnik_Click(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.Korisnik_Click(this, idPacijent);
        }
        private void PromeniTemu(object sender, RoutedEventArgs e)
        {
            PacijentPagesServis.PromeniTemu(SvetlaTema, tamnaTema);
        }

        private void Jezik_Click(object sender, RoutedEventArgs e)
        {
            /*var app = (App)Application.Current;
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
