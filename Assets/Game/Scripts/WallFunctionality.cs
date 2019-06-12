using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFunctionality : MonoBehaviour
{
	[SerializeField] private float m_NeededVelocity;
	[SerializeField] GameObject m_PristinePrefab;
	[SerializeField] GameObject m_BrokenPrefab;
	private BoxCollider[] m_Colliders = new BoxCollider[2];
    public bool m_Broken;
    // Start is called before the first frame update
    void Start()
    {
		m_Colliders = GetComponents<BoxCollider>();
    }

	private void OnTriggerEnter(Collider other)
	{
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Projectile") || collision.transform.CompareTag("Structure"))
		{
			Rigidbody rigidbody = collision.transform.GetComponent<Rigidbody>();
			
			if(rigidbody != null)
			{
				Debug.Log(rigidbody.velocity);
				if(rigidbody.velocity.x >= m_NeededVelocity || rigidbody.velocity.y >= m_NeededVelocity || rigidbody.velocity.z >= m_NeededVelocity || rigidbody.velocity.x <= -m_NeededVelocity || rigidbody.velocity.y <= -m_NeededVelocity || rigidbody.velocity.z <= -m_NeededVelocity)
				{
					SwitcheRoo();
				}
			}
		}
		else
		{
			Rigidbody myRigidbody = GetComponent<Rigidbody>();
			if (myRigidbody != null)
			{
				Debug.Log(myRigidbody.velocity);
				if (myRigidbody.velocity.x >= m_NeededVelocity || myRigidbody.velocity.y >= m_NeededVelocity || myRigidbody.velocity.z >= m_NeededVelocity || myRigidbody.velocity.x <= -m_NeededVelocity || myRigidbody.velocity.y <= -m_NeededVelocity || myRigidbody.velocity.z <= -m_NeededVelocity)
				{
					SwitcheRoo();
				}
			}
		}    }

    private void SwitcheRoo()
	{
		for (int i = 0; i < m_Colliders.Length; i++)
		{
			m_Colliders[i].enabled = false;
		}
		m_PristinePrefab.SetActive(false);
		m_BrokenPrefab.transform.parent = null;
		m_BrokenPrefab.SetActive(true);
        m_Broken = true;
		gameObject.SetActive(false);
	}
}


