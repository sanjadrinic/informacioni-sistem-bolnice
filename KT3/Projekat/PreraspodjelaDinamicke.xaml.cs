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
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for PreraspodjelaDinamicke.xaml
    /// </summary>
    public partial class PreraspodjelaDinamicke : Window
    {
        public Oprema izabranaOprema;
        public Sala salaDodavanje;
        public ObservableCollection<Sala> sale { get; set; }
        public ObservableCollection<Oprema> dinamicka { get; set; }
        public PreraspodjelaDinamicke(Sala izabranaSala)
        {
            InitializeComponent();
            this.DataContext = this;
            dinamicka = new ObservableCollection<Oprema>();
            sale = new ObservableCollection<Sala>();
            this.salaDodavanje = izabranaSala;
            foreach (Oprema o in OpremaMenadzer.NadjiSvuOpremu())
            {
                if (!o.Staticka)
                {
                    dinamicka.Add(o);
                }
            }
        }

        private void kombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.izabranaOprema = (Oprema)kombo.SelectedItem;
            azurirajSale(izabranaOprema);
        }

        private void azurirajSale(Oprema izabranaOprema)
        {
            sale.Clear();
            foreach (Sala s in SaleMenadzer.sale)
            {
                foreach (Oprema o in s.Oprema)
                {
                    if(o.IdOpreme == izabranaOprema.IdOpreme)
                    {
                        if (s.Id != salaDodavanje.Id)
                        {
                            sale.Add(s);
                        }
                        
                    }
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Sala izabranaSala = (Sala)komboSale.SelectedItem;
            int kolicina = int.Parse(Kolicina.Text);
            int x = 0;
            this.izabranaOprema = (Oprema)kombo.SelectedItem;
            foreach(Sala s in SaleMenadzer.sale)
            {
                if(s.Id == izabranaSala.Id)
                {
                    foreach(Oprema o in s.Oprema)
                    {
                        if(o.IdOpreme == izabranaOprema.IdOpreme)
                        {
                            o.Kolicina -= kolicina;
                            if(o.Kolicina == 0)
                            {
                                s.Oprema.Remove(o);
                                break;
                            }
                        }
                    }
                }
                if(s.Id == salaDodavanje.Id)
                {
                    foreach(Oprema o in s.Oprema)
                    {
                        if (o.IdOpreme == izabranaOprema.IdOpreme)
                        {
                            o.Kolicina += kolicina;
                            x += 1;
                            int idx = PrikazDinamicke.OpremaDinamicka.IndexOf(o);
                            PrikazDinamicke.OpremaDinamicka.RemoveAt(idx);
                            PrikazDinamicke.OpremaDinamicka.Insert(idx, o);
                        }
                        
                        
                    }
                    if(x == 0)
                    {
                        Oprema op = new Oprema(izabranaOprema.NazivOpreme, kolicina, false);
                        op.IdOpreme = izabranaOprema.IdOpreme;
                        PrikazDinamicke.OpremaDinamicka.Add(op);
                        s.Oprema.Add(op);
                    }
                    
                }
            }
            this.Close();
        }

        private void komboSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sala s = (Sala)komboSale.SelectedItem;
            azurirajKolicinu(s);
        }
        private void azurirajKolicinu(Sala s)
        {
            foreach (Sala sal in SaleMenadzer.sale)
            {
                if (s.Id == sal.Id)
                {
                    foreach (Oprema o in sal.Oprema)
                    {
                        if (o.IdOpreme == izabranaOprema.IdOpreme)
                        {
                            this.tekst.Text = "MAX:" + o.Kolicina.ToString();
                        }
                    }
                }
            }
        }
    }
}
