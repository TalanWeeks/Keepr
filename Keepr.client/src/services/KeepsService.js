
import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class KeepsService{
  async get(){
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
    logger.log('your keeps sir',AppState.keeps)
  }

  async getKeepById(id){
    const res = await api.getKeepById(`api/keeps/${id}`)
    logger.log("this is a single keep by its Id mi lord", res.data)
  }
  async getKeepsByProfileId(profileId){
    AppState.usersKeeps = []
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log("this is all the keeps on this profile", res.data)
    AppState.usersKeeps = res.data.map(k => new Keep(k))
  }
}
export const keepsService = new KeepsService()