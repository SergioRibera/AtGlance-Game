using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/Skins", fileName = "SkinsDataBase")]
public class SkinsDataBase : ScriptableObject{

    public List<SkinPlayer> skins = new List<SkinPlayer>();

    public List<SkinPlayer> GetPurchasedkins() => skins.FindAll(s => s.purchased);

    public SkinPlayer GetByID(int id) => skins.Find(s => s.id == id);
}
