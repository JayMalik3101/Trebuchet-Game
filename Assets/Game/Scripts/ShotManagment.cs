using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotManagment : MonoBehaviour
{
	[SerializeField] List<GameObject> m_Projectiles = new List<GameObject>();
	private StatKeeping m_Stats;
    // Start is called before the first frame update
    void Start()
    {
		m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Projectile"))
		{
			m_Projectiles.Add(other.gameObject);
			m_Stats.m_ShotsLeft++;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Projectile"))
		{
			for (int i = 0; i < m_Projectiles.Count; i++)
			{
				if(m_Projectiles[i] == other.gameObject)
				{
					m_Projectiles.RemoveAt(i);
					m_Stats.m_ShotsLeft--;
				}
			}
		}
	}
}
