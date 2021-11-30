import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data this data can be used throughout the app as long as you use a constructor to access the data in that specific component/page
export const AppState = reactive({
  user: {},
  account: {},
  profile: {},
  vault: {},
  keeps: [],
  vaults: [],
  usersKeeps: [],
  usersVaults: [],
  usersVaultKeeps: [],
  vaultKeeps: []
})
