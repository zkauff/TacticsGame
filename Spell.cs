using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell {

	public Util.spellEnum typeOfSpell;
	public bool spawner = false;
	public string spellName;
	public int damage;
	public int cost;
	public bool coolingDown;
	public int coolDownTurns = 4;
	public int coolDownTurnsLeft;

	public Spell(string name, int damage, int cost){
		this.spellName = name;
		this.damage = damage;
		this.cost = cost;
	}

	public abstract void effect(Player target);

	public void cast(Player caster, Player target) {
		caster.curMP -= this.cost;
		if(target != null){
			target.takeDamage(this.damage);
			this.effect(target);
		}
		this.coolingDown = true;
		this.coolDownTurnsLeft = coolDownTurns;
		caster.displaySpells = false;
	}


}
