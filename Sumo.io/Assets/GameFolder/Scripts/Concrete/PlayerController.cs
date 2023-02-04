using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Sumo.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public static PlayerController Instance;
		public ScoreScriptableObject scoreScriptable;
		public Animator animator;
		public TextMeshProUGUI textMesh;
		public TextMeshProUGUI maxScoreTextMesh;

		[SerializeField] private Joystick joystick;
		[SerializeField] private float movementSpeed;

		private Vector3 lookDirection;
		private Rigidbody rb;
		private bool isTouch = false;
		[HideInInspector]public bool isFall = false;
		[HideInInspector]public int score = 0;
		private int fakeScore;

		private void Awake()
		{
			Instance = this;
		}
		private void Start()
		{
			rb = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			if (GameController.Instance.firstTouch)
			{
				if (isFall)
					return;

				Fall();

				if (!isTouch)
					Move();
			}
		}

		private void Move()
		{
			if (Input.GetMouseButton(0))
			{

				float rotationMagnitude = new Vector3(joystick.Horizontal, 0, joystick.Vertical).magnitude;

				lookDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
				if (rotationMagnitude > 0.01f)
				{
					rb.velocity = transform.forward * movementSpeed * Time.fixedDeltaTime;
					transform.rotation = Quaternion.LookRotation(lookDirection);
					animator.SetFloat("Blend", rotationMagnitude);
				}

			}
			else
			{
				animator.SetFloat("Blend", 0);
				rb.velocity = Vector3.zero;
			}
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
			animator.SetFloat("Blend", 0);
			yield return new WaitForSeconds(2f);
			isTouch = false;
		}

		public void Fall()
		{
			Vector3 pos = transform.position;
			if (pos.x > 8f || pos.x < -8f)
			{
				isFall = true;
				animator.Play("Fall");
				transform.DOMoveY(-5f, 1f).OnComplete(() => {
					gameObject.SetActive(false);
				});
			}
			if (pos.z > 8f || pos.z < -8f)
			{
				isFall = true;
				animator.Play("Fall");
				transform.DOMoveY(-5f, 1f).OnComplete(() => {
					gameObject.SetActive(false);
				});
			}
			transform.position = pos;

			
		}

		public void Score()
		{
			score = scoreScriptable.score;
			scoreScriptable.score += 10;
			fakeScore += 10;
			textMesh.text = "Score: " + fakeScore.ToString();
			maxScoreTextMesh.text = "Max Score: " + score.ToString();
		}

		public void Win()
		{
			animator.Play("HappyDance");
		}
	}
}

