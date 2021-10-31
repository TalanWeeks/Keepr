using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
    }

    public List<VaultKeepViewModel> GetVaultKeepsByVaultId(int vaultKeepId, string userId)
    {
      List<VaultKeepViewModel> vaultKeeps = _vaultKeepsRepository.GetVaultKeepsByVaultId(vaultKeepId, userId);
      if (vaultKeeps == null)
      {
        throw new Exception("Unable to find VaultKeeps with that Id");
      }
      return vaultKeeps;
    }

    
    public VaultKeep GetById(int vaultKeepId, string userId)
    {
      VaultKeep foundVault = _vaultKeepsRepository.GetById(vaultKeepId);
      if (foundVault == null)
      {
        throw new Exception("Unable to find Vault with that Id");
      }
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("this vaultKeep is not yours guy");
      }
      return foundVault;
    }

    public void Delete(int VaultKeepId, string userId)
    {
      VaultKeep foundVaultKeep = GetById(VaultKeepId, userId);
      if (foundVaultKeep.CreatorId != userId)
      {
        throw new Exception("That aint your Vaults");
      }
      _vaultKeepsRepository.Delete(VaultKeepId);
    }

  }
}