using ATHCarRentNetworkSystem.Models;

namespace ATHCarRentNetworkSystem.Repositories
{
    public interface ISamochodRepositories : IDisposable
    {
        IEnumerable<Samochod> GetSamochods();
        Samochod GetSamochodById(int id);
        void InsertSamochod(Samochod samochod);
        void UpdateSamochod(Samochod samochod);
        void DeleteSamochod(int id);
        void Save();
    }
}
