using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationSquareMap : MonoBehaviour
{
    public float duration = 1f;
    public float rotationTo;

    [Space]
    [Header("Walls Transform")] 
    public Transform walls;
    [Header("Pivots")]
    public Transform pivotDownR;
    public Transform pivotDownL;
    [Space]
    
    public float timer = 0f;
    public bool canRotate = false;
    private bool canPressKey = true;

    private Transform currentParent;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPressKey)
        {
            // Choose correct pivot depending orientation
            int zAngle = Mathf.RoundToInt(walls.rotation.eulerAngles.z);
            if (zAngle == 0 || zAngle == -360)   // Down
            {
                walls.parent = pivotDownR;
                currentParent = pivotDownR;
            }
            if (zAngle == 180 || zAngle == -180)   // Up
            {
                walls.parent = pivotDownR;
                currentParent = pivotDownR;
            }
            if (zAngle == 90 || zAngle == -270)   // Left
            {
                walls.parent = pivotDownR;
                currentParent = pivotDownR;
            }
            if (zAngle == -90 || zAngle == 270)   // Right
            {
                walls.parent = pivotDownR;
                currentParent = pivotDownR;
            }
            
            print(currentParent);
            
            //transform.parent = pivotDownL;
            //currentParent = pivotDownL;

            rotationTo = currentParent.rotation.eulerAngles.z - 90f;
            canRotate = true;
            timer = 0f;
        }
        
        if (Input.GetKeyDown(KeyCode.Q) && canPressKey)
        {
            // Choose correct pivot depending orientation
            int zAngle = Mathf.RoundToInt(walls.rotation.eulerAngles.z);
            if (zAngle == 0 || zAngle == -360)   // Down
            {
                walls.parent = pivotDownL;
                currentParent = pivotDownL;
            }
            if (zAngle == 180 || zAngle == -180)   // Up
            {
                walls.parent = pivotDownL;
                currentParent = pivotDownL;
            }
            if (zAngle == 90 || zAngle == -270)   // Left
            {
                walls.parent = pivotDownL;
                currentParent = pivotDownL;
            }
            if (zAngle == -90 || zAngle == 270)   // Right
            {
                walls.parent = pivotDownL;
                currentParent = pivotDownL;
            }
            
            print(currentParent);
            
            rotationTo = currentParent.rotation.eulerAngles.z + 90f;
            canRotate = true;
            timer = 0f;
        }
        
        
        if (!canRotate)
            return;
        else
        {
            if (timer <= duration)
            {
                canPressKey = false;
                float lerp = timer / duration;
                //print(lerp);
                currentParent.rotation = Quaternion.Lerp(currentParent.rotation, Quaternion.Euler(0f, 0f, rotationTo), lerp / 15f);

                timer += Time.deltaTime;

            }
            else
            {
                canRotate = false;
                timer = 0f;

                currentParent.rotation = Quaternion.Euler(0, 0, rotationTo);

                canPressKey = true;

                walls.parent = transform;

                Vector3 wallsPos = walls.localPosition;
                
                pivotDownL.localPosition = new Vector3(wallsPos.x - 15f, wallsPos.y -15f, 0f);
                pivotDownR.localPosition = new Vector3(wallsPos.x + 15, wallsPos.y -15f, 0f);

            }
        }

    }
}
