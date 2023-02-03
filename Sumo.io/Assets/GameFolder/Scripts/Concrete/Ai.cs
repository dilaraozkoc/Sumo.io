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
	private float time = 0;
	private float actionTime = 0.8f;

	private AiManager aiParent;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
		aiParent = GetComponentInParent<AiManager>();
	}
	private void Update()
	{
		MoveClosestAi();
	}

	private void MoveClosestAi()
	{
		if (isTouch)
			return;

		Vector3 pos = (transform.forward - aiParent.gameObject.transform.position) * 3f;

		if (time > actionTime)
		{
			actionTime -= 0.01f;
			if(actionTime < 0.1f)
			{
				actionTime = 2f;
				time = 0;

			}
			rb.velocity = GetClosestAi().position * movementSpeed * Time.fixedDeltaTime;
		}
		else
		{
			time += Time.deltaTime;
			rb.velocity = pos * movementSpeed * Time.fixedDeltaTime;
			transform.LookAt(pos);
		}
	}
	public Transform GetClosestAi()
	{
		Transform minTransform = null;
		float minDistance = Mathf.Infinity;
		Transform currentTransform = transform;

		foreach (var item in AiManager.Instance.enemies)
		{
			if (transform.position != item.transform.position)
			{
				float distance = Vector3.Distance(currentTransform.position, item.transform.position);
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
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isTouch = true;
			Debug.Log("Touch!!");
			Rigidbody colliderRb = gameObject.GetComponent<Rigidbody>();
			colliderRb.AddForce((transform.forward * -1) * 900);
		}
		if (collision.gameObject.CompareTag("Ai"))
		{
			isTouch = true;
			Debug.Log("Touch!!");
			Rigidbody colliderRb = gameObject.GetComponent<Rigidbody>();
			colliderRb.AddForce((transform.forward * -1) * 400);
		}
	}
	//private Vector3 ClampMovements()
	//{


	//	Vector3 position = transform.position;
	//	position.x = Mathf.Clamp(position.x, -6.5f, 6.5f);
	//	position.z = Mathf.Clamp(position.z, -6.5f, 6.5f);

	//	transform.position = position;
	//	return position;
	//}
}
