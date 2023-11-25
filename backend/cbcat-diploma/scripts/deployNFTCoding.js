require("@nomiclabs/hardhat-ethers");
const { hre } = require("hardhat");

//Function that connects to metamask and verifies if a user has an NFT
/*async function verifyNFTMetamask(contract, tokenId) {
    const provider = new utils.providers.JsonRpcProvider("http://localhost:8545");
    const signer = provider.getSigner();
    const contractInstance = new ethers.Contract(contract.address, contract.interface, signer);

    verifyNFT(contractInstance, tokenId).then(function(token) {
        console.log("Token: ", token);
        return token;
    }).catch(function(e) {
        console.log("Error: ", e);
    }
}*/
/*
async function main() {
  const baseTokenURI = "ipfs://QmRhDhEfq1mFGMNev6yJiGV7G6zYv9yEsnx7bu4RPkBigV/";
  //const baseTokenURI = "ipfs:///";

  // Get owner/deployer's wallet address
  const [owner] = await hre.ethers.getSigners();

  // Get contract that we want to deploy
  const contractFactory = await hre.ethers.getContractFactory("IndexFriends");

  // Deploy contract with the correct constructor arguments
  const contract = await contractFactory.deploy(baseTokenURI);

  // Wait for this transaction to be mined
  await contract.deployed();

  // Get contract address
  console.log("Contract deployed to:", contract.address);

  // Reserve NFTs
  //let txn = await contract.reserveNFTs();
  //await txn.wait();
  //console.log("10 NFTs have been reserved");

  // Mint 3 NFTs by sending 0.03 ether
  txn = await contract.mintNFTs(1, { value: utils.parseEther("0.0001") });
  await txn.wait();

  // Get all token IDs of the owner
  let tokens = await contract.tokensOfOwner(owner.address);
  console.log("Owner has tokens: ", tokens);
}*/

async function main() {
  const [deployer] = await ethers.getSigners();
  const baseTokenURI = "ipfs://QmRhDhEfq1mFGMNev6yJiGV7G6zYv9yEsnx7bu4RPkBigV/";

  console.log("Deploying contracts with the account:", deployer.address);

  // Get contract that we want to deploy
  const contractFactory = await ethers.getContractFactory("CbcatDiploma");
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
