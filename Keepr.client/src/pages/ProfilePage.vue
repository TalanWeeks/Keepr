<template>
  <div class="container-fluid bg-black">
    <div class="row">
      <div class="col-md-1">
        <img :src="profile.picture" alt="">
      </div>
      <div class="col-md-8 text-light text-start">
        <h3>{{profile.name}}</h3>
      </div>
    </div>
    <div class="row text-light mt-5">
      <h3>Vaults</h3>
    </div>

    <div class="row">
      <Vault v-for="v in usersVaults"
      :key="v.id"
      :vault="v"
      class="p-0 m-0"/>
    </div>

    <div class="row text-light mt-5">
      <h3>Keeps</h3>
    </div>
    <div class="container-fluid masonry-bullish">
    <div class="row">
      <Keep v-for="k in usersKeeps"
      :key="k.id"
      :keep="k"
      class="p-0 m-0"/>
    </div>
  </div>
  </div>
</template>

<script>
import { computed, watchEffect } from '@vue/runtime-core'
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
import { profilesService } from "../services/ProfilesService"
import Pop from "../utils/Pop"
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'

export default {
  setup() {
    const route = useRoute()
    async function getKeepsByProfileId() {
      try {
       await keepsService.getKeepsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    }
     async function getVaultsByProfileId() {
      try {
       await vaultsService.getVaultsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    }
  watchEffect(async () => {
    if(route.params.id){
      await profilesService.getProfileById(route.params.id)
      getKeepsByProfileId()
      getVaultsByProfileId()
    }
  })
  return {
     usersKeeps: computed(()=> AppState.usersKeeps),
     usersVaults: computed(()=> AppState.usersVaults),
     profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-bullish{
  padding: 2rem;
  column-count: 4;
}
img{
  width: 100%;
  margin: 1rem;
}
</style>
