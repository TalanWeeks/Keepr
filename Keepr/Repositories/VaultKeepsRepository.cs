using System.Data;
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
      throw new System.NotImplementedException();
    }

    public VaultKeep Edit(VaultKeep data)
    {
      throw new System.NotImplementedException();
    }

    public System.Collections.Generic.List<VaultKeep> Get()
    {
      throw new System.NotImplementedException();
    }

    public VaultKeep Get(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}