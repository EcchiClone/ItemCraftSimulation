using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        Init();
    }
    public void Init()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                ManagersInit();
            }
        });
    }
    public void ManagersInit()
    {
        Managers.Data.Init();
        Managers.UI.Init();
        Managers.Object.Init();
        Managers.UI.mainUI.Init();
    }
}