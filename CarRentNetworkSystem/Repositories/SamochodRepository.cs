using ATHCarRentNetworkSystem.Data;
using ATHCarRentNetworkSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ATHCarRentNetworkSystem.Repositories
{
    public class SamochodRepository : ISamochodRepositories, IDisposable
    {
        private ApplicationDbContext _context;
        private bool _disposed = false;
        public SamochodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteSamochod(int id)
        {
            Samochod samochod = _context.samochods.Find(id);
            if (samochod != null)
                _context.samochods.Remove(samochod);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Samochod GetSamochodById(int id)
        {

            Samochod result = _context.samochods.FirstOrDefault(r => r.Id == id);
            if (result != null)
                return result;
            else 
                return null;
        }

        public IEnumerable<Samochod> GetSamochods()
        {
            return _context.samochods.ToList();
        }

        public void InsertSamochod(Samochod samochod)
        {
            _context.samochods.Add(samochod);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateSamochod(Samochod samochod)
        {
            _context.Entry(samochod).State = EntityState.Modified;
        }
    }
}
