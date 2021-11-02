export class Vault{
  constructor(data = {}){
    this.id = data.id
    this.creatorId = data.creatorId
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.name = data.name
    this.img = 'https://th.bing.com/th/id/R.261101385b1bc3498e93f34d26201d7f?rik=7ltnsr8gVINvGQ&pid=ImgRaw&r=0'
    this.description = data.description
    this.isPrivate = data.isPrivate
    this.creator = data.creator || {}
  }
}