using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatKeeping : MonoBehaviour
{

	public float m_ShotsFired;
	public float m_ShotsLeft;
	public float m_EnemiesAlive;
    public float m_CurrentLevel;

    [SerializeField] private TextMeshProUGUI m_ShotsFiredText;
    [SerializeField] private TextMeshProUGUI m_EnemiesAliveText;
    [SerializeField] private TextMeshProUGUI m_ShotsLeftText;

    private void Update()
    {
        if(m_ShotsFiredText != null)
        m_ShotsFiredText.text = m_ShotsFired.ToString();

        if (m_EnemiesAliveText != null)
            m_EnemiesAliveText.text = m_EnemiesAlive.ToString();

        if (m_ShotsLeftText != null)
            m_ShotsLeftText.text = m_ShotsLeft.ToString();
    }
}
