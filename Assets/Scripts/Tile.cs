using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public List<GameObject> neighbourList = new List<GameObject> ();

	private CircleCollider2D circleCollider2D;

	private int owner = 0;
	private Animator anim;
	private Renderer renderer;

	public void SetupTile()
	{
		anim = GetComponent<Animator> ();
		renderer = GetComponent<Renderer> ();

		circleCollider2D = GetComponent<CircleCollider2D> ();
		circleCollider2D.radius = HexGrid.outerRadius;

		FindNeighbours();

		circleCollider2D.radius = HexGrid.innerRadius; 
	}

	private void FindNeighbours()
	{
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (transform.position, HexGrid.outerRadius);

		foreach(Collider2D collider in hitColliders)
		{
			//Debug.Log ("Enter foreach");
			if (collider.name == "HexagonTile(Clone)" && collider.transform.position != transform.position) 
			{
				neighbourList.Add (collider.gameObject);
			}
		}
	}

	public void ChangeColor()
	{
		Debug.Log (owner);

		if (owner == 0) 
		{
			anim.SetTrigger ("GrayToBlue");
			owner = 1;
			renderer.material.color = Color.blue;

		} else if (owner == 1) 
		{
			Debug.Log ("Blue to Gray");
			anim.SetTrigger ("BlueToGray");
			owner = 0;
			renderer.material.color = Color.gray;
		}
	}
}
