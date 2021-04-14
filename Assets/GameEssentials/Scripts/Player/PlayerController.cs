using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour {

    [SerializeField] float playerVelocity = 2f;

    [Header("Detection System")]
    [SerializeField] List<Transform> pointsToDetect = new List<Transform>();
    [SerializeField] float maxDistanceDetect = 3, minDistanceDetect = .5f;

    // Privates

    float distance, size;
    Rigidbody2D rb;
    Vector2 pos, inputMovePlayer, inputMoveCursor;
    InputActions inputActions;
    float x, y;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        pos = Vector2.zero;
        inputActions = new InputActions();
        inputActions.Player.MovePlayer.performed += ctx => {
            inputMovePlayer = ctx.ReadValue<Vector2>();
        };
        inputActions.Player.MovePlayer.canceled += ctx => inputMovePlayer = Vector2.zero;

        inputActions.Player.MoveCursor.performed += ctx => inputMoveCursor = ctx.ReadValue<Vector2>();
        inputActions.Player.MoveCursor.canceled += ctx => inputMoveCursor = Vector2.zero;
        inputActions.Player.ClickCursor.performed += ctx => {
            if (Physics2D.Raycast(Vector2.zero, inputMoveCursor)){
                print("You Win");
            }
        };
        InputUser.onChange += (user, change, device) => {
            if (change == InputUserChange.ControlSchemeChanged) {
                switch(user.controlScheme.Value.name){
                    case "Gamepad":
                        //buttonImage.sprite = gamepadImage;
                        break;
                    default:
                        //buttonImage.sprite = keyboardImage;
                        break;
                }
            }
        };
    }
    void OnEnable() => inputActions.Enable();
    void Disable() => inputActions.Disable();

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

    Vector3 screenPos;
    void Update(){
        x = inputMovePlayer.x * playerVelocity * Time.deltaTime;
        y = inputMovePlayer.y * playerVelocity * Time.deltaTime;

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

    public void SetPoints(List<Transform> points) => pointsToDetect = points;
    void OnMouseUpAsButton () {
        print("Click Me");
    }
}
