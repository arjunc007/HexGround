using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HexGrid : MonoBehaviour {

	public int columns = 5;
	public int rows = 5;
	private float separation = 0.01f;

	public List<Tile> tileList;
	private Transform boardHolder;
	private float offsetX, offsetY;

	public const float outerRadius = 0.5f;
	public const float innerRadius = outerRadius * 0.866025404f; //Root 3 / 2

	//Sets up a grid of hexagons
	public void SetupBoard(Tile hexagon)
	{
		tileList = new List<Tile> ();

		//for (int i = 0; i < tileList.Count; i++)
		//	tileList [i].SetupTile ();
		
		boardHolder = new GameObject ("Board").transform;

		float offset = outerRadius + separation;

		offsetX = offset * Mathf.Sqrt(3);
		offsetY = offset * 1.5f;

		int x = columns % 2 == 0 ? columns / 2 : (columns + 1) / 2;
		int y = rows % 2 == 0 ? rows / 2 : (rows + 1) / 2;

		for( int i = -x; i < x ; i++ ) {
			for( int j = -y; j < y; j++ ) {
				Vector2 hexpos = HexOffset( i, j );
				Vector3 pos = new Vector3( hexpos.x, hexpos.y, 0 ) + transform.position;
				Tile instance = Instantiate(hexagon, pos, Quaternion.identity );

				tileList.Add (instance);

				instance.transform.SetParent (boardHolder);
			}
		}

		foreach(Tile tile in tileList)
			tile.SetupTile ();
	}

	//Helper function to calculate offsets for hexagonal grid
	Vector2 HexOffset( int x, int y ) {
		Vector2 position = Vector2.zero;
		if( y % 2 == 0 ) {
			position.x = x * offsetX;
		}
		else {
			position.x = ( x + 0.5f ) * offsetX;
		}

		position.y = y * offsetY;
		return position;
	}

	public bool CheckBoard()
	{
		foreach (Tile tile in tileList) 
		{
			if (tile.owner == 0)
				return false;
		}

		return true;
	}
}