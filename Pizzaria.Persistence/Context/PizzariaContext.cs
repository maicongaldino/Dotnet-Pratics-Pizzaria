using Microsoft.EntityFrameworkCore;
using Pizzaria.Domain;

namespace Pizzaria.Persistence.Context
{
    public class PizzariaContext : DbContext
    {
        public PizzariaContext(DbContextOptions<PizzariaContext> options) : base(options) {}

        public DbSet<Pizza> Pizzas { get; set; }
    }
}