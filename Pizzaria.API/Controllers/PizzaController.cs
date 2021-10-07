using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzaria.Application.Contratos;
using Pizzaria.Application.Dtos;

namespace Pizzaria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            this.pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPizzas()
        {
            try
            {
                var pizzas = await pizzaService.GetAllPizzasAsync();
                if (pizzas == null)
                {
                    return NoContent();
                }

                return Ok(pizzas);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pizzas. Erro: {ex.Message}");
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            try
            {
                var pizza = await pizzaService.GetPizzaByIdAsync(id);
                if (pizza == null)
                {
                    return NoContent();
                }
                return Ok(pizza);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pizza. Erro: {ex.Message}");
            }
        }

        [HttpGet("sabor/{sabor}")]
        public async Task<IActionResult> GetAllPizzasBySabor(string sabor)
        {
            try
            {
                var pizzas = await pizzaService.GetAllPizzasBySaborAsync(sabor);
                if (pizzas == null)
                {
                    return NoContent();
                }
                return Ok(pizzas);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pizzas. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPizza(PizzaDto pizza)
        {
            try
            {
                var pizzaRec = await pizzaService.AddPizza(pizza);
                if(pizzaRec == null)
                {
                    return BadRequest("Erro ao tentar adicionar pizza.");
                }

                return this.StatusCode(StatusCodes.Status201Created, pizzaRec);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar pizzas. Erro: {ex.Message}");
            }
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> UpdatePizza(int id, PizzaDto pizza)
        {
            try
            {
                var pizzaRec = await pizzaService.UpdatePizza(id, pizza);
                if (pizzaRec == null)
                {
                    return NoContent();
                }

                return Ok(pizzaRec);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar pizzas. Erro: {ex.Message}");
            }
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> RemovePizza(int id)
        {
            try
            {
                if(await pizzaService.RemoverPizza(id))
                {
                    return Ok("Deletado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar pizzas. Erro: {ex.Message}");
            }
        }
    }
}