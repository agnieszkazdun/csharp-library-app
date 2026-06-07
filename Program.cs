using System;
using System.Collections.Generic;

abstract class MaterialBiblioteczny
{
    public string Tytul { get; set; }

    public MaterialBiblioteczny(string tytul)
    {
        Tytul = tytul;
    }

    public abstract void WyswietlInfo();
    public abstract int MaksCzasWypozyczenia();
}

class Ksiazka : MaterialBiblioteczny
{
    public string Autor { get; set; }
    public int Rok { get; set; }

    public Ksiazka(string tytul, string autor, int rok) : base(tytul)
    {
        Autor = autor;
        Rok = rok;
    }

    public override void WyswietlInfo()
    {
        Console.WriteLine($"Książka: {Tytul}, autor: {Autor}, rok: {Rok}");
    }

    public override int MaksCzasWypozyczenia()
    {
        return 30;
    }
}

class Czasopismo : MaterialBiblioteczny
{
    public int Numer { get; set; }

    public Czasopismo(string tytul, int numer) : base(tytul)
    {
        Numer = numer;
    }

    public override void WyswietlInfo()
    {
        Console.WriteLine($"Czasopismo: {Tytul}, numer: {Numer}");
    }

    public override int MaksCzasWypozyczenia()
    {
        return 7;
    }
}

class Ebook : MaterialBiblioteczny
{
    public string Format { get; set; }

    public Ebook(string tytul, string format) : base(tytul)
    {
        Format = format;
    }

    public override void WyswietlInfo()
    {
        Console.WriteLine($"Ebook: {Tytul}, format: {Format}");
    }

    public override int MaksCzasWypozyczenia()
    {
        return 14;
    }
}

abstract class Uzytkownik
{
    public string Imie { get; set; }
    public List<MaterialBiblioteczny> Wypozyczone { get; set; }

    public Uzytkownik(string imie)
    {
        Imie = imie;
        Wypozyczone = new List<MaterialBiblioteczny>();
    }

    public abstract int LimitWypozyczen();
    public abstract bool CzyMoznaWypozyczyc(MaterialBiblioteczny material);

    public virtual void WyswietlInfo()
    {
        Console.WriteLine($"{Imie}, limit wypożyczeń: {LimitWypozyczen()}, aktualnie wypożyczone: {Wypozyczone.Count}");
    }
}

class Student : Uzytkownik
{
    public Student(string imie) : base(imie) { }

    public override int LimitWypozyczen()
    {
        return 3;
    }

    public override bool CzyMoznaWypozyczyc(MaterialBiblioteczny material)
    {
        return Wypozyczone.Count < LimitWypozyczen();
    }
}

class Nauczyciel : Uzytkownik
{
    public Nauczyciel(string imie) : base(imie) { }

    public override int LimitWypozyczen()
    {
        return 5;
    }

    public override bool CzyMoznaWypozyczyc(MaterialBiblioteczny material)
    {
        return Wypozyczone.Count < LimitWypozyczen();
    }
}

class Gosc : Uzytkownik
{
    public Gosc(string imie) : base(imie) { }

    public override int LimitWypozyczen()
    {
        return 1;
    }

    public override bool CzyMoznaWypozyczyc(MaterialBiblioteczny material)
    {
        return Wypozyczone.Count < LimitWypozyczen();
    }
}

class Program
{
    static void Main()
    {
        List<MaterialBiblioteczny> materialy = new List<MaterialBiblioteczny>();
        List<Uzytkownik> uzytkownicy = new List<Uzytkownik>();

        bool dziala = true;

        while (dziala)
        {
            Console.Clear();
            Console.WriteLine("=== MINI WYPOŻYCZALNIA ===");
            Console.WriteLine("1. Dodaj materiał");
            Console.WriteLine("2. Wyświetl materiały");
            Console.WriteLine("3. Dodaj użytkownika");
            Console.WriteLine("4. Wyświetl użytkowników");
            Console.WriteLine("5. Wypożycz materiał");
            Console.WriteLine("6. Wyświetl wypożyczenia użytkownika");
            Console.WriteLine("7. Usuń materiał");
            Console.WriteLine("8. Wyjdź");
            Console.Write("Wybierz opcję: ");

            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    DodajMaterial(materialy);
                    break;
                case "2":
                    WyswietlMaterialy(materialy);
                    break;
                case "3":
                    DodajUzytkownika(uzytkownicy);
                    break;
                case "4":
                    WyswietlUzytkownikow(uzytkownicy);
                    break;
                case "5":
                    WypozyczMaterial(uzytkownicy, materialy);
                    break;
                case "6":
                    WyswietlWypozyczenia(uzytkownicy);
                    break;
                case "7":
                    UsunMaterial(materialy);
                    break;
                case "8":
                    dziala = false;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    Pauza();
                    break;
            }
        }
    }

    static void DodajMaterial(List<MaterialBiblioteczny> materialy)
    {
        Console.Clear();
        Console.WriteLine("=== DODAWANIE MATERIAŁU ===");
        Console.WriteLine("1. Książka");
        Console.WriteLine("2. Czasopismo");
        Console.WriteLine("3. Ebook");
        Console.Write("Wybierz typ: ");

        string typ = Console.ReadLine();

        if (typ == "1")
        {
            Console.Write("Podaj tytuł: ");
            string tytul = Console.ReadLine();

            Console.Write("Podaj autora: ");
            string autor = Console.ReadLine();

            int rok;
            while (true)
            {
                Console.Write("Podaj rok: ");
                if (int.TryParse(Console.ReadLine(), out rok))
                    break;

                Console.WriteLine("Rok musi być liczbą.");
            }

            materialy.Add(new Ksiazka(tytul, autor, rok));
            Console.WriteLine("Dodano książkę.");
        }
        else if (typ == "2")
        {
            Console.Write("Podaj tytuł czasopisma: ");
            string tytul = Console.ReadLine();

            int numer;
            while (true)
            {
                Console.Write("Podaj numer: ");
                if (int.TryParse(Console.ReadLine(), out numer))
                    break;

                Console.WriteLine("Numer musi być liczbą.");
            }

            materialy.Add(new Czasopismo(tytul, numer));
            Console.WriteLine("Dodano czasopismo.");
        }
        else if (typ == "3")
        {
            Console.Write("Podaj tytuł ebooka: ");
            string tytul = Console.ReadLine();

            Console.Write("Podaj format, np. PDF/EPUB: ");
            string format = Console.ReadLine();

            materialy.Add(new Ebook(tytul, format));
            Console.WriteLine("Dodano ebook.");
        }
        else
        {
            Console.WriteLine("Nieprawidłowy typ.");
        }

        Pauza();
    }

    static void WyswietlMaterialy(List<MaterialBiblioteczny> materialy)
    {
        Console.Clear();
        Console.WriteLine("=== MATERIAŁY ===");

        if (materialy.Count == 0)
        {
            Console.WriteLine("Brak materiałów.");
        }
        else
        {
            for (int i = 0; i < materialy.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                materialy[i].WyswietlInfo();
                Console.WriteLine($"   Maksymalny czas wypożyczenia: {materialy[i].MaksCzasWypozyczenia()} dni");
            }
        }

        Pauza();
    }

    static void DodajUzytkownika(List<Uzytkownik> uzytkownicy)
    {
        Console.Clear();
        Console.WriteLine("=== DODAWANIE UŻYTKOWNIKA ===");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Nauczyciel");
        Console.WriteLine("3. Gość");
        Console.Write("Wybierz typ użytkownika: ");

        string typ = Console.ReadLine();

        Console.Write("Podaj imię: ");
        string imie = Console.ReadLine();

        if (typ == "1")
        {
            uzytkownicy.Add(new Student(imie));
            Console.WriteLine("Dodano studenta.");
        }
        else if (typ == "2")
        {
            uzytkownicy.Add(new Nauczyciel(imie));
            Console.WriteLine("Dodano nauczyciela.");
        }
        else if (typ == "3")
        {
            uzytkownicy.Add(new Gosc(imie));
            Console.WriteLine("Dodano gościa.");
        }
        else
        {
            Console.WriteLine("Nieprawidłowy typ użytkownika.");
        }

        Pauza();
    }

    static void WyswietlUzytkownikow(List<Uzytkownik> uzytkownicy)
    {
        Console.Clear();
        Console.WriteLine("=== UŻYTKOWNICY ===");

        if (uzytkownicy.Count == 0)
        {
            Console.WriteLine("Brak użytkowników.");
        }
        else
        {
            for (int i = 0; i < uzytkownicy.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                uzytkownicy[i].WyswietlInfo();
            }
        }

        Pauza();
    }

    static void WypozyczMaterial(List<Uzytkownik> uzytkownicy, List<MaterialBiblioteczny> materialy)
    {
        Console.Clear();
        Console.WriteLine("=== WYPOŻYCZANIE ===");

        if (uzytkownicy.Count == 0)
        {
            Console.WriteLine("Najpierw dodaj użytkownika.");
            Pauza();
            return;
        }

        if (materialy.Count == 0)
        {
            Console.WriteLine("Najpierw dodaj materiał.");
            Pauza();
            return;
        }

        Console.WriteLine("Wybierz użytkownika:");
        for (int i = 0; i < uzytkownicy.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            uzytkownicy[i].WyswietlInfo();
        }

        Console.Write("Numer użytkownika: ");
        if (!int.TryParse(Console.ReadLine(), out int nrUzytkownika) ||
            nrUzytkownika < 1 || nrUzytkownika > uzytkownicy.Count)
        {
            Console.WriteLine("Nieprawidłowy numer użytkownika.");
            Pauza();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Wybierz materiał:");
        for (int i = 0; i < materialy.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            materialy[i].WyswietlInfo();
        }

        Console.Write("Numer materiału: ");
        if (!int.TryParse(Console.ReadLine(), out int nrMaterialu) ||
            nrMaterialu < 1 || nrMaterialu > materialy.Count)
        {
            Console.WriteLine("Nieprawidłowy numer materiału.");
            Pauza();
            return;
        }

        Uzytkownik uzytkownik = uzytkownicy[nrUzytkownika - 1];
        MaterialBiblioteczny material = materialy[nrMaterialu - 1];

        if (!uzytkownik.CzyMoznaWypozyczyc(material))
        {
            Console.WriteLine("Brak możliwości wypożyczenia. Użytkownik osiągnął limit.");
            Pauza();
            return;
        }

        uzytkownik.Wypozyczone.Add(material);
        Console.WriteLine("Wypożyczono materiał.");
        Console.WriteLine($"Czas wypożyczenia: {material.MaksCzasWypozyczenia()} dni");

        Pauza();
    }

    static void WyswietlWypozyczenia(List<Uzytkownik> uzytkownicy)
    {
        Console.Clear();
        Console.WriteLine("=== WYPOŻYCZENIA UŻYTKOWNIKA ===");

        if (uzytkownicy.Count == 0)
        {
            Console.WriteLine("Brak użytkowników.");
            Pauza();
            return;
        }

        for (int i = 0; i < uzytkownicy.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {uzytkownicy[i].Imie}");
        }

        Console.Write("Wybierz użytkownika: ");

        if (!int.TryParse(Console.ReadLine(), out int numer) ||
            numer < 1 || numer > uzytkownicy.Count)
        {
            Console.WriteLine("Nieprawidłowy numer.");
            Pauza();
            return;
        }

        Uzytkownik u = uzytkownicy[numer - 1];

        Console.WriteLine();
        Console.WriteLine($"Użytkownik: {u.Imie}");
        Console.WriteLine($"Limit wypożyczeń: {u.LimitWypozyczen()}");
        Console.WriteLine($"Liczba wypożyczonych materiałów: {u.Wypozyczone.Count}");

        if (u.Wypozyczone.Count == 0)
        {
            Console.WriteLine("Brak wypożyczonych materiałów.");
        }
        else
        {
            Console.WriteLine("Wypożyczone materiały:");

            foreach (MaterialBiblioteczny material in u.Wypozyczone)
            {
                material.WyswietlInfo();
            }
        }

        Pauza();
    }

    static void UsunMaterial(List<MaterialBiblioteczny> materialy)
    {
        Console.Clear();
        Console.WriteLine("=== USUWANIE MATERIAŁU ===");

        if (materialy.Count == 0)
        {
            Console.WriteLine("Brak materiałów.");
            Pauza();
            return;
        }

        for (int i = 0; i < materialy.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            materialy[i].WyswietlInfo();
        }

        Console.Write("Podaj numer materiału do usunięcia: ");

        if (int.TryParse(Console.ReadLine(), out int numer) &&
            numer >= 1 && numer <= materialy.Count)
        {
            materialy.RemoveAt(numer - 1);
            Console.WriteLine("Materiał usunięty.");
        }
        else
        {
            Console.WriteLine("Nieprawidłowy numer.");
        }

        Pauza();
    }

    static void Pauza()
    {
        Console.WriteLine();
        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }
} 