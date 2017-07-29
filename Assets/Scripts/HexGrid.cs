using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour {
	public Tile spawnThis;
	public static HexGrid instance = null;   

	public int x = 5;
	public int y = 5;

	public float radius = 0.5f;
	public float separation = 0.01f;

	private Transform boardHolder;
	private List<Tile> tileList;
	private float offsetX, offsetY;

	void Awake() 
	{
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		tileList = new List<Tile> ();

		SetupBoard ();

		for (int i = 0; i < tileList.Count; i++)
			tileList [i].FindNeighbours ();
	}

	//Sets up a grid of hexagons
	void SetupBoard()
	{
		boardHolder = new GameObject ("Board").transform;

		float unitLength = radius + separation;

		offsetX = unitLength * Mathf.Sqrt(3);
		offsetY = unitLength * 1.5f;

		for( int i = 1; i < x + 1; i++ ) {
			for( int j = 1; j < y + 1; j++ ) {
				Vector2 hexpos = HexOffset( i, j );
				Vector3 pos = new Vector3( hexpos.x, hexpos.y, 0 ) + transform.position;
				Tile instance = Instantiate(spawnThis, pos, Quaternion.identity );

				tileList.Add (instance);

				instance.transform.SetParent (boardHolder);
			}
		}
	}

	//Helper function to calculate offsets for xexagonal grid
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
}