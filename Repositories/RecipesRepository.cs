using System;
using System.Collections.Generic;
using System.Data;
using allSpace.Models;
using Dapper;

namespace allSpace.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Recipe> GetAll()
    {
      string sql = "SELECT * FROM recipes";
      return _db.Query<Recipe>(sql);
    }

    internal Recipe GetById(int id)
    {
      string sql = "SELECT * FROM recipes WHERE id = @id";
      return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
    }

    internal Recipe Create(Recipe newRecipe)
    {
      string sql = @"
      INSERT INTO recipes
      (creatorId, type, name, description)
      VALUES
      (@CreatorId, @Type, @Name, @Description);
      SELECT LAST_INSERT_ID()";
      newRecipe.Id = _db.ExecuteScalar<int>(sql, newRecipe);
      return newRecipe;
    }

    internal bool Delete(int id)
    {
      string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }

    internal bool Update(Recipe original)
    {
      string sql = @"
      SET
        type = @Type, 
        name = @Name,
        description = @Description 
        WHERE id = @id
      ";
      int affectedRows = _db.Execute(sql, original);
      return affectedRows == 1;
    }
  }
}