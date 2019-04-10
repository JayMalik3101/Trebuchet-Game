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
	[SerializeField] private Transform m_String;
	[SerializeField] private Transform m_StringStartPoint;
	[SerializeField] private Transform m_SlingshotModel;

	private bool m_ReadyToShoot = true;
	private bool m_CurrentlyLaunching = false;
	private bool m_Shoot;
	private float m_Duration = 0;
	private float m_DurationTwo = 0;

	private Quaternion m_OriginalRotation;
	private bool m_Returning;
	private StatKeeping m_Stats;

	

	// Start is called before the first frame update
	void Start()
	{
		FreezeProjectile();
		m_OriginalRotation = m_String.localRotation;
		m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
		{
			SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || OVRInput.GetDown(OVRInput.Button.Two))
		{
			m_Power += 0.5f;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || OVRInput.GetDown(OVRInput.Button.One))
		{
			m_Power -= 0.5f;
		}
		if(m_Grabbable.grabbedBy != null && m_StringGrabbable.grabbedBy != null)
		{
			m_Grabbable.m_BothGrabbed = true;
			m_StringGrabbable.m_BothGrabbed = true;
			Vector3 relativePos = m_SlingshotModel.position - m_String.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
			m_SlingshotModel.rotation = rotation;
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
}
