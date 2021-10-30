using System.Collections.Generic;
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
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

// MIGHT HAVE TO MOVE ALL THIS SHIT INTO ACCOUNT CONTROLLER THE BREAK POINT HITS THERE INSTEAD OF IN HERE...
    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService  vaultsService)
    {
      _profilesService = profilesService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [Authorize]
    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
      try
      {
        Profile profile = _profilesService.GetProfileById(profileId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(int profileId)
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
    public ActionResult<List<Vault>> GetVaultsByProfile(int profileId)
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
  }
}
