using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float playerVelocity = 2f;

    [Header("Detection System")]
    [SerializeField] List<Transform> pointsToDetect = new List<Transform>();
    [SerializeField] float maxDistanceDetect = 3, minDistanceDetect = .5f;

    // Privates

    float distance, size;
    Rigidbody2D rb;
    Vector2 pos;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        pos = Vector2.zero;
    }

    void LateUpdate(){
        for (int i = 0; i < pointsToDetect.Count; i++){
            distance = Vector3.Distance(pointsToDetect[i].position, transform.position) / 10 * 2;
            size = (distance / maxDistanceDetect);
            // if distance is more than maxDistanceDetect then return max
            // distance, else if size less than minDistanceDetect then return
            // minDistanceDetect else else return size
            size = size > maxDistanceDetect ? maxDistanceDetect : size < minDistanceDetect ? minDistanceDetect : size;
            pointsToDetect[i].localScale = new Vector3(size, size, 1);
        }
    }

    float x, y;
    Vector3 screenPos;
    void FixedUpdate(){
        x = Input.GetAxisRaw("Horizontal") * playerVelocity * Time.fixedDeltaTime;
        y = Input.GetAxisRaw("Vertical") * playerVelocity * Time.fixedDeltaTime;

        screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y < Screen.height && y > 0) 
            transform.Translate(0, y, 0);
        if (screenPos.y > 0 && y < 0) 
            transform.Translate(0, y, 0);
        if (screenPos.x < Screen.width && x > 0) 
            transform.Translate(x, 0, 0);
        if (screenPos.x > 0 && x < 0) 
            transform.Translate(x, 0, 0);

    }

    void OnMouseDown() {
        print("Click Me");    
    }
}
