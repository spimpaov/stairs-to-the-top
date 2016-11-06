using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwipeManager : MonoBehaviour
{
    public int angleOffset;
    public Player player;
    public float swipeResistance;

    private float angle = 0;
    private Touch initialTouch;
    private Vector3 swipeDistance;
    private Vector3 cross;
    private Vector3 initialTouchPosition;
    private Vector3 currentTouchPosition;
    private bool hasSwiped = false;

    void Update()
    {
        inputTouch();
    }

    void inputTouch()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                initialTouch = t;
            }

            else if (t.phase == TouchPhase.Moved && !hasSwiped)
            {
                initialTouchPosition = initialTouch.position;
                currentTouchPosition = t.position;
                swipeDistance = currentTouchPosition - initialTouchPosition;

                
                angle = Vector3.Angle(swipeDistance, new Vector3(1, 0, 0));
                if (swipeDistance.y < 0) angle = 360 - angle;                

                Direction swipeType = swipeDirection();

                if (swipeType != Direction.NONE){
                    hasSwiped = true;
					if (swipeType == Direction.RIGHT_UP){
						player.criaEscada (Direction.RIGHT_UP);
						player.move (Direction.RIGHT_UP);
                    }
                    else if (swipeType == Direction.RIGHT_DOWN){
						player.criaEscada (Direction.RIGHT_DOWN);
						player.move (Direction.RIGHT_DOWN);
                    }
                    else if (swipeType == Direction.LEFT_UP){
						player.criaEscada (Direction.LEFT_UP);
						player.move (Direction.LEFT_UP);
                    }
                    else if (swipeType == Direction.LEFT_DOWN){
						player.criaEscada (Direction.LEFT_DOWN);
						player.move (Direction.LEFT_DOWN);
                    }
                }
            }

            else if (t.phase == TouchPhase.Ended){
                initialTouch = new Touch();
                hasSwiped = false;
            }
        }
    }

    bool swipeRightUp() { return angle > 0 + angleOffset && angle < 90 - angleOffset; }
    bool swipeLeftUp() { return angle > 90 + angleOffset && angle < 180 - angleOffset; }
    bool swipeLeftDown() { return angle > 180 + angleOffset && angle < 270 - angleOffset; }
    bool swipeRightDown() { return angle > 270 + angleOffset && angle < 360 - angleOffset; }

    Direction swipeDirection(){
        if (!hasSwiped)
        {
            if (Mathf.Abs(swipeDistance.magnitude) > swipeResistance)
            {
                if (swipeRightUp()) return Direction.RIGHT_UP;
                else if (swipeRightDown()) return Direction.RIGHT_DOWN;
                else if (swipeLeftUp()) return Direction.LEFT_UP;
                else if (swipeLeftDown()) return Direction.LEFT_DOWN;
            }
        }
        return Direction.NONE;
    }
}