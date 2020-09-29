using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;        
	public bool followY = false;

	private Vector3 offset;           

	void Start () 
	{
		offset = transform.position - player.transform.position;
	}

// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		if(followY)
		{
			transform.position = player.transform.position + offset; 
		}
		else
		{
			transform.position = new Vector3(player.transform.position.x, -12.53f, -10f); 
		}
	}
}