using System.Collections.Generic;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers

{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    public VaultsController(VaultsService vaultsService)
    {
      _vaultsService = vaultsService;
    }
    [HttpGet]
    public ActionResult<List<Vault>> GetAll()
    {
      try
      {
        return Ok(_vaultsService.GetAll());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{VaultsId}")]
    public ActionResult<Vault> GetById(int VaultsId)
    {
      try
      {
        return Ok(_vaultsService.GetById(VaultsId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Post([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // for node reference - req.body.creatorId = req.userInfo.id
        // FIXME NEVER TRUST THE CLIENT
        vaultData.CreatorId = userInfo.Id;
        Vault createdVault = _vaultsService.Create(vaultData);
        createdVault.Creator = userInfo;
        return createdVault;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]

    public async Task<ActionResult<Vault>> Edit([FromBody] Vault data, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = userInfo.Id;
        data.Id = id;
        return Ok(_vaultsService.Edit(data));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{VaultsId}")]
    public async Task<ActionResult<string>> RemoveVaults(int VaultsId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vaultsService.DeleteVault(VaultsId, userInfo.Id);
        return Ok("Vault was delorted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}