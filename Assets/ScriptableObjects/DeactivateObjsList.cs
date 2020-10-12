using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObjsList : MonoBehaviour
{
    [SerializeField] private ObjectsToActivateSides scriptableList;
    [SerializeField] private Side side;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (side)
            {
                case Side.Down:
                    foreach (GameObject go in scriptableList.downSide)
                        go.SetActive(true);
                    foreach (GameObject go in scriptableList.rightSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.upSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.leftSide)
                        go.SetActive(false);
                    break;
                case Side.Right:
                    foreach (GameObject go in scriptableList.downSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.rightSide)
                        go.SetActive(true);
                    foreach (GameObject go in scriptableList.upSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.leftSide)
                        go.SetActive(false);
                    break;
                case Side.Up:
                    foreach (GameObject go in scriptableList.downSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.rightSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.upSide)
                        go.SetActive(true);
                    foreach (GameObject go in scriptableList.leftSide)
                        go.SetActive(false);
                    break;
                case Side.Left:
                    foreach (GameObject go in scriptableList.downSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.rightSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.upSide)
                        go.SetActive(false);
                    foreach (GameObject go in scriptableList.leftSide)
                        go.SetActive(true);
                    break;
                    
            }
        }
    }
}
