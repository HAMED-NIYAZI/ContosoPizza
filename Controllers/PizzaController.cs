using System.Security.Cryptography;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
    PizzaService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {

            PizzaService.Add(pizza);
    return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);

        // try
        // {
        //     if (pizza is null)
        //     {
        //         return BadRequest();
        //     }

        //     if (pizza.Id != 0)
        //     {
        //         pizza.Id = 0;
        //     }

        //     PizzaService.Add(pizza);
        //     return Ok();

        // }
        // catch (System.Exception)
        // {
        //     return BadRequest();
        // }


    }
    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        try
        {
            if (id <= 0 || pizza == null)
            {
                return BadRequest();
            }

            var OldPizza = PizzaService.Get(id);
            if (OldPizza is null)
            {
                return NotFound();
            }

            OldPizza.Name = pizza.Name;
            OldPizza.IsGlutenFree = pizza.IsGlutenFree;
            PizzaService.Update(OldPizza);
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            if (id > 0)
            {
                PizzaService.Delete(id);
                return Ok();
            }
            return NotFound();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}