export class VaultKeep{
  constructor(data = {}){
        this.id = data.id
        this.creatorId = data.creatorId
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
        this.vaultId = data.vaultId
        this.keepId = data.keepId
        this.creator = data.creator || {}
      }
    }
