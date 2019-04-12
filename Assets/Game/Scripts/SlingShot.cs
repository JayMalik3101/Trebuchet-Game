using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SlingShot : MonoBehaviour
{
	public Rigidbody m_CurrentProjectile;
	public Transform m_Origin;

	public OVRGrabbable m_Grabbable;
	public OVRGrabbable m_StringGrabbable;

	[SerializeField] private float m_Power;
	[SerializeField] private Rigidbody m_String;
	[SerializeField] private Transform m_StringStartPoint;
	[SerializeField] private Transform m_Target;
	[SerializeField] private float m_OriginalTimer;

	private float m_Timer;
	private bool m_Shooting;

	private bool m_ReadyToShoot = true;
	private bool m_CurrentlyLaunching = false;
	private bool m_Shoot;
	private float m_Distance;
	private Vector3 m_AllDistances;

	private Quaternion m_OriginalRotation;
	private bool m_Returning;
	private StatKeeping m_Stats;

	private Tween m_StringTween;
	private TweenCallback m_ReturnComplete;



	// Start is called before the first frame update
	void Start()
	{
		m_Timer = m_OriginalTimer;
		if (m_CurrentProjectile != null)
		{
			FreezeProjectile();
		}
		m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>();
	}

	// Update is called once per frame
	void Update()
	{
		//CorrectOrientation();
		if (Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
		{
			SceneManager.LoadScene(0);
		}
		if (m_StringGrabbable.grabbedBy != null && m_Shooting == false)
		{
			m_Shoot = true;
			m_AllDistances = m_StringStartPoint.position - m_String.position;
			CheckWhatsBiggest();
		}
		if (m_Shoot && m_StringGrabbable.grabbedBy == null && m_Shooting == false)
		{
			m_Shoot = false;
			m_Shooting = true;
			transform.rotation = m_OriginalRotation;
		}
		if (m_Shooting)
		{
			m_Timer -= Time.deltaTime;
			Debug.Log("just shot");
			if (m_Timer <= 0)
			{
				Fire();
			}
		}
		//CorrectOrientation();
	}

	private void LateUpdate()
	{
		CorrectOrientation();
	}

	public void Fire()
	{
		if (m_CurrentProjectile != null)
		{
			UnFreezeProjectile();
			m_CurrentProjectile.AddForce(m_Origin.forward * (m_Distance * m_Power), ForceMode.Impulse);
			m_CurrentProjectile = null;
			m_Timer = m_OriginalTimer;
			m_Shooting = false;
			m_Distance = 0;
			//m_Stats.m_ShotsFired++;
		}
	}

	public void FreezeProjectile()
	{
		m_CurrentProjectile.velocity = Vector3.zero;
		m_CurrentProjectile.constraints = RigidbodyConstraints.FreezeAll;
		m_CurrentProjectile.transform.parent = m_Origin;
		m_CurrentProjectile.useGravity = false;
	}

	private void UnFreezeProjectile()
	{
		m_CurrentProjectile.velocity = Vector3.zero;
		m_CurrentProjectile.constraints = RigidbodyConstraints.None;
		m_CurrentProjectile.transform.parent = null;
		m_CurrentProjectile.useGravity = true;
	}

	private void CheckWhatsBiggest()
	{
		if(m_AllDistances.x >= m_Distance)
		{
			m_Distance = m_AllDistances.x;
		}
		if (m_AllDistances.y >= m_Distance)
		{
			m_Distance = m_AllDistances.y;
		}
		if (m_AllDistances.z >= m_Distance)
		{
			m_Distance = m_AllDistances.z;
		}
	}

	private void CorrectOrientation()
	{
		if (m_Grabbable.grabbedBy != null && m_StringGrabbable.grabbedBy != null && !m_Shooting && m_Timer == m_OriginalTimer)
		{
			Debug.Log("Correct Orientation: " + Time.time);
			m_OriginalRotation = transform.rotation;
			m_Grabbable.m_BothGrabbed = true;
			m_StringGrabbable.m_BothGrabbed = true;
			transform.rotation = Quaternion.identity;
			m_String.rotation = Quaternion.identity;
			Vector3 relativePos = m_Target.position - m_String.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
			transform.rotation = rotation;
			relativePos = m_String.position - m_Target.position;
			rotation = Quaternion.LookRotation(relativePos, Vector3.up);
			m_String.transform.rotation = rotation;
		}
		else
		{
			m_Grabbable.m_BothGrabbed = false;
			m_StringGrabbable.m_BothGrabbed = false;
		}
	}
}
