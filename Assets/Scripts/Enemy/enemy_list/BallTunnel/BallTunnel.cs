using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTunnel : MonoBehaviour
{
    [Header("Tunnel Settings")]
    [SerializeField]
    private float _launchTime = 2;
    [SerializeField]
    private float _firstLaunchOffset = 0;
    [SerializeField]
    private float _prefabSpeed = 4;
    [SerializeField]
    private bool _differentTypes    = false;
    [SerializeField]
    private bool _randomSpawn       = false;

    private bool _isHorizontal = false;
    private float _length;

    private bool _coIsRunning       = false;
    private bool _coLaunchIsRunning = false;

    private Vector3[] _spawnPos1 = new [] { Vector3.zero, Vector3.zero, Vector3.zero };
    private Vector3[] _spawnPos2 = new [] { Vector3.zero, Vector3.zero };

    private GameObject _prefab1 = null;
    private GameObject _prefab2 = null;

    private string _sortingLayer;
    private int    _layer;

    private AudioSource _audio = null;

    private void Start()
    {
        Transform exitTransform = transform.GetChild(1).transform;

        _audio = GetComponent<AudioSource>();
        _sortingLayer = GetSortingLayer();
        _layer = gameObject.layer;

        CalculateDistance();

        if (_isHorizontal)
            GenerateHorizontalSpawn();
        else
            GenerateVerticalSpawn();

        _prefab1 = Resources.Load("Prefabs/Enemis/Enemis_list/DeathBallSmall") as GameObject;

        if(_differentTypes)
            _prefab2 = Resources.Load("Prefabs/Enemis/Enemis_list/DeathBallBig") as GameObject;
        
        StartCoroutine(FirstLaunchCo());
    }
    
    private void Update()
    {
        if (!_coIsRunning && !_coLaunchIsRunning)
            StartCoroutine(NewBallCo());
    }

    /* ************************************************ */
    /* General functions */
    /* ************************************************ */
    // Get distance between entrance and exit
    private void CalculateDistance()
    {
        Vector2 exitPosition = transform.GetChild(1).transform.position;

        _isHorizontal = true;
        _length = exitPosition.x - transform.position.x;

        if(transform.position.x == exitPosition.x)
        {
            _isHorizontal = false;
            _length = exitPosition.y - transform.position.y;
        }
    }

    // Generate horizontal spawn position
    private void GenerateHorizontalSpawn()
    {
        int flag = (_length > 0 ? 1 : -1);

        _spawnPos1[0] = transform.position + new Vector3(_length - _length + flag, 0, 0);
        if (_randomSpawn){
            _spawnPos1[1] = transform.position + new Vector3(_length - _length + flag, -1, 0);
            _spawnPos1[2] = transform.position + new Vector3(_length - _length + flag, 1, 0);
        }
    }

    // Generate vertical spawn position
    private void GenerateVerticalSpawn()
    {
        int flag = (_length > 0 ? 1 : -1);

        _spawnPos1[0] = transform.position + new Vector3(0, _length - _length + flag, 0);
        if (_randomSpawn){
            _spawnPos1[1] = transform.position + new Vector3(-1, _length - _length + flag, 0);
            _spawnPos1[2] = transform.position + new Vector3(1, _length - _length + flag, 0);
        }

        if (_differentTypes)
        {
            if (_randomSpawn){
                _spawnPos2[0] = transform.position + new Vector3(-0.5f, _length - _length + flag, 0);
                _spawnPos2[1] = transform.position + new Vector3(0.5f, _length - _length + flag, 0);
            }
            else
                _spawnPos2[0] = transform.position + new Vector3(0, _length - _length + flag, 0);
        }
    }

    // Generate object depending of settings
    private GameObject InstantiateNewBall()
    {
        GameObject newPrefab            = NewPrefabType();
        Vector3 newPos                  = Vector3.zero;

        newPos = NewPrefabPosition(0);
        if (newPrefab == _prefab1)
            newPos = NewPrefabPosition(0);
        else
            newPos = NewPrefabPosition(1);

        return (Instantiate(newPrefab, newPos, Quaternion.identity));
    }

    private Vector3 NewPrefabPosition(int prefabType)
    {
        // PrefabType == 0 : smallBall
        // PrefabType == 1 : bigBall
        if (_randomSpawn)
        {
            if (prefabType == 1)
                return _spawnPos2[Random.Range(0, 2)];

            return _spawnPos1[Random.Range(0, 3)];
        }
        else if (prefabType == 1)
            return _spawnPos2[0];

        return _spawnPos1[0];
    }

    private GameObject NewPrefabType()
    {
        if (_differentTypes && Random.Range(0, 2) != 0)
            return _prefab2;

        return _prefab1;
    }

    // Set prefab direction movement
    private void SetPrefabParams(EnemyDirection enemyDirection)
    {
        enemyDirection.UpdateSpeed(_prefabSpeed);
        if (_isHorizontal)
        {
            if (_length > 0)
                enemyDirection.UpdateDirection("right");
            else
                enemyDirection.UpdateDirection("left");
        }
        else
        {
            if (_length > 0)
                enemyDirection.UpdateDirection("top");
            else
                enemyDirection.UpdateDirection("down");
        }
    }

    // Get sorting layer name depending of level
    private string GetSortingLayer()
    {
        if (gameObject.layer == 24)
            return "player_d1";
        else if(gameObject.layer == 25)
            return "player_0";
        
        return "player_u1";
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    // Offset time to launch first ball
    private IEnumerator FirstLaunchCo()
    {
        _coLaunchIsRunning = true;

        yield return new WaitForSeconds(_firstLaunchOffset);
        _coLaunchIsRunning = false;
    }

    // Generate new ball and wait
    private IEnumerator NewBallCo()
    {
        _coIsRunning = true;
        GameObject instantiatePrefab = InstantiateNewBall();

        _audio.Play();

        instantiatePrefab.transform.SetParent(transform);
        instantiatePrefab.layer = _layer;
        instantiatePrefab.GetComponent<SpriteRenderer>().sortingLayerName = _sortingLayer;
        SetPrefabParams(instantiatePrefab.transform.GetChild(0).gameObject.GetComponent<EnemyDirection>());

        yield return new WaitForSeconds(_launchTime);
        _coIsRunning = false;
    }
}
