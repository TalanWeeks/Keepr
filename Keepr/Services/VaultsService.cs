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

    public Vault GetById(int vaultId, string userId)
    {
      Vault foundVault = _vaultsRepository.Get(vaultId);
      if (foundVault == null)
      {
        throw new Exception("Unable to find Vault with that Id");
      }
      if (foundVault.IsPrivate == true && foundVault.CreatorId != userId)
      {
        throw new Exception("this vault is private and or not yours guy");
      }
      return foundVault;
    }

    public List<Vault> GetVaultsByProfile(string profileId)
    {
      return _vaultsRepository.GetVaultsByProfile(profileId);
    }

    public Vault Create(Vault VaultsData)
    {
      return _vaultsRepository.Create(VaultsData);
    }

    public Vault Edit(Vault editedVault)
    {
      Vault foundVault = GetById(editedVault.Id, editedVault.CreatorId);
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
      Vault foundVaults = GetById(VaultsId, userId);
      if (foundVaults.CreatorId != userId)
      {
        throw new Exception("That aint your Vaults");
      }
      _vaultsRepository.Delete(VaultsId);
    }

  }
}