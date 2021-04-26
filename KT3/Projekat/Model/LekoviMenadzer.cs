﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Projekat.Model
{
    class LekoviMenadzer
    {
        public static void DodajLijek(Lek lijek)
        {
            lijekovi.Add(lijek);
            Lijekovi.Lekovi.Add(lijek);
            sacuvajIzmjene();
        }

        public static void dodajZamjenskeLijekove(Lek izabraniLijek, List<Lek> zamjenskiLijekovi)
        {
            foreach(Lek lijek in lijekovi)
            {
                if(lijek.idLeka == izabraniLijek.idLeka)
                {
                    foreach(Lek zamjenski in zamjenskiLijekovi)
                    {
                        lijek.zamenskiLekovi.Add(zamjenski.idLeka);
                        ZamjenskiLijekovi.ZamjenskiLekovi.Add(zamjenski);//doda ih onoliko puta koliko ih ima?
                    }
                }
            }
            sacuvajIzmjene();
        }
        public static void dodajSastojak(Sastojak sastojak, Lek izabraniLijek)
        {
            foreach (Lek lijek in lijekovi)
            {
                if (lijek.idLeka == izabraniLijek.idLeka)
                {
                    lijek.sastojci.Add(sastojak);
                    Sastojci.SastojciLijeka.Add(sastojak);
                }
            }
            sacuvajIzmjene();
        }
        public static void obrisiLijek(Lek lijek)
        {
            lijekovi.Remove(lijek);
            Lijekovi.Lekovi.Remove(lijek);
            sacuvajIzmjene();
        }

        public static void izmjeniLijek(Lek izabraniLijek, Lek izmjenjeniLijek)
        {
            foreach(Lek lijek in lijekovi)
            {
                if(lijek.idLeka == izabraniLijek.idLeka)
                {
                    lijek.sifraLeka = izmjenjeniLijek.sifraLeka;
                    lijek.nazivLeka = izmjenjeniLijek.nazivLeka;
                    lijek.zamenskiLekovi = izmjenjeniLijek.zamenskiLekovi;
                    lijek.sastojci = izmjenjeniLijek.sastojci;
                    int idx = Lijekovi.Lekovi.IndexOf(izabraniLijek);
                    Lijekovi.Lekovi.RemoveAt(idx);
                    Lijekovi.Lekovi.Insert(idx, lijek);
                    if (ZamjenskiLijekovi.ZamjenskiLekovi != null)
                    {
                        int idx1 = ZamjenskiLijekovi.ZamjenskiLekovi.IndexOf(izabraniLijek);
                        ZamjenskiLijekovi.ZamjenskiLekovi.RemoveAt(idx1);
                        ZamjenskiLijekovi.ZamjenskiLekovi.Insert(idx1, lijek);
                    }
                }
            }
            sacuvajIzmjene();
        }
        public static void obrisiSastojakLijeka(Lek izabraniLijek, Sastojak sastojak)
        {
            foreach (Lek lijek in lijekovi)
            {
                if (lijek.idLeka == izabraniLijek.idLeka)
                {
                    lijek.sastojci.Remove(sastojak);
                    Sastojci.SastojciLijeka.Remove(sastojak);
                }
            }
            sacuvajIzmjene();
        }
        public static void izmjeniSastojakLijeka(Lek izabraniLijek, Sastojak stariSastojak, Sastojak noviSastojak)
        {
            foreach(Lek lijek in lijekovi)
            {
                if(lijek.idLeka == izabraniLijek.idLeka)
                {
                    foreach(Sastojak sastojak in lijek.sastojci)
                    {
                        if (sastojak.naziv.Equals(stariSastojak.naziv))
                        {
                            sastojak.naziv = noviSastojak.naziv;
                            sastojak.kolicina = noviSastojak.kolicina;
                            int idx = Sastojci.SastojciLijeka.IndexOf(stariSastojak);
                            Sastojci.SastojciLijeka.RemoveAt(idx);
                            Sastojci.SastojciLijeka.Insert(idx, sastojak);
                            break;
                        }
                    }
                }
            }
            sacuvajIzmjene();
        }

        public static void obrisiZamjenski(Lek izabraniLijek, Lek zamjenskiLijek)
        {
            foreach(Lek lijek in lijekovi)
            {
                if(lijek.idLeka == izabraniLijek.idLeka)
                {
                    lijek.zamenskiLekovi.Remove(zamjenskiLijek.idLeka);
                    ZamjenskiLijekovi.ZamjenskiLekovi.Remove(zamjenskiLijek);
                }
            }
            sacuvajIzmjene();
        }

        public static List<Lek> NadjiSveLijekove()
        {

            if (File.ReadAllText("lijekovi.xml").Trim().Equals(""))
            {
                return lijekovi;
            }
            else
            {
                FileStream filestream = File.OpenRead("lijekovi.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<Lek>));
                lijekovi = (List<Lek>)serializer.Deserialize(filestream);
                filestream.Close();
                return lijekovi;
            }
        }
        public static void sacuvajIzmjene()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Lek>));
            TextWriter filestream = new StreamWriter("lijekovi.xml");
            serializer.Serialize(filestream, lijekovi);
            filestream.Close();
        }

        public static int GenerisanjeIdLijeka()
        {
            bool pomocna = false;
            int id = 1;

            for (id = 1; id <= lijekovi.Count; id++)
            {
                foreach (Lek lijek in lijekovi)
                {
                    if (lijek.idLeka.Equals(id))
                    {
                        pomocna = true;
                        break;
                    }
                }

                if (!pomocna)
                {
                    return id;
                }
                pomocna = false;
            }

            return id;
        }
        public static List<Sastojak> nadjiSastojke(String sifraLeka)
        {
            foreach(Lek lek in lijekovi)
            {
                if (sifraLeka.Equals(lek.sifraLeka))
                {
                    foreach(Sastojak sastojak in lek.sastojci)
                    {
                        sastojci.Add(sastojak);
                    }
                }
            }
            return sastojci;
        }
        public static int GenerisanjeIdZahtjeva()
        {
            bool pomocna = false;
            int id = 1;

            for (id = 1; id <= zahteviZaLekove.Count; id++)
            {
                foreach (ZahtevZaLekove zahtjev in zahteviZaLekove)
                {
                    if (zahtjev.idZahteva.Equals(id))
                    {
                        pomocna = true;
                        break;
                    }
                }

                if (!pomocna)
                {
                    return id;
                }
                pomocna = false;
            }

            return id;
        }
        public static List<Sastojak> sastojci = new List<Sastojak>();
        public static List<Lek> lijekovi = new List<Lek>();
        public static List<ZahtevZaLekove> zahteviZaLekove = new List<ZahtevZaLekove>();

        


    }
}