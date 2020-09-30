using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationSquareMap : MonoBehaviour
{
    public float duration = 1f;
    public float rotationTo;

    [Space] [Header("Walls Transform")] public Transform walls;
    [Header("Pivots")] public Transform pivotDownR;
    public Transform pivotDownL;
    [Space] public float timer = 0f;
    public bool canRotate = false;
    
    private bool leftTriggerActive = false;
    private bool rightTriggerActive = false;

    private Transform currentParent;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Q) && canPressKey)
        {
            walls.parent = pivotDownL;
            currentParent = pivotDownL;
    
            rotationTo = currentParent.rotation.eulerAngles.z + 90f;
            canRotate = true;
            timer = 0f;
        }
        
        if (Input.GetKeyDown(KeyCode.E) && canPressKey)
        {
            walls.parent = pivotDownR;
            currentParent = pivotDownR;
    
            rotationTo = currentParent.rotation.eulerAngles.z - 90f;
            canRotate = true;
            timer = 0f;
        }
        */
        
        if (!canRotate)
            return;
        else
        {
            if (timer <= duration)
            {
                //canPressKey = false;
                
                float lerp = timer / duration;
                //print(lerp);
                currentParent.rotation = Quaternion.Lerp(currentParent.rotation, Quaternion.Euler(0f, 0f, rotationTo),
                    lerp / 15f);

                timer += Time.deltaTime;

            }
            else
            {
                canRotate = false;
                timer = 0f;

                currentParent.rotation = Quaternion.Euler(0, 0, rotationTo);
                
                //print(currentParent.rotation.eulerAngles.z);
                
                ReubicateTriggers();

                //canPressKey = true;

                walls.parent = transform;

                Vector3 wallsPos = walls.localPosition;

                pivotDownL.localPosition = new Vector3(wallsPos.x - 15f, wallsPos.y - 15f, 0f);
                pivotDownR.localPosition = new Vector3(wallsPos.x + 15, wallsPos.y - 15f, 0f);

            }
        }
    }

    private void ReubicateTriggers()
    {
        int rotation = Mathf.RoundToInt(rotationTo);

        //rotation = Mathf.Abs(rotation);
                
        print(rotation);

        BoxCollider2D box = currentParent.GetComponent<BoxCollider2D>();
                
        if(rightTriggerActive)
        {
            if (rotation == 0)
            {
                box.offset = new Vector2(5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }
            else if (rotation == 90)
            {
                box.offset = new Vector2(0, -5);
                box.size = new Vector2(11.7f, 1.3f);
            }
            else if (rotation == -90)
            {
                box.offset = new Vector2(0, 5);
                box.size = new Vector2(11.7f, 1.3f);
            }
            else if (rotation == 180)
            {
                box.offset = new Vector2(-5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }
            else if (rotation == -180)
            {
                box.offset = new Vector2(-5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }
            else if (rotation == 270)
            {
                box.offset = new Vector2(0, 5);
                box.size = new Vector2(11.7f, 1.3f);
            }
            else if (rotation == 360)
            {
                box.offset = new Vector2(5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }

            rightTriggerActive = false;
        }

        if (leftTriggerActive)
        {
            if (rotation == 0)
            {
                box.offset = new Vector2(-5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }
            else if (rotation == 90)
            {
                box.offset = new Vector2(0, 5);
                box.size = new Vector2(11.7f, 1.3f);
            }
            else if (rotation == -90)
            {
                box.offset = new Vector2(0, -5);
                box.size = new Vector2(11.7f, 1.3f);
            }
            else if (rotation == 270)
            {
                box.offset = new Vector2(0, -5);
                box.size = new Vector2(11.7f, 1.3f);
            }
            else if (rotation == 180)
            {
                box.offset = new Vector2(5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }
            else if (rotation == -180)
            {
                box.offset = new Vector2(5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }
            else if (rotation == 360)
            {
                box.offset = new Vector2(-5, 0);
                box.size = new Vector2(1.3f, 11.7f);
            }

            leftTriggerActive = false;
        }
    }


    public void RotateLeft()
    {
        walls.parent = pivotDownL;
        currentParent = pivotDownL;

        rotationTo = currentParent.rotation.eulerAngles.z + 90f;
        canRotate = true;
        timer = 0f;

        leftTriggerActive = true;

    }

    public void RotateRight()
    {
        walls.parent = pivotDownR;
        currentParent = pivotDownR;

        rotationTo = currentParent.rotation.eulerAngles.z - 90f;
        canRotate = true;
        timer = 0f;

        rightTriggerActive = true;

    }

}
