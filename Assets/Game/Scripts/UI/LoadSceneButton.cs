using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private int m_SceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")){
            SceneManager.LoadScene(m_SceneToLoad);
        }
    }
}
