using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Util : MonoBehaviour{

		public enum spellEnum{fire, water, life, death, earth, storm, arcane};

/* The old way of handling combat.
		public static float calculateDamage(Player attacker, Player target){
			bool hit = Random.Range(0.0f, 1.0f) <= attacker.attackChance;
			if(hit) {
				Debug.Log(attacker.playerName + "'s attack against " + target.playerName + " was succesful.");
				float damage;
				damage = attacker.damageBase - attacker.damageBase * target.armor;
				damage -= Util.calculateDistance(attacker, target) * .05f;
				if(damage <= 0) {
					return 0;
				} else {
					return damage;
				}
			} else {
				Debug.Log(attacker.playerName + "'s attack against " + target.playerName + " missed.");
				return 0.0f;
			}
		}
*/

		public static float calculateDistance(GameEntity a, GameEntity b){
			float distance = (float) System.Math.Sqrt(System.Math.Pow(a.getPosition().x - b.getPosition().x, 2) + System.Math.Pow(a.getPosition().y - b.getPosition().y, 2));
			return distance;
		}

		public static void highlightTilesAt(Vector2 originLocation, Color highlightColor, int distance) {
			List<GameTile> highlightedTiles = TileHighlighter.FindHighlight(GameManager.instance.map[(int)originLocation.x][(int)originLocation.y], distance);
			foreach(GameTile tile in highlightedTiles){
				tile.transform.GetComponent<Renderer>().material.color = highlightColor;
			}
		}

		public static void removeHighlights(){
			for(int i = 0; i < GameManager.instance.mapSize; i++){
				for(int j = 0; j < GameManager.instance.mapSize; j++){
					GameManager.instance.map[i][j].transform.GetComponent<Renderer>().material.color = GameManager.instance.tileColor;
				}
			}
		}

		public static void attributeXP(int XP){
			foreach(Player player in GameManager.instance.players){
				player.XP += XP;
				player.checkLevel();
			}
		}

		public static void animateSpell(Spell spell, Vector3 location){
			GameObject spellAnimation;
			switch(spell.typeOfSpell){
				case spellEnum.fire:
					spellAnimation = GameManager.instance.firePrefab;
					break;
				case spellEnum.water:
					spellAnimation = GameManager.instance.waterPrefab;
					break;
				case spellEnum.life:
					spellAnimation =  GameManager.instance.lifePrefab;
					break;
				case spellEnum.storm:
					spellAnimation =  GameManager.instance.stormPrefab;
					break;
				case spellEnum.earth:
					spellAnimation =  GameManager.instance.earthPrefab;
					break;
				case spellEnum.death:
					spellAnimation =  GameManager.instance.deathPrefab;
					break;
				case spellEnum.arcane:
				default:
					spellAnimation =  GameManager.instance.arcanePrefab;
					break;
			}
			GameObject spellBurst = ((GameObject)Instantiate(spellAnimation, location, Quaternion.Euler(new Vector3())));
			Destroy(spellBurst, 0.5f);
		}
}
