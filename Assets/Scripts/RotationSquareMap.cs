using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationSquareMap : MonoBehaviour
{
    public float duration = 1f;
    public float rotationTo;

    [Space] [Header("Scriptable List Sides")] 
    [SerializeField] private ObjectsToActivateSides scriptableList;
    
    [Space] 
    [Header("Walls Transform")] public Transform walls;
    [Header("Pivots")] public Transform pivotDownR;
    public Transform pivotDownL;
    [Space] public float timer = 0f;
    public bool canRotate = false;

    [Header("Trigger Pivots")] 
    [SerializeField] private BoxCollider2D leftPivot;
    [SerializeField] private BoxCollider2D rightPivot;
    
    private bool leftTriggerActive = false;
    private bool rightTriggerActive = false;

    private Transform currentParent;

    private void Awake()
    {
        scriptableList.Restore();
    }

    void Update()
    {
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
                    lerp);

                timer += Time.deltaTime;

            }
            else
            {
                canRotate = false;
                timer = 0f;

                currentParent.rotation = Quaternion.Euler(0, 0, rotationTo);

                ReubicateTriggers();

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

        BoxCollider2D box = currentParent.GetComponent<BoxCollider2D>();
                
        if(rightTriggerActive)
        {
            switch (rotation)
            {
                case 0:  //DOWN
                    box.offset = new Vector2(5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
                case 90:   //LEFT
                    box.offset = new Vector2(0, -5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case -90:   //RIGHT
                    box.offset = new Vector2(0, 5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case 180:    //UP
                    box.offset = new Vector2(-5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
                case -180:    //UP
                    box.offset = new Vector2(-5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
                case 270:   //LEFT
                    box.offset = new Vector2(0, 5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case -270:    //RIGHT
                    box.offset = new Vector2(0, -5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case 360:     //DOWN
                    box.offset = new Vector2(5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
            }

            rightTriggerActive = false;
            rightPivot.enabled = true;
        }

        if (leftTriggerActive)
        {
            switch (rotation)
            {
                case 0:
                    box.offset = new Vector2(-5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
                case 90:
                    box.offset = new Vector2(0, 5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case -90:
                    box.offset = new Vector2(0, -5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case 270:
                    box.offset = new Vector2(0, -5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case -270:
                    box.offset = new Vector2(0, 5);
                    box.size = new Vector2(11.7f, 1.3f);
                    break;
                case 180:
                    box.offset = new Vector2(5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
                case -180:
                    box.offset = new Vector2(5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
                case 360:
                    box.offset = new Vector2(-5, 0);
                    box.size = new Vector2(1.3f, 11.7f);
                    break;
            }

            leftTriggerActive = false;
            leftPivot.enabled = true;
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
        leftPivot.enabled = false;

    }

    public void RotateRight()
    {
        walls.parent = pivotDownR;
        currentParent = pivotDownR;

        rotationTo = currentParent.rotation.eulerAngles.z - 90f;
        canRotate = true;
        timer = 0f;

        rightTriggerActive = true;
        rightPivot.enabled = false;

    }

}
