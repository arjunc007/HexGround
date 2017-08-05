using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private Transform pauseMenu;

	private Text scoreText;
	private Text winText;
	private Text turnText;

	private void Awake()
	{
		pauseMenu = transform.Find ("PauseMenu");

		scoreText = transform.Find("Score").GetComponent<Text>();
		winText = transform.Find ("Winner").GetComponent<Text> ();
		turnText = transform.Find ("TurnText").GetComponent<Text> ();

		ResetUI ();
	}

	public void PauseGame()
	{
		if (GameManager.instance.isPaused == false) 
		{
			GameManager.instance.isPaused = true;
			ShowPauseMenu (true);
		}
		else if (GameManager.instance.isPaused == true) 
		{
			GameManager.instance.isPaused = false;
			ShowPauseMenu (false);
		}
	}

	public void SetTurnText(int turn)
	{
		turnText.text = "Player " + turn + "'s Turn";
		if (turn == 1)
			turnText.color = Color.blue;
		else if (turn == 2)
			turnText.color = Color.red;
	}

	private void ResetUI()
	{
		winText.text = scoreText.text = "";
		pauseMenu.gameObject.SetActive (false);
		turnText.text = "";
	}

	public void ShowPauseMenu(bool state)
	{
		Button pauseButton = transform.Find ("PauseButton").GetComponent<Button> ();
		SpriteState spriteState = new SpriteState ();
		if (state) {
			pauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite> ("play_button");
			spriteState.disabledSprite = Resources.Load<Sprite> ("play_button");
			spriteState.highlightedSprite = Resources.Load<Sprite> ("play_button_hover");
			spriteState.pressedSprite = Resources.Load<Sprite> ("play_button_clicked");
		} else 
		{
			pauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite> ("pause_button");
			spriteState.disabledSprite = Resources.Load<Sprite> ("pause_button");
			spriteState.highlightedSprite = Resources.Load<Sprite> ("pause_button_hover");
			spriteState.pressedSprite = Resources.Load<Sprite> ("pause_button_clicked");
		}

		pauseButton.spriteState = spriteState;
		pauseMenu.gameObject.SetActive (state);

	}

	public void ShowScore(int a, int b)
	{
		if (a > b) 
		{
			winText.text = "Player 1 Won!!!";
			scoreText.text = a + " | " + b;
		} else if (a < b) {
			winText.text = "Player 2 Won!!!";
			scoreText.text = b + " | " + a;
		} else {
			winText.text = "It's a Draw!!!";
			scoreText.text = a + " | " + b;
		}
	}
}
