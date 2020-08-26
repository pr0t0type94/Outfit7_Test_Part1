using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
    public void LoadGameScene()
    {
        m_audioSource.PlayOneShot(m_audioSource.clip);
        SceneManager.LoadScene(1);
    }
}
