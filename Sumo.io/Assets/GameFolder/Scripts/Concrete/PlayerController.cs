using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sumo.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public Animator animator;

        [SerializeField] private Joystick joystick;
        [SerializeField] private float movementSpeed;

        private Vector3 lookDirection;
        private Rigidbody rb;
		private bool isTouch = false;

		private void Start()
		{
            rb = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			if (!isTouch)
				Move();
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
			//isTouch = false;
		}
	}

	
}

