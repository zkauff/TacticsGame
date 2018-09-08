using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : GameTile {
	public GameObject wallArtwork;

	protected void Awake() {
			base.Awake();
	}

	public override void setup(){
		Instantiate(wallArtwork, new Vector3(this.transform.position.x, this.transform.position.y, -0.05f), Quaternion.Euler(new Vector3()));
		this.movementCost = 100;
	}


	void OnMouseDown(){
		base.OnMouseDown();
	}
}
