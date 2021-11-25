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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;
    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }
    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        return Ok(_keepsService.GetAll());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{KeepsId}")]
    public ActionResult<Keep> GetById(int KeepsId)
    {
      try
      {
        return Ok(_keepsService.GetById(KeepsId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> Post([FromBody] Keep keepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // for node reference - req.body.creatorId = req.userInfo.id
        // FIXME NEVER TRUST THE CLIENT
        keepData.CreatorId = userInfo.Id;
        Keep createdKeeps = _keepsService.Create(keepData);
        createdKeeps.Creator = userInfo;
        return createdKeeps;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]

    public async Task<ActionResult<Keep>> Edit([FromBody] Keep data, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = userInfo.Id;
        data.Id = id;
        return Ok(_keepsService.Edit(data));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{KeepsId}")]
    public async Task<ActionResult<string>> RemoveKeeps(int KeepsId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _keepsService.DeleteKeep(KeepsId, userInfo.Id);
        return Ok("keep was delorted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}