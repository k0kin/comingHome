using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Side
{
    Down,
    Right, 
    Up,
    Left
}


public class AddObjectToSidesList : MonoBehaviour
{
    [SerializeField] private ObjectsToActivateSides scriptableList;
    [SerializeField] private Side side;

    private void Start()
    {
        PutObjInCorrectListSide();
    }
    
    private void PutObjInCorrectListSide()
    {
        switch (side)
        {
            case Side.Down:
                scriptableList.downSide.Add(gameObject);
                break;
            case Side.Right:
                scriptableList.rightSide.Add(gameObject);
                break;
            case Side.Up:
                scriptableList.upSide.Add(gameObject);
                break;
            case Side.Left:
                scriptableList.leftSide.Add(gameObject);
                break;
        }
    }
}
