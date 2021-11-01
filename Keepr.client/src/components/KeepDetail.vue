<template>
  <div class="container-fluid">
    <div class="row text-dark">
      <div class="col-md-5 m-2">
        <img :src="keep.img" style="width: 35rem"/>
        </div>
        <div class="col-md-2"></div>
      <div class="col-md-4 m-2">
        <div class="row">
          <div class="col-12">Views: {{keep.views}} Keeps: {{keep.keeps}}  Shares: {{keep.shares}}</div>
          <div class="col-12">
            <h2>{{keep.title}}</h2>
          </div>
          <div class="col-12">
            <h2>{{keep.description}}</h2>
          </div>
          <div class="col-4">
            <button class="btn btn-success mt-1">ADD TO VAULT</button>
          </div>
          <div class="col-4">
            <i class="mdi mdi-delete text-danger"></i>
          </div>
          <div class="col-4">
            
            <img :src="keep.creator.picture" class="rounded-circle" style="width: 3rem"/>
            <small>{{keep.creator.name}}</small>
          </div>
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