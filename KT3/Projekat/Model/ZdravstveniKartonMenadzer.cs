﻿using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Projekat.Model
{
    class ZdravstveniKartonMenadzer
    {
        public static List<ZdravstveniKarton> kartoni = new List<ZdravstveniKarton>();
        public static List<LekarskiRecept> recepti = new List<LekarskiRecept>();

        public static int GenerisanjeIdRecepta(int idPac)
        {
            bool pomocna = false;
            int id = 1;
            foreach(Pacijent pac in PacijentiMenadzer.pacijenti)
            {
                if(pac.IdPacijenta == idPac)
                {
                    for (id = 1; id <= pac.Karton.LekarskiRecepti.Count; id++)
                    {
                        foreach (LekarskiRecept p in pac.Karton.LekarskiRecepti)
                        {
                            if (p.IdRecepta.Equals(id))
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
                }
            }
            

            return id;
        
        }
        
        public static int GenerisanjeIdAnamneze(int idPac)
        {
            bool pomocna = false;
            int id = 1;
            foreach(Pacijent pac in PacijentiMenadzer.pacijenti)
            {
                if(pac.IdPacijenta == idPac)
                {
                    for (id = 1; id <= pac.Karton.Anamneze.Count; id++)
                    {
                        foreach (Anamneza p in pac.Karton.Anamneze)
                        {
                            if (p.IdAnamneze.Equals(id))
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
                }
            }
            

            return id;
        }public static int GenerisanjeIdAlergena(int idPac)
        {
            bool pomocna = false;
            int id = 1;
            foreach(Pacijent pac in PacijentiMenadzer.pacijenti)
            {
                if(pac.IdPacijenta == idPac)
                {
                    for (id = 1; id <= pac.Karton.Alergeni.Count; id++)
                    {
                        foreach (Alergeni p in pac.Karton.Alergeni)
                        {
                            if (p.IdAlergena.Equals(id))
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
                }
            }
            

            return id;
        }

        public static void DodajRecept(LekarskiRecept recept)
        {
            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if(pacijent.IdPacijenta == recept.idPacijenta)
                {                    
                    pacijent.Karton.LekarskiRecepti.Add(recept);
                    ZdravstveniKartonLekar.PrikazRecepata.Add(recept);

                }
            }
        }
        
        public static void DodajAnamnezu(Anamneza anamneza)  
        {
            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if(pacijent.IdPacijenta == anamneza.IdPacijenta)
                {                    
                    pacijent.Karton.Anamneze.Add(anamneza);
                    ZdravstveniKartonLekar.TabelaAnamneza.Add(anamneza);
                }
            }
        }

        public static void IzmeniAnamnezu(Anamneza stara, Anamneza nova)
        {
            foreach(Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if(pacijent.IdPacijenta == stara.IdPacijenta)
                {
                    foreach(Anamneza a in pacijent.Karton.Anamneze)
                    {
                        if(a.IdAnamneze == stara.IdAnamneze)
                        {
                            a.OpisBolesti = nova.OpisBolesti;
                            a.Terapija = nova.Terapija;
                            a.Datum = nova.Datum;
                        }
                    }
                }
            }


            int idx = ZdravstveniKartonLekar.TabelaAnamneza.IndexOf(stara);
            ZdravstveniKartonLekar.TabelaAnamneza.RemoveAt(idx);
            ZdravstveniKartonLekar.TabelaAnamneza.Insert(idx, nova);
        }

        public static void DodajAlergen(Alergeni alergen)  
        {
            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if (pacijent.IdPacijenta == alergen.IdPacijenta)
                {
                    pacijent.Karton.Alergeni.Add(alergen);
                    ZdravstveniKartonLekar.TabelaAlergena.Add(alergen);
                }
            }
        }

        public static void IzmeniAlergen(Alergeni stariAlergen, Alergeni noviAlergen)
        {
            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if (pacijent.IdPacijenta == stariAlergen.IdPacijenta)
                {
                    foreach (Alergeni a in pacijent.Karton.Alergeni)
                    {
                        if (a.IdAlergena == stariAlergen.IdAlergena)
                        {
                            a.NuspojavaNaNastojak = noviAlergen.NuspojavaNaNastojak;
                            a.VremeReakcije = noviAlergen.VremeReakcije;
                        }
                    }
                }
            }


            int idx = ZdravstveniKartonLekar.TabelaAlergena.IndexOf(stariAlergen);
            ZdravstveniKartonLekar.TabelaAlergena.RemoveAt(idx);
            ZdravstveniKartonLekar.TabelaAlergena.Insert(idx, noviAlergen);
        }

        public static List<Lek> NadjiPacijentuDozvoljeneLekove(int idSelektovanogPacijenta)
        {
            List<Lek> dozvoljeniLekovi = new List<Lek>();


            foreach(Lek lek in LekoviMenadzer.lijekovi)
            {
                dozvoljeniLekovi.Add(lek);
            }

            foreach(Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if(idSelektovanogPacijenta == pacijent.IdPacijenta)
                {
                    
                        foreach (Lek lek in LekoviMenadzer.lijekovi.ToArray())
                        {
                            foreach (Alergeni alergen in pacijent.Karton.Alergeni.ToArray())
                            {

                                /*if (alergen.NazivSastojka.Equals(lek.sifraLeka))
                                {
                                    dozvoljeniLekovi.Remove(lek);
                                }*/

                                 foreach(Sastojak sastojak in lek.sastojci.ToArray())
                                 {
                                        if (sastojak.naziv.Equals(alergen.NazivSastojka))
                                        {
                                             dozvoljeniLekovi.Remove(lek);
                                        }
                                 }

                            }
                        }
                    
                }
            }

            return dozvoljeniLekovi;

        }


        public static int GenerisanjeIdUputa(int idPacijenta)
        {
            int idUputa = 1;

            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if (pacijent.IdPacijenta == idPacijenta)
                {
                    for (idUputa = 1; idUputa <= pacijent.Karton.Uputi.Count; idUputa++)
                    {  
                        if (!PostojiIdUputa(pacijent, idUputa))
                        {
                            return idUputa;
                        }       
                    }
                }
            }

            return idUputa;
        }

        private static bool PostojiIdUputa(Pacijent pacijent, int idUputa)
        {
            bool postojiUput = false;
            foreach (Uput uput in pacijent.Karton.Uputi)
            {
                if (uput.IdUputa.Equals(idUputa))
                {
                    postojiUput = true;
                    break;
                }
            }

            return postojiUput;
        }

        public static void DodajUput(Uput uput)
        {
            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if (pacijent.IdPacijenta == uput.idPacijenta)
                {
                    pacijent.Karton.Uputi.Add(uput);
                    if (uput.TipUputa.Equals(tipUputa.Laboratorija))
                    {
                        pacijent.Karton.brojLaboratorijskihUputa++;                     
                    } 
                    else if (uput.TipUputa.Equals(tipUputa.SpecijallistickiPregled))
                    {
                        pacijent.Karton.brojSpecijalistickihUputa++;
                    } 
                    else if (uput.TipUputa.Equals(tipUputa.StacionarnoLecenje))
                    {
                        pacijent.Karton.brojBolnickihUputa++;
                    }
                    ZdravstveniKartonLekar.TabelaUputa.Add(uput);
                }
            }
        }

        public static List<Uput> PronadjiSveSpecijalistickeUputePacijenta(int idPacijenta)
        {
            List<Uput> specijalistickiUputiPacijenta = new List<Uput>();
            Pacijent pacijent = PacijentiMenadzer.PronadjiPoId(idPacijenta);
            foreach(Uput uput in pacijent.Karton.Uputi)
            {
                // TODO: moze i labr upute i za stacionarno lecenje
                if (uput.TipUputa.Equals(tipUputa.SpecijallistickiPregled))
                {
                    specijalistickiUputiPacijenta.Add(uput);
                }
            }
            return specijalistickiUputiPacijenta;
        }




    }
}
