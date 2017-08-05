using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Tile hexagon;
	public static GameManager instance = null;

	private int playerTurn;
	private int p1Score;
	private int p2Score;
	[HideInInspector]public bool isPaused;
	private bool isEnded;
	private UIManager uiManager;
	private HexGrid gridManager;

	void Awake()
	{
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		gridManager = GetComponent<HexGrid> ();
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		InitGame ();
	}

	void OnEnable() 
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	private void InitGame() 
	{
		Reset ();

		gridManager.SetupBoard (hexagon);

		uiManager.SetTurnText (playerTurn);
	}

	void Update()
	{
		if (!isEnded && !isPaused && Input.GetMouseButtonDown (0)) {
			Tile clickedTile = GetClickedTile (Input.mousePosition);

			if (clickedTile != null && clickedTile.owner == 0) {
				clickedTile.ChangeColor (playerTurn);
				playerTurn = playerTurn == 1 ? ++playerTurn : --playerTurn;
			}

			uiManager.SetTurnText (playerTurn);
		}

		if ( !isEnded && gridManager.CheckBoard ())
			EndGame ();
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

	public void MenuAction(string action)
	{
		if (action == "Restart") 
		{
			SceneManager.LoadScene (0);
		} else if (action == "Settings") {
		} else if (action == "Exit") {
		}
	}

	private void EndGame()
	{
		isEnded = true;
		CalculateScore ();
		uiManager.ShowScore (p1Score, p2Score);
	}

	private void CalculateScore()
	{
		foreach (Tile tile in gridManager.tileList) 
		{
			if (tile.owner == 1)
				p1Score++;
			if (tile.owner == 2)
				p2Score++;
		}
	}

	private void Reset()
	{
		uiManager = GameObject.Find("GameCanvas").GetComponent<UIManager> ();
		isEnded = false;
		isPaused = false;
		playerTurn = 1;
		p1Score = p2Score = 0;
	}
}
