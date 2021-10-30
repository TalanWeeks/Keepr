using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

// MIGHT HAVE TO MOVE ALL THIS SHIT INTO ACCOUNT CONTROLLER THE BREAK POINT HITS THERE INSTEAD OF IN HERE...
    public ProfilesController(AccountService accountService, KeepsService keepsService, VaultsService  vaultsService)
    {
      _accountService = accountService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string profileId)
    {
      try
      {
        List<Keep> keeps = _keepsService.GetKeepsByProfile(profileId);
        return Ok(keeps);  
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByProfile(string profileId)
    {
      try
      {
        return Ok(_vaultsService.GetVaultsByProfile(profileId));  
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut]

    public async Task<ActionResult<Account>> Edit([FromBody] Account editedAccount)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return _accountService.Edit(editedAccount, userInfo.Email);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
