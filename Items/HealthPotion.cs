using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion {
	int healValue;

	public HealthPotion(int healValue, string name) : base(name){
		this.healValue = healValue;
	}

	public override void use(){
		if((GameManager.instance.players[GameManager.instance.currentPlayerIndex].curHP += healValue) <= GameManager.instance.players[GameManager.instance.currentPlayerIndex].maxHP){
			GameManager.instance.players[GameManager.instance.currentPlayerIndex].curHP += healValue;
		} else {
			GameManager.instance.players[GameManager.instance.currentPlayerIndex].curHP = GameManager.instance.players[GameManager.instance.currentPlayerIndex].maxHP;
		}
		Debug.Log(GameManager.instance.players[GameManager.instance.currentPlayerIndex].playerName + " drinks a health potion.");
		GameManager.instance.players[GameManager.instance.currentPlayerIndex].heal();
		GameManager.instance.players[GameManager.instance.currentPlayerIndex].inventory.Remove(this);
	}
}
