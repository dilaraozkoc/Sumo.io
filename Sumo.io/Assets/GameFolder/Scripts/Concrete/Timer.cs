using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaning = 60f;

	private void Update()
	{
		if (timeRemaning > 0)
		{
			timeRemaning -= Time.deltaTime;
			timerText.text = "Time: " + timeRemaning.ToString("F1");
		}
			
	}
}
