using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;
    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }
    internal Profile GetProfileById(string profileId)
    {
      var sql = @"
      SELECT
      *
      FROM
      accounts a
      WHERE a.id = @profileId
      ";
      return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
    }
  }
}