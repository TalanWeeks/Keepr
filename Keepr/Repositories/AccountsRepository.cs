using System.Data;
using Keepr.Models;
using Dapper;

// new db added runs out in 24 days :(
namespace Keepr.Repositories
{
  public class AccountsRepository
  {
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
      _db = db;
    }

    // this gets a profile form the db by its email address
    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    // this gets a profile by its unique ID
    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    // this is the post function that will create a new profile
    internal Account Create(Account newAccount)
    {
      string sql = @"
            INSERT INTO accounts
              (email, id)
            VALUES
              (@Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    // this is a PUT function that allows you to edit profile information
    internal Account Edit(Account update)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }
  }
}
