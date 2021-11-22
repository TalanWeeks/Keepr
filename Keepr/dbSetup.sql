CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS keeps (
   id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Foreign Key',
  name varchar(255) COMMENT 'Keep Name',
  description varchar(255) COMMENT 'Keep Description',
  img varchar(255) COMMENT 'Keep Picture',
  views int COMMENT 'Keep Views',
  shares int COMMENT 'Keep Shares',
  keeps int COMMENT 'Keep Keeps',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Foreign Key',
  name varchar(255) COMMENT 'Vault Name',
  description varchar(255) COMMENT 'Vault Description',
  isPrivate TINYINT DEFAULT 0 COMMENT 'Vault isPrivate',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vault_keeps (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL comment 'creator ID',
  keepId INT NOT NULL COMMENT 'Foreign Key',
  vaultId INT NOT NULL COMMENT 'Foreign Key',
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';