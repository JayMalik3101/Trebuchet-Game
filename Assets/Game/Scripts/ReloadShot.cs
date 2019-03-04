using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadShot : MonoBehaviour
{
	[SerializeField] private CatapultShot m_CatapultShot;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Projectile") && m_CatapultShot.m_CurrentBoulder == null)
		{
			m_CatapultShot.m_CurrentBoulder = other.GetComponent<Rigidbody>();
			m_CatapultShot.m_CurrentBoulder.transform.position = m_CatapultShot.m_Origin.position;
			m_CatapultShot.FreezeBoulder();
		}
	}
}
