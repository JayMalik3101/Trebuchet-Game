using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFunctionality : MonoBehaviour
{
	[SerializeField] private float m_NeededVelocity;
	[SerializeField] GameObject m_PristinePrefab;
	[SerializeField] GameObject m_BrokenPrefab;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Projectile") || other.CompareTag("Structure"))
		{
			Rigidbody rigidbody = other.GetComponent<Rigidbody>();
			if(rigidbody != null)
			{
				Debug.Log(rigidbody.velocity);
				if(rigidbody.velocity.x >= m_NeededVelocity || rigidbody.velocity.y >= m_NeededVelocity || rigidbody.velocity.z >= m_NeededVelocity)
				{
					
				}
			}
		}
	}
	private void SwitcheRoo()
	{
		m_PristinePrefab.SetActive(false);
		m_BrokenPrefab.SetActive(true);		
	}
}


