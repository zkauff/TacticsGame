using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBaddie : UserPlayer {


		public override void initializeSpells(){
			Fireball cataclysm = new Fireball(15, 10);
			spells.Add(cataclysm);
			SpawnWall wall = new SpawnWall(50);
			spells.Add(wall);
		}

		// Use this for initialization
		void Start () {
			this.maxHP = 30;
			this.curHP = maxHP;
			this.maxMP = 50;
			this.curMP = maxMP;
			this.movementPerTurn = 5;
			this.attackChance = .9f;
			this.strength = 7;
			this.XPForKill = 1000;
		}

		public override void levelUp(){
			this.playerLevel++;
			this.maxHP += 5;
			this.curHP += 5;
			if(this.playerLevel % 3 == 0){
				this.movementPerTurn++;
			}
			this.strength++;
			if(this.playerLevel % 5 == 0){
				armor += .10f;
			}
			Debug.Log(this.playerName + "leveled up! They are level " + playerLevel);
			}


}
