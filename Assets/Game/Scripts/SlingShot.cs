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

	private bool m_ReadyToShoot = true;
	private bool m_CurrentlyLaunching = false;
	private bool m_Shoot;
	private float m_Duration = 0;
	private float m_DurationTwo = 0;
	private Tween m_LauncherTween;
	private TweenCallback m_LaunchingCompelete;
	private TweenCallback m_ReturnComplete;
	private Quaternion m_OriginalRotation;
	private bool m_Returning;
	private StatKeeping m_Stats;
	private OVRGrabbable m_Grabbable;

	// Start is called before the first frame update
	void Start()
	{
		FreezeProjectile();
		m_OriginalRotation = m_String.localRotation;
		m_LaunchingCompelete += Returning;
		m_ReturnComplete += ReturningComplete;
		m_Grabbable = GetComponent<OVRGrabbable>();
		m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>();
	}

	// Update is called once per frame
	void Update()
	{
		if (m_ReadyToShoot && m_Returning == false )
		{
			m_CurrentlyLaunching = true;
			m_ReadyToShoot = false;
		}
		Launching();

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

	private void Launching()
	{
		if (m_CurrentlyLaunching == true)
		{
			//m_Duration = m_ShotDuration / m_Power;
			//m_LauncherTween = m_Launcher.DOLocalRotate(new Vector3(m_MinMaxRotations.y, 0, 0), m_Duration + 0.25f, RotateMode.Fast).OnComplete(m_LaunchingCompelete);
			m_CurrentlyLaunching = false;
		}
		if (m_Duration > 0)
		{
			m_Duration -= Time.deltaTime;
			if (m_Duration <= 0)
			{
				ThrowProjectile();
			}
		}
	}

	private void Returning()
	{
		m_Returning = true;
		m_DurationTwo = 1.5f;
		//m_Launcher.DOLocalRotate(m_OriginalEulerRotation, m_DurationTwo, RotateMode.Fast).OnComplete(m_ReturnComplete);
	}

	private void ReturningComplete()
	{
		m_String.localRotation = m_OriginalRotation;
		m_ReadyToShoot = true;
		m_Returning = false;
	}

	private void ThrowProjectile()
	{

		if (m_CurrentProjectile != null)
		{
			UnFreezeProjectile();
			m_CurrentProjectile.AddForce(m_Origin.up * m_Power, ForceMode.Impulse);
			m_CurrentProjectile = null;
			m_Stats.m_ShotsFired++;
		}
		m_CurrentlyLaunching = false;
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
