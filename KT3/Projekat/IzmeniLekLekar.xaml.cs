﻿using Projekat.Model;
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
    /// <summary>
    /// Interaction logic for IzmeniLekLekar.xaml
    /// </summary>
    public partial class IzmeniLekLekar : Window
    {
        public Lek izabraniLek;
        public bool popunjeno = true;
        public IzmeniLekLekar(Lek izabraniLek)
        {
            InitializeComponent();
            /*this.izabraniLek = izabraniLek;
            this.Potvrdi.IsEnabled = false;
            postaviElementeProzora();*/
        }


        /*private void postaviElementeProzora()
        {
            if (izabraniLek != null)
            {
                this.sifra.Text = izabraniLek.sifraLeka;
                this.naziv.Text = izabraniLek.nazivLeka;
            }
        }*/

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
           /* if (popunjeno)
            {
                string sifraLeka = this.sifra.Text;
                string nazivLeka = this.naziv.Text;
                Lek noviLek = new Lek(izabraniLek.idLeka, nazivLeka, sifraLeka);
                LekoviServis.IzmeniLekoveLekar(izabraniLek, noviLek);
                this.Close();
            }
            else
            {
                MessageBox.Show("Niste popunili sve podatke!");
            }*/
            
        }


        private void sifra_TextChanged(object sender, TextChangedEventArgs e)
        {
            postaviDugme();
        }

        private void naziv_TextChanged(object sender, TextChangedEventArgs e)
        {
            postaviDugme();
        }
        private void postaviDugme()
        {
            /*if (this.sifra.Text.Trim().Equals("") || this.naziv.Text.Trim().Equals("") || postojiSifraLijeka())
            {
                this.Potvrdi.IsEnabled = false;
                popunjeno = false;
            }
            else if (!this.sifra.Text.Trim().Equals("") && !this.naziv.Text.Trim().Equals("") && !postojiSifraLijeka())
            {
                this.Potvrdi.IsEnabled = true;
                popunjeno = true;
            }*/
        }

       /* private bool postojiSifraLijeka()
        {
            foreach (Lek lijek in LekoviServis.Lijekovi())
            {
                if (lijek.sifraLeka == this.sifra.Text && lijek.idLeka != this.izabraniLek.idLeka)
                {
                    return true;
                }
            }
            return false;
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

           /* if (e.Key == Key.X && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Button_Click(sender, e);
            }
            else if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Potvrdi_Click(sender, e);
            }*/
        }
    }
}

