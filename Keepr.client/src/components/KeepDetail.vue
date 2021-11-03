<template>
  <div class="container-fluid">
    <div class="row text-dark">
      <div class="col-md-5 m-2">
        <img :src="keep.img" style="width: 35rem" class="rounded"/>
        </div>
        <div class="col-md-2"></div>
      <div class="col-md-4 m-2">
        <div class="row">
          <div class="col-12 my-2 text-center">
            <span class="mdi mdi-eye f-20 mx-4" title="views"> {{keep.views}} </span>
            <span class="mdi mdi-temperature-kelvin f-20 mx-4" title="keeps"> {{keep.keeps}} </span>
            <span class="mdi mdi-share-variant f-20 mx-4" title="shares"> {{keep.shares}} </span>
             </div>
          <div class="col-12 my-2">
            <h2>{{keep.title}}</h2>
          </div>
          <div class="col-12 my-2">
            <h2 class="my-3 text-center">{{keep.name}}</h2>            
          </div>
          <div class="col-12 my-2">
            <h4 class="my-3 text-center">{{keep.description}}</h4>            
          </div>
          <div class="col-12 my-2 border-bottom">

          </div>
          <div class="row my-5">
          <div class="col-7 my-5">            
            <div class="dropdown ">
              <button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                Add To Vault
              </button>
              <ul class="dropdown-menu scroll-menu" aria-labelledby="dropdownMenuButton1">
                <li                
                  v-for="v in vaults"
                  :key="v.id"
                  :vault="v"
                  class="dropdown-item action"
                  @click="createVaultKeep(v.id, keep.id), keep.keeps++">
                  {{v.name}}
                </li>
              </ul>
            </div>
          </div>
          <div class="col-3 my-4 ms-5">
            <img :src="keep.creator.picture" class="rounded-circle" style="width: 4rem;" :title="keep.creator.name">
          </div>
        </div>          
      </div>
    </div>
  </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { Keep } from '../Models/Keep'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'

export default {
  props: {
    keep: {
      type: Keep,
      default: () => {return new Keep()}
    }
  },
  setup(){
    return {
      vaults: computed(() => AppState.usersVaults),
      async createVaultKeep(vaultId, keepId){
        await vaultsService.createVaultKeep(vaultId, keepId)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.scroll-menu{
  width: 350px;
  height: 200px;
  overflow-y: scroll;
  scroll-behavior: smooth;
}
</style>