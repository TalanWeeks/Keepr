<template>
  <div class="d-flex justify-content-center bg-black">
    
    <div class="card m-4  shadow rounded " style="width: 18rem">
      <img :src="keep.img" class="card-img-top"/>
      <div class="card-body">
        <h5>{{keep.name}}</h5>
        <small>{{keep.creator}}</small>
         <i
         class="selectable"
          data-bs-toggle="modal"
          :data-bs-target="'#keep-modal-' + keep.id"
        >keep detail</i>
      </div>      
    </div>
  </div>

    <Modal :id="'keep-modal-'+ keep.id" class="bg-dark text-light">
    <template #modal-title>
      <h5>{{ keep.title }}</h5>      
    </template>
    <template #modal-body>
      <KeepDetail :keep="keep" class="m-5" />
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