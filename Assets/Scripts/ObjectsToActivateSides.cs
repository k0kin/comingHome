using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =  "ScriptableObjects/ObjToActivateSides")]
public class ObjectsToActivateSides : ScriptableObject
{
    public List<GameObject> downSide = new List<GameObject>();
    public List<GameObject> rightSide= new List<GameObject>();
    public List<GameObject> upSide = new List<GameObject>();
    public List<GameObject> leftSide = new List<GameObject>();

    public void Restore()
    {
        downSide.Clear();
        rightSide.Clear();
        upSide.Clear();
        leftSide.Clear();
    }
}
