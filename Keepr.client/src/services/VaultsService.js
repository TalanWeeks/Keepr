
import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class VaultsService{
  async getVaultsByProfileId(profileId){
    AppState.usersVaults = []
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log("this is all the Vaults on this profile", res.data)
    AppState.usersVaults = res.data.map(k => new Keep(k))
  }
}
export const vaultsService = new VaultsService()