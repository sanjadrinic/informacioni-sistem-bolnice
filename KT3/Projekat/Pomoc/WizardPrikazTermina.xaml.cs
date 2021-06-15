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

namespace Projekat.Pomoc
{
    /// <summary>
    /// Interaction logic for WizardPrikazTermina.xaml
    /// </summary>
    public partial class WizardPrikazTermina : Window
    {
        public WizardPrikazTermina()
        {
            InitializeComponent();
        }

        private void Nastavi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            WizardBiranjePacijenta w = new WizardBiranjePacijenta();
            w.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Nastavi_Click(sender, e);
            }
            else if (e.Key == Key.S && Keyboard.IsKeyDown(Key.RightCtrl))
            {
                Nastavi_Click(sender, e);
            }
        }
    }
}
