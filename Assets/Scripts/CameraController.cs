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

	void FixedUpdate() 
	{
		if(followY)
		{
			transform.position = player.transform.position + offset; 
		}
		else
		{
			Vector3 desiredPos = new Vector3(Mathf.Clamp(player.transform.position.x, walls.position.x - offsetWalls.x, walls.position.x + offsetWalls.y), -11.4f, -10f);;
			
			Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, 1.5f);

			transform.position = smoothedPos;
		}
	}
}