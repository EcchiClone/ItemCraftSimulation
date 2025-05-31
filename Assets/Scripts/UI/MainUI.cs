using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Transform BagContent;
    public Transform TableContent;
    public Transform OutputContent;
    public Button CraftBtn;
    public void Init()
    {
        CraftBtn.onClick.RemoveAllListeners();
        CraftBtn.onClick.AddListener(() => Managers.Game.Craft());
    }
}