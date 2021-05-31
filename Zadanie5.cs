using System;
using System.Collections.Generic;

namespace projekt
{
    public class Zadanie5
    {
        static void Main(string[] args)
        {
            var naszaFirma = new FirmaLotnicza();
            var lotnisko1 = new Lotnisko("Gdansk", 54.366667, 18.633333);
            var lotnisko2 = new Lotnisko("Slupsk", 54.466667, 17.016667);

            naszaFirma.dodajSamolot("Boeing 645", 1, 2000, 2345);

            naszaFirma.dodajLot(1, lotnisko2, lotnisko1, 10, "20 /12/2021", "11:00", naszaFirma.samoloty[0]);
            naszaFirma.dodajLot(1, lotnisko1, lotnisko2, 5, "01/02/2022", "13:00", naszaFirma.samoloty[0]);

            naszaFirma.dodajPosrednika("RAJANER", "Warszawa 1234");
            naszaFirma.dodajPosrednika("Flyfast", "Bialystk Ogrodowa 4");

            naszaFirma.wyswietlPosrednikow();

            /*naszaFirma.wyświetlSamoloty();
            naszaFirma.wyświetlLoty();
            naszaFirma.wyświetlSamoloty();*/
        }

        public class FirmaLotnicza
        {
            public List<Samolot> samoloty = new List<Samolot>();
            public List<Lot> loty = new List<Lot>();
            public List<Bilet> bilety = new List<Bilet>();
            public List<Posrednik> sprzedawcy = new List<Posrednik>();

            /*Zarządzanie samolotami*/
            public int dodajSamolot(string typ, int liczba_miejsc, int zasieg, int numer_seryjny)
            {
                try
                {
                    Samolot nowy_samolot = new Samolot(typ, liczba_miejsc, zasieg, numer_seryjny);
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
            public int dodajLot(int numer_lotu,
                    Lotnisko z_lotniska,
                    Lotnisko do_lotniska,
                    float cena,
                    string data,
                    string godzina,
                    Samolot samolot)
            {
                try
                {
                    var lot = new Lot(numer_lotu, z_lotniska, do_lotniska, cena, data, godzina, samolot);
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
            public Bilet rezerwojBilet(Lot lot, string imie, string nazwisko)
            {
                if (biletJestDostepny(lot))
                {
                    var bilet = new Bilet(imie, nazwisko, lot);
                    bilety.Add(bilet);
                    return bilet;
                }
                else
                {
                    Console.WriteLine("Brak miejsc");
                }
                return null;
            }

            /*Zarządzanie pośrednikami*/
            public int dodajPosrednika(string nazwa, string adres)
            {
                try
                {
                    var posrednik = new Posrednik(nazwa, adres);
                    sprzedawcy.Add(posrednik);
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
                    sprzedawcy.RemoveAll(s => s.nazwa == nazwa);
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

            public void wyswietlPosrednikow()
            {
                foreach (Posrednik posrednik in sprzedawcy)
                {
                    Console.WriteLine(posrednik);
                }
            }
        }
        public class Samolot
        {
            public string typ;
            public int liczba_miejsc;
            public int zasieg;
            public int numer_seryjny;

            public Samolot(string typ, int liczba_miejsc, int zasieg, int numer_seryjny)
            {
                this.typ = typ;
                this.liczba_miejsc = liczba_miejsc;
                this.zasieg = zasieg;
                this.numer_seryjny = numer_seryjny;
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
            public double cena;
            public string data;
            public string godzina;
            public Samolot samolot;

            public Lot(int numer_lotu,
                    Lotnisko z_lotniska,
                    Lotnisko do_lotniska,
                    float cenaZaKilometr,
                    string data,
                    string godzina,
                    Samolot samolot)
            {
                this.numer_lotu = numer_lotu;
                this.z_lotniska = z_lotniska;
                this.do_lotniska = do_lotniska;
                this.cenaZaKilometr = cenaZaKilometr;
                this.data = data;
                this.godzina = godzina;
                this.samolot = samolot;

                policzOdleglosc();
                policzCene();
            }

            public void policzOdleglosc()
            {
                double stopnie1 = z_lotniska.stopnie;
                double stopnie2 = do_lotniska.stopnie;
                double minuty1 = z_lotniska.minuty;
                double minuty2 = do_lotniska.minuty;
                this.odleglosc = Math.Round(Math.Sqrt(Math.Pow(stopnie2 - stopnie1, 2) + Math.Pow((Math.Cos((stopnie1 * Math.PI) / 180) * (minuty2 - minuty1)), 2)) * ((40075.704) / 360));
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
                    $"Odleglosc: {odleglosc}\n" +
                    $"Data: {data}\n" +
                    $"Godzina: {godzina}\n" +
                    $"Cena: {cena}\n" +
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
            public string imie;
            public string nazwisko;
            public Lot lot;

            public Bilet() { }
            public Bilet(string imie, string nazwisko, Lot lot)
            {
                this.id = this.GetHashCode();
                this.imie = imie;
                this.nazwisko = nazwisko;
                this.lot = lot;
            }
            public override string ToString()
            {
                return $"Bilet:\n" +
                    $"ID: {id}\n" +
                    $"Imie: {imie}\n" +
                    $"Nazwisko: {nazwisko}\n" +
                    $"Cena: {lot.cena}\n" +
                    $"Data: {lot.data} {lot.godzina}\n" +
                    $"Z {lot.z_lotniska} do {lot.do_lotniska}\n";
            }
        }
        public abstract class Kupujacy
        {
            public string nazwa;
            public string adres;

            public Kupujacy() { }
            public Kupujacy(string nazwa)
            {
                this.nazwa = nazwa;
            }

        }
        public class Posrednik : Kupujacy
        {

            public Posrednik(string nazwa, string adres)
            {
                this.nazwa = nazwa;
                this.adres = adres;
            }

            public override string ToString()
            {
                return $"{nazwa} \n {adres}";
            }
        }

    }
}