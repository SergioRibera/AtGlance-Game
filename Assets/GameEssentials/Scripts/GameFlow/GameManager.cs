using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    [ SerializeField ] SkinsDataBase skinsDB;
    [ SerializeField ] GameObject prefabPlayer;
    [ SerializeField ] Transform parentAlerts;
    [ SerializeField ] Sprite spriteAlerts;

    [ SerializeField ] int alertsOnFirstLevel;
    [ SerializeField ] SkinPlayer skinSelected;

    List<Transform> points;
    int level = 0, multiplyDificulty = 1, alertsToInstantiate = 7;

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
        points = new List<Transform>();
        GameObject player = InstantiateIntoScreenSpace(prefabPlayer);
        SpriteRenderer playerSR = player.GetComponent<SpriteRenderer>();
        skinSelected = skinsDB.GetByID(DataManager.GetIdSkin);
        playerSR.sprite = skinSelected.skin;
        /*playerSR.color = skinSelected.tintable ?
            (skinSelected.colorRandom ? RandomColor : skinSelected.colorSelect) :
            new Color(1, 1, 1, .8f);*/
        playerSR.color = new Color(1, 1, 1, 0.003921569f);
        InstantiateAlerts();
        player.GetComponent<PlayerController>().SetPoints(points);
        yield return null;
    }

    void InstantiateAlerts(){
        GameObject go;
        ClearChildsParentAlerts();
        for (int i = 0; i <= alertsToInstantiate; i++){
            go = InstantiateIntoScreenSpace(null);
            go.name = $"Point {i + 1}";
            var goSR = go.AddComponent<SpriteRenderer>();
            goSR.color = RandomColor;
            goSR.sprite = spriteAlerts;
            goSR.transform.SetParent(parentAlerts);
            points.Add(goSR.transform);
        }
    }
    void ClearChildsParentAlerts(){
        points.Clear();
        for ( int i = 0; i < parentAlerts.childCount; i++ )
            Destroy(parentAlerts.GetChild(i));
    }

    GameObject InstantiateIntoScreenSpace(GameObject go){
        float spawnY = Random.Range (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        GameObject g = go == null ? new GameObject() : Instantiate(go);
        g.transform.SetPositionAndRotation(new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
        return g;
    }

}
