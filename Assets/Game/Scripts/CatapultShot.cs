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

    // Start is called before the first frame update
    void Start()
    {
		//m_Launcher.eulerAngles = new Vector3(m_MinMaxRotations.x, 0, 0);
		m_CurrentBoulder.useGravity = false;
		m_CurrentBoulder.velocity = Vector3.zero;
	}

    // Update is called once per frame
    void Update()
    {
		if(m_ReadyToThrow && Input.GetKeyDown(KeyCode.Space))
		{
			m_CurrentlyLaunching = true;
			m_ReadyToThrow = false;
		}

		if(m_CurrentlyLaunching == true)
		{
			m_Launcher.DORotate(new Vector3(m_MinMaxRotations.y, 0, 0), m_Power * 1.5f, RotateMode.FastBeyond360);
			if(m_Launcher.eulerAngles.x == m_MinMaxRotations.y)
			{
				ThrowBoulder();
			}
		}
		if(m_CurrentlyLaunching == false && m_ReadyToThrow == false)
		{
			m_Launcher.DORotate(new Vector3(m_MinMaxRotations.x, 0, 0), 1.5f, RotateMode.FastBeyond360);
			if(m_Launcher.eulerAngles.x == m_MinMaxRotations.x)
			{
				m_ReadyToThrow = true;
			}
		}
	}

	public void ThrowBoulder()
	{
		m_CurrentBoulder.useGravity = true;
		m_CurrentBoulder.AddForce(m_Origin.forward * m_Power, ForceMode.Impulse);
		m_CurrentlyLaunching = false;
	}
}
