using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadProjectile : MonoBehaviour
{
	[SerializeField] private SlingShot m_SlingShot;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Projectile") && m_SlingShot.m_CurrentProjectile == null)
		{
			m_SlingShot.m_CurrentProjectile = other.GetComponent<Rigidbody>();
			OVRGrabbable grabbable = m_SlingShot.m_CurrentProjectile.GetComponent<OVRGrabbable>();
			if (grabbable.grabbedBy != null)
			{
				grabbable.grabbedBy.ForceRelease(grabbable);
			}
			m_SlingShot.m_CurrentProjectile.transform.position = m_SlingShot.m_Origin.position;
			m_SlingShot.FreezeProjectile();
		}
	}
}
