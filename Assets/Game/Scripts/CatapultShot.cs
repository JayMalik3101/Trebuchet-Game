	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CatapultShot : MonoBehaviour
{
	public Rigidbody m_CurrentBoulder;
	public Transform m_Origin;
	[SerializeField] private Vector2 m_MinMaxRotations;
	[SerializeField] private Vector2 m_MinMaxPower;
	[SerializeField] private float m_Power;
	[SerializeField] private Transform m_Launcher;
	[SerializeField] private float m_ShotDuration;
	
	private bool m_ReadyToThrow = true;
	private bool m_CurrentlyLaunching = false;
	private bool m_Shoot;
	private float m_Duration = 0;
	private float m_DurationTwo = 0;
	private Tween m_LauncherTween;
	private TweenCallback m_LaunchingCompelete;
	private TweenCallback m_ReturnComplete;
	private Vector3 m_OriginalEulerRotation;
	private bool m_Returning;
	private StatKeeping m_Stats;
	private OVRGrabbable m_Grabbable;

	// Start is called before the first frame update
	void Start()
    {
        if(m_CurrentBoulder != null)
        {
            FreezeBoulder();
        }
		m_OriginalEulerRotation = m_Launcher.localEulerAngles;
		m_LaunchingCompelete += Returning;
		m_ReturnComplete += ReturningComplete;
		m_Grabbable = GetComponent<OVRGrabbable>();
		m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>(); 
	}

    // Update is called once per frame
    void Update()
    {
		if(m_ReadyToThrow && m_Returning == false && (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space))/* && m_Grabbable.grabbedBy == null*/)
		{	
			m_CurrentlyLaunching = true;
			m_ReadyToThrow = false;
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
			m_Duration = m_ShotDuration / m_Power;
			m_LauncherTween = m_Launcher.DOLocalRotate(new Vector3(m_MinMaxRotations.y, 0, 0), m_Duration + 0.25f, RotateMode.Fast).OnComplete(m_LaunchingCompelete);
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
		m_Returning = true;
		m_DurationTwo = 1.5f;
		m_Launcher.DOLocalRotate(m_OriginalEulerRotation, m_DurationTwo, RotateMode.Fast).OnComplete(m_ReturnComplete);
	}

	private void ReturningComplete()
	{
		m_Launcher.localEulerAngles = m_OriginalEulerRotation;
		m_ReadyToThrow = true;
		m_Returning = false;
	}

	private void ThrowBoulder()
	{
		
		if(m_CurrentBoulder != null)
		{
			UnFreezeBoulder();
			m_CurrentBoulder.AddForce(m_Origin.up * m_Power, ForceMode.Impulse);
			m_CurrentBoulder = null;
			m_Stats.m_ShotsFired ++;
		}
		m_CurrentlyLaunching = false;
	}

	public void FreezeBoulder()
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
