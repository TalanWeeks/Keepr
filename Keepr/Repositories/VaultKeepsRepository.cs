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

    public VaultKeep CreateVaultKeep(VaultKeep data)
    {
      string sql = @"
      INSERT INTO vault_keeps
      (creatorId, vaultId, keepId)
      VALUES(@creatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Delete(int id)
    {
      string sql = @"
      DELETE FROM vault_keeps WHERE id = @id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, new { id });
      if(rowsAffected == 0)
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

    public List<VaultKeepViewModel> GetVaultKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"
      SELECT
      *
      FROM vault_keeps vk
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.VaultId = @vaultId AND CreatorId = @userId;
      ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, a) => 
      {
        vk.Creator = a;
        return vk;
      }, new { vaultId, userId }).ToList();
    }

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