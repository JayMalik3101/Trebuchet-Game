using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatapultShot : MonoBehaviour
{
	[SerializeField] private Vector2 m_MinMaxRotations;
	[SerializeField] private Vector2 m_MinMaxPower;
	[SerializeField] private float m_Power;
	[SerializeField] private GameObject m_BoulderPrefab;
	[SerializeField] private Rigidbody m_CurrentBoulder;
	[SerializeField] private Transform m_Launcher;
	[SerializeField] private Transform m_Origin;
	private bool m_ReadyToThrow = true;
	private bool m_CurrentlyLaunching = false;
	private bool m_Returning;
	private float m_Duration = 0;
	private float m_DurationTwo = 0;

    // Start is called before the first frame update
    void Start()
    {
		//m_Launcher.eulerAngles = new Vector3(m_MinMaxRotations.x, 0, 0);
		m_CurrentBoulder.useGravity = false;
		m_CurrentBoulder.velocity = Vector3.zero;
		m_CurrentBoulder.transform.parent = m_Origin;
	}

    // Update is called once per frame
    void Update()
    {
		if(m_ReadyToThrow && Input.GetKeyDown(KeyCode.Space))
		{
			m_CurrentlyLaunching = true;
			m_ReadyToThrow = false;
		}

		Launching();

		Returning();
	}

	private void Launching()
	{
		if (m_CurrentlyLaunching == true)
		{
			m_Duration = 0.15f / m_Power;
			m_Launcher.DOLocalRotate(new Vector3(m_MinMaxRotations.y, 0, 0), m_Duration, RotateMode.Fast);
			m_CurrentlyLaunching = false;
			
		}
		if(m_Duration > 0)
		{
			m_Duration -= Time.deltaTime;
			if (m_Duration <= 0)
			{
				ThrowBoulder();
			}
		}
	}

	private void Returning()
	{
		if (m_Returning)
		{
			m_DurationTwo = 1.5f;
			m_Launcher.DOLocalRotate(new Vector3(m_MinMaxRotations.x, 0, 0), m_DurationTwo, RotateMode.Fast);
			m_Returning = false;
		}
		if (m_DurationTwo > 0)
		{
			m_DurationTwo -= Time.deltaTime;
			if (m_DurationTwo <= 0)
			{
				m_ReadyToThrow = true;
				m_Returning = false;
			}
		}
	}

	private void ThrowBoulder()
	{
		m_CurrentBoulder.transform.parent = null;
		m_CurrentBoulder.useGravity = true;
		m_CurrentBoulder.AddForce(m_Origin.forward * m_Power, ForceMode.Impulse);
		m_CurrentlyLaunching = false;
		m_Returning = true;
	}

	
}
