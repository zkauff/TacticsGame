using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Spell {

	public Heal(int healAmount, int cost) : base("Heal", 0 - healAmount, cost){
		//rather than 'healing' the game views heal as negative damage
		if(healAmount < 10){
			this.spellName = "Minor Heal";
		} else if(healAmount > 10 && healAmount < 20){
			this.spellName = "Greater Heal";
		} else if(healAmount > 20 && healAmount < 50){
			this.spellName = "Powerful Heal";
		} else{
			this.spellName = "DIVINE HEALING";
		}
		typeOfSpell = Util.spellEnum.life;
	}

	public override void effect(Player target){
		target.curMP += 5;
	}

}
