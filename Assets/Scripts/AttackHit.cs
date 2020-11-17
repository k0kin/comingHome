using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour {

	[SerializeField] enum AttacksWhat {EnemyBase, Player, EyeEnemy};
	[SerializeField] AttacksWhat attacksWhat;
	private int launchDirection = 1;
	[SerializeField] private GameObject parent;

	void Start () {

	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.GetComponent(attacksWhat.ToString()) != null) {
			if (parent.transform.position.x < col.transform.position.x) {
				launchDirection = 1;
			} else {
				launchDirection = -1;
			}
			col.gameObject.GetComponent(attacksWhat.ToString()).SendMessage("Hit", launchDirection);
		}

	}
}
