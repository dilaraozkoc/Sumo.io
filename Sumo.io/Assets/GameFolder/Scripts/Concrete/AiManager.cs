using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
	public static AiManager Instance;
	public GameObject enemyPrefab;

	[SerializeField] public List<GameObject> enemies;

	[SerializeField] private int enemyNumber;

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
		for (int i = 0; i < enemyNumber; i++)
		{
			float angle = (360 / enemyNumber) * i;
			GameObject enemyGO = Instantiate(enemyPrefab, PlaceEnemyArounCircle(Vector3.zero,5f,angle),Quaternion.identity,transform);
			enemies.Add(enemyGO);
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

	
}
