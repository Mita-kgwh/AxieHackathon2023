using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;
    
    public LinePlayer player;

    void Start()
    {
        //player = GetComponent<LinePlayer>();
        Debug.Log(player);
        //if (player == null)
        //    Debug.LogError("Player Null Exception!");
        //Debug.Log(this);
    }


    // Update is called once per frame
    void Update()
    {
        if (player)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.OnTurnUp();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                player.OnTurnDown();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                player.OnTurnLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //PlayerController.Instance.mainPlayer.OnTurnRight();

                player.OnTurnRight();
            }
#endif
        }

        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Moved:


                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    // MOVE RIGHT      
                                    Debug.Log("Player move right");
                                    player.OnTurnRight();
                                }
                                else
                                {
                                    // MOVE LEFT
                                    Debug.Log("Player move left");
                                    player.OnTurnLeft();
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    // MOVE UP
                                    Debug.Log("Player move up");
                                    player.OnTurnUp();
                                }
                                else
                                {
                                    // MOVE DOWN
                                    Debug.Log("Player move down");
                                    player.OnTurnDown();
                                }
                            }

                        }

                        break;
                }
            }
        }

    }
}
