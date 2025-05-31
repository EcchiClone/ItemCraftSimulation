
using System.Collections.Generic;
using System.Diagnostics;

public class ObjectManager
{
    public Dictionary<string, ItemElement> bagItems;
    public Dictionary<string, ItemElement> tableItems;
    public ItemElement outputItem;

    public void Init() // 시작 시 초기화
    {
        bagItems = new Dictionary<string, ItemElement>();
        tableItems = new Dictionary<string, ItemElement>();
        outputItem = null;

        foreach (var item in Managers.Data.ItemDic)
        {
            var itemValue = item.Value;
            if(itemValue.tier == 0)
                bagItems[itemValue.code] = new ItemElement(itemValue.code, 100);
            Managers.UI.RefreshAll();
        }
        
    }
    public void ChangeItems(Dictionary<string, ItemElement> target, string code, int num)
    {
        if (target.ContainsKey(code))
            target[code].quantity += num;
        else
            target[code] = new ItemElement(code, num);
        if (target[code].quantity <= 0)
            target.Remove(code);

        Managers.UI.RefreshAll();
    }
}
public class ItemElement
{
    public string code;
    public int quantity;
    public ItemElement(string code, int quantity)
    {
        this.code = code;
        this.quantity = quantity;
    }
}