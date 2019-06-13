using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> m_StarSprites;
    [SerializeField] private SpriteRenderer m_Stars;
    [Tooltip("Left is minimum right is maximum ammount of shots fired for the desired star score from 5 stars to 1")]
    [SerializeField] private List<Vector2> m_RequiredScores;
    [SerializeField] private StatKeeping m_Stats;

    private void Start()
    {
        if(m_Stats == null)
        {
            m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>();
        }
    }

    private void OnEnable()
    {
        if(m_Stats.m_ShotsFired >= m_RequiredScores[0].x && m_Stats.m_ShotsFired <= m_RequiredScores[0].y)
        {
            m_Stars.sprite = m_StarSprites[0];
        }
        else if (m_Stats.m_ShotsFired >= m_RequiredScores[1].x && m_Stats.m_ShotsFired <= m_RequiredScores[1].y)
        {
            m_Stars.sprite = m_StarSprites[1];
        }
        else if (m_Stats.m_ShotsFired >= m_RequiredScores[2].x && m_Stats.m_ShotsFired <= m_RequiredScores[2].y)
        {
            m_Stars.sprite = m_StarSprites[2];
        }
        else if (m_Stats.m_ShotsFired >= m_RequiredScores[3].x && m_Stats.m_ShotsFired <= m_RequiredScores[3].y)
        {
            m_Stars.sprite = m_StarSprites[3];
        }
        else if (m_Stats.m_ShotsFired >= m_RequiredScores[4].x && m_Stats.m_ShotsFired <= m_RequiredScores[4].y)
        {
            m_Stars.sprite = m_StarSprites[4];
        }
    }
}
