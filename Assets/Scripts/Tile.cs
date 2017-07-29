using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public List<GameObject> neighbourList = new List<GameObject> ();

	//private CircleCollider2D circleCollider2D;

	//private int owner = 0;
	//private Color color = Color.grey;

	//void Start()
	//{
	//	sideLength = HexGrid.instance.radius;
	//}

	public void FindNeighbours()
	{
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (transform.position, HexGrid.instance.radius);

		Debug.Log (hitColliders.Length);

		foreach(Collider2D collider in hitColliders)
		{
			Debug.Log (collider.transform.position);
			//Debug.Log ("Enter foreach");
			if (collider.name == "HexagonTile(Clone)" && collider.transform.position != transform.position) 
			{
				Debug.Log ("Enter if");
				neighbourList.Add (collider.gameObject);
				Debug.Log ("Add to list");
			}
		}
	}
}
