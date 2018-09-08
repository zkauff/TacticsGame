using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skal : UserPlayer {

	void Start() {
		this.maxHP = 40;
		this.curHP = maxHP;
		this.strength = 6;
		this.XPForKill = 1000;
	}



	public override void levelUp() {
		this.playerLevel++;
		this.maxHP += 3;
		this.curHP += 2;
		this.XPForKill += 150;
		if(this.playerLevel % 2 == 0){
			this.movementPerTurn++;
		}
		this.strength += 2;
		Debug.Log(this.playerName + "leveled up! They are level " + playerLevel);
	}
}
