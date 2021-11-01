<template>
  <div class="container-fluid">
    <div class="row text-dark">
      <div class="col-md-5 m-2">
        <!-- <img :src="vault.img" style="width: 35rem"/> -->
        </div>
        <div class="col-md-2"></div>
      <div class="col-md-4 m-2">
        <div class="row">
          <div class="col-12">Views: {{vault.views}} vaults: {{vault.vaults}}  Shares: {{vault.shares}}</div>
          <div class="col-12">
            <h2>{{vault.title}}</h2>
          </div>
          <div class="col-12">
            <h2>{{vault.description}}</h2>
          </div>
          <div class="col-4">
            <button class="btn btn-success mt-1">ADD TO VAULT</button>
          </div>
          <div class="col-4">
            <i class="mdi mdi-delete text-danger"></i>
          </div>
          <div class="col-4">
            
            <img :src="vault.creator.picture" class="rounded-circle" style="width: 3rem"/>
            <small>{{vault.creator.name}}</small>
          </div>
      </div>
    </div>
  </div>
  </div>
</template>


<script>
import { Vault } from '../Models/Vault'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => {return new Vault()}
    }
  },
  setup(){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>