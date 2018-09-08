using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : Spell {

	public Spawner(string name, int cost) : base(name, 0, cost){
		this.spawner = true;
	}

	public override void effect(Player target){
	}

	public abstract void spawn(Player caster, GameTile targetLocation);

}
