using AjdinBukvicASAOsiguranje.Models;
using Java.Util;

namespace AjdinBukvicASAOsiguranje;

public partial class MainPage : ContentPage
{
    public List<VrstaOsiguranja> listVrstaOsiguranja;
    public List<KlasaOsiguranja> listKlasaOsiguranja;

    public MainPage()
    {
        listVrstaOsiguranja = new List<VrstaOsiguranja>
            { new VrstaOsiguranja {Id=1, Naziv="PUTNO OSIGURANJE", Cijena=2.0f, OsiguranaSumaPoUsluzi=2000.0f},
              new VrstaOsiguranja {Id=2, Naziv="OSIGURANJE STANA", Cijena=4.0f, OsiguranaSumaPoUsluzi=4000.0f},
              new VrstaOsiguranja {Id=3, Naziv="ZDRAVSTVENO OSIGURANJE", Cijena=5.0f, OsiguranaSumaPoUsluzi=5000.0f},
              new VrstaOsiguranja {Id=4, Naziv="ASISTENCIJA NA PUTU", Cijena=50.0f, OsiguranaSumaPoUsluzi=1000.0f},
              new VrstaOsiguranja {Id=5, Naziv="MALI KASKO", Cijena=500.0f, OsiguranaSumaPoUsluzi=10000.0f},
              new VrstaOsiguranja {Id=6, Naziv="OSIGURANJE MOBITELA", Cijena=50.0f, OsiguranaSumaPoUsluzi=2000.0f}
            };

        listKlasaOsiguranja = new List<KlasaOsiguranja>
            { new KlasaOsiguranja {Id=1, NazivKlase="Osnovno", FaktorOsiguraneSume=1, FaktorCijene=1},
              new KlasaOsiguranja {Id=2, NazivKlase="Plus", FaktorOsiguraneSume=2, FaktorCijene=3},
              new KlasaOsiguranja {Id=3, NazivKlase="Premium", FaktorOsiguraneSume=3, FaktorCijene=5}
            };
        InitializeComponent();

        BindingContext = this;

        vrsta1.Text = listVrstaOsiguranja[0].Naziv;
        vrsta2.Text = listVrstaOsiguranja[1].Naziv;
        vrsta3.Text = listVrstaOsiguranja[2].Naziv;
        vrsta4.Text = listVrstaOsiguranja[3].Naziv;
        vrsta5.Text = listVrstaOsiguranja[4].Naziv;
        vrsta6.Text = listVrstaOsiguranja[5].Naziv;

        odabirKlase.ItemsSource = listKlasaOsiguranja;

        uneseniDatum.Date = DateTime.Today;
        uneseniDatum.MinimumDate = DateTime.Today;
        uneseniDatum.MaximumDate = DateTime.Today.AddDays(365);
    }

    public void OnClicked(object sender, EventArgs args)
    {
        double ukupnaCijena = 0;

        foreach (VrstaOsiguranja vrsta in listVrstaOsiguranja)
        {
            vrsta.Ugovorena = false;
            vrsta.Kolicina = 0;
        }

        if (unos1.Text is null && unos2.Text is null && unos3.Text is null && unos4.Text is null && unos5.Text is null && unos6.Text is null)
        {
            DisplayAlert("Greška", "Molimo unesite količinu za barem jednu vrstu!", "OK");
            return;
        }

        if (mjeseci.Text is null || int.Parse(mjeseci.Text) <= 0)
        {
            DisplayAlert("Greška", "Molimo unesite validan broj mjeseci!", "OK");
            return;
        }
        else
        {
            try
            {
                if (!string.IsNullOrEmpty(unos1.Text)) { listVrstaOsiguranja[0].Kolicina = int.Parse(unos1.Text); listVrstaOsiguranja[0].Ugovorena = true; }
                if (!string.IsNullOrEmpty(unos2.Text)) { listVrstaOsiguranja[1].Kolicina = int.Parse(unos2.Text); listVrstaOsiguranja[1].Ugovorena = true; }
                if (!string.IsNullOrEmpty(unos3.Text)) { listVrstaOsiguranja[2].Kolicina = int.Parse(unos3.Text); listVrstaOsiguranja[2].Ugovorena = true; }
                if (!string.IsNullOrEmpty(unos4.Text)) { listVrstaOsiguranja[3].Kolicina = int.Parse(unos4.Text); listVrstaOsiguranja[3].Ugovorena = true; }
                if (!string.IsNullOrEmpty(unos4.Text)) { listVrstaOsiguranja[4].Kolicina = int.Parse(unos5.Text); listVrstaOsiguranja[4].Ugovorena = true; }
                if (!string.IsNullOrEmpty(unos4.Text)) { listVrstaOsiguranja[5].Kolicina = int.Parse(unos6.Text); listVrstaOsiguranja[5].Ugovorena = true; }

                foreach (VrstaOsiguranja vrsta in listVrstaOsiguranja)
                {
                    if (vrsta.Ugovorena && vrsta.Kolicina <= 0)
                    {
                        DisplayAlert("Greška", "Unesena količina ne može biti nula ili negativna!", "OK");
                        return;
                    }
                }

                    float baznaCijena = 0;

                    int odabraneVrste = 0;
                    odabraneVrste = listVrstaOsiguranja.Where(x => x.Ugovorena).Count();
                    baznaCijena += listVrstaOsiguranja.Where(x => x.Ugovorena).Select(s => s.Cijena * s.Kolicina).Sum();

                    float baznaOsiguranaSuma = 0;
                    baznaOsiguranaSuma += listVrstaOsiguranja.Where(x => x.Ugovorena).Select(s => s.OsiguranaSumaPoUsluzi).Sum();

                    float konacnaBazna = odabraneVrste * baznaCijena;
                    float koacnaOsigurana = odabraneVrste * baznaOsiguranaSuma;

                    KlasaOsiguranja klasa = (KlasaOsiguranja)odabirKlase.SelectedItem;
                    ukupnaCijena += klasa.FaktorCijene;
                    koacnaOsigurana += klasa.FaktorOsiguraneSume;

                    ukupnaCijena += konacnaBazna;

                    if (porodicno.IsToggled) ukupnaCijena += ukupnaCijena * 0.3;

                    string porodicnoStr = porodicno.IsToggled == true ? "Da" : "Ne";



                string poruka = "***** Vaš iznos police *****" +
                            "\nUkupna vrijednost osiguranja: " + ukupnaCijena + " KM" +
                            "\nBroj odabranih vrsta: " + odabraneVrste +
                            "\nPorodični paket: " + porodicnoStr +
                            "\nOsigurana suma: " + koacnaOsigurana + " KM" +
                            "\nBroj mjeseci: " + mjeseci.Text +
                            "\nDatum važenja: " + uneseniDatum.Date.ToString("dd.M.yyyy.") + "-" + uneseniDatum.MaximumDate.ToString("dd.M.yyyy.");


                    DisplayAlert("Kalkulacija police", poruka, "OK");
                }
            catch
            {
                DisplayAlert("Greška", "Molimo unesite validne podatke za vašu uslugu!", "OK");
            }
        }

    }

}