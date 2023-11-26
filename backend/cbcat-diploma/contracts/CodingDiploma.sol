//SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/utils/math/SafeMath.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721Enumerable.sol";

contract CodingDiploma is ERC721Enumerable {
  using SafeMath for uint256;
  using Counters for Counters.Counter;

  Counters.Counter private _tokenIds;
  address[] public nftOwners;
  mapping(address => uint256) public scoreBoard;

  //uint public constant MAX_SUPPLY = 100;
  uint public constant PRICE = 0.0 ether;
  uint public constant MAX_PER_MINT = 1;

  string public baseTokenURI;

  constructor(string memory baseURI) ERC721("CodingDiploma", "CodingDiploma") {
    setBaseURI(baseURI);
  }

  function _baseURI() internal view virtual override returns (string memory) {
    return baseTokenURI;
  }

  function setBaseURI(string memory _baseTokenURI) public {
    baseTokenURI = _baseTokenURI;
  }

  function mintNFTs(uint _count) public payable {
    //not count anymore, only gonna mint one per player as is a certificate
    _mintSingleNFT();
  }

  function _mintSingleNFT() private {
    uint newTokenID = _tokenIds.current();
    _safeMint(msg.sender, newTokenID);
    _tokenIds.increment();

    nftOwners.push(msg.sender);
  }

  function tokensOfOwner(address _owner) external view returns (uint[] memory) {
    uint tokenCount = balanceOf(_owner);
    uint[] memory tokensId = new uint256[](tokenCount);

    for (uint i = 0; i < tokenCount; i++) {
      tokensId[i] = tokenOfOwnerByIndex(_owner, i);
    }
    return tokensId;
  }

  function withdraw() public payable {
    uint balance = address(this).balance;
    require(balance > 0, "No ether left to withdraw");

    (bool success, ) = (msg.sender).call{value: balance}("");
    require(success, "Transfer failed.");
  }

  // Función para verificar la propiedad de un NFT
  function isNFTOwner(address _owner) external view returns (bool) {
    for (uint256 i = 0; i < nftOwners.length; i++) {
      if (nftOwners[i] == _owner) {
        return true;
      }
    }
    return false;
  }

  function setScoreForPlayer(
    address player,
    uint256 score
  ) public returns (bool) {
    scoreBoard[player] = score;
  }

  // INTERNAL FUNCTION TO MAKE IT SOULDBOUND
  function beforeTokenTransfer(
    address from,
    address to,
    uint256 tokenIds
  ) internal virtual {
    require(from == address(0), "Err: token transfer is BLOCKED");
  }
}
