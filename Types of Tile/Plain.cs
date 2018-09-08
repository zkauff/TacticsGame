using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plain : GameTile {
	public GameObject plainArtwork;

	protected void Awake() {
		base.Awake();

	}

	public override void setup(){
		this.movementCost = 1;
		Instantiate(plainArtwork, new Vector3(this.transform.position.x, this.transform.position.y, 0.02f), Quaternion.Euler(new Vector3()));
	}

	void OnMouseDown(){
		base.OnMouseDown();
	}
}
