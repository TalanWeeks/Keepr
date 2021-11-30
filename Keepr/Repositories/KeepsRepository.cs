using Dapper;
using Keepr.Interfaces;
using Keepr.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Keepr.Repositories
{
  public class KeepsRepository : IRepository<Keep>
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // this is a post function that creates a keep/photo post
    public Keep Create(Keep data)
    {
      string sql = @"
      INSERT INTO keeps(CreatorId, Name, Description, Img, Views, Shares, Keeps)
      VALUES(@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    // this is a delete function that allows a user to delete only their keep
    public void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps  WHERE id = @id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected == 0)
      {
        throw new System.Exception("Keep delorte failed bud");
      }
    }

    // this is a put function that allows you to edit a keep
    public Keep Edit(Keep data)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @Id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, data);
      if (rowsAffected == 0)
      {
        throw new System.Exception("Keep delorte failed bud");
      }
      return data;
    }
    
    // this is a get function that allows you to get all keeps in the db
    public List<Keep> Get()
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a on a.id = k.creatorId";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }).ToList();
    }

    // this is a get function that allows a user to get a specific ID and also increments a view when its been grabbed 
    public Keep Get(int id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id;
      UPDATE keeps
      SET views = views + 1
      WHERE id = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }

    // this is a get function that grabs all keeps tied to a specific user
    public List<Keep> GetKeepsByProfile(string profileId)
    {
      string sql = @"
      SELECT
      *
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @profileId;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { profileId }).ToList();
    }
  }
}