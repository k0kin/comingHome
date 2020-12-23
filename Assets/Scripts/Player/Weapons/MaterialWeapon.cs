using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialWeapon : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private float maxLenght;
    [SerializeField] private GameObject prefabPlatform;

    private Vector2 mousePos;

    private bool isAiming = false;

    private Transform weaponSprite;
    private Camera mainCamera;
    
    void Start()
    {
        weaponSprite = transform.GetChild(0);
        
        mainCamera = Camera.main;
        weaponSprite.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isAiming = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var mousePos2 = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject platform = Instantiate(prefabPlatform, mousePos2, Quaternion.identity);
            
            transform.parent.parent.position = new Vector3(platform.transform.position.x, platform.transform.position.y + 1.5f, 0f);

            isAiming = false;
        }
        
        if(isAiming)
        {
            lr.enabled = true;
            
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            lr.SetPosition(0, transform.position);

            lr.SetPosition(1, mousePosition);

            Vector3 lookAtDirection = mousePosition - weaponSprite.position;
            weaponSprite.up = lookAtDirection;
        }
        else
        {
            lr.enabled = false;
            
            weaponSprite.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }
    
    
}
