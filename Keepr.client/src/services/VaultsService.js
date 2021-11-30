
import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { Vault } from "../Models/Vault"
import { VaultKeep } from "../Models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class VaultsService{

  // a node post method that allows a user to create a vault and will display it first in order
  async createVault(newVault){
    const res = await api.post('api/vaults', newVault)
    AppState.usersVaults.unshift(new Vault(res.data))
    logger.log('your new vault data sir', res.data)
  }

  // a node post method that allows a user to create a vaultKeep and will display it first in order
  async createVaultKeep(vaultId, keepId){
    const data = {vaultId: vaultId, keepId: keepId}
    const res = await api.post('api/vaultkeeps', data)
    AppState.usersVaultKeeps.unshift(new VaultKeep(res.data))
    logger.log('your new vault data sir', res.data)
  }

  // a node get method that allows a user to get all vaults by a specific profile ID
  async getVaultsByProfileId(profileId){
    AppState.usersVaults = []
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log("this is all the Vaults on this profile", res.data)
    AppState.usersVaults = res.data.map(v => new Vault(v))
  }

  // a node get method that allows a user to get one vault by its ID and is used to open/ view the vaults details
  async getVaultById(vaultId){
    AppState.vault = {}
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log("here is the vault tied to this page", res.data)
    AppState.vault = res.data
  }

  // a put method that allows a user to edit a vault typically used to add a vaultKeep into it
  async editVault(vaultData){
    const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
    logger.log('your edited vault data mi lord', res.data)
    let vaultIndex = AppState.usersVaults.findIndex(v => v.id == vaultData.id)
    AppState.usersVaults.splice(vaultIndex, 1, new Vault(res.data))
  }

  //  a delete method that is used to allows a user to delete a vault by its ID
  async delete(id) {
    const res = await api.delete(`api/vaults/${id}`)
    logger.log('delete res', res)
    AppState.usersVaults = AppState.usersVaults.filter(v => v.id !== id)
  }

}
export const vaultsService = new VaultsService()