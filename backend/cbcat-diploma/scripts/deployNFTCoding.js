require("@nomiclabs/hardhat-ethers");
const { hre } = require("hardhat");

async function main() {
  const [deployer] = await ethers.getSigners();
  const baseTokenURI = "ipfs://QmNaGgvSkr37qFnuvVTzQQwz7KdvXKzJExMaGBHrw6MuCz/";

  console.log("Deploying contracts with the account:", deployer.address);

  // Get contract that we want to deploy
  const contractFactory = await ethers.getContractFactory("CodingDiploma");
  // Deploy contract with the correct constructor arguments
  const contract = await contractFactory.deploy(baseTokenURI);

  // Wait for this transaction to be mined
  await contract.deployed();

  // Get contract address
  console.log("Contract deployed to:", await contract.address);

  /*// Mint 3 NFTs by sending 0.03 ether
  txn = await contract.mintNFTs(1, { value: utils.parseEther("0.0001") });
  await txn.wait();

  // Get all token IDs of the owner
  let tokens = await contract.tokensOfOwner(owner.address);
  console.log("Owner has tokens: ", tokens);*/
}

main()
  .then(() => process.exit(0))
  .catch((error) => {
    console.error(error);
    process.exit(1);
  });
