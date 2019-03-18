using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
	private Vector3 m_OriginalPosition;
	private OVRGrabbable m_StringGrabbable;
    // Start is called before the first frame update
    void Start()
    {
		m_StringGrabbable = GetComponent<OVRGrabbable>();
		m_OriginalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_StringGrabbable.grabbedBy == null)
		{
			transform.localPosition = m_OriginalPosition;
		}
    }
}
