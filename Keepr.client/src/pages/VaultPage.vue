<template>
  <div class="container-fluid bg-black text-light">
    <div class="row mt-5 mb-3 ms-3">
      <div class="col-md-4">
        <h1>{{vault.name}}</h1>
        <h4 class="mt-4">Keeps: {{vaultKeeps.length}}</h4>
      </div>
      <div class="col-md-6 text-end">
        <button class="btn btn-danger" @click="deleteVault()">Delete Vault</button>
      </div>
    </div>
  </div>
  <div class="masonryIsh bg-black m-5">
      <VaultKeep v-for="vk in vaultKeeps"
      :key="vk.id"
      :keep="vk"
      class="p-0 m-0"/>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { router } from '../router'
export default {
  setup() {
    const route = useRoute()
  watchEffect(async () => {
    try {
      if(route.params.vaultId){
      await keepsService.getKeepByVaultId(route.params.vaultId)
      }
    } catch (error) {
      Pop.toast(error.message, 'error')
    }
  }),
  onMounted(() => {
    try {
      if(route.params.vaultId){
      vaultsService.getVaultById(route.params.vaultId)      
      }
    } catch (error) {
      Pop.toast(error.message, 'error')
      router.push({ name: 'Home'})
    }
  })
  return {
      vaultKeeps: computed(()=> AppState.vaultKeeps),
      vault: computed(() => AppState.vault),
      async deleteVault(){
        try {
          if(await Pop.confirm()) {
            await vaultsService.delete(route.params.vaultId)
            router.push({ name: 'Home'})
            Pop.toast("Deleted vault", 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }       
      }
    }
  }
}
</script>

<style scoped lang="scss">
.masonryIsh{
  columns: 5 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    background: #EC985A;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
  }
}
</style>
