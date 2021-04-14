using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinPlayer {
    public int id;
    public float cost;
    public bool purchased, tintable, colorRandom;
    public Color colorSelect;
    public Sprite skin;
    public List<AudioClip> winClips, backgroundMusics;

    public SkinPlayer(int _id){
        id = _id;
        cost = 0f;
        purchased = tintable = false;
        colorSelect = Color.white;
        skin = null;
        winClips = new List<AudioClip>();
        backgroundMusics = new List<AudioClip>();
    }
}
