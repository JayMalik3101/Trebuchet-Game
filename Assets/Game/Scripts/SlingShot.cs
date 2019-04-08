using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SlingShot : MonoBehaviour
{
	public Rigidbody m_CurrentProjectile;
	public Transform m_Origin;
	[SerializeField] private float m_Power;
	[SerializeField] private Transform m_String;
	[SerializeField] private Transform m_StringStartPoint;


	private bool m_ReadyToShoot = true;
	private bool m_CurrentlyLaunching = false;
	private bool m_Shoot;
	private float m_Duration = 0;
	private float m_DurationTwo = 0;

	private Quaternion m_OriginalRotation;
	private bool m_Returning;
	private StatKeeping m_Stats;
	private OVRGrabbable m_Grabbable;
	private OVRGrabbable m_StringGrabbable;

	// Start is called before the first frame update
	void Start()
	{
		FreezeProjectile();
		m_OriginalRotation = m_String.localRotation;
		m_Grabbable = GetComponent<OVRGrabbable>();
		m_StringGrabbable = m_String.GetComponent<OVRGrabbable>();
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
	}

	private void LateUpdate()
	{
		PullString();
	}

	private void PullString()
	{
		if(m_StringGrabbable.grabbedBy != null)
		{
			float dist = (m_StringStartPoint.transform.position - m_String.position).magnitude;
			m_String.transform.localPosition = m_StringStartPoint.transform.localPosition + new Vector3(5f * dist, 0f, 0f);

			//if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
			//{
			//	Fire();
			//}
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
