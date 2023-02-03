using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public GameObject collectablePrefab;
	public int numberOfCollectable;
	[HideInInspector]public List<GameObject> collectables = new List<GameObject>();

	private void Start()
	{
		SpawnCollectable(numberOfCollectable);
	}
	public void SpawnCollectable(int number)
	{
		for (int i = 0; i <number; i++)
		{
			GameObject collectableGO = Instantiate(collectablePrefab, transform);
			collectables.Add(collectableGO);
			PlaceCollectables();
		}
		
	}

	private void PlaceCollectables()
	{
		foreach (var item in collectables)
		{
			Vector3 pos = new Vector3(Random.Range(-5f, 5f), item.transform.position.y, Random.Range(-5f, 5f));
			float distance = Vector3.Distance(item.transform.position,pos);
			if(distance < 10f)
			{
				item.transform.position = pos;
			}
		}
	}
}
