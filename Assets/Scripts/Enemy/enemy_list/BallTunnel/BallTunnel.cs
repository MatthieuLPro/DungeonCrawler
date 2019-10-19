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
    private int _launchTime = 2;

    private bool _coIsRunning = false;
    private Vector3[] _spawnPos1 = new [] { Vector3.zero, Vector3.zero, Vector3.zero };
    private Vector3[] _spawnPos2 = new [] { Vector3.zero, Vector3.zero };
    private GameObject _prefab1 = null;
    private GameObject _prefab2 = null;

    private void Start()
    {
        Transform exitTransform = transform.GetChild(1).transform;

        if (_isHorizontal)
        {
            exitTransform.position = exitTransform.position + new Vector3(_length, 0, 0);
            if (_length > 0)
            {
                _spawnPos1[0] = transform.position + new Vector3(_length - _length + 1, -1, 0);
                _spawnPos1[1] = transform.position + new Vector3(_length - _length + 1, 0, 0);
                _spawnPos1[2] = transform.position + new Vector3(_length - _length + 1, 1, 0);
            }
            else
            {
                _spawnPos1[0] = transform.position + new Vector3(_length - _length - 1, -1, 0);
                _spawnPos1[1] = transform.position + new Vector3(_length - _length - 1, 0, 0);
                _spawnPos1[2] = transform.position + new Vector3(_length - _length - 1, 1, 0);
            }
        }
        else
        {
            exitTransform.position = exitTransform.position + new Vector3(0, _length, 0);
            if (_length > 0)
            {
                _spawnPos1[0] = transform.position + new Vector3(-1, _length - _length + 1, 0);
                _spawnPos1[1] = transform.position + new Vector3(0, _length - _length + 1, 0);
                _spawnPos1[2] = transform.position + new Vector3(1, _length - _length + 1, 0);
            }
            else
            {
                _spawnPos1[0] = transform.position + new Vector3(-1, _length - _length - 1, 0);
                _spawnPos1[1] = transform.position + new Vector3(0, _length - _length - 1, 0);
                _spawnPos1[2] = transform.position + new Vector3(1, _length - _length - 1, 0);
                _spawnPos2[0] = transform.position + new Vector3(-0.5f, _length - _length - 1, 0);
                _spawnPos2[1] = transform.position + new Vector3(0.5f, _length - _length - 1, 0);
            }
        }

        _prefab1 = Resources.Load("Prefabs/Enemis/Enemis_list/DeathBallSmall") as GameObject;
        _prefab2 = Resources.Load("Prefabs/Enemis/Enemis_list/DeathBallBig") as GameObject;
    }
    
    private void Update()
    {
        if (!_coIsRunning)
            StartCoroutine(NewBallCo());
    }

    private IEnumerator NewBallCo()
    {
        var randomBall = Random.Range(0, 2);

        _coIsRunning = true;
        GameObject InstantiatePrefab = Instantiate((randomBall == 0 ? _prefab1 : _prefab2),
                                                    randomBall == 0 ? _spawnPos1[Random.Range(0, 3)] : _spawnPos2[Random.Range(0, 2)],
                                                    Quaternion.identity);
        InstantiatePrefab.transform.SetParent(transform);
        yield return new WaitForSeconds(_launchTime);
        _coIsRunning = false;
    }
}
