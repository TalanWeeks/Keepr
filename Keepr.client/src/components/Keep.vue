<template>
  <div class="d-flex bg-black">    
    <div class="card m-4 shadow rounded " style="width: 18rem">
      <img :src="keep.img" 
          class="card-img"
          data-bs-toggle="modal"
          :data-bs-target="'#keep-modal-' + keep.id"/>
      <div class="card-img-overlay text-light">
        <h5 class="align-self-baseline">{{keep.name}}</h5>
        <router-link :to="{name: 'Profile', params: {id: keep.creatorId}}" class="selectable align-self-baseline">
          <img :src="keep.creator.picture" class="circle" style="width: 3rem;">
        </router-link>        
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
import { Keep } from '../Models/Keep'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => {return new Keep()}
    }
  },
  setup(props){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>