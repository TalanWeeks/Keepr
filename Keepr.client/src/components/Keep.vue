<template>
  <div class="bg-black ">    
    <div class="card m-2 shadow rounded ">
      <img :src="keep.img" 
          class="card-img"
          />
      <div class="card-img-overlay text-dark action" title="details" v-if="keep.creator" @click="openModal(keep.views++)">
        <span class="position-absolute bottom-0 end-0 me-0 action glass-bg"><h5>{{keep.name}}</h5></span>
        <span @click.stop="">
          <router-link :to="{name: 'Profile', params: {id: keep.creatorId}}" class="action position-absolute  bottom-0 start-0 m-2" title="profile page" >
            <img :src="keep.creator.picture" class="rounded-circle" style="width: 2.5rem;" >
          </router-link> 
        </span>
        <div class="on-hover position-absolute" style="right: 1rem; top: 1rem" v-if="account.id == keep.creatorId" @click.stop="deleteKeep()">
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
export default {
  props: {
    keep: {
      type: Keep,
      default: () => {return new Keep()}
    }
  },
  setup(props){
    return {
      account: computed(() => AppState.account),
      async deleteKeep() {
        try {
          if(await Pop.confirm()) {
            await keepsService.delete(props.keep.id)
            Pop.toast("Deleted the keep", 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async openModal(){
        const modal = Modal.getOrCreateInstance(document.getElementById('keep-modal-' + props.keep.id))
        modal.show()
        await keepsService.getKeepById(props.keep.id)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.glass-bg{
  padding: .2rem;
  border-radius: 10%;
  background-color: #ffffff5e;
  backdrop-filter: blur(20px);
}
</style>