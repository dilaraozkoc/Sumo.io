using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ai : MonoBehaviour
{
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();

	}
	private void Update()
	{
		MoveClosestEnemy();
	}

	private void MoveClosestEnemy()
	{
		transform.DOKill();
		transform.DOMove(GetClosestEnemy().position, GetTweenTime(GetClosestEnemy().position));
	}
	public Transform GetClosestEnemy()
	{
		Transform minTransform = null;
		float minDistance = Mathf.Infinity;
		Transform currentTransform = transform;

		foreach (var item in AiManager.Instance.enemies)
		{
			if (transform.position != item.transform.position)
			{
				float distance = Vector3.Distance(item.transform.position, currentTransform.position);
				if (distance < minDistance)
				{
					minTransform = item.transform;
					minDistance = distance;
					transform.LookAt(minTransform);
					
				}
			}
		}
		return minTransform;
	}

	public float GetTweenTime(Vector3 targetPos)
	{
		return Vector3.Distance(transform.position, targetPos) / 1f;
	}
}
