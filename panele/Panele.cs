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
                        Console.WriteLine("Nierpawidlowo cos tma ble");
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
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        firma.wyświetlSamoloty();
                        Console.WriteLine("Podaj numer seryjny samolotu do usuniecia");
                        firma.usunSamolot(Convert.ToInt32(Console.ReadLine()));
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
                        Console.WriteLine("Nierpawidlowo cos tma ble");
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
                Console.WriteLine("3. Wyswietl Loty");
                Console.WriteLine("4. Generuj wszystkie mozliwe loty");
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
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        firma.wyświetlLoty();
                        Console.WriteLine("Podaj numer lotu do usunięcia");
                        firma.usunLot(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 3:
                        Console.Clear();
                        firma.wyświetlLoty();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Podaj cene za kilometr dla tych lotow");
                        firma.generujLoty(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 9:
                        Console.Clear();
                        kontynuuj = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nierpawidlowo cos tma ble");
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
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        firma.wyswietlPosrednikow();
                        Console.WriteLine("Podaj NIP posrednika do usuniecia");
                        firma.UsunPosrednika(Console.ReadLine());
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
                        Console.WriteLine("Nierpawidlowo cos tma ble");
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
                        Console.WriteLine("Podaj nazwisko, imie, numer karty, PESEL oraz adres. (W jednej linii po przecinku)");
                        string[] toSplit = Console.ReadLine().Split(',');
                        if (toSplit.Length == 5)
                        {
                            firma.dodajKlienta(toSplit[0], toSplit[1], toSplit[2], toSplit[3], toSplit[4]);
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidlowy format, sprobuj jeszcze raz");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        firma.wyswietlKlientow();
                        Console.WriteLine("Podaj PESEL klienta do usuniecia");
                        firma.UsunKlienta(Console.ReadLine());
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
                        Console.WriteLine("Nierpawidlowo cos tma ble");
                        break;
                }
            }
        }

    }
}
