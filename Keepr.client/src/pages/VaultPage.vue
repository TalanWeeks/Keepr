<template>
  <div class="container-fluid bg-black">
    <h1>heelllerrrr from vault page</h1>
    <div class="row">
      <Keep v-for="vk in vaultKeeps"
      :key="vk.id"
      :keep="vk"
      class="p-0 m-0"/>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
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
  })
  return {
      vaultKeeps: computed(()=> AppState.vaultKeeps)
    }
  }
}
</script>

<style scoped lang="scss">
.container-fluid{
  padding: 2rem;
  column-count: 4;
}
img{
  width: 100%;
  margin: 1rem;
}
</style>
