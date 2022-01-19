using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;
using SceneEnv;

public class InputController : MonoBehaviour
{
    [SerializeField] float dragDistance = 10f;  

    Vector3 firstTouchPosition;                 
    Vector3 lastTouchPosition;                  
    List<Vector3> touchPositions = new List<Vector3>();


    void Start()
    {
        dragDistance *= Screen.height / 100;
    }

    void Update()
    {
        MobileInput();

#if UNITY_EDITOR
        UnityEditorInput();
#endif
    }

    void MobileInput()
    {
        if (Input.touchCount <= 0)
            return;

        if (Input.touches[0].phase == TouchPhase.Moved)
            touchPositions.Add(Input.touches[0].position);

        if (Input.touches[0].phase == TouchPhase.Ended && touchPositions.Count == 0)
            Main.self.Player.State = States.Jump;

        if (Input.touches[0].phase == TouchPhase.Ended && touchPositions.Count > 0)
        {
            firstTouchPosition = touchPositions[0];
            lastTouchPosition = touchPositions[touchPositions.Count - 1];

            if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) >
                Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y))
            {
                if (lastTouchPosition.x < firstTouchPosition.x)
                    Main.self.Player.State = States.Left;
                if (lastTouchPosition.x > firstTouchPosition.x)
                    Main.self.Player.State = States.Right;
            }
            touchPositions.Clear();
        }
    }

    void UnityEditorInput()
    {
        if (Input.GetKeyDown("space"))
            Main.self.Player.State = States.Jump;

        if (Input.GetKeyDown("left"))
            Main.self.Player.State = States.Left;

        if (Input.GetKeyDown("right"))
            Main.self.Player.State = States.Right;
    }
}
