﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour {
	public Tile spawnThis;
	public static HexGrid instance = null;   

	public int x = 5;
	public int y = 5;
	public float separation = 0.01f;

	private Ray ray = new Ray();
	private Transform boardHolder;
	private List<Tile> tileList;
	private float offsetX, offsetY;

	public const float outerRadius = 0.5f;
	public const float innerRadius = outerRadius * 0.866025404f; //Root 3 / 2

	void Awake() 
	{
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		tileList = new List<Tile> ();

		SetupBoard ();

		for (int i = 0; i < tileList.Count; i++)
			tileList [i].SetupTile ();
	}

	void Update()
	{
		
		if (Input.GetMouseButtonDown (0)) 
		{
			Tile clickedTile = GetClickedTile (Input.mousePosition);

			if (clickedTile != null) 
			{
				clickedTile.ChangeColor ();
			}
		}
	}

	//Sets up a grid of hexagons
	void SetupBoard()
	{
		boardHolder = new GameObject ("Board").transform;

		float offset = outerRadius + separation;

		offsetX = offset * Mathf.Sqrt(3);
		offsetY = offset * 1.5f;

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

	private Tile GetClickedTile(Vector3 mousePosition)
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

		if (hit.collider != null) 
		{
			return hit.collider.GetComponent<Tile> ();
		}

		return null;
	}
}