<template>
  <div class="component">
      <div class="d-flex justify-content-center bg-black">
    
    <div class="card m-4  shadow rounded " style="width: 18rem">
      <img :src="keep.img" class="card-img-top"/>
      <div class="card-body">
        <h5>{{keep.name}}</h5>
        <small>{{keep.creator}}</small>
      </div>      
    </div>
  </div>

  </div>
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
  setup(){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>