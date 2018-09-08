// @author: zkauff (aka ztk, Zachary Kauffman)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
General class hierarchy:
	GameManager manages the game (duh)
	Util handles various static methods, such as calculating distance, calculating the attack/defense values in combat, etc.
	GameEntity is a class that contains one variable and one corresponding get method: a gridPosition.
	GameTile implements GameEntity and represents an individual tile in the grid system.
	Player also implements GameEntity and is inherited by AIPlayer (not yet implmented) and UserPlayer.
	UserPlayer is then extended by various game classes, such as warlock, mage, archer, etc.

Thanks to Paul Metcalf, his youtube videos really helped me figure out how to use Unity and how to structure a game like this.
Music courtesy of bensound.com
*/
public class GameManager : MonoBehaviour {
	public static GameManager instance;
	//tile types
	public GameObject GameTilePrefab, ForestPrefab, PlainPrefab, WallPrefab;
	//character types
	public GameObject TentacleEnemyPrefab;
	public GameObject WarlockPrefab;
	public GameObject SkalPrefab;
	public GameObject HeartBaddiePrefab;
	public GameObject HeroPrefab;
//public GameObject AIPlayerPrefab;


	//spell animation prefabs
	public GameObject firePrefab, waterPrefab, lifePrefab, deathPrefab, earthPrefab, stormPrefab, arcanePrefab;

	public int mapSize = 11;
	public bool endlessMode;
	public int endlessCount = 1;
	public List <List<GameTile>> map = new List<List<GameTile>>();
	public List <Player> players = new List<Player>();
	public int currentPlayerIndex = 0;
	public Color tileColor, redColor, blueColor, greenColor, spellColor;

	public GUISkin skin = null;

	void Awake() {
		instance = this;
		instance.endlessMode = true;
	}
	// Use this for initialization
	void Start() {
			generatePlayers();
			generateMap();
	}

	// Update is called once per frame
	void Update() {
		players[currentPlayerIndex].TurnUpdate();
	}

	void OnGUI(){
		players[currentPlayerIndex].TurnOnGUI();
	}

	public void nextTurn(){
		Util.removeHighlights();
			if(currentPlayerIndex + 1 < players.Count){
				currentPlayerIndex++;
			} else {
				currentPlayerIndex = 0;
			}
			players[currentPlayerIndex].resetStats();
			if(players[currentPlayerIndex].curMP < players[currentPlayerIndex].maxMP - 1){
				players[currentPlayerIndex].curMP += 2;
			}
			foreach(Spell spell in players[currentPlayerIndex].spells){
				if(spell.coolingDown){
					spell.coolDownTurnsLeft--;
					if(spell.coolDownTurnsLeft == 0){
						spell.coolingDown = false;
					}
				}
			}
	}


	void generateMap() {
		map = new List<List<GameTile>>();
		for(int i = 0; i < mapSize; i++){
			List<GameTile> row = new List<GameTile>();
			for(int j = 0; j < mapSize; j++){
				if((i == 5 || i == 6) && (j == 5 || j == 6)){
					GameTile tile = ((GameObject)Instantiate(ForestPrefab, new Vector3(i - Mathf.Floor(mapSize/2), -j + Mathf.Floor(mapSize/2), 0), Quaternion.Euler(new Vector3()))).GetComponent<GameTile>();
					tile.gridPosition = new Vector2(i,j);
					row.Add(tile);
				} else if (i != 9 || i == 9 && j == 4) {
					GameTile tile = ((GameObject)Instantiate(PlainPrefab, new Vector3(i - Mathf.Floor(mapSize/2), -j + Mathf.Floor(mapSize/2), 0), Quaternion.Euler(new Vector3()))).GetComponent<GameTile>();
					tile.gridPosition = new Vector2(i,j);
					row.Add(tile);
				} else{
					GameTile tile = ((GameObject)Instantiate(WallPrefab, new Vector3(i - Mathf.Floor(mapSize/2), -j + Mathf.Floor(mapSize/2), 0), Quaternion.Euler(new Vector3()))).GetComponent<GameTile>();
					tile.gridPosition = new Vector2(i,j);
					row.Add(tile);
				}
			}
			map.Add(row);
		}
	}

	void generatePlayers(){
		UserPlayer player;
		player = ((GameObject)Instantiate(WarlockPrefab, new Vector3(0- Mathf.Floor(mapSize/2), 0 + Mathf.Floor(mapSize/2), -1.5f), Quaternion.Euler(new Vector3()))).GetComponent<Warlock>();
		player.gridPosition = new Vector2(0, 0);
		players.Add(player);
		player = ((GameObject)Instantiate(TentacleEnemyPrefab, new Vector3( (mapSize - 1) - Mathf.Floor(mapSize/2), - (mapSize - 1) + Mathf.Floor(mapSize/2), -1.5f), Quaternion.Euler(new Vector3()))).GetComponent<TentacleEnemy>();
		player.gridPosition = new Vector2(mapSize - 1, mapSize - 1);
		players.Add(player);
		player = ((GameObject)Instantiate(SkalPrefab, new Vector3(4 - Mathf.Floor(mapSize/2), -4 + Mathf.Floor(mapSize/2), -1.5f), Quaternion.Euler(new Vector3()))).GetComponent<Skal>();
		player.gridPosition = new Vector2(4, 4);
		players.Add(player);
		player = ((GameObject)Instantiate(HeroPrefab, new Vector3(7 - Mathf.Floor(mapSize/2), 0 + Mathf.Floor(mapSize/2), -1.5f), Quaternion.Euler(new Vector3()))).GetComponent<Hero>();
		player.gridPosition = new Vector2(7, 0);
		players.Add(player);

		player = ((GameObject)Instantiate(HeartBaddiePrefab, new Vector3(8 - Mathf.Floor(mapSize/2), -2 + Mathf.Floor(mapSize/2), -1.5f), Quaternion.Euler(new Vector3()))).GetComponent<HeartBaddie>();
		player.gridPosition = new Vector2(8, 2);
		players.Add(player);
	//	EnemyPlayer ai = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(8 - Mathf.Floor(mapSize/2), 1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<EnemyPlayer>();
  //	players.Add(ai);
	}

	void generateMoreEnemies(){
		//change to AIPlayer when AI is finished
		UserPlayer enemy;
		int row;
		int col;
		for(int i = 0; i < 3; i++){
			row = (int) Random.Range(0.0f, mapSize);
			col = (int) Random.Range(0.0f, mapSize);
			enemy = ((GameObject)Instantiate(TentacleEnemyPrefab, new Vector3(row - Mathf.Floor(mapSize/2), - col + Mathf.Floor(mapSize/2), -1.5f), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
			enemy.gridPosition = new Vector2(row, col);
			for(int j = 0; j < endlessCount; j++){
				enemy.levelUp();
			}
			if(enemy.gridPosition != this.players[currentPlayerIndex].gridPosition){
				players.Add(enemy);
			}
		}
	}

	public void moveCurrentPlayer(GameTile destTile){
		if(destTile.transform.GetComponent<Renderer>().material.color == instance.blueColor ){
			bool canMove = true;
			foreach(Player enemy in players){
				if(enemy.gridPosition == destTile.gridPosition){
					canMove = false;
					Debug.Log("Two players cannot occupy the same space");
				}
			}
			if(canMove){
				players[currentPlayerIndex].moveDestination = destTile.transform.position + new Vector3(0, 0, - 1.5f);
				players[currentPlayerIndex].moving = false;

				foreach(GameTile tile in TilePathFinder.FindPath(map[(int)players[currentPlayerIndex].gridPosition.x][(int)players[currentPlayerIndex].gridPosition.y], destTile).tiles){
					players[currentPlayerIndex].positionQueue.Add(map[(int)tile.gridPosition.x][(int)tile.gridPosition.y].transform.position);
				}
				players[currentPlayerIndex].gridPosition = destTile.gridPosition;
				foreach(Vector3 tile in players[currentPlayerIndex].positionQueue){
					players[currentPlayerIndex].moveDestination = tile + new Vector3(0, 0, - 2.5f);
				}
			}
		} else{
			Debug.Log("You cannot move that far.");
		}

	}

	public void attackWithCurrentPlayer(GameTile destTile){
		Player target = null;
		foreach(Player enemy in players){
			if(enemy.gridPosition == destTile.gridPosition && destTile.transform.GetComponent<Renderer>().material.color == instance.redColor){
				target = enemy;
			}
		}
		//a player cannot target themselves.
		if(target != null && target != players[currentPlayerIndex]){
			players[currentPlayerIndex].hasAttacked = true;
			players[currentPlayerIndex].attacking = false;
			players[currentPlayerIndex].equippedWeapon.attack(target);
			if(target.curHP <= 0){
				target.die();
			}
			Util.removeHighlights();
		} else{
			Debug.Log("Please select a valid target");
		}
	}


		public void castSpellAt(GameTile destTile){
			Player source = players[currentPlayerIndex];
			Player target = null;
			foreach(Player enemy in players){
				if(enemy.gridPosition == destTile.gridPosition && destTile.transform.GetComponent<Renderer>().material.color == instance.spellColor){
					target = enemy;
				} else if(destTile.transform.GetComponent<Renderer>().material.color == instance.greenColor){
					target = source;
				}
			}
			if(target == null && source.casting.spawner){
				((Spawner)source.casting).spawn(source, destTile);
				source.hasAttacked = true;
				Util.removeHighlights();
			} else if(target != null && !source.casting.spawner){ //a player CAN target themselves with a spell
				source.castingSpell = false;
				source.casting.cast(source, target);
				Util.animateSpell(source.casting, target.transform.localPosition);
				if(source.casting.damage > 0){
					target.hurt();
				} else if(source.casting.damage < 0){
					target.heal();
				}
				target.takeDamage(source.casting.damage - source.casting.damage * target.magicResistance);
				if(target.curHP <= 0){
					target.die();
				}
				source.hasAttacked = true;
				Util.removeHighlights();
			} else{
				Debug.Log("Please select a valid target");
			}

		}



	public void gameWin(Player winner){
		if(!endlessMode){
			Debug.Log(winner.playerName + " has won!");
			Application.Quit();
		//	SceneManager.LoadScene(1);
		}	else{
			this.generateMoreEnemies();
		}
	}

}
