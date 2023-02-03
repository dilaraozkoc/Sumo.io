using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ai : MonoBehaviour, IForceable
{
	private float thurst;
	[HideInInspector]public float scale = 1f;
	private float scaleAmount = 0.3f;
	private float thrustForce = 80f;
	private float maxThrustForce = 120f;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
		scale = Random.Range(0.5f, 1.5f);
		SetScale();
	}
	private void SetScale()
	{
		transform.localScale = Vector3.one * scale;
	}
	public void Force()
	{
		thurst = scale * Mathf.Abs(maxThrustForce - thrustForce);
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
