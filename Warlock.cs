using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : UserPlayer {

	public override void initializeSpells(){
		Heal minorHeal = new Heal(15, 10);
		spells.Add(minorHeal);
		Fireball cataclysm = new Fireball(15, 10);
		spells.Add(cataclysm);

	}

	public override void initializeInventory(){
		Weapon starterBow = new Bow("Hero's Bow", 10, 15, .30f);
		starterBow.bestowUpon(this);
		this.equippedWeapon = starterBow;
	}

	void Start () {
		this.maxHP = 50;
		this.curHP = maxHP;
		this.maxMP = 20;
		this.curMP = maxMP;
		this.movementPerTurn = 4;
		this.attackChance = .75f;
		this.strength = 10;
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
