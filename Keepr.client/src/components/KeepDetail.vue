<template>
  <div class="container-fluid">
    <div class="row text-dark">
      <div class="col-md-5 m-2">
        <img :src="keep.img" style="width: 35rem"/>
        </div>
        <div class="col-md-2"></div>
      <div class="col-md-4 m-2">
        <div class="row">
          <div class="col-12 my-2"><h5>{{keep.views}} Keeps: {{keep.keeps}}  Shares: {{keep.shares}}</h5></div>
          <div class="col-12 my-2">
            <h2>{{keep.title}}</h2>
          </div>
          <div class="col-12 my-2">
            <h2>{{keep.description}}</h2>
          </div>
          <div class="row my-5">
          <div class="col-7 my-5">            
            <div class="dropdown">
              <button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                Add To Vault
              </button>
              <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li                
                  v-for="v in vaults"
                  :key="v.id"
                  :vault="v"
                  class="dropdown-item">
                  {{v.name}}
                </li>
              </ul>
            </div>
          </div>
          <div class="col-3 my-5 ms-5">
            <img :src="keep.creator.picture" class="rounded-circle" style="width: 2.5rem;">
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
export default {
  props: {
    keep: {
      type: Keep,
      default: () => {return new Keep()}
    }
  },
  setup(){
    return {
      vaults: computed(() => AppState.usersVaults)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>