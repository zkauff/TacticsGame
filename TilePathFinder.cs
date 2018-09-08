using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePathFinder : MonoBehaviour {

		public static TilePath FindPath(GameTile originTile, GameTile destinationTile){
			List<GameTile> closed = new List<GameTile>();
			List<TilePath> open = new List<TilePath>();

			TilePath originPath = new TilePath();
			originPath.addTile(originTile);

			open.Add(originPath);

			while (open.Count > 0) {
				TilePath current = open[0];
				open.Remove(open[0]);

				if (closed.Contains(current.lastTile)) {
					continue;
				}
				if(current.lastTile == destinationTile){
						return current;
				}

				closed.Add(current.lastTile);

				foreach (GameTile tile in current.lastTile.neighbors) {
					TilePath newTilePath = new TilePath(current);
					newTilePath.addTile(tile);
					open.Add(newTilePath);


				}
			}
			closed.Remove(originTile);
			return null;
		}
}
