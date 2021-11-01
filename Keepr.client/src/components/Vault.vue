<template>
  <div class=" bg-black col-md-3">    
    <div class="card m-4  shadow rounded " style="width: 18rem">
      <!-- <img :src="vault.img" 
          class="card-img-top selectable"
          data-bs-toggle="modal"
          :data-bs-target="'#vault-modal-' + vault.id"/> -->
      <div class="">
        <h5>{{vault.name}}</h5>
        <router-link :to="{name: 'Profile', params: {id: vault.creatorId}}" class="selectable">
          <img :src="vault.creator.picture" class="circle-rounded" style="width: 3rem;">
        </router-link>        
      </div>      
    </div>
  </div>

    <Modal :id="'vault-modal-'+ vault.id" class="bg-dark text-light">
    <template #modal-title>
      <h5>{{ vault.title }}</h5>      
    </template>
    <template #modal-body>
      <VaultDetail :vault="vault" class="m-1 container-fluid" />
    </template>
  </Modal>
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
  setup(props){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>