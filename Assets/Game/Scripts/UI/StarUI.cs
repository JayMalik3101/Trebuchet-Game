using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> m_StarSprites;
    [SerializeField] private SpriteRenderer m_Stars;
    private StatKeeping m_Stats;

    private void Start()
    {
        m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>();
    }

    private void OnEnable()
    {
        if(m_Stats.m_ShotsFired <= 1)
        {
            m_Stars.sprite = m_StarSprites[0];
        }
        else if (m_Stats.m_ShotsFired == 2)
        {
            m_Stars.sprite = m_StarSprites[1];
        }
        else if (m_Stats.m_ShotsFired == 3)
        {
            m_Stars.sprite = m_StarSprites[2];
        }
        else if (m_Stats.m_ShotsFired == 4)
        {
            m_Stars.sprite = m_StarSprites[3];
        }
        else if (m_Stats.m_ShotsFired >= 5)
        {
            m_Stars.sprite = m_StarSprites[4];
        }
    }
}
