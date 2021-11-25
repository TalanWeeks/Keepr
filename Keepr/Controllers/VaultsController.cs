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
    private readonly VaultKeepsService _vaultKeepsService;
    public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService)
    {
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
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

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetById(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // for node reference - req.body.creatorId = req.userInfo.id
        // FIXME NEVER TRUST THE CLIENT
        return Ok(_vaultsService.GetById(vaultId, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeep>>> GetVaultKeepsByVaultId(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // for node reference - req.body.creatorId = req.userInfo.id
        // FIXME NEVER TRUST THE CLIENT
        return Ok(_vaultKeepsService.GetVaultKeepsByVaultId(vaultId, userInfo?.Id));
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