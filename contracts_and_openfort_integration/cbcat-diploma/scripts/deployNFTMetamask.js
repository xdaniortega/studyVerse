// scripts/deployNFTMetamask.js
require("@nomiclabs/hardhat-ethers");

async function main() {
  const [deployer] = await ethers.getSigners();
  const baseTokenURI = "ipfs://QmSS1ib6DB4smkz3NroEdMkzdnUhirWAnYQ6hWJmuETQ9K/";

  console.log("Deploying contracts with the account:", deployer.address);

  // Get contract that we want to deploy
  const contractFactory = await ethers.getContractFactory("MetamaskDiploma");
  // Deploy contract with the correct constructor arguments
  const contract = await contractFactory.deploy(baseTokenURI);

  // Wait for this transaction to be mined
  await contract.deployed();

  // Get contract address
  console.log("Contract deployed to:", await contract.address);
}

main()
  .then(() => process.exit(0))
  .catch((error) => {
    console.error(error);
    process.exit(1);
  });