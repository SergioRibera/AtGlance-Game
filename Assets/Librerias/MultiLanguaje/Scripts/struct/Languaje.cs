using System;
using System.Collections.Generic;

[Serializable]
public class Languaje
{
    public string Name = "";
    public bool editorShow = true;
    public List<LanguajeContent> content = new List<LanguajeContent>();

    public bool ExistContent(int id) => content.Find(i => i.id == id) != null;
    public void AddContent(string cont) => content.Add(new LanguajeContent() { id = content.Count + 1, content = cont });
    public void EditContent(int id, string cont) => content.Find(i => i.id == id).content = cont;
    public void RemoveContent(int id) => content.Remove(GetContent(id));
    public LanguajeContent GetContent(int id) => content.Find(i => i.id == id);
}
