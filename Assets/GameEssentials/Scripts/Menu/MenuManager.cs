using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [ SerializeField ] float speedHueEffect = .25f;

    Camera mainCamera;
    Color initialHue;

    void Awake() => DataManager.Init();
    void Start(){
        mainCamera = Camera.main;
        initialHue = Color.HSVToRGB(.34f, .84f, .67f);
    }

    float h, s, v;
    void Update(){
        Color.RGBToHSV(mainCamera.backgroundColor, out h, out s, out v);
        mainCamera.backgroundColor = Color.HSVToRGB(h + Time.deltaTime * speedHueEffect, s, v);
    }

    public void Play() => SceneManager.LoadScene("Game");

}
