using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public interface IState
{
	void OnEnter();
	void Update();
	void OnExit();
}

public class SlingShot : MonoBehaviour
{
	public Rigidbody m_CurrentProjectile;
	public Transform m_Origin;

	public OVRGrabbable m_Grabbable;
	public OVRGrabbable m_StringGrabbable;

	[SerializeField] private float m_Power;
	[SerializeField] public Rigidbody m_String;
	[SerializeField] public Transform m_StringStartPoint;
	[SerializeField] private Transform m_Target;
	[SerializeField] private float m_OriginalTimer;

	public float m_Timer;

	private bool m_ReadyToShoot = true;
	private bool m_CurrentlyLaunching = false;
	private float m_Distance;
	private Vector3 m_AllDistances;
	public Vector3 AllDistances { get { return m_AllDistances; } set { m_AllDistances = value; } }

	private Quaternion m_OriginalRotation;
	private StatKeeping m_Stats;

	public IState m_CurrentState;

	private void Awake()
	{
		m_CurrentState = new NotHolding(this);
	}

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
		if (Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
		{
			SceneManager.LoadScene(0);
		}
		if (m_Grabbable.grabbedBy == null)
		{
			SwitchState(new NotHolding(this));
		}
		if (m_CurrentState != null)
			m_CurrentState.Update();
	}

	public void Fire()
	{
		if (m_CurrentProjectile != null)
		{
			UnFreezeProjectile();
			m_CurrentProjectile.AddForce(m_Origin.forward * (m_Distance * m_Power), ForceMode.Impulse);
			m_CurrentProjectile = null;
			m_Timer = m_OriginalTimer;
			m_Distance = 0;
			m_Stats.m_ShotsFired++;
		}
	}

	public void CorrectOrientation()
	{

		transform.rotation = Quaternion.identity;
		m_String.rotation = Quaternion.identity;
		Vector3 relativePos = m_Target.position - m_String.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
		transform.rotation = rotation;
		//relativePos = m_String.position - m_Target.position;
		//rotation = Quaternion.LookRotation(relativePos, Vector3.up);
		m_String.transform.LookAt(m_Target.position);
		m_String.transform.eulerAngles = m_String.transform.eulerAngles + Vector3.forward * 180;
	}

	public void FreezeProjectile()
	{
		m_CurrentProjectile.velocity = Vector3.zero;
		m_CurrentProjectile.GetComponent<Collider>().enabled = false;
		m_CurrentProjectile.constraints = RigidbodyConstraints.FreezeAll;
		m_CurrentProjectile.transform.parent = m_Origin;
		m_CurrentProjectile.useGravity = false;
	}

	private void UnFreezeProjectile()
	{
		m_CurrentProjectile.velocity = Vector3.zero;
		m_CurrentProjectile.GetComponent<Collider>().enabled = true;
		m_CurrentProjectile.constraints = RigidbodyConstraints.None;
		m_CurrentProjectile.transform.parent = null;
		m_CurrentProjectile.useGravity = true;
	}

	public void CheckWhatsBiggest()
	{
		if (m_AllDistances.x >= m_Distance)
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

	public void SwitchState(IState stateToSwitch)
	{
		m_CurrentState.OnExit();
		m_CurrentState = stateToSwitch;
		if (stateToSwitch != null)
			m_CurrentState.OnEnter();
	}
}

public class NotHolding : IState
{
	SlingShot m_SlingShot;

	public NotHolding(SlingShot refObject)
	{
		m_SlingShot = refObject;
	}

	public void OnEnter()
	{
		m_SlingShot.m_Grabbable.m_BothGrabbed = false;
		m_SlingShot.m_StringGrabbable.m_BothGrabbed = false;
	}
	public void Update()
	{
		if (m_SlingShot.m_Grabbable.grabbedBy != null)
		{
			m_SlingShot.SwitchState(new Holding(m_SlingShot));
		}
	}
	public void OnExit()
	{

	}
}




public class Holding : IState
{
	SlingShot m_SlingShot;

	public Holding(SlingShot refObject)
	{
		m_SlingShot = refObject;
	}

	public void OnEnter()
	{
		m_SlingShot.m_Grabbable.m_BothGrabbed = false;
		m_SlingShot.m_StringGrabbable.m_BothGrabbed = false;
		m_SlingShot.m_Grabbable.m_OneGrabbed = true;
	}
	public void Update()
	{

		if (m_SlingShot.m_Grabbable.grabbedBy != null && m_SlingShot.m_StringGrabbable.grabbedBy != null)
		{
			m_SlingShot.SwitchState(new DoubleHolding(m_SlingShot));
		}
	}
	public void OnExit()
	{
		m_SlingShot.m_Grabbable.m_OneGrabbed = false;
	}
}

public class DoubleHolding : IState
{
	SlingShot m_SlingShot;

	public DoubleHolding(SlingShot refObject)
	{
		m_SlingShot = refObject;
	}

	public void OnEnter()
	{
		m_SlingShot.m_Grabbable.m_BothGrabbed = true;
		m_SlingShot.m_StringGrabbable.m_BothGrabbed = true;
	}
	public void Update()
	{
		m_SlingShot.AllDistances = m_SlingShot.m_StringStartPoint.position - m_SlingShot.m_String.position;
		m_SlingShot.CheckWhatsBiggest();
		m_SlingShot.CorrectOrientation();
		if (m_SlingShot.m_StringGrabbable.grabbedBy == null)
		{
			m_SlingShot.SwitchState(new Shooting(m_SlingShot));
		}
	}
	public void OnExit()
	{
	}
}

public class Shooting : IState
{
	SlingShot m_SlingShot;

	public Shooting(SlingShot refObject)
	{
		m_SlingShot = refObject;
	}

	public void OnEnter()
	{
		m_SlingShot.m_Grabbable.m_BothGrabbed = true;
		m_SlingShot.m_StringGrabbable.m_BothGrabbed = true;
	}
	public void Update()
	{
		m_SlingShot.m_Timer -= Time.deltaTime;
		if (m_SlingShot.m_Timer <= 0)
		{
			m_SlingShot.SwitchState(new Holding(m_SlingShot));
		}
	}
	public void OnExit()
	{
		m_SlingShot.Fire();
	}
}
