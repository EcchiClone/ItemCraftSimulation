using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager
{
    public GameObject MainUIPrefab;
    public GameObject ItemElementUIPrefab;

    public MainUI mainUI;

    public Dictionary<ItemElement, ItemElementUI> BagItemUIs;
    public Dictionary<ItemElement, ItemElementUI> TableItemUIs;
    public ItemElementUI OutputUI;

    public void Init()
    {
        BagItemUIs = new();
        TableItemUIs = new();

        MainUIPrefab = Managers.Resource.Load<GameObject>("MainUI.prefab");
        ItemElementUIPrefab = Managers.Resource.Load<GameObject>("ItemElementUI.prefab");

        GameObject go = MonoBehaviour.Instantiate(MainUIPrefab);
        mainUI = go.GetComponent<MainUI>();
    }

    public void RefreshAll()
    {
        RefreshBag();
        RefreshTable();
        RefreshOutput();
    }
    public void RefreshBag()
    {
        foreach (var itemElement in Managers.Object.bagItems.Values)
        {
            if (BagItemUIs.ContainsKey(itemElement))
                BagItemUIs[itemElement].Refresh();
            else
            {
                GameObject go = MonoBehaviour.Instantiate(ItemElementUIPrefab);
                ItemElementUI ui = go.GetComponent<ItemElementUI>();
                ui.SetData(itemElement, ItemElementUIParent.Bag);
                BagItemUIs[itemElement] = ui;
                go.transform.SetParent(mainUI.BagContent);
            }
        }
        List<ItemElement> RemoveList = new List<ItemElement>();
        foreach (var itemUI in BagItemUIs)
        {
            if (!Managers.Object.bagItems.ContainsKey(itemUI.Value.item.code))
                RemoveList.Add(itemUI.Key);
        }
        foreach (var itemElement in RemoveList)
        {
            MonoBehaviour.Destroy(BagItemUIs[itemElement].gameObject);
            BagItemUIs.Remove(itemElement);
        }
    }
    public void RefreshTable()
    {
        foreach (var itemElement in Managers.Object.tableItems.Values)
        {
            if (TableItemUIs.ContainsKey(itemElement))
                TableItemUIs[itemElement].Refresh();
            else
            {
                GameObject go = MonoBehaviour.Instantiate(ItemElementUIPrefab);
                ItemElementUI ui = go.GetComponent<ItemElementUI>();
                ui.SetData(itemElement, ItemElementUIParent.Table);
                TableItemUIs[itemElement] = ui;
                go.transform.SetParent(mainUI.TableContent);
            }
        }
        List<ItemElement> RemoveList = new List<ItemElement>();
        foreach (var itemUI in TableItemUIs)
        {
            if (!Managers.Object.tableItems.ContainsKey(itemUI.Value.item.code))
                RemoveList.Add(itemUI.Key);
        }
        foreach (var itemElement in RemoveList)
        {
            MonoBehaviour.Destroy(TableItemUIs[itemElement].gameObject);
            TableItemUIs.Remove(itemElement);
        }
    }
    public void RefreshOutput()
    {
        if(OutputUI != null)
        {
            MonoBehaviour.Destroy(OutputUI.transform.gameObject);
            OutputUI = null;
        }

        var curRecipe = Managers.Recipe.currentRecipe;

        if (curRecipe != null)
        {
            mainUI.CraftBtn.interactable = true;
            GameObject go = MonoBehaviour.Instantiate(ItemElementUIPrefab);
            ItemElementUI ui = go.GetComponent<ItemElementUI>();
            ui.SetData(new ItemElement(curRecipe.code, curRecipe.quantity), ItemElementUIParent.None);
            OutputUI = ui;
            OutputUI.transform.SetParent(mainUI.OutputContent);
            OutputUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
        else
            mainUI.CraftBtn.interactable = false;
    }
}
