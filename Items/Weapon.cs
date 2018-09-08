using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item {
		public int damage;
		public int attackRange;
		public float critModifier = 1.5f;
		public float critChance;
		public int durability;

		public Weapon(string weaponName, int damage, int attackRange, int durability, float critChance) : base(weaponName){
			this.damage = damage;
			this.attackRange = attackRange;
			this.durability = durability;
			this.critChance = critChance;
			//critModifier is always 1.0 unless the weapon is a special variety. As such, it will be dealt with in subclasses or changed on specific weapon instances.
		}

		public virtual void attack(Player target){
			Random.seed = (int)Time.time;
			bool hit = Random.Range(0.0f, 1.0f) <= this.owner.attackChance;
			Debug.Log(this.owner.playerName + "attacks " + target.playerName + " with " + this.name + ". ");
			if(hit){
				float damage = this.damage - this.damage * target.armor;
				damage += .3f * this.owner.strength;
				damage -= Util.calculateDistance(this.owner, target) * .05f; //find a way to ignore this on bows, guns
				if(damage > 0){
					target.hurt();
				}
				bool crit = Random.Range(0.0f, 1.0f) <= this.critChance;
				if(crit){
					damage = damage * critModifier;
					Debug.Log("Critical hit!");
				}
				target.takeDamage(damage);
				Debug.Log("The attack is successful, and deals " + damage + "damage.");
			} else{
				Debug.Log("The attack missed.");
			}
			this.durability -= 1;
		}

		public override void use(){
			GameManager.instance.players[GameManager.instance.currentPlayerIndex].equippedWeapon = this;
			Debug.Log(GameManager.instance.players[GameManager.instance.currentPlayerIndex].playerName + " equipped the " + this.name);
		}
}
