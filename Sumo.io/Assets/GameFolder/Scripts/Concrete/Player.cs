using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IForceable
{
	private float thurst;
	[HideInInspector]public float scale = 1f;
	private float scaleAmount = 0.3f;
	private float thrustForce = 80f;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
	}

	private void SetScale()
	{
		transform.localScale = Vector3.one * scale;
	}
	public void Force()
	{
		thurst = scale * thrustForce;
		rb.AddForce((transform.forward * -1) * thurst);
	}

	public void ScaleDown()
	{
		scale -= scaleAmount;
		SetScale();
	}

	public void ScaleUp()
	{
		scale += scaleAmount;
		SetScale();
	}
}
