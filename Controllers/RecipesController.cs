using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using allSpace.Models;
using allSpace.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace allSpace.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rService;
    private readonly AccountsService _acctService;

    public RecipesController(RecipesService rService, AccountsService acctService)
    {
      _rService = rService;
      _acctService = acctService;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Recipe>> GetAll()
    {
      try
      {
        IEnumerable<Recipe> recipes = _rService.GetAll();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<Recipe> GetById(int id)
    {
      try
      {
        Recipe found = _rService.GetById(id);
        return Ok(found);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _acctService.GetOrCreateAccount(userInfo);
        newRecipe.CreatorId = userInfo.Id;
        Recipe recipe = _rService.Create(newRecipe);
        return Ok(recipe);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<Recipe>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _rService.Delete(id, userInfo.Id);
        return Ok("Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> Update(int id, [FromBody] Recipe update)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // _rService.Update(id, userInfo.Id);
        update.Id = id;
        Recipe updated = _rService.Update(update);
        return Ok(updated);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



  }
}