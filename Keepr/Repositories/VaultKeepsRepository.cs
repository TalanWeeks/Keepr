using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // this is a post method that allows you to create a vaultKeep
    public VaultKeep CreateVaultKeep(VaultKeep data)
    {
      string sql = @"
      INSERT INTO vault_keeps
      (creatorId, vaultId, keepId)
      VALUES(@creatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE id = @keepId;
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    // this is a delete function that allows a user to delete a specific vaultKeep
    public void Delete(int id)
    {
      string sql = @"
      DELETE FROM vault_keeps WHERE id = @id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected == 0)
      {
        throw new System.Exception("VaultKeep delorte failed bud");
      }
    }

    public VaultKeep Edit(VaultKeep data)
    {
      throw new System.NotImplementedException();
    }

    public System.Collections.Generic.List<VaultKeep> Get()
    {
      throw new System.NotImplementedException();
    }

    // this is a get method that allows a user to grab all keeps that are tied to a vault
    public List<VaultKeepViewModel> GetVaultKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"
      SELECT
      vk.id AS vaultKeepId,
      k.*,
      a.*
      FROM vault_keeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.vaultId = @vaultId;
      ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, a) =>
      {
        vk.Creator = a;
        return vk;
      }, new { vaultId, userId }).ToList();
    }

    // this a get method that allows you to get a vault keep by its specific ID
    public VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT
      vk.*,
      a.*
      FROM vault_keeps vk
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.id = @Id;
      ";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, a) =>
      {
        vk.Creator = a;
        return vk;
      }, new { id }).FirstOrDefault();
    }
  }
}