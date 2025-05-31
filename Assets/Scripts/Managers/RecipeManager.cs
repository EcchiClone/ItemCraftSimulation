using Data;
using System.Collections.Generic;

public class RecipeManager
{
    public RecipeData currentRecipe; // 일단, Table과 Recipe가 완전히 일치하는 경우만 반영
    public void Init()
    {

    }

    public void CheckRecipe()
    {
        var tableDict = new Dictionary<string, int>();
        foreach (var kv in Managers.Object.tableItems)
            tableDict[kv.Key] = kv.Value.quantity;

        foreach (var recipe in Managers.Data.RecipeDic.Values)
        {
            // 레시피의 input을 Dictionary<string, int>로 변환
            var recipeDict = new Dictionary<string, int>();
            foreach (var input in recipe.inputs)
                recipeDict[input.code] = input.quantity;

            // 비교
            if (Utils.IsSameDict(tableDict, recipeDict))
            {
                currentRecipe = recipe;
                UnityEngine.Debug.Log($"조합 성공: {recipe.code}");
                return;
            }
        }

        currentRecipe = null; // 일치하는 레시피 없음
    }
}