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
      <label for="published">Private</label>
      <input
        type="checkbox"
        class="ms-3 mt-3"
        name="isPrivate"
        v-model="editable.isPrivate"
      />
    </div>
    <button type="submit" class="btn btn-success pt-2">Submit</button>
  </form>
</template>


<script>
import { watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { ref } from "@vue/reactivity"
import { Vault } from '../Models/Vault'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => new Vault()
    }
  },
  setup(props){
    const editable = ref({})
    watchEffect(() => {
      editable.value = {...props.vault}
    })
    const route = useRoute()
    return {
      editable,
      async handleSubmit(){
      try { 
        if(editable.value.id){
          await vaultsService.editVault(editable.value)
          const editModal = Modal.getInstance(document.getElementById(`edit-vault-${props.vault.id}`))
          editModal.hide()
          Pop.toast('Keep Edited mi lord')
          return
        }
        // this is fucked rick
        await vaultsService.createVault(editable.value)
        const modal = Modal.getInstance(document.getElementById(`create-vault`))
          modal.hide()
          editable.value = {}
          Pop.toast('Vault Created mi lord', 'success')
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