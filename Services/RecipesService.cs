using System;
using System.Collections.Generic;
using allSpace.Models;
using allSpace.Repositories;

namespace allSpace.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Recipe> GetAll()
    {
      return _repo.GetAll();

    }

    internal Recipe GetById(int id)
    {
      Recipe recipe = _repo.GetById(id);
      if (recipe == null)
      {
        throw new Exception("Invalid recipe id");

      }
      return recipe;
    }

    internal Recipe Create(Recipe newRecipe)
    {
      return _repo.Create(newRecipe);
    }

    internal void Delete(int id, string creatorId)
    {
      Recipe recipe = GetById(id);
      if (recipe.CreatorId != creatorId)
      {
        throw new Exception("You cannot delort another users recipe!");
      }
      if (!_repo.Delete(id))
      {
        throw new Exception("Something has gone terribly wrong!");
      }
    }

    internal Recipe Update(Recipe update)
    {
      Recipe original = GetById(update.Id);
      original.Type = update.Type.Length > 0 ? update.Type : original.Type;
      original.Name = update.Name.Length > 0 ? update.Name : original.Name;
      original.Description = update.Description.Length > 0 ? update.Description : original.Description;
      if (_repo.Update(original))
      {
        return original;
      }
      throw new Exception("Something went wrong??");
    }
  }
}