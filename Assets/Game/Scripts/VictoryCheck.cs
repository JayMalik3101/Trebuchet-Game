using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCheck : MonoBehaviour
{
    [SerializeField] List<WallFunctionality> m_AllWalls;
    [SerializeField] int m_ToBeDestroyed;
    [SerializeField] private GameObject m_UI;
    private int m_CurrentlyDestroyed;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_AllWalls.Count; i++)
        {
            if (m_AllWalls[i].m_Broken)
            {
                m_CurrentlyDestroyed++;
                Debug.Log(m_AllWalls[i].gameObject.name + "Pre Destruction");
                Destroy(m_AllWalls[i].gameObject);
                Debug.Log(m_AllWalls[i].gameObject.name + "Post Destruction");
                m_AllWalls.RemoveAt(i);
                Debug.Log(m_AllWalls[i].gameObject.name + "PostListRemoval");
            }
        }
        if(m_CurrentlyDestroyed >= m_ToBeDestroyed)
        {
            m_UI.SetActive(true);
        }
    }
}
