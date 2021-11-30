<template>
  <div class=" masonryIsh bg-black m-5">
      <Keep v-for="k in keeps"
      :key="k.id"
      :keep="k"
      class="p-0 m-0"/>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { AppState } from '../AppState'

export default {
    name: 'Home',
  setup() {
 onMounted(() => {
    try {
      keepsService.get()
    } catch (error) {
      Pop.toast(error.message, 'error')
    }
  })
  return {
      keeps: computed(()=> AppState.keeps),
      vaults: computed(() => AppState.usersVaults),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style scoped lang="scss">
// this css creates the masonry layout in the homepage
.masonryIsh{
  columns: 4 200px;
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
