using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LanguajeDataBase", menuName ="Lenguaje DataBase")]
public class ScriptableLanguaje : ScriptableObject
{
    List<Languaje> idiomas = new List<Languaje>();
    // Esta funcion permite
    public void AddLanguaje(Languaje idioma) {
        if (GetLanguaje(idioma.Name) == null)
            idiomas.Add(idioma);
        else
            throw new System.Exception("Ya existe un idioma con ese Nombre, prueba con otro");
    }
    public void AddContent(string lang, string content) =>
        GetLanguaje(lang).AddContent(content);
    public List<string> GetLanguajes()
    {
        List<string> id = new List<string>();
        foreach (var idioma in idiomas)
            id.Add(idioma.Name);
        return id;
    }
    public Languaje GetLanguaje(string name) => idiomas.Find(i => i.Name.ToUpper() == name.ToUpper());
    public Languaje GetLanguaje(int id) => idiomas[id];
    public LanguajeContent GetLanguajeContent(string idioma, int idContent) => GetLanguaje(idioma).GetContent(idContent);
    public string GetContent(string idioma, int idContent) => GetLanguajeContent(idioma, idContent).content;
    public int LanguajesCount => idiomas.Count;
    public int LanguajeContentCount(string name) => GetLanguaje(name).content.Count;
}
