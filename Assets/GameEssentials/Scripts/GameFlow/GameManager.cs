using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public List<SkinPlayer> skinsAviable = new List<SkinPlayer>();

    Color RandomColor {
        get {
            return new Color(
                    Random.Range(0f, 1f), 
                    Random.Range(0f, 1f), 
                    Random.Range(0f, 1f)
                );
        }
    }

    IEnumerator Start(){
        yield return null;
    }

    GameObject InstantiateIntoScreenSpace(GameObject go){
        float spawnY = Random.Range (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        return Instantiate(go, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
    }

}
