<template>
  <div class="d-flex bg-black action" title="details" data-bs-toggle="modal"
          :data-bs-target="'#keep-modal-' + keep.id">    
    <div class="card m-4 shadow rounded " style="width: 18rem">
      <img :src="keep.img" 
          class="card-img"
          />
      <div class="card-img-overlay text-light" v-if="keep.creator">
        <span class="position-absolute bottom-0 end-0 me-2 action"><h5>{{keep.name}}</h5></span>
        <router-link :to="{name: 'Profile', params: {id: keep.creatorId}}" class="action position-absolute bottom-0 start-0 m-2" title="profile page">
          <img :src="keep.creator.picture" class="rounded-circle" style="width: 2.5rem;">
        </router-link> 
        <div class="on-hover position-absolute" style="right: 1rem; top: 1rem" v-if="account.id == keep.creatorId">
          <i class="mdi mdi-delete-forever text-danger f-20 action" title="delete" @click="deleteKeep()"></i>
        </div>       
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
</template>


<script>
import { computed } from '@vue/reactivity'
import { Keep } from '../Models/Keep'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
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