<template>
  <div class="bg-black">    
    <div class="card m-2 shadow rounded ">
      <img :src="keep.img" 
          class="card-img"
          />
      <div class="card-img-overlay text-light action" title="details" v-if="keep.creator" @click="openModal()">
        <span class="position-absolute bottom-0 end-0 me-2 action"><h5>{{keep.name}}</h5></span>
        <span @click.stop="">
          <router-link :to="{name: 'Profile', params: {id: keep.creatorId}}" class="action position-absolute  bottom-0 start-0 m-2" title="profile page" >
            <img :src="keep.creator.picture" class="rounded-circle" style="width: 2.5rem;" >
          </router-link> 
        </span>
        <div class="on-hover position-absolute" style="right: 1rem; top: 1rem" v-if="account.id == vault.creatorId"  @click.stop="deleteVaultKeep()">
          <i class="mdi mdi-delete-forever text-danger f-20 action" title="delete"></i>
        </div>       
    </div>      
    </div>
    <Modal :id="'keep-modal-'+ keep.id" class="bg-dark text-light">
      <template #modal-title>
      <h5>{{ keep.title }}</h5>      
      </template>
      <template #modal-body>
      <KeepDetail :keep="keep" class="m-1 container-fluid" />
      </template>
    </Modal>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { Keep } from '../Models/Keep'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
import { VaultKeep } from '../Models/VaultKeep'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => {return new Keep()}
    },
    vaultKeep: {
      type: VaultKeep,
      default: () => {return new VaultKeep()}
    }
  },
  setup(props){
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      async deleteVaultKeep() {
        try {
          if(await Pop.confirm()) {
            await keepsService.deleteVaultKeep(props.keep.vaultKeepId)
            Pop.toast("Deleted the keep", 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      openModal(){
        const modal = Modal.getOrCreateInstance(document.getElementById('keep-modal-' + props.keep.id))
        modal.show()
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>