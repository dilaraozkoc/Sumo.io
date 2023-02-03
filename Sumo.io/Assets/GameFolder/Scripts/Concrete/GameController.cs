using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sumo.Controllers;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController Instance;
	public GameObject startScreen;
	public GameObject inGameScreen;
	public GameObject winScreen;
	public GameObject failScreen;
	public bool firstTouch = false;

	private void Awake()
	{
		Instance = this;
	}
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startScreen.SetActive(false);
			inGameScreen.SetActive(true);
			firstTouch = true;
		}

		WinCondition();
		FailCondition();
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}
	public void ResumeGame()
	{
		Time.timeScale = 1;
	}
	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void Win()
	{
		winScreen.SetActive(true);
	}

	public void Fail()
	{
		failScreen.SetActive(false);
	}

	public void WinCondition()
	{
		if(AiManager.Instance.aiElements.Count == 0 && !PlayerController.Instance.isFall)
		{
			Win();
		}
	}
	public void FailCondition()
	{
		if (AiManager.Instance.aiElements.Count > 0 && PlayerController.Instance.isFall)
		{
			Fail();
		}
		else if (AiManager.Instance.aiElements.Count == 0 && PlayerController.Instance.isFall)
		{
			Fail();
		}
	}

}
