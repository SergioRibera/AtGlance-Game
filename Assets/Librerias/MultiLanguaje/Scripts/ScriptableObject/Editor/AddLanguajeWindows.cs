using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AddLanguajeWindows : EditorWindow {

    private static ScriptableLanguaje database;
    private static EditorWindow window;

    static string[] contents;
    static string firstLanguaje;

    private static Languaje newItem;
    private GUILayoutOption[] options = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(20.0f) };

    static GUIStyle h1 = new GUIStyle();
    static GUIStyle textAreaStyle = new GUIStyle();
    static GUIStyle valueStyle = new GUIStyle();
    public static void ShowWindow(ScriptableLanguaje db)
    {
        database = db;
        window = GetWindow<AddLanguajeWindows>();
        window.minSize = new Vector2(300, 380);
        newItem = new Languaje();
        textAreaStyle.wordWrap = true;
        valueStyle.wordWrap = true;
        valueStyle.alignment = TextAnchor.MiddleLeft;
        h1.fontSize = 16;
        window.titleContent = new GUIContent("Add new Idioma");
        if(db.LanguajesCount > 0){
            contents = new string[db.LanguajeContentCount(db.GetLanguaje(0).Name)];
            firstLanguaje = db.GetLanguaje(0).Name;
        } else{
            contents = new string[0];
            firstLanguaje = string.Empty;
        }
    }

    public void OnGUI()
    {
        DisplayItem(newItem);
        if (GUILayout.Button("Confirm"))
            AddItem();
    }
    private void DisplayItem(Languaje idioma)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name: ");
        idioma.Name = EditorGUILayout.TextField(idioma.Name, options);
        EditorGUILayout.EndHorizontal();
        if(database.LanguajesCount > 0 && firstLanguaje != string.Empty){
            GUILayout.Label("Contents: ");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(database.GetLanguaje(firstLanguaje).Name, h1);
            GUILayout.Label("New Idioma", h1);
            EditorGUILayout.EndHorizontal();
            for (int i = 0; i < database.LanguajeContentCount(firstLanguaje); i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(database.GetLanguaje(firstLanguaje).content[i].content, valueStyle);
                EditorGUILayout.BeginVertical();
                contents[i] = EditorGUILayout.TextField(contents[i], GUILayout.MinWidth(150.0f));
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }

    private void AddItem()
    {
        Undo.RecordObject(database, "Item Added");
        database.AddLanguaje(newItem);
        var agregedIdioma = database.GetLanguaje(newItem.Name);
        if(contents.Length > 0)
            foreach (var content in contents)
                agregedIdioma.AddContent(content);
        EditorUtility.SetDirty(database);
        window.Close();
    }
}
