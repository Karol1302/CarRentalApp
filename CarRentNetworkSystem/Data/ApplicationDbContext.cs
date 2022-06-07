using ATHCarRentNetworkSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ATHCarRentNetworkSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Samochod> samochods { get; set; }
        public DbSet<TypSamochodu> typSamochodus { get; set; }
        public DbSet<Wypozyczenie> wypozyczenies { get; set; }
        public DbSet<Wypozyczalnia> wypozyczalnias { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            if (this.Database.EnsureCreated())
            {
                AddRentPlaces();
                AddCarType();
                AddCars();
                AddWypozyczenies();
            }
        }
        void AddRentPlaces()
        {
            Wypozyczalnia wypozyczalnia = new Wypozyczalnia() { Name = "Ath1" };
            wypozyczalnias.Add(wypozyczalnia);
            wypozyczalnia = new Wypozyczalnia() { Name = "Ath2" };
            wypozyczalnias.Add(wypozyczalnia);
            SaveChanges();
        }

        void AddCarType()
        {
            TypSamochodu typSamochodu = new TypSamochodu() { Nazwa = "Combi" };
            typSamochodus.Add(typSamochodu);

            typSamochodu = new TypSamochodu() { Nazwa = "Sedan" };
            typSamochodus.Add(typSamochodu);

            typSamochodu = new TypSamochodu() { Nazwa = "Coupe" };
            typSamochodus.Add(typSamochodu);

            SaveChanges();
        }

        void AddCars()
        {
            TypSamochodu typ = typSamochodus.FirstOrDefault(x => x.Nazwa == "Combi");
            Wypozyczalnia wypozyczalnia = wypozyczalnias.FirstOrDefault(x => x.Name == "Ath1");
            Samochod samochod = new Samochod()
            {
                Nazwa = "samochod1",
                CenaZaGodzine = 10,
                Opis = "Opis 1 samochodu",
                TypSamochodu = typ,
                TypSamochoduId = typ.Id,
                WypozyczalniaId = wypozyczalnia.Id
            };
            samochods.Add(samochod);
            typ = typSamochodus.FirstOrDefault(x => x.Nazwa == "Sedan");
            samochod = new Samochod()
            {
                Nazwa = "samochod2",
                CenaZaGodzine = 100,
                Opis = "Opis 2 samochodu",
                TypSamochodu = typ,
                TypSamochoduId = typ.Id,
                WypozyczalniaId = wypozyczalnia.Id
            };
            samochods.Add(samochod);
            typ = typSamochodus.FirstOrDefault(x => x.Nazwa == "Coupe");
            samochod = new Samochod()
            {
                Nazwa = "samochod3",
                CenaZaGodzine = 200,
                Opis = "Opis 3 samochodu",
                TypSamochodu = typ,
                TypSamochoduId = typ.Id,
                WypozyczalniaId = wypozyczalnia.Id
            };
            samochods.Add(samochod);
            SaveChanges();
        }

        void AddWypozyczenies()
        {
            var samochod = samochods.First(r => r.Id == 1);
            Wypozyczalnia wypozyczalnia = wypozyczalnias.FirstOrDefault(x => x.Name == "Ath1");
            Wypozyczenie wypozyczenie = new Wypozyczenie()
            {
                DataRozpoczecia = DateTime.Now,
                DataUtworzenia = DateTime.Now,
                Samochod = samochod,
                SamochodId = samochod.Id,
                status = StatusWypozyczenia.Rezerwacja,
                WypozyczalniaId = wypozyczalnia.Id
            };
            wypozyczenies.Add(wypozyczenie);
        }
    }
}