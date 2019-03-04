﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatapultShot : MonoBehaviour
{
	[SerializeField] private Vector2 m_MinMaxRotations;
	[SerializeField] private Vector2 m_MinMaxPower;
	[SerializeField] private float m_Power;
	[SerializeField] private Rigidbody m_CurrentBoulder;
	[SerializeField] private Transform m_Launcher;
	[SerializeField] private Transform m_Origin;
	private bool m_ReadyToThrow = true;
	private bool m_CurrentlyLaunching = false;
	private bool m_Shoot;
	private float m_Duration = 0;
	private float m_DurationTwo = 0;
	Tween m_LauncherTween;
	TweenCallback m_LaunchingCompelete;
	TweenCallback m_ReturnComplete;
	private Vector3 m_OriginalEulerRotation;
	
    // Start is called before the first frame update
    void Start()
    {
		FreezeBoulder();
		m_OriginalEulerRotation = m_Launcher.localEulerAngles;
		m_LaunchingCompelete += Returning;
		m_ReturnComplete += ReturningComplete;
	}

    // Update is called once per frame
    void Update()
    {
		if(m_ReadyToThrow && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
		{
			m_CurrentlyLaunching = true;
			m_ReadyToThrow = false;
		}

		Launching();
	}

	private void Launching()
	{
		if (m_CurrentlyLaunching == true)
		{
			m_Duration = 0.15f / m_Power;
			m_LauncherTween = m_Launcher.DOLocalRotate(new Vector3(m_MinMaxRotations.y, 0, 0), m_Duration + 0.25f, RotateMode.Fast).OnComplete(Returning);
			m_CurrentlyLaunching = false;			
		}
		if (m_Duration > 0)
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
		m_DurationTwo = 1.5f;
		m_Launcher.DOLocalRotate(m_OriginalEulerRotation, m_DurationTwo, RotateMode.Fast).OnComplete(m_ReturnComplete);
	}

	private void ReturningComplete()
	{
		m_Launcher.localEulerAngles = m_OriginalEulerRotation;
		m_ReadyToThrow = true;
	}

	private void ThrowBoulder()
	{
		
		if(m_CurrentBoulder != null)
		{
			UnFreezeBoulder();
			m_CurrentBoulder.AddForce(m_Origin.forward * m_Power, ForceMode.Impulse);
			m_CurrentBoulder = null;
		}
		m_CurrentlyLaunching = false;
	}

	private void FreezeBoulder()
	{
		m_CurrentBoulder.velocity = Vector3.zero;
		m_CurrentBoulder.constraints = RigidbodyConstraints.FreezeAll;
		m_CurrentBoulder.transform.parent = m_Origin;
		m_CurrentBoulder.useGravity = false;
	}

	private void UnFreezeBoulder()
	{
		m_CurrentBoulder.velocity = Vector3.zero;
		m_CurrentBoulder.constraints = RigidbodyConstraints.None;
		m_CurrentBoulder.transform.parent = null;
		m_CurrentBoulder.useGravity = true;
	}
}
