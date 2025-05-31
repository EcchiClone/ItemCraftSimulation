using Data;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    // 테이블에 올리기
    public void BagToTable(string code, int num = 1)
    {
        Managers.Object.ChangeItems(Managers.Object.bagItems, code, -num);
        Managers.Object.ChangeItems(Managers.Object.tableItems, code, num);
        Managers.Recipe.CheckRecipe();
        Managers.UI.RefreshOutput();
    }

    // 가방으로 돌려놓기
    public void TableToBag(string code, int num = 1)
    {
        Managers.Object.ChangeItems(Managers.Object.bagItems, code, num);
        Managers.Object.ChangeItems(Managers.Object.tableItems, code, -num);
        Managers.Recipe.CheckRecipe();
        Managers.UI.RefreshOutput();
    }

    // 제작
    public void Craft()
    {
        var recipe = Managers.Recipe.currentRecipe;
        if (recipe == null)
            return;

        foreach (var src in recipe.inputs)
        {
            Managers.Object.ChangeItems(Managers.Object.tableItems, src.code, -src.quantity);
        }
        Managers.Object.ChangeItems(Managers.Object.bagItems, recipe.code, recipe.quantity);

        Managers.Recipe.CheckRecipe();
        Managers.UI.RefreshAll();
    }
}
