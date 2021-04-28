using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Model;
using Projekat.Model;
using System.Threading;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace Projekat
{
    public partial class PrikaziTermin : Page
    {
        public static Pacijent prijavljeniPacijent;
        public static int idPacijent;
        public static bool pacijentProzor;
        private int colNum = 0;
        public static ObservableCollection<Termin> Termini { get; set; }
        public static ObservableCollection<Obavestenja> ObavestenjaPacijent { get; set; }
        public Thread thread;
        private static int minBrojTerminaZaAnketu = 2;
        public PrikaziTermin(int idPrijavljeniPacijent)
        {
            InitializeComponent();
            this.DataContext = this;
            Termini = new ObservableCollection<Termin>();
            ObavestenjaPacijent = new ObservableCollection<Obavestenja>();
            idPacijent = idPrijavljeniPacijent;
            pacijentProzor = true;
            thread = new Thread(izvrsiNit);
            thread.Start();
            foreach (Termin t in TerminMenadzer.termini)
            {
                if (t.Pacijent.IdPacijenta == idPacijent)
                {
                    Termini.Add(t);
                }
            }
            prijavljeniPacijent = PacijentiMenadzer.PronadjiPoId(idPacijent);
            ProveriDostupnostAnketeZaKlinku();
            DodajObavestenja();
        }

        private static void DodajObavestenja()
        {
            foreach (Obavestenja o in ObavestenjaMenadzer.obavestenja)
            {
                if (o.ListaIdPacijenata.Contains(idPacijent))
                {
                    if (o.TipObavestenja.Equals("Terapija"))
                    {
                        DodajObavestenjaZaTerapije(o);
                    }
                }
                
                if (!o.TipObavestenja.Equals("Terapija"))
                {
                    // filtirana obavestenja za specificne pacijente	
                    if (o.ListaIdPacijenata.Contains(prijavljeniPacijent.IdPacijenta) /*|| o.IdPacijenta == prijavljeniPacijent.IdPacijenta*/ || o.Oznaka.Equals("pacijenti") || o.Oznaka.Equals("svi"))
                    {
                        ObavestenjaPacijent.Add(o);
                    }
                }
            }
        }

        private static void DodajObavestenjaZaTerapije(Obavestenja o)
        {
            DateTime dt = DateTime.Parse(o.Datum);
            if (dt.Date <= DateTime.Now.Date)
            {
                if (dt.TimeOfDay <= DateTime.Now.TimeOfDay)
                {
                    ObavestenjaPacijent.Add(o);
                }
            }
        }

        public void izvrsiNit()
        {
            while (pacijentProzor == true)
            {
                Thread.Sleep(1000);  //30000
                ProveriRecepte();
            }
        }

        private static void ProveriRecepte()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                foreach (LekarskiRecept recept in prijavljeniPacijent.Karton.LekarskiRecepti)
                {
                    foreach (DateTime datum in recept.UzimanjeTerapije)
                    {
                        //DateTime sadasnjeVreme = DateTime.Parse(DateTime.Now.Date.ToString("HH:mm:00"));
                        if (datum.Date == DateTime.Now.Date)
                        {
                            bool postojiObavestenje = proveriObavestenja(recept.NazivLeka, datum);
                            string trenutnoVreme = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
                            string vremeZaTerapiju = datum.TimeOfDay.ToString().Substring(0, 5);
                            if (vremeZaTerapiju.Equals(trenutnoVreme))
                            {
                                MessageBox.Show("Ovde");
                                if (!postojiObavestenje)
                                {
                                    ObavestenjaPacijent.Add(new Obavestenja(datum.ToString("MM/dd/yyyy"), "Terapija", "Uzmite terapiju: " + recept.NazivLeka));
                                    // TODO: prosiriti konsturktor
                                    int id = ObavestenjaMenadzer.GenerisanjeIdObavestenja();
                                    Obavestenja.Add(new Obavestenja(id, d.ToString("MM/dd/yyyy HH:mm"), "Terapija", "Uzmite terapiju: " + lp.NazivLeka, true));
                                    Console.Beep();
                                    ObavestenjaMenadzer.obavestenja.Add(new Obavestenja(datum.ToString("MM/dd/yyyy"), "Terapija", "Uzmite terapiju: " + recept.NazivLeka)); 
                                }
                            }
                        }
                    }
                }
            });
        }
        private static bool proveriObavestenja(string nazivLeka, DateTime datumUzimanjaTerapije) 
        {
            foreach(Obavestenja o in ObavestenjaPacijent)
            {
                if (o.TipObavestenja.Equals("Terapija"))
                {
                    string sadrzaj = "Uzmite terapiju: " + nazivLeka;
                    string datum = datumUzimanjaTerapije.ToString();
                    if (!ObavestenjaPacijent.Any(x => x.SadrzajObavestenja == sadrzaj) && !ObavestenjaPacijent.Any(y => y.Datum == datum))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            colNum++;
            if (colNum == 8) // **
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void dataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void obavestenja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void anketa_Click(object sender, RoutedEventArgs e)
        {
            Page prikaziAnkete = new PrikaziAnkete(idPacijent);
            this.NavigationService.Navigate(prikaziAnkete);
        }

        private void ProveriDostupnostAnketeZaKlinku()
        {
            int brojacProslihTermina = 0;
            foreach (Termin termin in TerminMenadzer.termini)
            {
                if (termin.Pacijent.IdPacijenta == idPacijent)
                {
                    DateTime danasnjiDatum = DateTime.Now.Date;
                    if (DateTime.Parse(termin.Datum) <= danasnjiDatum)
                    {
                        brojacProslihTermina++;
                        if (brojacProslihTermina >= minBrojTerminaZaAnketu)
                        {
                            this.anketa.IsEnabled = true;
                        }
                        else
                        {
                            this.anketa.IsEnabled = false;
                        }
                    }
                }
            }
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
    }
}
