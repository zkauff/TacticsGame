using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : GameTile {
	public GameObject forestArtwork;

	protected void Awake() {
		base.Awake();
	}

	public override void setup(){
		Instantiate(forestArtwork, new Vector3(this.transform.position.x, this.transform.position.y, 0.02f), Quaternion.Euler(new Vector3()));
		movementCost = 2;
	}

	void OnMouseDown(){
		base.OnMouseDown();
	}
}
