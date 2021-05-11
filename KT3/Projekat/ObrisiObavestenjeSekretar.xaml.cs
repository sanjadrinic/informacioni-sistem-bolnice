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
using Projekat.Model;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for ObrisiObavestenjeSekretar.xaml
    /// </summary>
    public partial class ObrisiObavestenjeSekretar : Window
    {
        Obavestenja obavestenje;
        public ObrisiObavestenjeSekretar(Obavestenja selektovanoObavestenje)
        {
            InitializeComponent();
            obavestenje = selektovanoObavestenje;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ObavestenjaMenadzer.ObrisiObavestenje(obavestenje);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}