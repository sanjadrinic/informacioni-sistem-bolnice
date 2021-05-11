﻿using System;
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
using Model;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for OtkaziTerminSekretar.xaml
    /// </summary>
    public partial class OtkaziTerminSekretar : Window
    {
        Termin terminZaOtkazivanje;
        public OtkaziTerminSekretar(Termin zaBrisanje)
        {
            InitializeComponent();
            terminZaOtkazivanje = zaBrisanje;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TerminMenadzer.OtkaziTerminSekretar(terminZaOtkazivanje);
            TerminMenadzer.sacuvajIzmene();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
