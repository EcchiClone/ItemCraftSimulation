using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemElementUI : MonoBehaviour, IPointerClickHandler
{
    public ItemElementUIParent parent;
    public ItemElement item;
    public Image icon;
    public TextMeshProUGUI text;
    public Image outerBG;

    public ItemElementUI(ItemElement item, ItemElementUIParent parent)
    {
        SetData(item, parent);
    }
    public void SetData(ItemElement item, ItemElementUIParent parent)
    {
        this.item = item;
        this.parent = parent;

        //Debug.Log($"{item.code}.psd");
        // TODO : Set icon Sprite
        //Debug.Log(Managers.Resource.Load<Object>($"{item.code}.psd"));
        //icon.sprite = Managers.Resource.Load<Sprite>($"{item.code}.psd");
        Managers.Resource.LoadSpriteFromTexture($"{item.code}.psd", $"{item.code}", sprite => {
            icon.sprite = sprite;
        });

        text.text = item.code + " x " + item.quantity.ToString();
        outerBG.color = TierColors.ColorArray[Managers.Data.ItemDic[item.code].tier];
    }
    public void Refresh()
    {
        text.text = item.code + " x " + item.quantity.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(parent == ItemElementUIParent.Bag)
            Managers.Game.BagToTable(item.code);
        if(parent == ItemElementUIParent.Table)
            Managers.Game.TableToBag(item.code);
    }
}
public enum ItemElementUIParent
{
    None,
    Bag,
    Table,
}