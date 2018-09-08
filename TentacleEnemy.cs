using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TentacleEnemies are the most common, and one of the weakest, enemies in the game.
public class TentacleEnemy : UserPlayer {

	void Start() {
		this.maxHP = 10;
		this.curHP = maxHP;
		this.XPForKill = 100;
	}



	public override void levelUp(){
		this.playerLevel++;
		this.maxHP += 5;
		this.curHP += 2;
		this.XPForKill += 100;
		if(this.playerLevel % 3 == 0){
			this.movementPerTurn++;
		}
		this.strength++;
		Debug.Log(this.playerName + "leveled up! They are level " + playerLevel);
	}
}
