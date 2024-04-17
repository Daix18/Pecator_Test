using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneSwapController : MonoBehaviour
{
    public static SceneSwapController THIS;

    private GameObject _player;
    private Collider2D _playerCollider;
    private Collider2D _interactiveCollider;
    private Collider2D _boundsCollider;
    private Vector3 _playerSpawnPosition;
    private CinemachineConfiner2D _confiner;

    public bool _loadedFromInteractive;

    private FinishPoint.SpawnPointAt _spawnPointTo;


    private void Awake()
    {
        if (THIS == null) 
        {
            THIS = this;
        }

        _boundsCollider = GameObject.FindGameObjectWithTag("Bounds").GetComponent<Collider2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _player.GetComponentInChildren<Collider2D>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public static void SwapScene(SceneField myScene, FinishPoint.SpawnPointAt spawnPointAt)
    {
        THIS._loadedFromInteractive = true;
        THIS.StartCoroutine(THIS.FadeOutTheChangeScene(myScene, spawnPointAt));
        THIS.StartCoroutine(THIS.BoundsChange());        
    }

    private IEnumerator FadeOutTheChangeScene(SceneField myScene, FinishPoint.SpawnPointAt spawnPointAt = FinishPoint.SpawnPointAt.None)
    {
        SceneFadeManager.THIS.StartFadeOut();

        while (SceneFadeManager.THIS.IsFadingOut) 
        {
            yield return null;   
        }

        _spawnPointTo = spawnPointAt;
        SceneManager.LoadScene(myScene);
    }

    private IEnumerator BoundsChange()
    {
        _confiner.m_BoundingShape2D = _boundsCollider;
        yield return new WaitForSeconds(1f);
        _confiner.InvalidateCache();
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        SceneFadeManager.THIS.StartFadeIn();
    
        if (_loadedFromInteractive)
        {
            FindSpawnPoint(_spawnPointTo);
            _player.transform.position = _playerSpawnPosition;
            _loadedFromInteractive = false;
        }
    }

    private void FindSpawnPoint(FinishPoint.SpawnPointAt finishPointNumber)
    {
        FinishPoint[] finishPoints = FindObjectsOfType<FinishPoint>();

        for (int i = 0; i < finishPoints.Length; i++)
        {
            if (finishPoints[i].CurrentFinishPointPosition == finishPointNumber)
            {
                _interactiveCollider = finishPoints[i].gameObject.GetComponent<Collider2D>();

                CalcualteSpawnPositon();
                return;
            }
        }
    }

    private void CalcualteSpawnPositon()
    {
        float colliderHeight = _playerCollider.bounds.extents.y;
        _playerSpawnPosition  = _interactiveCollider.transform.position - new Vector3(0f, colliderHeight, 0f);
    }
}
