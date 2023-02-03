using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sumo.Controllers;

public class Collectable : MonoBehaviour
{
	private bool canSpawn = false;
	private void Start()
	{
		transform.DOMoveY(0.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
	}
	private void Update()
	{
		if (canSpawn)
		{
			StartCoroutine(OpenActive());
			canSpawn = false;
		}
		
	}
	private void OnTriggerEnter(Collider other)
	{
		canSpawn = true;

		IForceable forceable = other.GetComponentInParent<IForceable>();
		if (other.CompareTag("Player"))
		{
			other.GetComponentInParent<PlayerController>().Score();
		}
		if (forceable != null)
			forceable.ScaleUp();

		GetComponentInParent<Collectables>().collectables.Remove(gameObject);
		Destroy(gameObject);
		
	}

	private IEnumerator OpenActive()
	{
		yield return new WaitForSeconds(Random.Range(1f,2f));
		GetComponentInParent<Collectables>().SpawnCollectable(1);
		
	}
}
