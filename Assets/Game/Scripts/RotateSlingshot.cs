using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlingshot : MonoBehaviour
{
	public Transform target;
	public Transform helper;

	private void Update()
	{
		Vector3 relativePos = transform.position - target.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
		transform.rotation = rotation;
	}
}
