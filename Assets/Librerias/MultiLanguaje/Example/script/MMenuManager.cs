using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMenuManager : MonoBehaviour {

    [SerializeField] string languajeSelected = "en";
    [SerializeField] ScriptableLanguaje dbLanguaje;
    [SerializeField] Dropdown languajeSelect;
    [SerializeField] List<TextForChange> texts;

    void Start(){
        languajeSelect.onValueChanged.AddListener((index) => {
            languajeSelected = dbLanguaje.GetLanguaje(index).Name;
            SetTextsLanguajes();
        });
        var langs = dbLanguaje.GetLanguajes();
        languajeSelect.AddOptions(langs);
        for(int i = 0; i < langs.Count; i++)
            if(langs[i] == languajeSelected)
                languajeSelect.value = i;
        SetTextsLanguajes();
    }
    void SetTextsLanguajes(){
        foreach(var t in texts)
            t.textUI.text = dbLanguaje.GetLanguajeContent(languajeSelected, t.indexContent).content;
    }
}
