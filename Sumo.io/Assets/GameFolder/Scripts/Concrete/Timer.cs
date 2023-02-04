using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaning =30f;
	private bool timeIsUp = false;

	private void Update()
	{
		if (GameController.Instance.firstTouch)
		{
			if (timeIsUp)
				return;
			if (timeRemaning > 0)
			{
				timeRemaning -= Time.deltaTime;
				timerText.text = "Time: " + timeRemaning.ToString("F1");
			}
			else
			{
				timeIsUp = true;
				GameController.Instance.WinCondition(true);

			}
		}
		
			
	}
}
