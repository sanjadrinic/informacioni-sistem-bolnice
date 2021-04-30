﻿using Projekat.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for PrikaziAnketuZaKliniku.xaml
    /// </summary>
    public partial class PrikaziAnketuZaKliniku : Page
    {
        private static int idPacijent;
        private static int idAnkete;
        private static string prvoPitanje = null;
        private static string drugoPitanje = null;
        private static string trecePitanje = null;
        private static string cetvrtoPitanje = null;
        private static string petoPitanje = null;
        public PrikaziAnketuZaKliniku(int idPrijavljenogPacijenta, int idSelektovaneAnkete)
        {
            InitializeComponent();
            this.DataContext = this;
            idPacijent = idPrijavljenogPacijenta;
            idAnkete = idSelektovaneAnkete;
            this.potvrdi.IsEnabled = false;
        }
        public void jedan1_Click(object sender, RoutedEventArgs e)
        {
            // brPitanja = odgovor ; ......
            prvoPitanje = "1=";
            if ((bool)jedan1.IsChecked)
            {
                prvoPitanje += "1";
                MessageBox.Show("Vas odgovor: " + prvoPitanje);
            }
            else if ((bool)dva1.IsChecked)
            {
                prvoPitanje += "2";
                MessageBox.Show("Vas odgovor: " + prvoPitanje);
            }
            else if ((bool)tri1.IsChecked)
            {
                prvoPitanje += "3";
            }
            else if ((bool)cetiri1.IsChecked)
            {
                prvoPitanje += "4";
            }
            else if ((bool)pet1.IsChecked)
            {
                MessageBox.Show("Vas odgovor: 5");
                prvoPitanje += "5";
            }
            odgovorenoNaSvaPitanja();
        }

        private void jedan2_Click(object sender, RoutedEventArgs e)
        {
            drugoPitanje = "2=";
            if ((bool)jedan2.IsChecked)
            {
                drugoPitanje += "1";
                MessageBox.Show("Vas odgovor: " + drugoPitanje);
            }
            else if ((bool)dva2.IsChecked)
            {
                drugoPitanje += "2";
                MessageBox.Show("Vas odgovor: " + drugoPitanje);
            }
            else if ((bool)tri2.IsChecked)
            {
                drugoPitanje += "3";
            }
            else if ((bool)cetiri2.IsChecked)
            {
                drugoPitanje += "4";
            }
            else if ((bool)pet2.IsChecked)
            {
                drugoPitanje += "5";
            }
            odgovorenoNaSvaPitanja();
        }

        private void jedan3_Click(object sender, RoutedEventArgs e)
        {
            trecePitanje = "3=";
            if ((bool)jedan3.IsChecked)
            {
                trecePitanje += "1";
                MessageBox.Show("Vas odgovor: " + trecePitanje);
            }
            else if ((bool)dva3.IsChecked)
            {
                trecePitanje += "2";
                MessageBox.Show("Vas odgovor: " + trecePitanje);
            }
            else if ((bool)tri3.IsChecked)
            {
                trecePitanje += "3";
            }
            else if ((bool)cetiri3.IsChecked)
            {
                trecePitanje += "4";
            }
            else if ((bool)pet3.IsChecked)
            {
                trecePitanje += "5";
            }
            odgovorenoNaSvaPitanja();
        }

        private void jedan4_Click(object sender, RoutedEventArgs e)
        {
            cetvrtoPitanje = "4=";
            if ((bool)jedan4.IsChecked)
            {
                cetvrtoPitanje += "1";
                MessageBox.Show("Vas odgovor: " + cetvrtoPitanje);
            }
            else if ((bool)dva4.IsChecked)
            {
                cetvrtoPitanje += "2";
                MessageBox.Show("Vas odgovor: " + cetvrtoPitanje);
            }
            else if ((bool)tri4.IsChecked)
            {
                cetvrtoPitanje += "3";
            }
            else if ((bool)cetiri4.IsChecked)
            {
                cetvrtoPitanje += "4";
            }
            else if ((bool)pet4.IsChecked)
            {
                cetvrtoPitanje += "5";
            }
            odgovorenoNaSvaPitanja();
        }

        private void jedan5_Click(object sender, RoutedEventArgs e)
        {
            petoPitanje = "5=";
            if ((bool)jedan5.IsChecked)
            {
                petoPitanje += "1";
                MessageBox.Show("Vas odgovor: " + petoPitanje);
            }
            else if ((bool)dva5.IsChecked)
            {
                petoPitanje += "2";
                MessageBox.Show("Vas odgovor: " + petoPitanje);
            }
            else if ((bool)tri5.IsChecked)
            {
                petoPitanje += "3";
            }
            else if ((bool)cetiri5.IsChecked)
            {
                petoPitanje += "4";
            }
            else if ((bool)pet5.IsChecked)
            {
                petoPitanje += "5";
            }
            odgovorenoNaSvaPitanja();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string odgovoriPacijenta = prvoPitanje + ";" + drugoPitanje + ";" + trecePitanje + ";" + cetvrtoPitanje + ";" + petoPitanje;
            Anketa anketa = AnketaMenadzer.NadjiAnketuPoId(idAnkete);
            anketa.odgovori = odgovoriPacijenta;
            anketa.popunjenaAnketa = true;

            Page prikaziAnkete = new PrikaziAnkete(idPacijent);
            this.NavigationService.Navigate(prikaziAnkete);
        }

        private void odgovorenoNaSvaPitanja()
        {
            if (prvoPitanje != null && drugoPitanje != null && trecePitanje != null && cetvrtoPitanje != null && petoPitanje != null)
            {
                this.potvrdi.IsEnabled = true;
            }
        }

    }
}
