using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    public VaultKeepsController(VaultsService vaultsService)
    {
      _vaultsService = vaultsService;
    }

    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep data)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = userInfo.Id;
        VaultKeep CreatedVaultKeep = _vaultsService.CreateVaultKeep(data);
        return Ok(CreatedVaultKeep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}