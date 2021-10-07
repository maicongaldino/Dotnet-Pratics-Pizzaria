using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Domain;
using Pizzaria.Persistence.Context;
using Pizzaria.Persistence.Contratos;

namespace Pizzaria.Persistence.ContratosImpls
{
    public class PizzaPersistImpl : IPizzaPersist
    {
        private readonly PizzariaContext _context;
        public PizzaPersistImpl(PizzariaContext context)
        {
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public async Task<Pizza[]> GetAllPizzasAsync()
        {
            IQueryable<Pizza> query = _context.Pizzas;

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Pizza> GetPizzaByIdAsync(int id)
        {
            IQueryable<Pizza> query = _context.Pizzas;

            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Pizza[]> GetAllPizzasBySaborAsync(string sabor)
        {
            IQueryable<Pizza> query = _context.Pizzas;

            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Sabor.ToLower().Contains(sabor.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}