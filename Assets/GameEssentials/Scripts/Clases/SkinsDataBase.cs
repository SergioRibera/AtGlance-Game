using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinsDataBase : ScriptableObject{

    public List<SkinPlayer> skins = new List<SkinPlayer>();

    public List<SkinPlayer> GetPurchasedkins(){
        List<SkinPlayer> tmpList = new List<SkinPlayer>();
        foreach (var skin in skins)
            if(skin.purchased)
                tmpList.Add(skin);
        return tmpList;
    }

}
