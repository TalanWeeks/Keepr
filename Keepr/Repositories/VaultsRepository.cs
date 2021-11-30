using Dapper;
using Keepr.Interfaces;
using Keepr.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Keepr.Repositories
{
  public class VaultsRepository : IRepository<Vault>
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    // this is post method that allows a user to create a vault
    public Vault Create(Vault data)
    {
      string sql = @"
      INSERT INTO vaults(CreatorId, Name, Description, IsPrivate)
      VALUES(@CreatorId, @Name, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    // this is a delete method that allows the user to delete a vault and doing so cascading deletes all keeps inside
    public void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected == 0)
      {
        throw new System.Exception("Vault delorte failed bud");
      }
    }

    // this is a put method that allows a user to edit a vault
    public Vault Edit(Vault data)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
      WHERE id = @Id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, data);
      if (rowsAffected == 0)
      {
        throw new System.Exception("vault edito failed bud");
      }
      return data;
    }

    // this is a get method that allows a user to get all vaults
    public List<Vault> Get()
    {
      string sql = "SELECT * FROM vaults;";
      return _db.Query<Vault>(sql).ToList();
    }

    // this is a get method that allows a user to get a specifc vault
    public Vault Get(int id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @Id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }

    // this is a get method that allows a user to get all vaults tied to them specifically
    public List<Vault> GetVaultsByProfile(string profileId)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @profileId;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { profileId }).ToList();
    }
    

    // this is a get method that allows a user to get another users vaults by that users IDs
    public List<Vault> GetOtherUsersVaults(string profileId)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @profileId AND IsPrivate = 0;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { profileId }).ToList();
    }
  }
}