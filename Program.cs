using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZarzadzanieLotniskiem.panele;

namespace projekt
{
    public class ZarzadzanieLotniskiem
    {
        static void Main(string[] args)
        {
            FirmaLotnicza firma = new FirmaLotnicza();
            firma.dodajPosrednika("brak posrednika", "Adres firmy", "NIP firmy"); //dla rezerwacji bezposrednich
            Menu(firma);
/*            firma.dodajSamolot("Boimg", 21, 2100, 1234, 700);
            firma.dodajLotnisko("Warwa", 21, 21);
            firma.dodajLotnisko("Bial", 22, 21);
            firma.dodajLot(firma.lotniska[0].idLotniska, firma.lotniska[0].idLotniska, 2, "12/12/2020", 1234, firma);
            firma.dodajKlienta("Oel", "Olek", "1234222212", "123", "Sokolka 1234");
            firma.dodajPosrednika("RAJANER", "Bialsarf 123", "123444");
            firma.rezerwojBilet(firma.loty[0].numer_lotu, "123", "123444", firma);

            firma.wyswietlLotniska();
            firma.wyswietlKlientow();
            Console.WriteLine("\n");
            Console.WriteLine(firma.bilety[0].jaki_klient("123", firma) ); */


        }

        private static void Menu(FirmaLotnicza firma)
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("MENU:");
                Console.WriteLine("1. Zarzadanie Lotniskami"); // +
                Console.WriteLine("2. Zarzadanie samolotami");// +
                Console.WriteLine("3. Zarzadanie Lotami"); //+
                Console.WriteLine("4. Zarzadanie Posrednikami");// +
                Console.WriteLine("5. Zarzadanie Klientami");// +
                Console.WriteLine("6. Zarzadanie Biletami");
                Console.WriteLine("7. Zapisz stan programu na dysk");// +
                Console.WriteLine("8. Wczytaj stan programu z dysku");// +
                Console.WriteLine("9. Zakoncz program");// +


                int menu = int.Parse(Console.ReadLine());


                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        Panele.panellotniska(firma);
                        break;
                    case 2:
                        Console.Clear();
                        Panele.panelSamoloty(firma);
                        break;
                    case 3:
                        Console.Clear();
                        Panele.panelLoty(firma);
                        break;
                    case 4:
                        Console.Clear();
                        Panele.panelPosrednicy(firma);
                        break;
                    case 5:
                        Console.Clear();
                        Panele.panelKlienci(firma);
                        break;
                    case 6:
                        Console.Clear();
                        Panele.panelBilety(firma);
                        break;
                    case 7:
                        Console.Clear();
                        firma.zapisDoPliku();
                        Console.WriteLine("Zapisano");
                        break;
                    case 8:
                        Console.Clear();
                        firma.odczytListZPliku();
                        Console.WriteLine("Odczytano");
                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("Zamykanie programu...");
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidlowa wartosc z Menu, sprobuj jeszcze raz..");
                        break;
                }
            }
        }
       

        public class FirmaLotnicza
        {
            public List<Samolot> samoloty = new List<Samolot>();
            public List<Lot> loty = new List<Lot>();
            public List<Bilet> bilety = new List<Bilet>();
            public List<Posrednik> posrednicy = new List<Posrednik>();
            public List<Klient> klienci = new List<Klient>();
            public List<Lotnisko> lotniska = new List<Lotnisko>();


            /*Zapis list na dysk*/
            public void zapisDoPliku(string nazwaPliku = "listy.json")
            {
                try
                {
                    var listy = new
                    {
                        samoloty = samoloty,
                        loty = loty,
                        bilety = bilety,
                        posrednicy = posrednicy,
                        klienci = klienci,
                        lotniska = lotniska
                    };

                    string samolotSerializer = JsonConvert.SerializeObject(listy, Formatting.Indented);
                    TextWriter tw = new StreamWriter(nazwaPliku);

                    tw.WriteLine(samolotSerializer);
                    tw.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                /*przypisywanie nazw talbicom w jsonie zeby pozniej sie do nich dostac*/
            }

            public void odczytListZPliku(string nazwaPliku = "listy.json")
            {
                try
                {
                    TextReader tr = new StreamReader(nazwaPliku);
                    string text = tr.ReadToEnd();
                    var obiekt = JObject.Parse(text); //obiekt do wybierania poszczegolnych tablic

                    /*wybieranie poszczegolncyh tablic*/
                    var samoloty_obiekt = obiekt["samoloty"].ToString();
                    var loty_obiekt = obiekt["loty"].ToString();
                    var bilety_obiekt = obiekt["bilety"].ToString();
                    var posrednicy_obiekt = obiekt["posrednicy"].ToString();
                    var klienci_obiekt = obiekt["klienci"].ToString();
                    var lotniska_obiekt = obiekt["lotniska"].ToString();

                    /*zmienianie obiektu jsona w obiekt c#*/
                    samoloty = JsonConvert.DeserializeObject<List<Samolot>>(samoloty_obiekt);
                    loty = JsonConvert.DeserializeObject<List<Lot>>(loty_obiekt);
                    bilety = JsonConvert.DeserializeObject<List<Bilet>>(bilety_obiekt);
                    posrednicy = JsonConvert.DeserializeObject<List<Posrednik>>(posrednicy_obiekt);
                    klienci = JsonConvert.DeserializeObject<List<Klient>>(klienci_obiekt);
                    lotniska = JsonConvert.DeserializeObject<List<Lotnisko>>(lotniska_obiekt);

                    tr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            /*Zarządzanie lotniskami*/

            /*Dla kazdego samolotu tworzy wszystkie mozliwe dla niego loty*/
            public void generujLoty(double cenaZaKilometr)
            {
                foreach (Samolot samolot in samoloty)
                {
                    for (int i = 0; i < lotniska.Count; i++)
                    {
                        for (int j = 0; j < lotniska.Count; j++)
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
            public void PowielLot(int numer_lotu, int ile_lotow, int coIle, FirmaLotnicza firma)
            {
                var lot = firma.loty.Find(x => x.numer_lotu == numer_lotu);
                while (ile_lotow > 0)
                {
                    var nowy_lot = new Lot(lot.z_lotniska, lot.do_lotniska, lot.cenaZaKilometr, lot.dataLotu.AddDays(coIle * ile_lotow), lot.samolot);
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
                if (lotniska.RemoveAll(s => s.nazwaLotniska == nazwa) != 0)
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
            public void usunSamolot(int _numer_seryjny)
            {
                try
                {
                    if(samoloty.RemoveAll(s => s.numer_seryjny == _numer_seryjny) == 1)
                    {
                        Console.WriteLine("Usunieto");
                    }
                    else
                    {
                        Console.WriteLine("Blad podczas usuwania");
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
            public int dodajLot(int z_lotniska,
                    int do_lotniska,
                    double cenaZakilomentr,
                    string dataLotu,
                    int numer_seryjny,
                    FirmaLotnicza firma)
            {
                try
                {
                    var lot = new Lot(z_lotniska, do_lotniska, cenaZakilomentr, dataLotu, numer_seryjny, firma);
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
            public bool biletJestDostepny(int id_lotu)
            {
                var lot = loty.Find(x => x.numer_lotu == id_lotu);
                return lot.samolot.liczba_miejsc > bilety.Count;
            }
            public Bilet rezerwojBilet(int id_lotu, string PESEL, string id_posrednika, FirmaLotnicza firma)
            {
                if (biletJestDostepny(id_lotu))
                {
                    var bilet = new Bilet(id_lotu, PESEL, id_posrednika, firma);
                    bilety.Add(bilet);
                    return bilet;
                }
                else
                {
                    Console.WriteLine("Brak miejsc");
                    return null;
                }
                
            }
            public void usunBilet(int id)
            {
                try
                {
                    bilety.RemoveAll(s => s.id == id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
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
            public void wyswietlPosrednikow()
            {
                foreach (Posrednik posrednik in posrednicy)
                {
                    Console.WriteLine(posrednik);
                }
            }
            /*Zarządzanie Klientami*/
            public int dodajKlienta(string nazwisko, string imie, string numerKarty, string PESEL, string adres)
            {
                try
                {
                    var klient = new Klient(imie, nazwisko, numerKarty, PESEL, adres);
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
                    klienci.RemoveAll(s => s.pesel == PESEL);
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
                this.typ = typ.Trim();
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

            //konstruktor do znaleznia samolotu po jego id
            public Lot(int id_z, int id_do, double cenaZaKilometr, string dataLotu, int idSamolotu, FirmaLotnicza firma)
            {
                this.numer_lotu = this.GetHashCode();
                this.z_lotniska = jakie_lotnisko(id_z, firma);
                this.do_lotniska = jakie_lotnisko(id_do, firma);
                this.cenaZaKilometr = cenaZaKilometr;
                this.dataLotu = Convert.ToDateTime(dataLotu);
                this.odleglosc = policzOdleglosc(jakie_lotnisko(id_z, firma), jakie_lotnisko(id_do, firma));
                this.czas = policzCzaspodrozy(jaki_samolot(idSamolotu, firma));
                this.samolot = jaki_samolot(idSamolotu, firma);
            }
            

            public Lotnisko jakie_lotnisko(int id, FirmaLotnicza firma)
            {
                var lotnisko = firma.lotniska.Find(x => x.idLotniska == id);
                if (lotnisko == null)
                {
                    Console.WriteLine("Takie lotnisko nie istnieje");
                }
                return lotnisko;
            }
            public Samolot jaki_samolot(int numer_seryjny, FirmaLotnicza firma)
            {
                var samolot = firma.samoloty.Find(x => x.numer_seryjny == numer_seryjny);
                if (samolot == null)
                {
                    Console.WriteLine("Taki samolot nie istnieje");
                }
                return samolot;
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
            public int idLotniska;

            public Lotnisko(string nazwaLotniska, double stopnie, double minuty)
            {
                this.nazwaLotniska = nazwaLotniska.Trim();
                this.stopnie = stopnie;
                this.minuty = minuty;
                this.idLotniska = this.GetHashCode();
            }

            public override string ToString()
            {
                return $"{nazwaLotniska}. Polozenie: {stopnie}° {minuty}′. ID: {idLotniska}";
            }
        }

        public class Bilet
        {
            public int id;
            public Posrednik posrednik; //dla klientow indywidualnych przyjmuje wartosc NULL
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
            public Bilet(int id_lotu, string PESEL, string id_posrednika, FirmaLotnicza firma)
            {
                this.id = this.GetHashCode();
                this.lot = jaki_lot(id_lotu, firma);
                this.klient = jaki_klient(PESEL, firma);
                this.posrednik = jaki_posrednik(id_posrednika, firma);
            }
            public Klient jaki_klient(string PESEL, FirmaLotnicza firma)
            {
                var klient = firma.klienci.Find(x => String.Equals(x.pesel, PESEL));
                if (klient == null) Console.WriteLine("Brak takiego klienta");
                return klient;
            }

            public Posrednik jaki_posrednik(string NIP, FirmaLotnicza firma)
            {
                var posrednik = firma.posrednicy.Find(x => x.NIP == NIP);
                if(posrednik == null)
                {
                    Console.WriteLine("Brak takiego posrednika");
                }
                return posrednik;
            }

            public Lot jaki_lot(int id, FirmaLotnicza firma)
            {
                var lot = firma.loty.Find(x => x.numer_lotu == id);
                if (lot == null)
                {
                    Console.WriteLine("Brak takiego lotu");
                }
                return lot;
            }

            public override string ToString()
            {
                return $"Bilet:\n" +
                    $"ID: {id}\n" +
                    $"Imie: {klient.imie}\n" +
                    $"Nazwisko: {klient.nazwisko}\n" +
                    $"Cena: {lot.cena}\n" +
                    $"Data: {lot.dataLotu.Date} \n" +
                    $"Z {lot.z_lotniska} do {lot.do_lotniska}\n";
            }
        }
        public abstract class Kupujacy
        {
            public string adres;

            public Kupujacy() { }
            public Kupujacy(string nazwa, string adres)
            {
                this.adres = adres.Trim();
            }

        }
        public class Posrednik : Kupujacy
        {
            public string NIP;
            public string nazwa;
            public Posrednik(string nazwa, string adres, string NIP)
            {
                this.nazwa = nazwa.Trim();
                this.adres = adres.Trim();
                this.NIP = NIP.Trim();
            }

            public override string ToString()
            {
                return $"{nazwa} {adres} \n NIP: {NIP}";
            }
        }
        public class Klient : Kupujacy
        {
            private string numerKarty;
            public string imie;
            public string nazwisko;
            public string pesel;

            public Klient(string imie, string nazwisko, string numerKarty, string pesel, string adres)
            {
                this.imie = imie.Trim();
                this.nazwisko = nazwisko.Trim();
                this.numerKarty = numerKarty.Trim();
                this.adres = adres.Trim();
                this.pesel = pesel.Trim();
            }

            public override string ToString()
            {
                return $"Dane klienta: {imie} {nazwisko}\n" +
                     $"nr karty: {numerKarty}\n" +
                     $"nr PESEL: { pesel } \n" +
                     $"Adres: { adres } \n";
            }
        }
    }
}