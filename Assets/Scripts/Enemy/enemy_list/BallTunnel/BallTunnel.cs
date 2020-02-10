using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTunnel : MonoBehaviour
{
    [Header("Tunnel Settings")]
    [SerializeField]
    private float _launchDelay = 0;
    [SerializeField]
    private float _prefabSpeed = 4;
    [SerializeField]
    private bool _differentTypes    = false;
    [SerializeField]
    private bool _randomSpawn       = false;

    private bool _isHorizontal = false;
    private float _length;
    private int _directionX = 0;
    private int _directionY = 0;

    private Vector3[] _spawnType1 = new [] { Vector3.zero, Vector3.zero, Vector3.zero };
    private Vector3[] _spawnType2 = new [] { Vector3.zero, Vector3.zero };

    private GameObject _prefab1 = null;
    private GameObject _prefab2 = null;

    private string _sortingLayer;
    private int    _layer;

    private AudioSource _audio = null;

    public int numberOfBalls;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        
        _SetObjectLayer();
        _SetDistanceEntranceExit();
        _SetObjectDirection();
        _SetSpawnPosition();
        _SetPrefabModel();

        //_audio.Play();

        StartCoroutine(_FirstLaunchCo());
    }
    
    /* ************************************************ */
    /* Initialize functions & setters */
    /* ************************************************ */
    // Get distance between entrance and exit
    private void _SetDistanceEntranceExit()
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

    // Generate spwan position
    private void _SetSpawnPosition()
    {
        if (_isHorizontal)
            _SetHorizontalSpawnPosition();
        else
            _SetVerticalSpawnPosition();
    }
    // Generate horizontal spawn position
    private void _SetHorizontalSpawnPosition()
    {
        int flag = (_length > 0 ? 1 : -1);

        _spawnType1[0] = transform.position + new Vector3(_length - _length + flag, 0, 0);
        if (_randomSpawn){
            _spawnType1[1] = transform.position + new Vector3(_length - _length + flag, -1, 0);
            _spawnType1[2] = transform.position + new Vector3(_length - _length + flag, 1, 0);
        }
    }

    // Generate vertical spawn position
    private void _SetVerticalSpawnPosition()
    {
        int flag = (_length > 0 ? 1 : -1);

        _spawnType1[0] = transform.position + new Vector3(0, _length - _length + flag, 0);
        if (_randomSpawn){
            _spawnType1[1] = transform.position + new Vector3(-1, _length - _length + flag, 0);
            _spawnType1[2] = transform.position + new Vector3(1, _length - _length + flag, 0);
        }

        if (_differentTypes)
        {
            if (_randomSpawn){
                _spawnType2[0] = transform.position + new Vector3(-0.5f, _length - _length + flag, 0);
                _spawnType2[1] = transform.position + new Vector3(0.5f, _length - _length + flag, 0);
            }
            else
                _spawnType2[0] = transform.position + new Vector3(0, _length - _length + flag, 0);
        }
    }

    // Generate prefab model from resource
    private void _SetPrefabModel()
    {
        //_prefab1 = Resources.Load("Prefabs/Enemis/Enemis_list/DeathBallSmall") as GameObject;
        _prefab1 = Resources.Load("Prefabs/Enemis/DtmSingleMvtTest") as GameObject;

        if(_differentTypes)
            _prefab2 = Resources.Load("Prefabs/Enemis/DtmSingleMvtTestBis") as GameObject;
            //_prefab2 = Resources.Load("Prefabs/Enemis/Enemis_list/DeathBallBig") as GameObject;
    }

    // Set sorting layer name depending of level
    private void _SetObjectLayer()
    {
        if (gameObject.layer == 24)
            _sortingLayer = "player_d1";
        else if(gameObject.layer == 25)
            _sortingLayer = "player_0";
        else 
            _sortingLayer = "player_u1";

        _layer = gameObject.layer;
    }

    private void _SetObjectDirection()
    {
        if (_isHorizontal)
        {
            if (_length > 0)
                _directionX = 1;
            else
                _directionX = -1;
        }
        else
        {
            if (_length > 0)
                _directionY = 1;
            else
                _directionY = -1;
        }
    }
    /* ************************************************ */
    /* Create functions */
    /* ************************************************ */
    // Generate object depending of settings
    private GameObject _InstantiateNewBall()
    {
        GameObject newPrefab            = _NewPrefabType();
        Vector3 newPos                  = Vector3.zero;

        newPos = _NewPrefabPosition(0);
        if (newPrefab == _prefab1)
            newPos = _NewPrefabPosition(0);
        else
            newPos = _NewPrefabPosition(1);

        return (Instantiate(newPrefab, newPos, Quaternion.identity));
    }

    private Vector3 _NewPrefabPosition(int prefabType)
    {
        // PrefabType == 0 : smallBall
        // PrefabType == 1 : bigBall
        if (_randomSpawn)
        {
            if (prefabType == 1)
                return _spawnType2[Random.Range(0, 2)];

            return _spawnType1[Random.Range(0, 3)];
        }
        else if (prefabType == 1)
            return _spawnType2[0];

        return _spawnType1[0];
    }

    private GameObject _NewPrefabType()
    {
        if (_differentTypes && Random.Range(0, 2) != 0)
            return _prefab2;

        return _prefab1;
    }

    // Set prefab direction movement
    private void _UpdatePrefabParams(DeterminateSingleDirections enemyDirection, NpcGeneralAddForces enemySpeed)
    {
        enemyDirection._DirectionX  = _directionX;
        enemyDirection._DirectionY  = _directionY;
        enemySpeed._Speed           = _prefabSpeed;
    }

    /* ************************************************ */
    /* Getter */
    /* ************************************************ */
    public Vector3[] GetSpawnVector()
    {
        if (_spawnType1[0] == Vector3.zero)
            return _spawnType2;
        
        return _spawnType1;
    }

    public bool GetRandomSpawn(){
        return _randomSpawn;
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    // Offset time to launch first ball
    private IEnumerator _FirstLaunchCo()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            yield return new WaitForSeconds(_launchDelay);

            GameObject myBall                           = _InstantiateNewBall();
            DeterminateSingleDirections myBallDirection = myBall.transform.GetChild(0).transform.GetChild(0).GetComponent<DeterminateSingleDirections>();
            NpcGeneralAddForces myBallSpeed             = myBall.transform.GetChild(0).transform.GetChild(1).GetComponent<NpcGeneralAddForces>();

            myBall.transform.SetParent(transform);
            myBall.layer = _layer;
            myBall.transform.GetChild(0).gameObject.layer = _layer;
            myBall.transform.GetChild(1).gameObject.layer = _layer;
            myBall.GetComponent<SpriteRenderer>().sortingLayerName = _sortingLayer;
            _UpdatePrefabParams(myBallDirection, myBallSpeed);
        }
    }
}
