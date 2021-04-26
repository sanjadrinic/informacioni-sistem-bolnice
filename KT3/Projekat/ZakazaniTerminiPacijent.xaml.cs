﻿using Model;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ZakazaniTerminiPacijent : Page
    {
        private int colNum = 0;
        public static ObservableCollection<Termin> Termini { get; set; }
        public static int idPacijent;
        public static Pacijent prijavljeniPacijent;

        public ZakazaniTerminiPacijent(int idPrijavljenogPacijenta)
        {
            InitializeComponent();
            this.DataContext = this;
            idPacijent = idPrijavljenogPacijenta;
            Termini = new ObservableCollection<Termin>();
            foreach (Termin t in TerminMenadzer.termini)
            {
                if (t.Pacijent.IdPacijenta == idPacijent)
                {
                    Termini.Add(t);
                }
                //Termini.Add(t);
            }
            prijavljeniPacijent = PacijentiMenadzer.PronadjiPoId(idPacijent);

        }

        private void dataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            colNum++;
            if (colNum == 8) // **
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // izmeni
            Termin izabraniTermin = (Termin)dataGridTermini.SelectedItem;
            if (izabraniTermin != null)
            {
                Page izmeniTermin = new IzmeniTermin(izabraniTermin);
                this.NavigationService.Navigate(izmeniTermin);
            }
            else
            {
                MessageBox.Show("Selektujte termin koji zelite da izmenite", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // brisanje
            Termin zaBrisanje = (Termin)dataGridTermini.SelectedItem;
            if (zaBrisanje != null)
            {
                Page otkazivanjeTermina = new OtkaziTermin(zaBrisanje);
                this.NavigationService.Navigate(otkazivanjeTermina);
            }
            else
            {
                MessageBox.Show("Selektujte termin koji zelite da otkazete", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            // TODO: prijava 
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
    }
}