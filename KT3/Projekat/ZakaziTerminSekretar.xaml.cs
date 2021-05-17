﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Projekat.Model;
using Projekat.Servis;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for ZakaziTerminSekretar.xaml
    /// </summary>
    public partial class ZakaziTerminSekretar : Window
    {
        public List<Pacijent> AzuriranaLista = new List<Pacijent>();
        public Pacijent Pacijent;
        public Lekar Lekar;
        public Sala Sala;
        public Termin t;
        public Uput uputZaPregled;
        public string dat;
        public string PocetnoVreme;
        int izbaceniSlotoviMinuti;
        public static ObservableCollection<string> SlobodnoVremePocetka { get; set; }
        public static ObservableCollection<string> SlobodnoVremeKraja { get; set; }
        public static ObservableCollection<string> pomocnaSviSlobodniTermini { get; set; }
        public static ObservableCollection<string> pomocnaSviSlobodniTerminiKraj { get; set; }
        public static ObservableCollection<string> pomocna { get; set; }

        public ZakaziTerminSekretar()
        {
            InitializeComponent();
            datum.BlackoutDates.AddDatesInPast();

            this.listaPacijenata.ItemsSource = PacijentiMenadzer.pacijenti;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listaPacijenata.ItemsSource);
            view.Filter = UserFilterPacijenti;

            this.listaLekara.ItemsSource = LekariMenadzer.lekari;
            CollectionView viewLekari = (CollectionView)CollectionViewSource.GetDefaultView(listaLekara.ItemsSource);
            viewLekari.Filter = UserFilterLekari;

            SlobodnoVremePocetka = new ObservableCollection<string>() {"07:00", "07:30", "08:00", "08:30", "09:00", "09:30",  "10:00", "10:30",
                                                               "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                                               "15:00", "15:30", "16:00", "16:30","17:00", "17:30", "18:00", "18:30",
                                                               "19:00", "19:30", "20:00"};

            SlobodnoVremeKraja = new ObservableCollection<string>() {"07:30", "08:00", "08:30", "09:00", "09:30",  "10:00", "10:30",
                                                               "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                                               "15:00", "15:30", "16:00", "16:30","17:00", "17:30", "18:00", "18:30",
                                                               "19:00", "19:30", "20:00"};

            pomocna = new ObservableCollection<string>() {"07:00", "07:30", "08:00", "08:30", "09:00", "09:30",  "10:00", "10:30",
                                                               "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                                               "15:00", "15:30", "16:00", "16:30","17:00", "17:30", "18:00", "18:30",
                                                               "19:00", "19:30", "20:00"};

            // TODO: izemni
            pomocnaSviSlobodniTermini = new ObservableCollection<string>() { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30",  "10:00", "10:30",
                                                               "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                                               "15:00", "15:30", "16:00", "16:30","17:00", "17:30", "18:00", "18:30",
                                                               "19:00", "19:30", "20:00"};
        
            pomocnaSviSlobodniTerminiKraj = new ObservableCollection<string>() { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30",  "10:00", "10:30",
                                                               "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                                               "15:00", "15:30", "16:00", "16:30","17:00", "17:30", "18:00", "18:30",
                                                               "19:00", "19:30", "20:00", "20:30"};
            
        }

        private bool UserFilterPacijenti(object item)
        {
            if (String.IsNullOrEmpty(pretraga.Text))
            {
                return true;
            }
            else
            {
                return ((item as Pacijent).ImePacijenta.IndexOf(pretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as Pacijent).PrezimePacijenta.IndexOf(pretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                       || ((item as Pacijent).Jmbg.ToString().IndexOf(pretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private bool UserFilterLekari(object item)
        {
            if (String.IsNullOrEmpty(pretraga.Text))
            {
                return true;
            }
            else
            {
                return ((item as Lekar).ImeLek.IndexOf(pretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as Lekar).PrezimeLek.IndexOf(pretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                       || ((item as Lekar).specijalizacija.ToString().IndexOf(pretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        // potvrdi
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // vreme pocetka i kraja
            string vp = vremePocetka.Text;
            string vk = vremeKraja.Text;

            string[] pocetak = vp.Split(':');
            string pocetakSati = pocetak[0];
            string pocetakMinuti = pocetak[1];

            string[] kraj = vk.Split(':');
            string krajSati = kraj[0];
            string krajMinuti = kraj[1];


            // datum
            DateTime? selectedDate = datum.SelectedDate;
            if (selectedDate.HasValue)
            {
                dat = selectedDate.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            // tip termina
            TipTermina tp;
            if (tip.Text.Equals("Pregled"))
            {
                tp = TipTermina.Pregled;
            }
            else
            {
                tp = TipTermina.Operacija;
            }


            Lekar l = LekariServis.NadjiPoId(Lekar.IdLekara);
            Pacijent pacijent = PacijentiMenadzer.PronadjiPoId(Pacijent.IdPacijenta);
            Sala = SaleServis.NadjiSaluPoId((int)prostorije.SelectedItem);
            t = new Termin(TerminMenadzer.GenerisanjeIdTermina(), dat, vp, vk, tp, l, Sala, pacijent);

            // TODO: premesti u TerminMenadzer
            if (Sala.zauzetiTermini.Count != 0)  // ako postoje zauzeti termini
            {
                TerminMenadzer.ZakaziTerminSekretar(t);
                ZauzeceSale z = new ZauzeceSale(vp, vk, dat, t.IdTermin);
                Sala.zauzetiTermini.Add(z);

                // za svaki termin koji je zakazan u istoj prostoriji s, dodati to novo zauzece u zauzeca te prostorije
                foreach (Termin t1 in TerminMenadzer.termini)
                {
                    if (t1.Prostorija.Id == Sala.Id)
                    {
                        t1.Prostorija = Sala;
                    }
                }

                TerminMenadzer.sacuvajIzmene();
                SaleServis.sacuvajIzmjene();

                this.Close();
            }
            else  // ako ne postoje zauzeti termini
            {
                TerminMenadzer.ZakaziTerminSekretar(t);
                ZauzeceSale z = new ZauzeceSale(vp, vk, dat, t.IdTermin);
                Sala.zauzetiTermini.Add(z);

                TerminMenadzer.sacuvajIzmene();
                SaleServis.sacuvajIzmjene();

                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // kreiranje guest naloga
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DodajPacijentaGuest dodavanje = new DodajPacijentaGuest(this);  // prosledjujemo u DodajPacijentaGuest konstruktor klase ZakaziTerminSekretar
            dodavanje.Show();
        }

        // azuriranje liste pacijenata prilikom dodavanja guest pacijenta
        public void AzurirajListuPacijenata()
        {
            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                AzuriranaLista.Add(pacijent);
            }

            listaPacijenata.ItemsSource = AzuriranaLista;
            int duzina = AzuriranaLista.Count;
            listaPacijenata.SelectedIndex = duzina - 1;
        }

        // pacijenti list view
        private void Pretraga_Pacijenata(object sender, RoutedEventArgs e)
        {
            listaPacijenata.Visibility = Visibility.Visible;
            listaLekara.Visibility = Visibility.Hidden;
        }

        // lekari list view
        private void Pretraga_Lekara(object sender, RoutedEventArgs e)
        {
            listaLekara.Visibility = Visibility.Visible;
            listaPacijenata.Visibility = Visibility.Hidden;
        }

        private void pretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listaPacijenata.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(listaLekara.ItemsSource).Refresh();
        }

        private void listaPacijenata_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (listaPacijenata.SelectedItem != null)
            {
                Pacijent = (Pacijent)listaPacijenata.SelectedItem;
                pacijenti.Text = Pacijent.ImePacijenta + " " + Pacijent.PrezimePacijenta;
            }
        }

        private void listaLekara_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (listaLekara.SelectedItem != null)
            {
                Lekar = (Lekar)listaLekara.SelectedItem;
                lekari.Text = Lekar.ImeLek + " " + Lekar.PrezimeLek;
            }
        }

        private void IzbaciDanasnjeProsleTermine()
        {
            if (datum.SelectedDate == DateTime.Now.Date)
            {
                foreach (string slot in SlobodnoVremePocetka.ToList())
                {
                    DateTime vreme = DateTime.Parse(slot);
                    DateTime sada = DateTime.Now;
                    if (vreme.TimeOfDay <= sada.TimeOfDay)
                    {
                        SlobodnoVremePocetka.Remove(slot);
                    }
                }
            }
        }

        private void RenovacijaSale(ZauzeceSale z)
        {
            if (z.idTermina == 0) // ako sala ima zakazano renoviranje
            {
                DateTime datumPocetka = DateTime.Parse(z.datumPocetkaTermina);
                DateTime datumKraja = DateTime.Parse(z.datumKrajaTermina);
                DateTime datumZakazivanja = DateTime.Parse(dat);

                if (z.datumPocetkaTermina.Equals(dat) && z.datumKrajaTermina.Equals(dat)) // isti dan i pocinje i zavrsava se
                {
                    int trajanjeSati = Convert.ToInt32(z.krajTermina.Split(':')[0]) - Convert.ToInt32(z.pocetakTermina.Split(':')[0]);
                    int trajanjeMinuti = Convert.ToInt32(z.krajTermina.Split(':')[1]) - Convert.ToInt32(z.pocetakTermina.Split(':')[1]);

                    if (trajanjeMinuti == 0)
                    {
                        izbaceniSlotoviMinuti = 0;
                    }
                    else if (trajanjeMinuti == 30)
                    {
                        izbaceniSlotoviMinuti = 1;
                    }
                    else if (trajanjeMinuti == -30)
                    {
                        izbaceniSlotoviMinuti = -1;
                    }

                    int izbaceniSlotovi = trajanjeSati * 2 + izbaceniSlotoviMinuti;
                    int index = SlobodnoVremePocetka.IndexOf(z.pocetakTermina);

                    if (index != -1)
                    {
                        for (int i = 0; i < izbaceniSlotovi; i++)
                        {
                            SlobodnoVremePocetka.RemoveAt(index);
                            SlobodnoVremeKraja.RemoveAt(index);
                        }
                    }
                }
                else if (z.datumPocetkaTermina.Equals(dat) && !z.datumKrajaTermina.Equals(dat))
                {
                    int trajanjeSati = Convert.ToInt32("20") - Convert.ToInt32(z.pocetakTermina.Split(':')[0]);
                    int trajanjeMinuti = Convert.ToInt32("00") - Convert.ToInt32(z.pocetakTermina.Split(':')[1]);

                    if (trajanjeMinuti == 0)
                    {
                        izbaceniSlotoviMinuti = 0;
                    }
                    else if (trajanjeMinuti == 30)
                    {
                        izbaceniSlotoviMinuti = 1;
                    }
                    else if (trajanjeMinuti == -30)
                    {
                        izbaceniSlotoviMinuti = -1;
                    }

                    int izbaceniSlotovi = trajanjeSati * 2 + izbaceniSlotoviMinuti;
                    int index = SlobodnoVremePocetka.IndexOf(z.pocetakTermina);

                    if (index != -1)
                    {
                        for (int i = 0; i <= izbaceniSlotovi; i++)
                        {
                            SlobodnoVremePocetka.RemoveAt(index);

                            if (i != 0) // ne brisemo vreme pocetka renovacije iz ove liste jer termin moze da traje do vremena pocetka renovacije
                            {
                                if (SlobodnoVremeKraja.Count > 0)
                                {
                                    SlobodnoVremeKraja.RemoveAt(index + 1);
                                }
                            }
                        }
                    }
                }
                else if (!z.datumPocetkaTermina.Equals(dat) && z.datumKrajaTermina.Equals(dat))
                {
                    // od pocetka dana do kraja renovacije sve izbacujemo
                    int trajanjeSati = Convert.ToInt32(z.krajTermina.Split(':')[0]) - Convert.ToInt32("07");
                    int trajanjeMinuti = Convert.ToInt32(z.krajTermina.Split(':')[1]) - Convert.ToInt32("00");

                    if (trajanjeMinuti == 0)
                    {
                        izbaceniSlotoviMinuti = 0;
                    }
                    else if (trajanjeMinuti == 30)
                    {
                        izbaceniSlotoviMinuti = 1;
                    }
                    else if (trajanjeMinuti == -30)
                    {
                        izbaceniSlotoviMinuti = -1;
                    }

                    int izbaceniSlotovi = trajanjeSati * 2 + izbaceniSlotoviMinuti;

                    for (int i = 0; i <= izbaceniSlotovi; i++)
                    {
                        if (i != izbaceniSlotovi)
                        {
                            if (SlobodnoVremePocetka.Count > 0)
                            {
                                SlobodnoVremePocetka.RemoveAt(0);
                            }
                        }
                        if (SlobodnoVremeKraja.Count > 0)
                        {
                            SlobodnoVremeKraja.RemoveAt(0);
                        }
                    }
                }
                else if (datumZakazivanja < datumKraja && datumZakazivanja > datumPocetka) // ako je trenutni datum izmedju vremena pocetka i vremena kraja renovacije
                {
                    SlobodnoVremePocetka.Clear();
                    SlobodnoVremeKraja.Clear();

                    MessageBox.Show("Prostorija se renovira u to vreme!");
                }
            }
        }

        private void SlobodanTerminSale()
        {
            foreach (ZauzeceSale z in Sala.zauzetiTermini)
            {
                RenovacijaSale(z);

                // slobodni termini u selektovanoj sali
                if (TerminMenadzer.NadjiTerminPoId(z.idTermina) != null)  // za zauzece nadjemo koji je to termin
                {
                    if (z.datumPocetkaTermina.Equals(dat))
                    {
                        int trajanjeSati = Convert.ToInt32(z.krajTermina.Split(':')[0]) - Convert.ToInt32(z.pocetakTermina.Split(':')[0]);  // 0 1 2 ... koliko slotova izbacujemo
                        int trajanjeMinuti = Convert.ToInt32(z.krajTermina.Split(':')[1]) - Convert.ToInt32(z.pocetakTermina.Split(':')[1]);  // 0 30 -30

                        if (trajanjeMinuti == 0)
                        {
                            izbaceniSlotoviMinuti = 0;
                        }
                        else if (trajanjeMinuti == 30)
                        {
                            izbaceniSlotoviMinuti = 1;
                        }
                        else if (trajanjeMinuti == -30)
                        {
                            izbaceniSlotoviMinuti = -1;
                        }

                        int izbaceniSlotovi = trajanjeSati * 2 + izbaceniSlotoviMinuti;
                        int index = SlobodnoVremePocetka.IndexOf(z.pocetakTermina);

                        if (index != -1)
                        {
                            for (int i = 0; i < izbaceniSlotovi; i++)
                            {
                                SlobodnoVremePocetka.RemoveAt(index);
                                SlobodnoVremeKraja.RemoveAt(index);
                            }
                        }
                    }
                }
            }
        }

        // slobodni termini lekara
        private void SlobodanTerminLekara()
        {
            foreach (Sala s in SaleMenadzer.sale)
            {
                foreach (ZauzeceSale z in s.zauzetiTermini)
                {
                    if (z.datumPocetkaTermina.Equals(dat)) // za selektovani datum gledamo zauzetost selektovanog lekara
                    {
                        if (TerminMenadzer.NadjiTerminPoId(z.idTermina) != null)
                        {
                            Termin pomocna = TerminMenadzer.NadjiTerminPoId(z.idTermina);

                            if (pomocna.Lekar.IdLekara == Lekar.IdLekara)
                            {
                                int trajanjeSati = Convert.ToInt32(z.krajTermina.Split(':')[0]) - Convert.ToInt32(z.pocetakTermina.Split(':')[0]);  // 0 1 2 ... koliko slotova izbacujemo
                                int trajanjeMinuti = Convert.ToInt32(z.krajTermina.Split(':')[1]) - Convert.ToInt32(z.pocetakTermina.Split(':')[1]);  // 0 30 -30

                                if (trajanjeMinuti == 0)
                                {
                                    izbaceniSlotoviMinuti = 0;
                                }
                                else if (trajanjeMinuti == 30)
                                {
                                    izbaceniSlotoviMinuti = 1;
                                }
                                else if (trajanjeMinuti == -30)
                                {
                                    izbaceniSlotoviMinuti = -1;
                                }

                                int izbaceniSlotovi = trajanjeSati * 2 + izbaceniSlotoviMinuti;
                                int index = SlobodnoVremePocetka.IndexOf(z.pocetakTermina);

                                if (index != -1)
                                {
                                    for (int i = 0; i < izbaceniSlotovi; i++)
                                    {
                                        SlobodnoVremePocetka.RemoveAt(index);
                                        SlobodnoVremeKraja.RemoveAt(index);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // slobodni termini pacijenta - pacijent ne moze biti na istim mestima u isto vreme
        private void SlobodanTerminPacijenta()
        {
            foreach (Sala s in SaleMenadzer.sale)
            {
                foreach (ZauzeceSale z in s.zauzetiTermini)
                {
                    if (z.datumPocetkaTermina.Equals(dat)) // za selektovani datum gledamo zauzetost selektovanog lekara
                    {
                        if (TerminMenadzer.NadjiTerminPoId(z.idTermina) != null)
                        {
                            Termin pomocna = TerminMenadzer.NadjiTerminPoId(z.idTermina);

                            if (pomocna.Pacijent.IdPacijenta == Pacijent.IdPacijenta)
                            {
                                int trajanjeSati = Convert.ToInt32(z.krajTermina.Split(':')[0]) - Convert.ToInt32(z.pocetakTermina.Split(':')[0]);  // 0 1 2 ... koliko slotova izbacujemo
                                int trajanjeMinuti = Convert.ToInt32(z.krajTermina.Split(':')[1]) - Convert.ToInt32(z.pocetakTermina.Split(':')[1]);  // 0 30 -30

                                if (trajanjeMinuti == 0)
                                {
                                    izbaceniSlotoviMinuti = 0;
                                }
                                else if (trajanjeMinuti == 30)
                                {
                                    izbaceniSlotoviMinuti = 1;
                                }
                                else if (trajanjeMinuti == -30)
                                {
                                    izbaceniSlotoviMinuti = -1;
                                }

                                int izbaceniSlotovi = trajanjeSati * 2 + izbaceniSlotoviMinuti;
                                int index = SlobodnoVremePocetka.IndexOf(z.pocetakTermina);

                                if (index != -1)
                                {
                                    for (int i = 0; i < izbaceniSlotovi; i++)
                                    {
                                        SlobodnoVremePocetka.RemoveAt(index);
                                        SlobodnoVremeKraja.RemoveAt(index);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void IzbaciZauzeteTermine()
        {
            if (prostorije.SelectedItem != null)
            {
                Sala = SaleServis.NadjiSaluPoId((int)prostorije.SelectedItem);

                SlobodanTerminSale();
                SlobodanTerminLekara();
                SlobodanTerminPacijenta();
            }
            else
            {
                MessageBox.Show("Unesite prostoriju.");
            }
        }

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            IzbaciDanasnjeProsleTermine();

            DateTime? selectedDate = datum.SelectedDate;
            if (selectedDate.HasValue)
            {
                dat = selectedDate.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            vremePocetka.ItemsSource = SlobodnoVremePocetka;
            vremeKraja.ItemsSource = SlobodnoVremeKraja;  // dodato ovde

            IzbaciZauzeteTermine();
        }

        private void tip_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)  // tip_SelectionChanged
        {
            prostorije.Items.Clear();

            if (tip.SelectedIndex == 0 || tip.SelectedIndex == 1) // pregled
            {
                foreach (Sala s in SaleMenadzer.sale)
                {
                    if (s.TipSale.Equals(tipSale.SalaZaPregled) && !s.Namjena.Equals("Skladiste"))
                    {
                        if (!prostorije.Items.Contains(s.Id))
                        {
                            prostorije.Items.Add(s.Id);
                        }
                    }
                }
            }
            else if (tip.SelectedIndex == 2) // operacija
            {
                foreach (Sala s in SaleMenadzer.sale)
                {
                    if (s.TipSale.Equals(tipSale.OperacionaSala) && !s.Namjena.Equals("Skladiste"))
                    {
                        if (!prostorije.Items.Contains(s.Id))
                        {
                            prostorije.Items.Add(s.Id);
                        }
                    }
                }
            }

            prostorije.SelectedIndex = 0;
        }

        private void vremeKraja_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            nadjiDozvoljeneTermineKraja();
        }

        private void vremePocetka_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PocetnoVreme = vremePocetka.Text;
        }

        private void nadjiDozvoljeneTermineKraja()
        {
            string[] pocetak = PocetnoVreme.Split(':');
            string pocetakSati = pocetak[0];
            string pocetakMinuti = pocetak[1];
            /*Izbaci neodgovarajuce termine za kraj*/
            foreach (string s in pomocnaSviSlobodniTerminiKraj.ToList())
            {
                string[] kraj = s.Split(':');
                string krajTerminaSati = kraj[0];
                string krajTerminaMinuti = kraj[1];

                if (Convert.ToInt32(pocetakSati) >= Convert.ToInt32(krajTerminaSati) && Convert.ToInt32(pocetakMinuti) == 30)
                {
                    SlobodnoVremeKraja.Remove(s);
                }
                else if (Convert.ToInt32(pocetakSati) > Convert.ToInt32(krajTerminaSati))
                {
                    SlobodnoVremeKraja.Remove(s);
                }
            }

            izbaciKrajTermina();

            SlobodnoVremeKraja.Remove(vremePocetka.Text);
            vremeKraja.ItemsSource = SlobodnoVremeKraja;

            //DORADITI-----------------------------------------------------------------------------------------------------------------------------------------------------------

            foreach (string s in pomocnaSviSlobodniTerminiKraj)
            {

                foreach (ZauzeceSale zauzece in Sala.zauzetiTermini)
                {
                    string[] pocetakZauzeca = zauzece.pocetakTermina.Split(':');
                    string pocetakSatiZauzeca = pocetakZauzeca[0];
                    string pocetakMinutiZauzeca = pocetakZauzeca[1];

                    string[] krajZauzeca = zauzece.krajTermina.Split(':');
                    string krajSatiZauzeca = krajZauzeca[0];
                    string krajMinutiZauzeca = krajZauzeca[1];


                    string[] kraj = s.Split(':');
                    string krajTerminaSati = kraj[0];
                    string krajTerminaMinuti = kraj[1];

                    if (Convert.ToInt32(pocetakSatiZauzeca) > Convert.ToInt32(pocetakSati) && dat == zauzece.datumPocetkaTermina) //dodati i ako je isto
                    {
                        if (!SlobodnoVremeKraja.Contains(s) && ((Convert.ToInt32(pocetakSati) <= Convert.ToInt32(krajSatiZauzeca))))
                        {
                            SlobodnoVremeKraja.Add(s);
                            SlobodnoVremeKraja.Remove(vremePocetka.Text);

                        }
                    }
                    /*else if (Convert.ToInt32(pocetakSatiZauzeca) == Convert.ToInt32(pocetakSati) && dat == zauzece.datumPocetkaTermina && Convert.ToInt32(pocetakMinutiZauzeca) > Convert.ToInt32(pocetakMinuti)) //dodati i ako je isto
                    {
                        if (!sviSlobodniTerminiKraj.Contains(s) && ((Convert.ToInt32(pocetakSati) <= Convert.ToInt32(krajSatiZauzeca))))
                        {
                            sviSlobodniTerminiKraj.Add(s);
                            sviSlobodniTerminiKraj.Remove(vpp.Text);

                        }
                    }*/
                    else if (!SlobodnoVremeKraja.Contains(s) && ((Convert.ToInt32(pocetakSati) < Convert.ToInt32(krajTerminaSati))))
                    {
                        SlobodnoVremeKraja.Add(s);
                        SlobodnoVremeKraja.Remove(vremePocetka.Text);

                    }
                    else if (!SlobodnoVremeKraja.Contains(s) && ((Convert.ToInt32(pocetakSati) == Convert.ToInt32(krajTerminaSati)) && ((Convert.ToInt32(pocetakMinuti) < Convert.ToInt32(krajTerminaMinuti)))))
                    {
                        SlobodnoVremeKraja.Add(s);
                        SlobodnoVremeKraja.Remove(vremePocetka.Text);

                    }
                }

            }
            SlobodnoVremeKraja.Add(pocetakSati + ":" + "30");
            foreach (string s in pomocnaSviSlobodniTerminiKraj.ToList())
            {
                string[] kraj = s.Split(':');
                string krajTerminaSati = kraj[0];
                string krajTerminaMinuti = kraj[1];
                if (SlobodnoVremeKraja.Contains(s) && Convert.ToInt32(pocetakSati) >= Convert.ToInt32(krajTerminaSati)) //provjeri kad je isti sat a razliciti minuti
                {
                    SlobodnoVremeKraja.Remove(s);
                }
            }


            izbaciKrajTermina();
        }

        private void izbaciKrajTermina()
        {
            string[] pocetak = PocetnoVreme.Split(':');
            string pocetakSati = pocetak[0];
            string pocetakMinuti = pocetak[1];

            foreach (ZauzeceSale zauzece in Sala.zauzetiTermini)
            {
                string[] pocetakZauzeca = zauzece.pocetakTermina.Split(':');
                string pocetakSatiZauzeca = pocetakZauzeca[0];
                string pocetakMinutiZauzeca = pocetakZauzeca[1];

                //sprecava preklapanje termina (da kraj jednog termina bude posle/u toku drugog)
                if (Convert.ToInt32(pocetakSatiZauzeca) > Convert.ToInt32(pocetakSati) && dat == zauzece.datumPocetkaTermina) //dodati i ako je isto
                {
                    foreach (string s in pomocnaSviSlobodniTerminiKraj.ToList())
                    {
                        string[] pocetakKraja = s.Split(':');
                        string pocetakSatiKraja = pocetakKraja[0];
                        string pocetakMinutiKraja = pocetakKraja[1];

                        if (Convert.ToInt32(pocetakSatiKraja) > Convert.ToInt32(pocetakSatiZauzeca) && SlobodnoVremeKraja.Contains(s))
                        {
                            SlobodnoVremeKraja.Remove(s);
                        }
                        else if (Convert.ToInt32(pocetakSatiKraja) == Convert.ToInt32(pocetakSatiZauzeca) && Convert.ToInt32(pocetakMinutiKraja) > Convert.ToInt32(pocetakMinutiZauzeca) && SlobodnoVremeKraja.Contains(s))
                        {
                            SlobodnoVremeKraja.Remove(s);
                        }
                    }

                }
                //ako je termin npr. u 17:30 a izaberemo vreme pocetka u 17:00 -> izbacuje sve termine posle 17:30
                else if (Convert.ToInt32(pocetakSatiZauzeca) == Convert.ToInt32(pocetakSati) && dat == zauzece.datumPocetkaTermina && Convert.ToInt32(pocetakMinutiZauzeca) > Convert.ToInt32(pocetakMinuti)) //dodati i ako je isto
                {
                    foreach (string s in pomocnaSviSlobodniTerminiKraj.ToList())
                    {
                        string[] pocetakKraja = s.Split(':');
                        string pocetakSatiKraja = pocetakKraja[0];
                        string pocetakMinutiKraja = pocetakKraja[1];

                        if (Convert.ToInt32(pocetakSatiKraja) > Convert.ToInt32(pocetakSatiZauzeca) && SlobodnoVremeKraja.Contains(s))
                        {
                            SlobodnoVremeKraja.Remove(s);
                        }
                        else if (Convert.ToInt32(pocetakSatiKraja) == Convert.ToInt32(pocetakSatiZauzeca) && Convert.ToInt32(pocetakMinutiKraja) < Convert.ToInt32(pocetakMinutiZauzeca) && SlobodnoVremeKraja.Contains(s))
                        {
                            SlobodnoVremeKraja.Remove(s);
                        }
                    }

                }
            }
        }
        
        private void vremePocetka_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private void vremePocetka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PocetnoVreme = vremePocetka.Text;
        }

        private void Uputi_Click(object sender, RoutedEventArgs e)
        {
            if (pacijenti.Text.Equals(""))
            {
                MessageBox.Show("Izaberite pacijenta!");
            }
            else
            {
                PrikazeUputeSekretar uputi = new PrikazeUputeSekretar(Pacijent, this);
                uputi.Show();
            }
        }

    }
}
