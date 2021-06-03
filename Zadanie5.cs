using System;
using System.Collections.Generic;

namespace projekt
{
    public class Zadanie5
    {
        static void Main(string[] args)
        {
            var firma = new FirmaLotnicza();
            firma.dodajLotnisko("Ragsdale Road Airport", -85.95359802246094, 35.515899658203125);
            firma.dodajLotnisko("San Jacinto Methodist Hospital Heliport", -94.980201, 29.7377);
            firma.dodajLotnisko("Harnsd", 21, 29.7377);
            firma.usunLotnisko("Ragsdale Road Airprt");
            firma.dodajSamolot("BOJING", 1, 13000, 123455234, 100);
            firma.dodajSamolot("BOCIAN", 16, 700000, 3425345, 100);
            firma.generujLoty(2);
            firma.PowielLot(firma.loty[0], 2, 7);

            firma.wyświetlLoty();
        }

        public class FirmaLotnicza
        {
            public List<Samolot> samoloty = new List<Samolot>();
            public List<Lot> loty = new List<Lot>();
            public List<Bilet> bilety = new List<Bilet>();
            public List<Posrednik> posrednicy = new List<Posrednik>();
            public List<Klient> klienci = new List<Klient>();
            public List<Lotnisko> lotniska = new List<Lotnisko>();

            /*Zarządzanie lotniskami*/

            /*Dla kazdego samolotu tworzy wszystkie mozliwe dla niego loty*/
            public void generujLoty(float cenaZaKilometr)
            {
                foreach (Samolot samolot in samoloty)
                {
                for (int i = 0; i < lotniska.Count; i++)
                {
                    for(int j = 0; j < lotniska.Count; j++)
                    {
                            var lot = new Lot(lotniska[i], lotniska[j], samolot);
                            if (lot.policzOdleglosc(lotniska[i], lotniska[j]) < samolot.zasieg && lot.policzOdleglosc(lotniska[i], lotniska[j]) > 0) //sprawdza czy samolot sie nadaje
                            {
                                var lot1 = new Lot(lotniska[i], lotniska[j], cenaZaKilometr, DateTime.Now.AddDays(1), samolot);
                                loty.Add(lot1);
                            }
                    }
                }
                }
            }
            /* Tworzy loty dany okres czasu*/
            public void PowielLot(Lot lot, int ile_lotow, int coIle)
            {
                while(ile_lotow > 0)
                {
                    var nowy_lot = new Lot(lot.z_lotniska, lot.do_lotniska, lot.cenaZaKilometr, lot.dataLotu.AddDays(coIle*ile_lotow), lot.samolot);
                    loty.Add(nowy_lot);
                    ile_lotow--;
                }

            }

            public int dodajLotnisko(string nazwa, double stopnie, double minuty)
            {
                try
                {
                    Lotnisko lotnisko = new Lotnisko(nazwa, stopnie, minuty);
                    lotniska.Add(lotnisko); 
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
            public int usunLotnisko(string nazwa)
            {
                if ( lotniska.RemoveAll(s => s.nazwaLotniska == nazwa) != 0)
                {
                    Console.WriteLine("Pomyslnie usunieto");
                }
                else
                {
                    Console.WriteLine("Blad podczas usuwania");
                }
                return lotniska.RemoveAll(s => s.nazwaLotniska == nazwa);
            }
            public void wyswietlLotniska()
            {
                foreach (Lotnisko lotnisko in lotniska)
                {
                    Console.WriteLine(lotnisko);
                }
            }
            /*Zarządzanie samolotami*/
            public int dodajSamolot(string typ, int liczba_miejsc, int zasieg, int numer_seryjny, int predkosc)
            {
                try
                {
                    Samolot nowy_samolot = new Samolot(typ, liczba_miejsc, zasieg, numer_seryjny, predkosc);
                    samoloty.Add(nowy_samolot);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
            public int usunSamolot(int _numer_seryjny)
            {
                try
                {
                    samoloty.RemoveAll(s => s.numer_seryjny == _numer_seryjny);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }

            }
            public void wyświetlSamoloty()
            {
                foreach (Samolot samolot in samoloty)
                {
                    Console.WriteLine(samolot);
                }
            }
            /*Zarządzanie lotami*/
            public int dodajLot(Lotnisko z_lotniska,
                    Lotnisko do_lotniska,
                    float cena,
                    double czas,
                    DateTime dataLotu,
                    Samolot samolot)
            {
                try
                {
                    var lot = new Lot(z_lotniska, do_lotniska, cena, dataLotu, samolot);
                    loty.Add(lot);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

            public int usunLot(int _numer_lotu)
            {
                try
                {
                    loty.RemoveAll(s => s.numer_lotu == _numer_lotu);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
            public void wyświetlLoty()
            {
                foreach (Lot lot in loty)
                {
                    Console.WriteLine(lot);
                }
            }
            /*Zarządzanie kupującymi */
            public bool biletJestDostepny(Lot lot)
            {
                return lot.samolot.liczba_miejsc > bilety.Count;
            }
            public Bilet rezerwojBilet(Lot lot, Klient klient)
            {
                if (biletJestDostepny(lot))
                {
                    var bilet = new Bilet(klient, lot);
                    bilety.Add(bilet);
                    return bilet;
                }
                else
                {
                    Console.WriteLine("Brak miejsc");
                }
                return null;
            }
            public void wyswietlZarezerwowaneBilety()
            {
                foreach (Bilet bilet in bilety)
                {
                    Console.WriteLine(bilet);
                }
            }
            /*Zarządzanie pośrednikami*/
            public int dodajPosrednika(string nazwa, string adres, string NIP)
            {
                try
                {
                    var posrednik = new Posrednik(nazwa, adres, NIP);
                    posrednicy.Add(posrednik);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

            public int UsunPosrednika(string nazwa)
            {
                try
                {
                    posrednicy.RemoveAll(s => s.nazwa == nazwa);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
            public Bilet rezerwojBilet(Lot lot, Posrednik posrednik, Klient klient)
            {
                if (biletJestDostepny(lot))
                {
                    var bilet = new Bilet(posrednik, klient, lot);
                    bilety.Add(bilet);
                    return bilet;
                }
                else
                {
                    Console.WriteLine("Brak miejsc");
                }
                return null;
            }
            public void wyswietlPosrednikow()
            {
                foreach (Posrednik posrednik in posrednicy)
                {
                    Console.WriteLine(posrednik);
                }
            }
            /*Zarządzanie Klientami*/
            public int dodajKlienta(string nazwisko,string imie, string numerKarty, string PESEL)
            {
                try
                {
                    var klient = new Klient(imie, nazwisko, numerKarty, PESEL);
                    klienci.Add(klient);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

            public int UsunKlienta(string PESEL)
            {
                try
                {
                    klienci.RemoveAll(s => s.Pesel == PESEL);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

            public void wyswietlKlientow()
            {
                foreach (Klient klient in klienci)
                {
                    Console.WriteLine(klient);
                }
            }
        }
        public class Samolot
        {
            public string typ;
            public int liczba_miejsc;
            public int zasieg;
            public int numer_seryjny;
            public int predkosc;

            public Samolot(string typ, int liczba_miejsc, int zasieg, int numer_seryjny, int predkosc)
            {
                this.typ = typ;
                this.liczba_miejsc = liczba_miejsc;
                this.zasieg = zasieg;
                this.numer_seryjny = numer_seryjny;
                this.predkosc = predkosc;
            }

            public override string ToString()
            {
                return $"Typ: {typ}\n Liczba miejsc: {liczba_miejsc}\n Zasieg: {zasieg}\n Numer seryjny: {numer_seryjny}\n\n";
            }
        }

        public class Lot
        {
            public int numer_lotu;
            public Lotnisko z_lotniska;
            public Lotnisko do_lotniska;
            public double odleglosc;
            public double cenaZaKilometr;
            public double czas;
            public double cena;
            public DateTime dataLotu;
            public Samolot samolot;

            public Lot(Lotnisko z_lotniska,
                    Lotnisko do_lotniska,
                    double cenaZaKilometr,
                    DateTime dataLotu,
                    Samolot samolot)
            {
                this.numer_lotu = this.GetHashCode();
                this.z_lotniska = z_lotniska;
                this.do_lotniska = do_lotniska;
                this.cenaZaKilometr = cenaZaKilometr;
                this.dataLotu = dataLotu;
                this.odleglosc = policzOdleglosc(z_lotniska, do_lotniska);
                this.czas = policzCzaspodrozy(samolot);
                this.samolot = samolot;
                
                policzCene();
            }
            public Lot(Lotnisko z_lotniska, Lotnisko do_lotniska, Samolot samolot)
            {
                this.z_lotniska = z_lotniska;
                this.do_lotniska = do_lotniska;
                this.odleglosc = policzOdleglosc(z_lotniska, do_lotniska);
                this.czas = policzCzaspodrozy(samolot);
                this.samolot = samolot;
            }
            public Lot() { }
            public double policzOdleglosc(Lotnisko z_lotniska, Lotnisko do_lotniska)
            {
                double stopnie1 = z_lotniska.stopnie;
                double stopnie2 = do_lotniska.stopnie;
                double minuty1 = z_lotniska.minuty;
                double minuty2 = do_lotniska.minuty;
                return Math.Round(Math.Sqrt(Math.Pow(stopnie2 - stopnie1, 2) + Math.Pow((Math.Cos((stopnie1 * Math.PI) / 180) * (minuty2 - minuty1)), 2)) * ((40075.704) / 360));
            }
            public double policzCzaspodrozy()
            {
                return odleglosc / samolot.predkosc;
            }
            public double policzCzaspodrozy(Samolot samolot)
            {
                return odleglosc / samolot.predkosc;
            }
            public void policzCene()
            {
                this.cena = this.cenaZaKilometr * this.odleglosc;
            }

            public override string ToString()
            {
                return $"Informacja o locie: \n" +
                    $"ID: {numer_lotu}\n" +
                    $"Z: {z_lotniska}\n" +
                    $"DO: {do_lotniska}\n" +
                    $"Odleglosc: {odleglosc} km\n" +
                    $"Data: {dataLotu.Date }\n" +
                    $"Czas: { Math.Round(czas, 2) } h\n" +
                    $"Cena: {cena} zl\n" +
                    $"Samolot: { samolot }\n";
            }
        }

        public class Lotnisko
        {
            public string nazwaLotniska;
            public double stopnie;
            public double minuty; //polozenie geograficzne

            public Lotnisko(string nazwaLotniska, double stopnie, double minuty)
            {
                this.nazwaLotniska = nazwaLotniska;
                this.stopnie = stopnie;
                this.minuty = minuty;
            }

            public override string ToString()
            {
                return $"{nazwaLotniska}. Polozenie: {stopnie}° {minuty}′";
            }
        }

        public class Bilet
        {
            public int id;
            public Posrednik posrednik;
            public Klient klient;
            public Lot lot;

            public Bilet() { }
            /* Konstruktor dla klienta indywidualnego*/
            public Bilet(Klient klient, Lot lot)
            {
                this.id = this.GetHashCode();
                this.klient = klient;
                this.lot = lot;
            }
            /* Konstruktor dla posrednika*/
            public Bilet(Posrednik posrednik, Klient klient, Lot lot)
            {
                this.posrednik = posrednik;
                this.klient = klient;
                this.lot = lot;
            }
            public override string ToString()
            {
                return $"Bilet:\n" +
                    $"ID: {id}\n" +
                    $"Imie: {klient.imie}\n" +
                    $"Nazwisko: {klient.nazwisko}\n" +
                    $"Cena: {lot.cena}\n" +
                    $"Data: {lot.dataLotu.Date} {lot.dataLotu.TimeOfDay}\n" +
                    $"Z {lot.z_lotniska} do {lot.do_lotniska}\n";
            }
        }
        public abstract class Kupujacy
        {
            public string nazwa;
            public string adres;

            public Kupujacy() { }
            public Kupujacy(string nazwa, string adres)
            {
                this.nazwa = nazwa;
                this.adres = adres;
            }

        }
        public class Posrednik : Kupujacy
        {
            public string NIP;
            public Posrednik(string nazwa, string adres, string NIP)
            {
                this.nazwa = nazwa;
                this.adres = adres;
                this.NIP = NIP;
            }

            public override string ToString()
            {
                return $"{nazwa} {adres} \n {NIP}";
            }
        }
        public class Klient : Kupujacy
        {
            private string numerKarty;
            public string imie;
            public string nazwisko;
            private string pesel;
            
            public Klient(string imie, string nazwisko, string numerKarty, string pesel)
            {
                this.imie = imie;
                this.nazwisko = nazwisko;
                this.numerKarty = numerKarty;
                this.Pesel = pesel;
            }
            public string Pesel { get { return pesel;  }
                set { if (value.Length == 11)
                    {
                        pesel = value;
                    }
                    else
                    {
                        pesel = "INCORRECT VALUE";
                    }
                }
            }

            public override string ToString()
            {
                return $"Dane klienta: {imie} {nazwisko}\n" +
                     $"nr karty: {numerKarty}\n" +
                     $"nr PESEL: { Pesel } "; 
            }
        }
    }
}