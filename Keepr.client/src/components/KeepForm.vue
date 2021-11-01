<template>
  <form @submit.prevent="handleSubmit()">
    <!-- Keep Name -->
    <div class="form-group">
      <label for="name" class="sr-only"></label>
      <textarea
        type="text"
        name="name"
        class="form-control"
        placeholder="name"
        required
        v-model="editable.name"
      />
    </div>

    <!-- Keep Description -->
    <div class="form-group">
      <label for="description" class="sr-only"></label>
      <textarea
        type="text"
        name="description"
        class="form-control"
        placeholder="description"
        required
        v-model="editable.description"
      />
    </div>
    <!-- Keep Img -->
    <div class="form-group">
      <label for="img" class="sr-only"></label>
      <textarea
        type="text"
        name="img"
        class="form-control"
        placeholder="img url"
        required
        v-model="editable.img"
      />
    </div>
    <button type="submit" class="btn btn-success mt-1">Submit</button>
  </form>
</template>


<script>
import { watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { Keep } from '../Models/Keep'
import { ref } from "@vue/reactivity"
export default {
  props: {
    keep: {
      type: Keep,
      default: () => new Keep()
    }
  },
  setup(props){
    const editable = ref({})
    watchEffect(() => {
      editable.value = {...props.keep}
    })
    const route = useRoute()
    return {
      editable,
      async handleSubmit(){
      try { 
        if(editable.value.id){
          await keepsService.editKeep(editable.value)
          const editModal = Modal.getInstance(document.getElementById(`edit-keep-${props.keep.id}`))
          editModal.hide()
          Pop.toast('Keep Edited mi lord')
          return
        }
        // this is fucked rick
        await keepsService.createKeep(editable.value)
        const modal = Modal.getInstance(document.getElementById(`create-keep`))
          modal.hide()
          editable.value = {}
          Pop.toast('Keep Created mi lord', 'success')
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>