
import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class KeepsService{

  async createKeep(newKeep){
    const res = await api.post('api/keeps', newKeep)
    AppState.usersKeeps.unshift(new Keep(res.data))
    logger.log('your new keep data sir', res.data)
  }
  async get(){
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
    logger.log('your keeps sir',AppState.keeps)
  }

  async getKeepById(id){
    const res = await api.getKeepById(`api/keeps/${id}`)
    logger.log("this is a single keep by its Id mi lord", res.data)
  }
  async getKeepByVaultId(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log("this is your keeps in this your keeps in this vault", res.data)
  }
  async getKeepsByProfileId(profileId){
    AppState.usersKeeps = []
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log("this is all the keeps on this profile", res.data)
    AppState.usersKeeps = res.data.map(k => new Keep(k))
  }

  async editKeep(keepData){
    const res = await api.put(`api/keeps/${keepData.id}`, keepData)
    logger.log('your edited keep data mi lord', res.data)
    let keepIndex = AppState.keeps.findIndex(k => k.id == keepData.id)
    AppState.keeps.splice(keepIndex, 1, new Keep(res.data))
  }
}
export const keepsService = new KeepsService()