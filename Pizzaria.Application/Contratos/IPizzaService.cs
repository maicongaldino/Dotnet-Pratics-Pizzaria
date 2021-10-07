using System.Threading.Tasks;
using Pizzaria.Application.Dtos;

namespace Pizzaria.Application.Contratos
{
    public interface IPizzaService
    {
        //Criado
        Task<PizzaDto> AddPizza(PizzaDto pizza);
        Task<PizzaDto> UpdatePizza(int pizzaId, PizzaDto pizza);
        Task<bool> RemoverPizza(int pizzaId);

        //Copiado
        Task<PizzaDto[]> GetAllPizzasAsync();
        Task<PizzaDto> GetPizzaByIdAsync(int id);
        Task<PizzaDto[]> GetAllPizzasBySaborAsync(string sabor);
    }
}