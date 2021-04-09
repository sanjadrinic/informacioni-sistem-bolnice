﻿using Model;
using Projekat.Model;
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
    /// Interaction logic for DetaljiAnamneze.xaml
    /// </summary>
    public partial class DetaljiAnamneze : Window
    {
        Pacijent pacijent;
        public DetaljiAnamneze(Anamneza izabranaAnamneza)
        {
            InitializeComponent();
            foreach(Pacijent pac in PacijentiMenadzer.pacijenti)
            {
                if(pac.IdPacijenta == izabranaAnamneza.IdPacijenta)
                {
                    this.datum.SelectedDate = DateTime.Parse(izabranaAnamneza.Datum);
                    this.bolest.Text = izabranaAnamneza.OpisBolesti;
                    this.terapija.Text = izabranaAnamneza.Terapija;
                }
            }
    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
