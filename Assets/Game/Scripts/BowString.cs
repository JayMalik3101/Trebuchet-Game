using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
	private Vector3 m_OriginalPosition;
	private Quaternion m_OriginalRotation;
	private Rigidbody m_Rigidbody;
	private OVRGrabbable m_StringGrabbable;
    // Start is called before the first frame update
    void Start()
    {
		m_StringGrabbable = GetComponent<OVRGrabbable>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_OriginalPosition = transform.localPosition;
		m_OriginalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_StringGrabbable.grabbedBy == null)
		{
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			transform.localPosition = m_OriginalPosition;
			transform.localRotation = m_OriginalRotation;
		}
		else
		{
			m_Rigidbody.constraints = RigidbodyConstraints.None;
		}
    }
}
