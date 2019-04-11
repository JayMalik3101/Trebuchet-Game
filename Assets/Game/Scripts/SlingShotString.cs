using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotString : MonoBehaviour
{
	[SerializeField] private Transform m_SlingShot;
	[SerializeField] private Transform m_StartPoint;
	[SerializeField] private Transform m_StartPointLeft;
	[SerializeField] private Transform m_StartPointRight;
	[SerializeField] private Transform m_Pull;
	[SerializeField] private Transform m_Left;
	[SerializeField] private Transform m_Right;
	private Vector3 m_OriginalPosition;
	private Quaternion m_OriginalRotation;
	[SerializeField] private OVRGrabbable m_StringGrabbable;
    // Start is called before the first frame update
    void Start()
    {
		m_Left.parent = m_StartPointLeft;
		m_Right.parent = m_StartPointRight;
		m_Right.localPosition = new Vector3(m_Right.localPosition.x, 0, m_Right.localPosition.z);
		m_Left.localPosition = new Vector3(m_Left.localPosition.x, 0, m_Left.localPosition.z);
	}

    // Update is called once per frame
    void Update()
    {
        if(m_StringGrabbable.grabbedBy == null)
		{
			m_Pull.position = m_StartPoint.position;
			m_Pull.rotation = m_StartPoint.rotation;
		}
		//m_Left.position = m_StartPointLeft.position;
		//m_Right.position = m_StartPointRight.position;
	}
}
