using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;        
	public bool followY = false;
	[SerializeField] private Transform walls;
	[SerializeField] private Vector2 offsetWalls;

	private Vector3 offset;           

	void Start () 
	{
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () 
	{
		if(followY)
		{
			transform.position = player.transform.position + offset; 
		}
		else
		{
			Vector3 newPos;
			
			newPos = new Vector3(player.transform.position.x, -11.4f, -10f);

			transform.position = newPos;
		}
	}
}