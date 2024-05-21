using System;
using System.Collections.Generic;
using System.Linq;
using MoralisUnity;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Web3Api.Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Inventory
{


    public void OpenInventory()
    {

        //if (uiPanel.activeSelf) return;

        ActivatePanel(true);
        Opened?.Invoke();

        LoadPurchasedItems();
    }

    private async void LoadPurchasedItems()
    {
        //We get our wallet address.
        MoralisUser user = await Moralis.GetUserAsync();
        var playerAddress = user.authData["moralisEth"]["id"].ToString();

        try
        {
            NftOwnerCollection noc =
                await Moralis.GetClient().Web3Api.Account.GetNFTsForContract(playerAddress.ToLower(),
                    GameManager.ContractAddress,
                    GameManager.ContractChain);

            List<NftOwner> nftOwners = noc.Result;

            // We only proceed if we find some
            if (!nftOwners.Any())
            {
                Debug.Log("You don't own any NFT");
                return;
            }

            foreach (var nftOwner in nftOwners)
            {
                if (nftOwner.Metadata == null)
                {
                    // Sometimes GetNFTsForContract fails to get NFT Metadata. We need to re-sync
                    Moralis.GetClient().Web3Api.Token.ReSyncMetadata(nftOwner.TokenAddress, nftOwner.TokenId, GameManager.ContractChain);
                    Debug.Log("We couldn't get NFT Metadata. Re-syncing...");
                    continue;
                }

                var nftMetaData = nftOwner.Metadata;
                NftMetadata formattedMetaData = JsonUtility.FromJson<NftMetadata>(nftMetaData);

                PopulatePlayerItem(nftOwner.TokenId, formattedMetaData);
            }
        }
        catch (Exception exp)
        {
            Debug.LogError(exp.Message);
        }
    }

    private void PopulatePlayerItem(string tokenId, NftMetadata data)
    {
        InventoryItem newItem = Instantiate(item, itemsGrid.transform);

        newItem.Init(tokenId, data);
    }
}

[Serializable]
public class NftMetadata
{
    public string Name;
    public int Power;
    public int Curve;
    public int TopSpin;
    public int BackSpin;
    public int BallGuide;
    public int Impact;
    public int ClubType;
    public string ImageUrl;
    public float ClubPrice;
}