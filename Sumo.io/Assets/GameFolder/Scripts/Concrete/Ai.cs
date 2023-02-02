using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ai : MonoBehaviour
{
	[SerializeField]private float movementSpeed;
	[SerializeField]public float thrustForce;
	private Rigidbody rb;
	private bool isTouch;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();

	}
	private void FixedUpdate()
	{
		MoveClosestEnemy();
	}

	private void MoveClosestEnemy()
	{
		if (isTouch)
			return;

		//transform.DOMove(GetClosestEnemy().position, GetTweenTime(GetClosestEnemy().position));
		rb.velocity = GetClosestEnemy().position * movementSpeed * Time.fixedDeltaTime;
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
					Touch(distance);
					minTransform = item.transform;
					minDistance = distance;
					transform.LookAt(minTransform);
					
				}
			}
		}
		return minTransform;
		
	}
	private void Touch(float distance)
	{

		if (distance < 1f)
		{
			//transform.DOMove((transform.forward * -1f) * 10f, 1f);
			rb.AddForce((transform.forward * -1f) * thrustForce * Time.deltaTime);
			Debug.Log("Deðdii");
			isTouch = true;
		}
			
		
	}
	
	//private float GetTweenTime(Vector3 targetPos)
	//{
	//	return Vector3.Distance(transform.position, targetPos) / 1f;
	//}
}
