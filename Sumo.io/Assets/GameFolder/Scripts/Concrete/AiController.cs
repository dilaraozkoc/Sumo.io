using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Sumo.Controllers
{
	public class AiController : MonoBehaviour
	{
		public Animator animator;
		[SerializeField] private float movementSpeed;
		private Rigidbody rb;
		private float time = 0;
		private float actionTime = 1.5f;
		private bool isTouch = false;
		private bool isFall = false;
		private bool isWin = false;

		private void Start()
		{
			rb = GetComponentInParent<Rigidbody>();
		}
		private void FixedUpdate()
		{
			
			if (GameController.Instance.firstTouch)
			{
				if (isWin)
					return;

				Fall();
				MoveClosestAi();
			}
		}

		private void MoveClosestAi()
		{
			if (!isTouch && !isWin)
			{
				animator.Play("Walk");
				Vector3 pos = (transform.forward - PlayerController.Instance.gameObject.transform.position);

				if (time > actionTime)
				{
					actionTime -= 0.01f;
					if (actionTime < 0.1f)
					{
						actionTime = 2f;
						time = 0;

					}
					if(AiManager.Instance.aiElements.Count > 1)
					{
						Vector3 pos2 = (GetClosestAi().position - transform.position).normalized;
						rb.velocity = pos2 * movementSpeed * Time.fixedDeltaTime;
					}
						
					
					
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

			foreach (var item in AiManager.Instance.aiElements)
			{
				if (transform.position != item.transform.position && item != null)
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
			StartCoroutine(NoTouchCorutine());

		}

		private IEnumerator NoTouchCorutine()
		{
			animator.Play("Idle");
			yield return new WaitForSeconds(2f);
			isTouch = false;
		}

		public void Fall()
		{
			
			Vector3 pos = transform.position;
			if (pos.x > 8f || pos.x < -8f)
			{
				isFall = true;
				transform.DOMoveY(-5f,1f).OnComplete(()=> {
					gameObject.SetActive(false);
				});
				AiManager.Instance.aiElements.Remove(gameObject);
				animator.Play("Fall");
			}
			if (pos.z > 8f || pos.z < -8f)
			{
				isFall = true;
				transform.DOMoveY(-5f, 1f).OnComplete(() => {
					gameObject.SetActive(false);
				});
				AiManager.Instance.aiElements.Remove(gameObject);
				animator.Play("Fall");
			}
			transform.position = pos;
		}
		public void Win()
		{
			isWin = true;
			animator.Play("HappyDance");
		}
	}

}
