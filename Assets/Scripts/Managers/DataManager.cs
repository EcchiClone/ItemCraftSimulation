using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<string, Data.ItemData> ItemDic { get; private set; } = new Dictionary<string, Data.ItemData>();
    public Dictionary<string, Data.RecipeData> RecipeDic { get; private set; } = new Dictionary<string, Data.RecipeData>();

    public void Init()
    {
        ItemDic = LoadXml<Data.ItemDataLoader, string, Data.ItemData>("ItemData.xml").MakeDict();
        RecipeDic = LoadXml<Data.RecipeDataLoader, string, Data.RecipeData>("RecipeData.xml").MakeDict();

        foreach (var item in ItemDic)
        {
            Debug.Log(item.Key);
        }

    }
    Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
            return (Loader)xs.Deserialize(stream);
    }
}