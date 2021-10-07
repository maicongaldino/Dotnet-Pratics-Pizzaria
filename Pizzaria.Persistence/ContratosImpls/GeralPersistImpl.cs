using System.Threading.Tasks;
using Pizzaria.Persistence.Context;
using Pizzaria.Persistence.Contratos;

namespace Pizzaria.Persistence.ContratosImpls
{
    public class GeralPersistImpl : IGeralPersist
    {
        private readonly PizzariaContext _context;
        public GeralPersistImpl(PizzariaContext context)
        {
            this._context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}