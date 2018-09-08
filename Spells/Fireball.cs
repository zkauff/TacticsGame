using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell {

	public Fireball(int damage, int cost) : base("Fireball", damage, cost){
		if(damage < 10){
			this.spellName = "Lick of Flame";
		} else if(damage > 10 && damage < 20){
			this.spellName = "Fireball";
		} else if(damage > 20 && damage < 50){
			this.spellName = "Fireblast";
		} else{
			this.spellName = "CATACLYSMIC FLAMES";
		}
		typeOfSpell = Util.spellEnum.fire;
	}

	public override void effect(Player target){
		//no effects besides damage. Down the line, if fire is implemented, maybe there will be a chance to set on fire?
	}

}
