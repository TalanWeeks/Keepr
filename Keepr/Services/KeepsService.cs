using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository KeepsRepository)
    {
      _keepsRepository = KeepsRepository;
    }

    public List<Keep> GetAll()
    {
      return _keepsRepository.Get();
    }

    public Keep GetById(int KeepsId)
    {
      Keep foundKeep = _keepsRepository.Get(KeepsId);
      if (foundKeep == null)
      {
        throw new Exception("Unable to find Keeps");
      }
      return foundKeep;
    }

    public Keep Create(Keep KeepsData)
    {
      return _keepsRepository.Create(KeepsData);
    }

    public Keep Edit(Keep editedKeep)
    {
      Keep foundKeep = GetById(editedKeep.Id);
      if (foundKeep.CreatorId != editedKeep.CreatorId)
      {
        throw new Exception("Unauthorized to edit");
      }
      foundKeep.Name = editedKeep.Name ?? foundKeep.Name;
      foundKeep.Description = editedKeep.Description ?? foundKeep.Description;
      foundKeep.Img = editedKeep.Img ?? foundKeep.Img;
      return _keepsRepository.Edit(foundKeep);
    }

    public void DeleteKeep(int KeepsId, string userId)
    {
      Keep foundKeeps = GetById(KeepsId);
      if (foundKeeps.CreatorId != userId)
      {
        throw new Exception("That aint your Keeps");
      }
      _keepsRepository.Delete(KeepsId);
    }

  }
}