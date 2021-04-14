using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableLanguaje))]
public class LanguajeBaseEditor : Editor {
    private ScriptableLanguaje database;
    private string searchString = "";
    private bool shouldSearch;

    Languaje newItem;

    GUIStyle layoutWrap = new GUIStyle();
    GUIStyle h1 = new GUIStyle();

    private void OnEnable()
    {
        database = (ScriptableLanguaje)target;
        layoutWrap.wordWrap = true;
        h1.fontSize = 16;
    }
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (database)
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Label("Languajes in DataBase: " + database.LanguajesCount);
            if(database.LanguajesCount > 0)
            {
                EditorGUILayout.BeginHorizontal("Box");
                GUILayout.Label("Search: ");
                searchString = GUILayout.TextField(searchString);
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add new Idioma"))
                AddLanguajeWindows.ShowWindow(database);
            if (GUILayout.Button("Add new Content"))
                AddContentWindows.ShowWindow(database);
            EditorGUILayout.EndVertical();
            shouldSearch = !string.IsNullOrEmpty(searchString);
            foreach (var item in database.GetLanguajes())
            {
                if (shouldSearch)
                {
                    if (item == searchString || item.Contains(searchString))
                        DisplayIdioma(database.GetLanguaje(item));
                }
                else
                    DisplayIdioma(database.GetLanguaje(item));
            }
            //if(deleteItem != null)
            //    database.items.Remove(deleteItem);
        }
    }

    //Item deleteItem;

    private void DisplayIdioma(Languaje idioma)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
            GUILayout.Label(idioma.Name, h1);
            if (GUILayout.Button(idioma.editorShow ? "Hidden" : "Show", GUILayout.MaxWidth(80)))
                idioma.editorShow = !idioma.editorShow;
        EditorGUILayout.EndHorizontal();
        if (idioma.content.Count >= 0 && idioma.editorShow)
        {
            EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Contenido:");
            EditorGUILayout.EndHorizontal();
            foreach (var content in idioma.content)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                    GUILayout.Label(content.id + ".- " + content.content, layoutWrap);
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical(GUILayout.MaxWidth(5));
                if (GUILayout.Button("Edit"))
                    ContentModifiWindows.ShowWindow(database, content.id);
                if (GUILayout.Button("Delete"))
                    if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to delete this item?", "Acept"))
                        RemoveElement(content.id);
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                /*if (GUILayout.Button("Delete"))
                {
                    deleteItem = item;
                }
                else
                    deleteItem = null;*/
            }
        }
        EditorGUILayout.EndVertical();
    }
    void RemoveElement(int id)
    {
        foreach (var idioma in database.GetLanguajes())
            database.GetLanguaje(idioma).RemoveContent(id);
    }
}
