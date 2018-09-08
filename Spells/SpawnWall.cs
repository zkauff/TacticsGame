using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//not functional
public class SpawnWall : Spawner {

	public SpawnWall(int cost) : base("Create Wall", cost){
		typeOfSpell = Util.spellEnum.earth;
	}
	public override void spawn(Player caster, GameTile targetLocation){
		Util.removeHighlights();
		GameManager.instance.map[(int)targetLocation.gridPosition.x][(int)targetLocation.gridPosition.y] = ((GameObject)MonoBehaviour.Instantiate(GameManager.instance.WallPrefab, new Vector3(targetLocation.transform.position.x, targetLocation.transform.position.y, 0), Quaternion.Euler(new Vector3()))).GetComponent<Wall>();
		MonoBehaviour.Destroy(targetLocation);
		foreach(List<GameTile> row in GameManager.instance.map){
			foreach(GameTile tile in row){
				tile.generateNeighbors();
			}
		}
		caster.curMP -= this.cost;
		this.coolingDown = true;
		this.coolDownTurnsLeft = coolDownTurns;
		GameManager.instance.map[(int)targetLocation.gridPosition.x][(int)targetLocation.gridPosition.y].transform.GetComponent<Renderer>().material.color = GameManager.instance.tileColor;
	}
}
