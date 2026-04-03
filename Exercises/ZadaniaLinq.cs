using LinqConsoleLab.PL.Data;
using LinqConsoleLab.PL.Models;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        var studenciWarszawa = DaneUczelni.Studenci.Where(s => s.Miasto == "Warsaw").Select(s =>  $"{s.Imie} {s.Nazwisko}");
        return studenciWarszawa;
        throw Niezaimplementowano(nameof(Zadanie01_StudenciZWarszawy));
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        var studenciEmail = DaneUczelni.Studenci.Select(s =>  $"{s.Email}");
        return studenciEmail;
        throw Niezaimplementowano(nameof(Zadanie02_AdresyEmailStudentow));
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        var studenciSorted =  DaneUczelni.Studenci
            .OrderBy(s => s.Imie)
            .ThenBy(s => s.Nazwisko)
            .Select(s => $"{s.NumerIndeksu} {s.Imie} {s.Nazwisko}");
        return studenciSorted;
        throw Niezaimplementowano(nameof(Zadanie03_StudenciPosortowani));
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        var pierwszyPrzedmiotAnalityczny = DaneUczelni.Przedmioty
                .Where( p => p.Kategoria == "Analytics")
                .Select(p => p.Nazwa)
                .DefaultIfEmpty("Brak przedmiotu").First();
        
        return new List<string>().Append(pierwszyPrzedmiotAnalityczny) ;
        throw Niezaimplementowano(nameof(Zadanie04_PierwszyPrzedmiotAnalityczny));
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        var nieaktywnyZapis = DaneUczelni.Zapisy
            .Where(z => z.CzyAktywny == false)
            .Select(z => z.PrzedmiotId.ToString())
            .FirstOrDefault();
        string res = string.IsNullOrEmpty(nieaktywnyZapis) ? "Nie" : "Tak";
        return new List<string>().Append(res) ;
        throw Niezaimplementowano(nameof(Zadanie05_CzyIstniejeNieaktywneZapisanie));
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        var prowadzacyKatedry = DaneUczelni.Prowadzacy
            .All(p => p.Katedra != null);
        string res = prowadzacyKatedry ? "Tak" : "Nie";
        return new List<string>().Append(res) ;
        throw Niezaimplementowano(nameof(Zadanie06_CzyWszyscyProwadzacyMajaKatedre));
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        var aktywneZapisy = DaneUczelni.Zapisy
            .Where(z => z.CzyAktywny)
            .Count()
            .ToString();
        return new List<string>().Append(aktywneZapisy) ;
        throw Niezaimplementowano(nameof(Zadanie07_LiczbaAktywnychZapisow));
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
        return DaneUczelni.Studenci
            .Select(s => s.Miasto)
            .Distinct()
            .OrderBy(s => s);
        
        throw Niezaimplementowano(nameof(Zadanie08_UnikalneMiastaStudentow));
    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        return DaneUczelni.Zapisy
            .OrderByDescending(z => z.DataZapisu)
            .Take(3)
            .Select(z => $"{z.DataZapisu} {z.StudentId} {z.PrzedmiotId}");
            
        throw Niezaimplementowano(nameof(Zadanie09_TrzyNajnowszeZapisy));
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        return DaneUczelni.Przedmioty
            .OrderBy(p => p.Nazwa)
            .Skip(2)
            .Take(2)
            .Select(z => $"{z.Nazwa} {z.Kategoria}");
        throw Niezaimplementowano(nameof(Zadanie10_DrugaStronaPrzedmiotow));
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        return DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId, (s, z) => new { s, z })
            .Select(sz => $"{sz.s.Imie} {sz.s.Nazwisko} {sz.z.DataZapisu}");
        throw Niezaimplementowano(nameof(Zadanie11_PolaczStudentowIZapisy));
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {

        return DaneUczelni.Zapisy
            .Join(DaneUczelni.Studenci, z => z.StudentId, s => s.Id, (z, s) => new { z, s })
            .Join(DaneUczelni.Przedmioty, sz => sz.z.StudentId, p => p.Id, (sz, p) => new { sz, p })
            .Select(szp => $"{szp.sz.s.Imie} {szp.sz.s.Nazwisko} {szp.p.Nazwa}");
            
        throw Niezaimplementowano(nameof(Zadanie12_ParyStudentPrzedmiot));
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        return DaneUczelni.Przedmioty
            .GroupJoin(DaneUczelni.Zapisy, p => p.Id, z => z.PrzedmiotId, (p, z) => new { p, z })
            .Select(pz => (pz.p.Nazwa, pz.z.Count())
                .ToString());
        throw Niezaimplementowano(nameof(Zadanie13_GrupowanieZapisowWedlugPrzedmiotu));
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        return DaneUczelni.Zapisy
            .Join(DaneUczelni.Przedmioty, z => z.PrzedmiotId, p => p.Id, (z, p) => new { z, p })
            .Where(zp => zp.z.OcenaKoncowa != null)
            .Select(zp => new {zp.p.Nazwa, zp.z.OcenaKoncowa})
            .GroupBy(no => no.Nazwa, no => no.OcenaKoncowa)
            .Select(no => $"{no.Key} {no.Average()}");
            
        throw Niezaimplementowano(nameof(Zadanie14_SredniaOcenaNaPrzedmiot));
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        return DaneUczelni.Prowadzacy
            .GroupJoin(DaneUczelni.Przedmioty, pro => pro.Id, prz => prz.ProwadzacyId, (pro, prz) => new { pro, prz })
            .Select(group => $"{group.pro.Imie} {group.pro.Nazwisko}, {group.prz.Count()}");
        throw Niezaimplementowano(nameof(Zadanie15_ProwadzacyILiczbaPrzedmiotow));
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        return DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId, (s, z) => new { s, z })
            .GroupBy(sz => (sz.s.Imie, sz.s.Nazwisko), sz => sz.z.OcenaKoncowa)
            .Select(sz => $"{sz.Key.Imie} {sz.Key.Nazwisko} {sz.Max()}");
        throw Niezaimplementowano(nameof(Zadanie16_NajwyzszaOcenaKazdegoStudenta));
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        return DaneUczelni.Studenci
            .Where(s => DaneUczelni.Zapisy.Any(z => z.StudentId == s.Id && z.CzyAktywny))
            .GroupJoin(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId, (s, z) => new { s, z })
            .Where(sz => sz.z.Count() > 1)
            .Select(sz => $"{sz.s.Imie} {sz.s.Nazwisko} {sz.z.Count()}");
        throw Niezaimplementowano(nameof(Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem));
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        return DaneUczelni.Przedmioty
            .Join(DaneUczelni.Zapisy, p => p.Id, z => z.PrzedmiotId, (p, z) => new { p, z })
            .Where(pz => pz.p.DataStartu > new DateTime(2026, 4, 1))
            .Where(pz => pz.z.OcenaKoncowa == null)
            .Select(pz => pz.p.Nazwa);
        throw Niezaimplementowano(nameof(Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych));
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        return DaneUczelni.Prowadzacy
            .Join(DaneUczelni.Przedmioty.DefaultIfEmpty(), pro => pro.Id, prz => prz.ProwadzacyId, (pro, prz) => new { pro, prz })
            .Join(DaneUczelni.Zapisy.Where(z => z.OcenaKoncowa != null), pp => pp.prz.Id, z => z.PrzedmiotId, (pp, z) => new {pp, z})
            .Select(ppz => new{ppz.pp, ppz.z.OcenaKoncowa})
            .GroupBy(ppo => (ppo.pp.pro.Imie, ppo.pp.pro.Nazwisko), ppo => ppo.OcenaKoncowa)
            .Select(ppo => $"{ppo.Key.Imie}  {ppo.Key.Nazwisko} {ppo.Average()}");
        
        throw Niezaimplementowano(nameof(Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach));
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        return DaneUczelni.Studenci
            .GroupJoin(DaneUczelni.Zapisy.Where(z => z.CzyAktywny), s => s.Id, z => z.StudentId, (s, z) => new { s, z })
            .GroupBy(sz => sz.s.Miasto)
            .OrderByDescending(sz => sz.Count())
            .Select(sz => $"{sz.Key} {sz.Count()}");
        throw Niezaimplementowano(nameof(Wyzwanie04_MiastaILiczbaAktywnychZapisow));
    }

    private static NotImplementedException Niezaimplementowano(string nazwaMetody)
    {
        return new NotImplementedException(
            $"Uzupełnij metodę {nazwaMetody} w pliku Exercises/ZadaniaLinq.cs i uruchom polecenie ponownie.");
    }
}
