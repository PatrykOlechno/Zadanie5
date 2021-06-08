using System;
using System.Collections.Generic;
using System.Text;
using static projekt.ZarzadzanieLotniskiem;

namespace ZarzadzanieLotniskiem.panele
{
    class Panele
    {
        public static void panellotniska(FirmaLotnicza firma)
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("1. Dodaj Lotnisko");
                Console.WriteLine("2. Usun Lotnisko");
                Console.WriteLine("3. Wyswietl Lotniska");
                Console.WriteLine("9. Powrot");

                int switch_on = int.Parse(Console.ReadLine());


                switch (switch_on)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Podaj nazwe, minuty i stopnie. (W jednej linii po przecinku)");
                        string[] toSplit = Console.ReadLine().Split(',');
                        if (toSplit.Length == 3)
                        {
                            firma.dodajLotnisko(toSplit[0], Convert.ToDouble(toSplit[1]), Convert.ToDouble(toSplit[2]));
                            Console.WriteLine("Dodano");
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        firma.wyswietlLotniska();
                        Console.WriteLine("Podaj nazwe lotniska do usuniecia");
                        firma.usunLotnisko(Console.ReadLine());
                        break;
                    case 3:
                        Console.Clear();
                        firma.wyswietlLotniska();
                        break;
                    case 9:
                        Console.Clear();
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nierpawidlowo wartosc");
                        break;
                }
            }
        }
        public static void panelSamoloty(FirmaLotnicza firma)
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("1. Dodaj Samolot");
                Console.WriteLine("2. Usun Samolot");
                Console.WriteLine("3. Wyswietl Samoloty");
                Console.WriteLine("9. Powrot");

                int switch_on = int.Parse(Console.ReadLine());


                switch (switch_on)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Podaj typ, liczbe miejsc, zasieg, numer seryjny oraz predkosc. (W jednej linii po przecinku)");
                        string[] toSplit = Console.ReadLine().Split(',');
                        if (toSplit.Length == 5)
                        {
                            firma.dodajSamolot(toSplit[0], Convert.ToInt32(toSplit[1]), Convert.ToInt32(toSplit[2]), Convert.ToInt32(toSplit[3]), Convert.ToInt32(toSplit[4]));
                            Console.WriteLine("Dodano");
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        try
                        {
                            firma.wyświetlSamoloty();
                            Console.WriteLine("Podaj numer seryjny samolotu do usuniecia");
                            firma.usunSamolot(Convert.ToInt32(Console.ReadLine()));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nie udalo sie usunac.. sprobuj ponownie");
                        }

                        break;
                    case 3:
                        Console.Clear();
                        firma.wyświetlSamoloty();
                        break;
                    case 9:
                        Console.Clear();
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nierpawidlowa wartosc");
                        break;
                }
            }
        }

        public static void panelLoty(FirmaLotnicza firma)
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("1. Dodaj Lot");
                Console.WriteLine("2. Usun Lot");
                Console.WriteLine("3. Powiel lot");
                Console.WriteLine("4. Wyswietl Loty");
                Console.WriteLine("5. Generuj wszystkie mozliwe loty");
                Console.WriteLine("9. Powrot");

                int switch_on = int.Parse(Console.ReadLine());


                switch (switch_on)
                {
                    case 1:
                        Console.Clear();
                        firma.wyświetlSamoloty();
                        firma.wyswietlLotniska();
                        Console.WriteLine("Podaj id lotniska startu, id lotniska lądowania, cene za kilometr, date lotu i numer seryjny samolotu (W jednej linii po przecinku)");
                        string[] toSplit = Console.ReadLine().Split(',');
                        if (toSplit.Length == 5)
                        {
                            firma.dodajLot(Convert.ToInt32(toSplit[0]), Convert.ToInt32(toSplit[1]), Convert.ToInt32(toSplit[2]), toSplit[3], Convert.ToInt32(toSplit[4]), firma);
                            Console.WriteLine("Dodano");
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        try
                        {
                            firma.wyświetlLoty();
                            Console.WriteLine("Podaj numer lotu do usunięcia");
                            firma.usunLot(Convert.ToInt32(Console.ReadLine()));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nie udalo sie usunac");
                        }

                        break;
                    case 3:
                        Console.Clear();
                        try
                        {
                            firma.wyświetlLoty();
                            Console.WriteLine("Podaj numer lotu do powielania, co ile powtarzac oraz ile lotow utowrzyc (w jendnej linii, oddzielone przecinkami)");
                            string[] toSplit2 = Console.ReadLine().Split(',');
                            if (toSplit2.Length == 3)
                            {
                                firma.PowielLot(Convert.ToInt32(toSplit2[0]), Convert.ToInt32(toSplit2[1]), Convert.ToInt32(toSplit2[2]), firma);
                                Console.WriteLine("Powielono");
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidlowy format");
                            }
                            
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nie udalo sie powielic lotu");
                        }
                        break;
                    case 4:
                        Console.Clear();
                        firma.wyświetlLoty();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Podaj cene za kilometr dla tych lotow");
                        try
                        {
                            firma.generujLoty(Convert.ToDouble(Console.ReadLine()));
                            Console.WriteLine("Dodano");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nieprawidlowy format");
                        }
                        
                        break;
                    case 9:
                        Console.Clear();
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nierpawidlowa wartosc");
                        break;
                }
            }
        }

        public static void panelPosrednicy(FirmaLotnicza firma)
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("1. Dodaj Posrednika");
                Console.WriteLine("2. Usun Posrednika");
                Console.WriteLine("3. Wyswietl Posrednikow");
                Console.WriteLine("9. Powrot");

                int switch_on = int.Parse(Console.ReadLine());

                switch (switch_on)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Podaj nazwe, adres i NIP. (W jednej linii po przecinku)");
                        string[] toSplit = Console.ReadLine().Split(',');
                        if (toSplit.Length == 3)
                        {
                            firma.dodajPosrednika(toSplit[0], toSplit[1], toSplit[2]);
                            Console.WriteLine("Dodano");
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        try
                        {
                            firma.wyswietlPosrednikow();
                            Console.WriteLine("Podaj NIP posrednika do usuniecia");
                            firma.UsunPosrednika(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nie udalo sie usnac");
                        }

                        break;
                    case 3:
                        Console.Clear();
                        firma.wyswietlPosrednikow();
                        break;
                    case 9:
                        Console.Clear();
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nierpawidlowa wartosc");
                        break;
                }
            }
        }

        public static void panelKlienci(FirmaLotnicza firma)
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("1. Dodaj Klienta");
                Console.WriteLine("2. Usun Klienta");
                Console.WriteLine("3. Wyswietl Klientow");
                Console.WriteLine("9. Powrot");

                int switch_on = int.Parse(Console.ReadLine());

                switch (switch_on)
                {
                    case 1:
                        Console.Clear();
                        try
                        {
                            Console.WriteLine("Podaj nazwisko, imie, numer karty, PESEL oraz adres. (W jednej linii po przecinku)");
                            string[] toSplit = Console.ReadLine().Split(',');
                            if (toSplit.Length == 5)
                            {
                                firma.dodajKlienta(toSplit[0], toSplit[1], toSplit[2], toSplit[3], toSplit[4]);
                                Console.WriteLine("Dodano");
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nie udalo sie dodac klienta");
                        }

                        break;
                    case 2:
                        Console.Clear();
                        try
                        {
                            firma.wyswietlKlientow();
                            Console.WriteLine("Podaj PESEL klienta do usuniecia");
                            firma.UsunKlienta(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nie udalo sie usunac");
                        }

                        break;
                    case 3:
                        Console.Clear();
                        firma.wyswietlKlientow();
                        break;
                    case 9:
                        Console.Clear();
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nierpawidlowa wartosc");
                        break;
                }
            }
        }

        public static void panelBilety(FirmaLotnicza firma)
        {
            bool kontynuuj = true;
            while (kontynuuj)
            {
                Console.WriteLine("1. Rezerwuj indywidualnie");
                Console.WriteLine("2. Rezerwuj przez posrednika");
                Console.WriteLine("3. Cofnij rezerwacje biletu");
                Console.WriteLine("4. Wyswietl Wszystkie Bilety");
                Console.WriteLine("9. Powrot");

                int switch_on = int.Parse(Console.ReadLine());

                switch (switch_on)
                {
                    case 1:
                        Console.Clear();
                        try
                        {
                            firma.wyświetlLoty();
                            firma.wyswietlKlientow();
                            Console.WriteLine("Podaj id lotu i PESEL klienta(W jednej linii po przecinku)");
                            string[] toSplit = Console.ReadLine().Split(',');
                            if (toSplit.Length == 2)
                            {
                                firma.rezerwojBilet(Convert.ToInt32(toSplit[0]), toSplit[1], "1111", firma);
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nie udalo sie dodac biletu");
                        }
                       
                        break;
                    case 2:
                        Console.Clear();
                        try
                        {
                            firma.wyświetlLoty();
                            firma.wyswietlKlientow();
                            firma.wyswietlPosrednikow();
                            Console.WriteLine("Podaj id lotu, PESEL klienta i NIP posrednika (W jednej linii po przecinku)");
                            string[] toSplit1 = Console.ReadLine().Split(',');
                            if (toSplit1.Length == 3)
                            {
                                firma.rezerwojBilet(Convert.ToInt32(toSplit1[0]), toSplit1[1], toSplit1[2], firma);
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                            }
                            }
                        catch (Exception)
                        {
                            Console.WriteLine("Cos poszlo nie tak. Czy taki obiekt na pewno istnieje? Sprobuj jeszcze raz...");
                        }

                        break;
                    case 3:
                        Console.Clear();
                        firma.wyswietlZarezerwowaneBilety();
                        Console.WriteLine("Podaj id biletu do usuniecia");
                        firma.usunBilet(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 4:
                        Console.Clear();
                        firma.wyswietlZarezerwowaneBilety();
                        break;
                    case 9:
                        Console.Clear();
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidlowa wartosc");
                        break;
                }
            }
        }
    }
}
