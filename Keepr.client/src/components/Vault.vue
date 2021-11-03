<template>
  <div class=" bg-black col-md-3">    
    <div class="card m-4 shadow rounded " style="width: 18rem">

      <img :src="vault.img" 
          class="card-img"
          />        
      <div class="card-img-overlay text-light">
      <router-link :to="{name: 'Vault', params: {vaultId: vault.id}}" class="action" title="vault page">
        <h5 class="text-light action position-absolute bottom-0 start-0 m-2">{{vault.name}}</h5>
        </router-link>
              <div class="on-hover position-absolute" style="right: 1rem; top: 1rem" v-if="account.id == vault.creatorId">
        <i class="mdi mdi-delete-forever text-danger f-20 action" title="delete" @click="deleteVault()"></i>
      </div>      
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
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { Vault } from '../Models/Vault'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => {return new Vault()}
    }
  },
  setup(props){
    return {
      account: computed(() => AppState.account),
      async deleteVault() {
        try {
          if(await Pop.confirm()) {
            await vaultsService.delete(props.vault.id)
            Pop.toast("Deleted the vault", 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }  
    }
  }
}
</script>


<style lang="scss" scoped>

</style>