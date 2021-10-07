using System.Threading.Tasks;
using Pizzaria.Domain;

namespace Pizzaria.Persistence.Contratos
{
    public interface IPizzaPersist
    {
        Task<Pizza[]> GetAllPizzasAsync();
        Task<Pizza> GetPizzaByIdAsync(int id);
        Task<Pizza[]> GetAllPizzasBySaborAsync(string sabor);
    }
}