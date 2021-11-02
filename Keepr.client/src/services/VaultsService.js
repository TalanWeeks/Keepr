
import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { Vault } from "../Models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class VaultsService{

  async createVault(newVault){
    const res = await api.post('api/vaults', newVault)
    AppState.usersVaults.unshift(new Vault(res.data))
    logger.log('your new vault data sir', res.data)
  }
  async getVaultsByProfileId(profileId){
    AppState.usersVaults = []
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log("this is all the Vaults on this profile", res.data)
    AppState.usersVaults = res.data.map(v => new Vault(v))
  }
  async editVault(vaultData){
    const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
    logger.log('your edited vault data mi lord', res.data)
    let vaultIndex = AppState.usersVaults.findIndex(v => v.id == vaultData.id)
    AppState.usersVaults.splice(vaultIndex, 1, new Vault(res.data))
  }

  async delete(id) {
    const res = await api.delete(`api/vaults/${id}`)
    logger.log('delete res', res)
    AppState.usersVaults = AppState.usersVaults.filter(v => v.id !== id)
  }
}
export const vaultsService = new VaultsService()