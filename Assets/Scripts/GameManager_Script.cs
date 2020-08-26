using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager_Script : MonoBehaviour
{
    private static GameManager_Script _instance;
    public static GameManager_Script m_gameManagerInstance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager null");
            }
            return _instance;
            
        }
    }
    [Header("Object Pooling")]
    [SerializeField]
    private GameObject m_spaceShipPrefab = null;
    [SerializeField]
    private GameObject m_bulletPrefab = null;
    [SerializeField]
    private GameObject m_asteroidPrefab = null;
    [HideInInspector]
    public GameobjectsPool_Script m_basicEnemiesPool;
    [HideInInspector]
    public GameobjectsPool_Script m_bulletPool;
    [HideInInspector]
    public GameobjectsPool_Script m_asteroidsPool;

    [Header("Canvas & Points")]
    [SerializeField]
    private TextMeshProUGUI m_playerPointsTextMeshPro = null;
    [SerializeField]
    private GameObject m_pauseMenuCanvasPanel = null;
    [SerializeField]
    private GameObject m_gameoverCanvasPanel = null;
    [SerializeField]
    private GameObject m_powerUpsCanvasPanel = null;
    [SerializeField]
    private TextMeshProUGUI m_playerFinalScoreText = null;
    [SerializeField]
    private PowerUpsManager m_powerUpsManager = null;

    private float m_playerPointsForKills = 0;
    private float m_playerFinalScore = 0;
    private WaveSpawner m_waveSpawner = null;


    public event Action StopAllBehaviours = delegate { };
    public event Action ContinueAllBehaviours = delegate { };

    private void Awake()
    {
        // simple singleton pattern
        _instance = this;
        m_playerPointsForKills = 0;

        //initialize objects pools & generate new parents on GameManager object in order to have the hierarchy clean
        GameObject l_poolParentHolder = new GameObject("PoolsObjHandler");
        l_poolParentHolder.transform.parent = gameObject.transform;

        GameObject l_enemiesPoolParent = new GameObject("EnemiesPool");
        l_enemiesPoolParent.transform.parent = l_poolParentHolder.transform;
        m_basicEnemiesPool = new GameobjectsPool_Script(100, m_spaceShipPrefab, l_enemiesPoolParent.transform); 
        
        GameObject l_bulletsPoolParent = new GameObject("BulletsPool");
        l_bulletsPoolParent.transform.parent = l_poolParentHolder.transform;
        m_bulletPool = new GameobjectsPool_Script(200, m_bulletPrefab, l_bulletsPoolParent.transform);

        //GameObject l_asteroidsPoolParent = new GameObject("AsteroidsPool");
        //l_asteroidsPoolParent.transform.parent = l_poolParentHolder.transform;
        //m_asteroidsPool = new GameobjectsPool_Script(10, m_asteroidPrefab, l_asteroidsPoolParent.transform);

        m_waveSpawner = GetComponent<WaveSpawner>();
    }
    private void Start()
    {
        TimeController.m_timerInstance.StartTimer();
        m_powerUpsManager.ReceivePowerUpEffect += ClosePowerUpMenu;
        m_waveSpawner.RewardPlayer += OpenPowerUpMenu;
        //TimeController.m_timerInstance.OneMinutePlaying += OpenPowerUpMenu;
    }

    private void Update()
    {
        CalculatePlayerPoints(out m_playerFinalScore);

        //for debugging purposes  
        if (Input.GetKeyDown(KeyCode.P)) OpenPowerUpMenu();
    }

    public void GameOver()
    {
        StopAllBehaviours();

        foreach(GameObject go in m_bulletPool.m_Elements)
        {
            go.gameObject.SetActive(false);
        } 
        m_playerFinalScoreText.text = $"Score:   {m_playerFinalScore.ToString()}";
        m_gameoverCanvasPanel.SetActive(true);
        TimeController.m_timerInstance.PauseTimer();

    }
    private void CalculatePlayerPoints(out float _playerFinalPoints)
    {
        TimeSpan l_elapsedTimeSpan = TimeController.m_timerInstance.m_timePlaying;
        float l_pointsForTime = l_elapsedTimeSpan.Seconds / 2;
        _playerFinalPoints = l_pointsForTime + m_playerPointsForKills;
        m_playerPointsTextMeshPro.text = _playerFinalPoints.ToString();

    }
    public void AddPoints(float _amount)
    {
        m_playerPointsForKills += _amount; 
    }

    public void OpenPauseMenu()
    {

        if (!m_pauseMenuCanvasPanel.activeInHierarchy)
        {
            m_pauseMenuCanvasPanel.SetActive(true);
            TimeController.m_timerInstance.PauseTimer();
            StopAllBehaviours();

        }
        else
        {
            m_pauseMenuCanvasPanel.SetActive(false);
            TimeController.m_timerInstance.ResumeTimer();
            ContinueAllBehaviours();
        }
    }

    public void OpenPowerUpMenu()
    {
        m_powerUpsManager.enabled = true;
        m_powerUpsCanvasPanel.SetActive(true);
        TimeController.m_timerInstance.PauseTimer();
        StopAllBehaviours();

    }
    public void ClosePowerUpMenu()
    {
        m_powerUpsManager.enabled = false;
        m_powerUpsCanvasPanel.SetActive(false);
        TimeController.m_timerInstance.ResumeTimer();
        ContinueAllBehaviours();
    }

    public void ResetLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    //private IEnumerator waitForKeyPress(KeyCode _key)
    //{
    //    bool l_done = false;
    //    while (!l_done)
    //    {
    //        if (Input.GetKeyDown(_key))
    //        {
    //            l_done = true;
    //        }
    //        yield return null;
    //    }

    //}
}
