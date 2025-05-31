using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Data
{
    #region ItemData
    public class ItemData
    {
        [XmlAttribute]
        public string code;
        [XmlAttribute]
        public int tier;
    }

    [Serializable, XmlRoot("ItemDatas")]
    public class ItemDataLoader : ILoader<string, ItemData>
    {
        [XmlElement("ItemData")]
        public List<ItemData> items = new List<ItemData>();

        public Dictionary<string, ItemData> MakeDict()
        {
            Dictionary<string, ItemData> dict = new Dictionary<string, ItemData>();
            foreach (ItemData item in items)
                dict.Add(item.code, item);
            return dict;
        }
    }
    #endregion

    #region RecipeData
    public class RecipeElement
    {
        [XmlAttribute]
        public string code;
        [XmlAttribute]
        public int quantity;
    }

    public class RecipeData
    {
        [XmlAttribute]
        public string code;
        [XmlAttribute]
        public int quantity;

        [XmlElement("Input")]
        public List<RecipeElement> inputs = new List<RecipeElement>();

        [XmlElement("Action")]
        public List<RecipeElement> actions = new List<RecipeElement>();
    }

    [Serializable, XmlRoot("RecipeDatas")]
    public class RecipeDataLoader : ILoader<string, RecipeData>
    {
        [XmlElement("RecipeData")]
        public List<RecipeData> recipes = new List<RecipeData>();

        public Dictionary<string, RecipeData> MakeDict()
        {
            Dictionary<string, RecipeData> dict = new Dictionary<string, RecipeData>();
            foreach (var recipe in recipes)
                dict.Add(recipe.code, recipe);
            return dict;
        }
    }
    #endregion
}