using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MoralisUnity;
using MoralisUnity.Platform.Queries;

public class ShopInventory : Inventory
{
    private MoralisQuery<Golf_Clubs> _getAllItemsQuery;
    private MoralisLiveQueryCallbacks<Golf_Clubs> _callbacks;


    #region UNITY_LIFECYCLE

    //protected void Awake()
    //{

    //    //titleText.text = "NFT SHOP INVENTORY";
    //}

    private void OnEnable()
    {
        AppManager.Started += SubscribeToDatabaseEvents;
        //ShopCounterCollider.PlayerEnteredShop += OpenShop;
        ShopItem.Selected += OnItemSelectedHandler;
        PurchaseItemManager.PurchaseCompleted += DeletePurchasedItem;
        PurchaseItemManager.ItemPanelClosed += OpenShop;
    }

    private void OnDisable()
    {
        AppManager.Started -= SubscribeToDatabaseEvents;
        //ShopCounterCollider.PlayerEnteredShop -= OpenShop;
        ShopItem.Selected -= OnItemSelectedHandler;
        PurchaseItemManager.PurchaseCompleted -= DeletePurchasedItem;
        PurchaseItemManager.ItemPanelClosed -= OpenShop;
    }

    #endregion


    #region EVENT_HANDLERS

    public async void SubscribeToDatabaseEvents()
    {
        _getAllItemsQuery = await Moralis.GetClient().Query<Golf_Clubs>();

        _callbacks = new MoralisLiveQueryCallbacks<Golf_Clubs>();
        _callbacks.OnConnectedEvent += (() => { Debug.Log("Connection Established."); });
        _callbacks.OnSubscribedEvent += ((requestId) => { Debug.Log($"Subscription {requestId} created."); });
        _callbacks.OnUnsubscribedEvent += ((requestId) => { Debug.Log($"Unsubscribed from {requestId}."); });
        _callbacks.OnCreateEvent += ((item, requestId) =>
        {
            Debug.Log("New item created on DB");
            PopulateShopItem(item);
        });
        _callbacks.OnUpdateEvent += ((item, requestId) =>
        {
            Debug.Log("Item updated");
            UpdateItem(item.objectId, item);
        });
        _callbacks.OnDeleteEvent += ((item, requestId) =>
        {
            Debug.Log("Item deleted from DB");
            DeleteItem(item.objectId);
        });

        MoralisLiveQueryController.AddSubscription<Golf_Clubs>("Golf_Clubs", _getAllItemsQuery, _callbacks);
    }

    public void OpenShop()
    {
        ActivatePanel(true);
        Opened?.Invoke();

        GetItemsFromDB();
    }

    private void OnItemSelectedHandler(InventoryItem selectedItem)
    {
        //ClearAllItems();
        //ActivatePanel(false);
    }

    private async void DeletePurchasedItem(string purchasedId)
    {
        MoralisQuery<Golf_Clubs> purchasedItemQuery = _getAllItemsQuery.WhereEqualTo("objectId", purchasedId);
        IEnumerable<Golf_Clubs> purchasedItems = await purchasedItemQuery.FindAsync();

        var purchasedItemsList = purchasedItems.ToList();

        if (!purchasedItemsList.Any()) return;

        await purchasedItemsList.First().DeleteAsync();
    }

    #endregion


    #region PRIVATE_METHODS

    private async void GetItemsFromDB()
    {
        IEnumerable<Golf_Clubs> databaseItems = await _getAllItemsQuery.FindAsync();

        var databaseItemsList = databaseItems.ToList();
        if (!databaseItemsList.Any()) return;

        foreach (var databaseItem in databaseItemsList)
        {
            PopulateShopItem(databaseItem);
        }
    }

    private void PopulateShopItem(Golf_Clubs data)
    {
        InventoryItem newItem = Instantiate(item, itemsGrid.transform);

        newItem.Init(data);
    }

    #endregion
}
