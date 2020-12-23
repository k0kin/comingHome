using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private List<Transform> weapons;
    
    void Start()
    {
        weapons = new List<Transform>();
        
        foreach (Transform child in transform)
            weapons.Add(child);
        
        foreach (Transform c in weapons)
        {
            if (c == weapons[0])
            {
                c.gameObject.SetActive(true);
                continue;
            }
                
            c.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Transform c in weapons)
            {
                if (c == weapons[0])
                {
                    c.gameObject.SetActive(true);
                    continue;
                }
                
                c.gameObject.SetActive(false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (Transform c in weapons)
            {
                if (c == weapons[1])
                {
                    c.gameObject.SetActive(true);
                    continue;
                }
                
                c.gameObject.SetActive(false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (Transform c in weapons)
            {
                if (c == weapons[2])
                {
                    c.gameObject.SetActive(true);
                    continue;
                }
                
                c.gameObject.SetActive(false);
            }
        }
    }
}
