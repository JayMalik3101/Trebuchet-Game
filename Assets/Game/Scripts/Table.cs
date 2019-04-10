using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
	[SerializeField] private float m_TurnDifference;
	[SerializeField] private float m_UpDownDifference;
	private Vector3 m_OriginalTablePosition;
	private Vector3 m_OriginalTableRotation;
	private Vector3 m_OriginalHandPosition;
	private Vector3 m_Difference;
	private Transform m_GrabbedBy;
	private bool m_Grabbed;
	private bool m_Rotating;
	private bool m_UpDown;

    // Update is called once per frame
    void Update()
    {
		if (m_Grabbed)
		{
			m_Difference = m_GrabbedBy.position - m_OriginalHandPosition;
			if (m_Rotating == false && m_UpDown == false)
			{
				if ((m_Difference.y >= m_UpDownDifference || m_Difference.y <= -m_UpDownDifference) && m_Rotating == false)
				{
					m_UpDown = true;
				}
				if ((m_Difference.x >= m_TurnDifference || m_Difference.x <= -m_TurnDifference) && m_UpDown == false)
				{
					m_Rotating = true;
				}
			}
			if (m_UpDown)
			{
				transform.position = new Vector3(transform.position.x, m_OriginalTablePosition.y + m_Difference.y, transform.position.z);
			}
			if (m_Rotating)
			{
				transform.Rotate(new Vector3(transform.eulerAngles.x, m_OriginalTableRotation.y - m_Difference.x, transform.eulerAngles.z));
				//transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_OriginalTableRotation.y - m_Difference.x * 20, transform.eulerAngles.z);
				//m_OriginalTableRotation = transform.eulerAngles;
			}
		}
    }

	private void OnTriggerStay(Collider other)
	{
		if ((OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))&& other.CompareTag("Hand"))
		{
			m_Grabbed = true;
			m_GrabbedBy = other.GetComponent<Transform>();
			m_OriginalTablePosition = transform.position;
			m_OriginalTableRotation = transform.eulerAngles;
			m_OriginalHandPosition = m_GrabbedBy.position;
		}
		if ((OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)) && other.CompareTag("Hand"))
		{
			m_Grabbed = false;
			m_GrabbedBy = null;
			m_OriginalHandPosition = Vector3.zero;
			m_OriginalTableRotation = Vector3.zero;
			m_OriginalTablePosition = Vector3.zero;
			m_Rotating = false;
			m_UpDown = false;
		}
	}
}
