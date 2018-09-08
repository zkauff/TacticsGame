using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TilePath {
	public List<GameTile> tiles = new List<GameTile>();
	public int costOfPath = 0;
	public GameTile lastTile;

	public TilePath() {
	}

	public TilePath(TilePath myTilePath){
		tiles = myTilePath.tiles.ToList();
		costOfPath = myTilePath.costOfPath;
		lastTile = myTilePath.lastTile;
	}

	public void addTile(GameTile newTile){
		tiles.Add(newTile);
		this.costOfPath = costOfPath + newTile.movementCost;
		lastTile = newTile;
	}

}
