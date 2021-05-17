/***********************************************************************
 * Module:  SaleMenadzer.cs
 * Author:  pc
 * Purpose: Definition of the Class SaleMenadzer
 ***********************************************************************/

using Projekat;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Model
{
    public class SaleMenadzer
    {
        public static void DodajSalu(Sala sala)
        {
            sale.Add(sala);
            PrikaziSalu.Sale.Add(sala);
            sacuvajIzmjene();
        }

        public static void ObrisiSalu(Sala sala)
        {
            sale.Remove(sala);
            PrikaziSalu.Sale.Remove(sala);
            obrisiTermineUSali(sala);
            sacuvajIzmjene();
        }

        private static void obrisiTermineUSali(Sala sala)
        {
            foreach (Termin t in TerminMenadzer.termini.ToArray())
            {
                if (t.Prostorija.Id == sala.Id)
                {
                    TerminMenadzer.termini.Remove(t);
                    TerminMenadzer.sacuvajIzmene();
                }
            }
        }

        public static void IzmjeniSalu(Sala izSale, Sala uSalu)
        {
            foreach (Sala sala in sale)
            {
                if (sala.Id == izSale.Id)
                {
                    sala.brojSale = uSalu.brojSale;
                    sala.Namjena = uSalu.Namjena;
                    sala.TipSale = uSalu.TipSale;
                    int idx = PrikaziSalu.Sale.IndexOf(izSale);
                    PrikaziSalu.Sale.RemoveAt(idx);
                    PrikaziSalu.Sale.Insert(idx, sala);
                }
            }
            sacuvajIzmjene();
        }

        public static List<Sala> NadjiSveSale()
        {
            if (File.ReadAllText("sale.xml").Trim().Equals(""))
            {
                return sale;
            }
            else {
                ucitajSaleIzFajla();
                return sale;
            }
        }

        private static void ucitajSaleIzFajla()
        {
            FileStream filestream = File.OpenRead("sale.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Sala>));
            sale = (List<Sala>)serializer.Deserialize(filestream);
            filestream.Close();
        }

        public static Sala NadjiSaluPoId(int id)
        {
            foreach (Sala sala in sale)
            {
                if (sala.Id == id)
                {
                    return sala;
                }
            }
            return null;
        }
        
        public static Krevet NadjiKrevetPoId(int id, Sala soba)
        {
            foreach (Krevet krevet in soba.Kreveti) //OpremaMenadzer.kreveti
            {
                if (krevet.IdKreveta == id)
                {
                    return krevet;
                }
            }
            return null;
        }

        public static void sacuvajIzmjene()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Sala>));
            TextWriter filestream = new StreamWriter("sale.xml");
            serializer.Serialize(filestream, sale);
            filestream.Close();
        }

        public static int GenerisanjeIdSale()
        {
            int id;
            for (id = 1; id <= sale.Count; id++)
            {
                if (!postojiIdSale(id))
                {
                    return id;
                }
            }
            return id;
        }

        private static bool postojiIdSale(int id)
        {
            foreach (Sala sala in sale)
            {
                if (sala.Id.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        public static ZauzeceSale NadjiZauzece(int idProstorije, int idTermin, string datum, string pocetak, string kraj)
        {
            Sala sala = NadjiSaluPoId(idProstorije);
            foreach (ZauzeceSale zauzece in sala.zauzetiTermini)
            {
                if (idTermin == zauzece.idTermina && datum.Equals(zauzece.datumPocetkaTermina) && pocetak.Equals(zauzece.pocetakTermina) && kraj.Equals(zauzece.krajTermina))
                {
                    return zauzece;
                }
            }
            return null;
        }

        public static void ObrisiZauzeceSale(int IdSale, int IdTermina)
        {
            Sala sala = NadjiSaluPoId(IdSale);
            foreach (ZauzeceSale zauzeceSale in sala.zauzetiTermini)
            {
                if (zauzeceSale.idTermina == IdTermina)
                {
                    sala.zauzetiTermini.Remove(zauzeceSale);
                    Console.WriteLine("Obrisano zauzuce sale: " + sala.Id);
                    return;
                }
            }
        }

        public static List<Sala> PronadjiSaleZaPregled()
        {
            List<Sala> slobodneSaleZaPregled = new List<Sala>();
            foreach (Sala sala in sale)
            {
                if (sala.TipSale.Equals(tipSale.SalaZaPregled) && !sala.Namjena.Equals("Skladiste"))
                {
                    slobodneSaleZaPregled.Add(sala);
                }
            }
            return slobodneSaleZaPregled;
        }

        public static List<Sala> PronadjiSaleZaOperaciju() 
        {
            List<Sala> slobodneSaleZaOperaciju = new List<Sala>();
            foreach (Sala sala in sale)
            {
                if (sala.TipSale.Equals(tipSale.OperacionaSala) && !sala.Namjena.Equals("Skladiste"))
                {
                    slobodneSaleZaOperaciju.Add(sala);
                }
            }
            return slobodneSaleZaOperaciju;
        }

        public static ObservableCollection<string> InicijalizujSveSlotove()
        {
            return new ObservableCollection<string>() { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30",  "10:00", "10:30","11:00", "11:30",
                                                        "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                                        "15:00", "15:30", "16:00", "16:30","17:00", "17:30",
                                                        "18:00", "18:30", "19:00", "19:30", "20:00"}; 
        }

        public static List<Sala> sale = new List<Sala>();
   }
}