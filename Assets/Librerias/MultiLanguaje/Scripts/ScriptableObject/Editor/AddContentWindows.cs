using UnityEngine;
using UnityEditor;

public class AddContentWindows : EditorWindow
{

    static ScriptableLanguaje database;
    static EditorWindow window;

    static string[] contents;

    static GUILayoutOption[] options = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(20.0f) };
    public static void ShowWindow(ScriptableLanguaje db) {
        database = db;
        window = GetWindow<AddContentWindows>();
        window.titleContent = new GUIContent("Add new Content For Languajes");
        window.minSize = new Vector2(300, 380);
        contents = new string[db.LanguajesCount];
    }

    public void OnGUI()
    {
        for (int i = 0;i < database.LanguajesCount;i++)
            DisplayIdioma(i, database.GetLanguaje(i));
        if (GUILayout.Button("Confirm"))
            AddContents();
    }
    private void DisplayIdioma(int index, Languaje lang)
    {
        EditorGUILayout.BeginVertical("Box");
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name: " + lang.Name);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Content: ");
        contents[index] = EditorGUILayout.TextArea(contents[index]);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.EndVertical();
    }

    private void AddContents()
    {
        Undo.RecordObject(database, "Content Added");
        for (int i = 0; i < database.LanguajesCount; i++)
            database.AddContent(database.GetLanguaje(i).Name, contents[i]);
        EditorUtility.SetDirty(database);
        window.Close();
    }
}
