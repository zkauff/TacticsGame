using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//generic tile class
public abstract class GameTile : GameEntity {

	public int movementCost = 0;
	public List<GameTile> neighbors = new List<GameTile>();

	protected void Awake() {
			this.setup();
	}

	public abstract void setup();

	protected void Start() {
		generateNeighbors();
	}

	public void generateNeighbors(){
		neighbors = new List<GameTile>();
		//above
		if(gridPosition.y > 0){
			Vector2 neighborPosition = new Vector2(gridPosition.x, gridPosition.y - 1);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(neighborPosition.x)][(int)Mathf.Round(neighborPosition.y)]);
		}
		//below
		if(gridPosition.y < GameManager.instance.mapSize - 1){
			Vector2 neighborPosition = new Vector2(gridPosition.x, gridPosition.y + 1);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(neighborPosition.x)][(int)Mathf.Round(neighborPosition.y)]);
		}
		//to the left
		if(gridPosition.x > 0){
			Vector2 neighborPosition = new Vector2(gridPosition.x - 1, gridPosition.y);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(neighborPosition.x)][(int)Mathf.Round(neighborPosition.y)]);
		}
		//to the right
		if(gridPosition.x < GameManager.instance.mapSize - 1){
			Vector2 neighborPosition = new Vector2(gridPosition.x + 1, gridPosition.y);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(neighborPosition.x)][(int)Mathf.Round(neighborPosition.y)]);
		}
	}


	public void OnMouseDown(){
		if(Input.GetMouseButtonDown(0)){
			if(GameManager.instance.players[GameManager.instance.currentPlayerIndex].moving) {
					GameManager.instance.moveCurrentPlayer(this);
			} else if(GameManager.instance.players[GameManager.instance.currentPlayerIndex].attacking){
					GameManager.instance.attackWithCurrentPlayer(this);
			} else if(GameManager.instance.players[GameManager.instance.currentPlayerIndex].castingSpell){
				GameManager.instance.castSpellAt(this);
			}
		}
	}

}
