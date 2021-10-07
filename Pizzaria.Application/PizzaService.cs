using System;
using System.Threading.Tasks;
using Pizzaria.Application.Contratos;
using Pizzaria.Application.Dtos;
using Pizzaria.Domain;
using Pizzaria.Persistence.Contratos;
using AutoMapper;

namespace Pizzaria.Application
{
    public class PizzaService : IPizzaService
    {
        private readonly IGeralPersist geralPersist;
        private readonly IPizzaPersist pizzaPersist;
        private readonly IMapper mapper;
        public PizzaService(IGeralPersist geralPersist, IPizzaPersist pizzaPersist, IMapper mapper)
        {
            this.geralPersist = geralPersist;
            this.pizzaPersist = pizzaPersist;
            this.mapper = mapper;
        }

        public async Task<PizzaDto> AddPizza(PizzaDto pizzaDto)
        {
            try
            {
                var pizza = mapper.Map<Pizza>(pizzaDto);

                geralPersist.Add<Pizza>(pizza);

                if (await geralPersist.SaveChangesAsync())
                {
                    var pizzaRetorn = await pizzaPersist.GetPizzaByIdAsync(pizza.Id); 

                    return mapper.Map<PizzaDto>(pizza);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PizzaDto> UpdatePizza(int pizzaId, PizzaDto pizzaDto)
        {
            try
            {
                var pizzaTem = pizzaPersist.GetPizzaByIdAsync(pizzaId);

                if (pizzaTem == null)
                {
                    return null;
                }

                pizzaDto.Id = pizzaId;

                var pizza = mapper.Map<Pizza>(pizzaDto);

                geralPersist.Update<Pizza>(pizza);

                if (await geralPersist.SaveChangesAsync())
                {
                    var pizzaRetorn = await pizzaPersist.GetPizzaByIdAsync(pizza.Id);
                    return mapper.Map<PizzaDto>(pizzaRetorn);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoverPizza(int pizzaId)
        {
            try
            {
                var pizza = await pizzaPersist.GetPizzaByIdAsync(pizzaId);

                if (pizza == null)
                {
                    throw new Exception($"Pizza com Id: {pizzaId}, não encontrada !");
                }

                geralPersist.Delete<Pizza>(pizza);

                return await geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PizzaDto[]> GetAllPizzasAsync()
        {
            try
            {
                var pizza = await pizzaPersist.GetAllPizzasAsync();

                if (pizza == null)
                {
                    return null;
                }

                return mapper.Map<PizzaDto[]>(pizza);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PizzaDto> GetPizzaByIdAsync(int id)
        {
            try
            {
                var pizza = await pizzaPersist.GetPizzaByIdAsync(id);

                if (pizza == null)
                {
                    return null;
                }

                return mapper.Map<PizzaDto>(pizza);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PizzaDto[]> GetAllPizzasBySaborAsync(string sabor)
        {
            try
            {
                var pizza = await pizzaPersist.GetAllPizzasBySaborAsync(sabor);

                if (pizza == null)
                {
                    return null;
                }

                return mapper.Map<PizzaDto[]>(pizza);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}