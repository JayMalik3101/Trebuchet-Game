using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentLevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;
    private StatKeeping m_Stats;

    private void Start()
    {
        m_Stats = GameObject.Find("Stats").GetComponent<StatKeeping>();
    }

    private void OnEnable()
    {
        m_Text.text = m_Stats.m_CurrentLevel.ToString();
    }
}
