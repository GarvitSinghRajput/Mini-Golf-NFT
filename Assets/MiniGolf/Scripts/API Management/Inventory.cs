using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Action Opened;
    public static Action Closed;

    [Header("Item Prefab")]
    [SerializeField] protected InventoryItem item;

    [Header("UI Elements")]
    [SerializeField] protected GameObject uiPanel;
    //[SerializeField] protected TextMeshProUGUI titleText;
    [SerializeField] protected GridLayoutGroup itemsGrid;
    //[SerializeField] protected GameObject logo;

    protected void UpdateItem(string idToUpdate, Golf_Clubs newData)
    {
        foreach (Transform childItem in itemsGrid.transform)
        {
            InventoryItem item = childItem.GetComponent<InventoryItem>();

            if (item.GetId() == idToUpdate)
            {
                item.SetData(newData);
            }
        }
    }

    public void ClearAllItems()
    {
        foreach (Transform childItem in itemsGrid.transform)
        {
            Destroy(childItem.gameObject);
        }
    }

    protected void DeleteItem(string idToDelete)
    {
        foreach (Transform childItem in itemsGrid.transform)
        {
            InventoryItem item = childItem.GetComponent<InventoryItem>();

            if (item.GetId() == idToDelete)
            {
                Destroy(item.gameObject);
            }
        }
    }

    protected void ActivatePanel(bool activate)
    {

        uiPanel.SetActive(activate);

        //if (logo == null) return;
        //logo.SetActive(activate);
    }

    public void OnCloseButtonClicked()
    {
        ClearAllItems();
        ActivatePanel(false);

        Closed?.Invoke();
    }
}
