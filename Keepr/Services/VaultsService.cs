using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository VaultsRepository)
    {
      _vaultsRepository = VaultsRepository;
    }

    public List<Vault> GetAll()
    {
      return _vaultsRepository.Get();
    }

    public Vault GetById(int vaultId)
    {
      Vault foundVault = _vaultsRepository.Get(vaultId);
      if (foundVault == null)
      {
        throw new Exception("Unable to find Vaults");
      }
      if(foundVault.IsPrivate == true)
      {
        throw new Exception("this vault is private");
      }
      return foundVault;
    }

    public Vault Create(Vault VaultsData)
    {
      return _vaultsRepository.Create(VaultsData);
    }

    public Vault Edit(Vault editedVault)
    {
      Vault foundVault = GetById(editedVault.Id);
      if (foundVault.CreatorId != editedVault.CreatorId)
      {
        throw new Exception("Unauthorized to edit");
      }
      foundVault.Name = editedVault.Name ?? foundVault.Name;
      foundVault.Description = editedVault.Description ?? foundVault.Description;
      foundVault.IsPrivate = editedVault.IsPrivate;
      return _vaultsRepository.Edit(foundVault);
    }

    public void DeleteVault(int VaultsId, string userId)
    {
      Vault foundVaults = GetById(VaultsId);
      if (foundVaults.CreatorId != userId)
      {
        throw new Exception("That aint your Vaults");
      }
      _vaultsRepository.Delete(VaultsId);
    }

  }
}