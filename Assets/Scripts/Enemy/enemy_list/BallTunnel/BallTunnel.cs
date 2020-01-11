using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTunnel : MonoBehaviour
{
    [Header("Tunnel Settings")]
    [SerializeField]
    private bool _isHorizontal = false;
    [SerializeField]
    private float _length = 2;
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

    private bool _coIsRunning       = false;
    private bool _coLaunchIsRunning = false;

    private Vector3[] _spawnPos1 = new [] { Vector3.zero, Vector3.zero, Vector3.zero };
    private Vector3[] _spawnPos2 = new [] { Vector3.zero, Vector3.zero };

    private GameObject _prefab1 = null;
    private GameObject _prefab2 = null;

    private AudioSource _audio = null;

    private void Start()
    {
        Transform exitTransform = transform.GetChild(1).transform;

        _audio = GetComponent<AudioSource>();

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
    // Generate horizontal position
    private void GenerateHorizontalSpawn()
    {
        Transform exitTransform = transform.GetChild(1).transform;

        exitTransform.position = exitTransform.position + new Vector3(_length, 0, 0);
        if (_length > 0)
        {
            _spawnPos1[0] = transform.position + new Vector3(_length - _length + 1, 0, 0);
            if (_randomSpawn){
                _spawnPos1[1] = transform.position + new Vector3(_length - _length + 1, -1, 0);
                _spawnPos1[2] = transform.position + new Vector3(_length - _length + 1, 1, 0);
            }
        }
        else
        {
            _spawnPos1[0] = transform.position + new Vector3(_length - _length - 1, 0, 0);
            if (_randomSpawn){
                _spawnPos1[1] = transform.position + new Vector3(_length - _length - 1, -1, 0);
                _spawnPos1[2] = transform.position + new Vector3(_length - _length - 1, 1, 0);
            }
        }
    }

    private void GenerateVerticalSpawn()
    {
        Transform exitTransform = transform.GetChild(1).transform;

        exitTransform.position = exitTransform.position + new Vector3(0, _length, 0);
        if (_length > 0)
        {
            _spawnPos1[0] = transform.position + new Vector3(0, _length - _length + 1, 0);
            if (_randomSpawn){
                _spawnPos1[1] = transform.position + new Vector3(-1, _length - _length + 1, 0);
                _spawnPos1[2] = transform.position + new Vector3(1, _length - _length + 1, 0);
            }

            if (_differentTypes)
            {
                if (_randomSpawn){
                    _spawnPos2[0] = transform.position + new Vector3(-0.5f, _length - _length + 1, 0);
                    _spawnPos2[1] = transform.position + new Vector3(0.5f, _length - _length + 1, 0);
                }
                else
                    _spawnPos2[0] = transform.position + new Vector3(0, _length - _length + 1, 0);
            }
        }
        else
        {
            _spawnPos1[0] = transform.position + new Vector3(0, _length - _length - 1, 0);
            if (_randomSpawn){
                _spawnPos1[1] = transform.position + new Vector3(-1, _length - _length - 1, 0);
                _spawnPos1[2] = transform.position + new Vector3(1, _length - _length - 1, 0);
            }

            if (_differentTypes)
            {
                if (_randomSpawn){
                    _spawnPos2[0] = transform.position + new Vector3(-0.5f, _length - _length - 1, 0);
                    _spawnPos2[1] = transform.position + new Vector3(0.5f, _length - _length - 1, 0);
                }
                else
                    _spawnPos2[0] = transform.position + new Vector3(0, _length - _length - 1, 0);
            }
        }
    }

    // Generate object depending of settings
    private GameObject InstantiateNewBall()
    {
        GameObject InstantiatePrefab = null;

        if (_randomSpawn && _differentTypes)
        {
            var randomBall = Random.Range(0, 2);

            InstantiatePrefab =  Instantiate((randomBall == 0 ? _prefab1 : _prefab2),
                                              randomBall == 0 ? _spawnPos1[Random.Range(0, 3)] : _spawnPos2[Random.Range(0, 2)],
                                              Quaternion.identity);
        }
        else if(_randomSpawn)
        {
            InstantiatePrefab =  Instantiate(_prefab1,
                                             _spawnPos1[Random.Range(0, 3)],
                                             Quaternion.identity);
        }
        else if(_differentTypes)
        {
            var randomBall = Random.Range(0, 2);

            InstantiatePrefab =  Instantiate((randomBall == 0 ? _prefab1 : _prefab2),
                                              randomBall == 0 ? _spawnPos1[0] : _spawnPos2[0],
                                              Quaternion.identity);
        }
        else
        {
            InstantiatePrefab =  Instantiate(_prefab1,
                                             _spawnPos1[0],
                                             Quaternion.identity);            
        }
        return InstantiatePrefab;
    }

    // Set prefab direction movement
    private void SetPrefabParams(GameObject prefabMovement)
    {
        prefabMovement.GetComponent<EnemyDirection>().speed = _prefabSpeed;

        if (_isHorizontal)
        {
            if (_length > 0)
                prefabMovement.GetComponent<EnemyDirection>().onlyRight = true;
            else
                prefabMovement.GetComponent<EnemyDirection>().onlyLeft = true;
        }
        else
        {
            if (_length > 0)
                prefabMovement.GetComponent<EnemyDirection>().onlyTop = true;
            else
                prefabMovement.GetComponent<EnemyDirection>().onlyDown = true;                
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
        instantiatePrefab.layer = gameObject.layer;
        instantiatePrefab.GetComponent<SpriteRenderer>().sortingLayerName = GetSortingLayer();
        SetPrefabParams(instantiatePrefab.transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(_launchTime);
        _coIsRunning = false;
    }
}
