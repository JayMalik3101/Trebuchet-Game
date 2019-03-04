using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainObject : MonoBehaviour
{
	[SerializeField] private Transform m_TopOfTable;
	[SerializeField] private Vector2 m_UpForceLimit;
	[SerializeField] private Vector2 m_HorizontalForceLimit;
	private OVRGrabbable m_Grabbable;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Grabbable != null)
		{
			if(m_Grabbable.isGrabbed == false)
			{
				m_Grabbable.transform.position = new Vector3(m_Grabbable.transform.position.x, m_TopOfTable.position.y, m_Grabbable.transform.position.z);
				Rigidbody gRigidbody = m_Grabbable.GetComponent<Rigidbody>();
				m_Grabbable.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0, 360), transform.eulerAngles.z);
				gRigidbody.AddForce(transform.forward * Random.Range(m_HorizontalForceLimit.x, m_HorizontalForceLimit.y), ForceMode.Impulse);
				gRigidbody.AddForce(transform.up * Random.Range(m_UpForceLimit.x,m_UpForceLimit.y), ForceMode.Impulse);
			}
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		
		if(other.CompareTag("Launcher"))
		{
			if(m_Grabbable == null)
			{
				m_Grabbable = other.GetComponent<OVRGrabbable>();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(m_Grabbable != null && other.name == m_Grabbable.gameObject.name)
		{
			m_Grabbable = null;
		}
	}
}
