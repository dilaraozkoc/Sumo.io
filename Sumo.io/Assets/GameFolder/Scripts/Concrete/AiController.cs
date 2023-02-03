using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AiController : MonoBehaviour
{
	[SerializeField]private float movementSpeed;
	[SerializeField]public float thrustForce;
	private AiManager aiParent;
	private Rigidbody rb;
	private float time = 0;
	private float actionTime = 0.8f;
	private bool isTouch = false;
	private IForceable forceable;


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
		if (!isTouch)
		{
			Vector3 pos = (transform.forward - aiParent.gameObject.transform.position) * 3f;

			if (time > actionTime)
			{
				actionTime -= 0.01f;
				if (actionTime < 0.1f)
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
		isTouch = true;
		IForceable forceable = collision.gameObject.GetComponentInChildren<IForceable>();
		
		if (forceable != null)
			forceable.Force();

	}
	private void OnCollisionExit(Collision collision)
	{
		//isTouch = false;
	}
}
