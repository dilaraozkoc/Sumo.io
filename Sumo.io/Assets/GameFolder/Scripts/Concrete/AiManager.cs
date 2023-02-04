using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sumo.Controllers;

public class AiManager : MonoBehaviour
{
	public static AiManager Instance;
	public GameObject aiPrefab;

	[SerializeField] public List<GameObject> aiElements;

	[SerializeField] private int aiNumber;

	private void Awake()
	{
		Instance = this;
	}
	private void Start()
	{
		SpawnEnemies();
	}
	private void SpawnEnemies()
	{
		for (int i = 0; i < aiNumber; i++)
		{
			float angle = (360 / aiNumber) * i;
			GameObject enemyGO = Instantiate(aiPrefab, PlaceEnemyArounCircle(Vector3.zero,5f,angle),Quaternion.identity,transform);
			aiElements.Add(enemyGO);
			enemyGO.transform.LookAt(Vector3.zero);
		}
		
	}

	private Vector3 PlaceEnemyArounCircle(Vector3 center, float radius, float _angle)
	{
		float angle = _angle;
		Vector3 position;
		position.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
		position.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
		position.y = center.y;
		return position;
	}

	public void SetWinAnimation()
	{
		foreach (var item in aiElements)
		{
			item.GetComponent<AiController>().Win();
		}
	}
	
}
