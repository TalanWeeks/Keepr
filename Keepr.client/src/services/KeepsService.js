
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

  async getKeepsById(id){
    const res = await api.getKeepsByProfile(`api/keeps/${id}`)
    logger.log("this is a single keep by its Id mi lord", res.data)
  }
}
export const keepsService = new KeepsService()