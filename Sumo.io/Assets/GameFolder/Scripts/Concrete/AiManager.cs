using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
	public GameObject enemyPrefab;

	[SerializeField] private List<GameObject> enemies;

	[SerializeField] private int enemyNumber;

	private void Start()
	{
		SpawnEnemies();
	}
	private void SpawnEnemies()
	{
		for (int i = 0; i < enemyNumber; i++)
		{
			int angle = (360 / enemyNumber) * i;
			GameObject enemyGO = Instantiate(enemyPrefab, PlaceEnemyArounCircle(Vector3.zero,5f,angle),Quaternion.identity,transform);
			enemies.Add(enemyGO);
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
}
