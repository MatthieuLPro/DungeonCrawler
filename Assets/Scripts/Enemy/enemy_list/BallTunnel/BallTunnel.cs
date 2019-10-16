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
    private Vector3 _spawnPos = Vector3.zero;
    private GameObject _prefab = null;

    private void Start()
    {
        Transform exitTransform = transform.GetChild(1).transform;

        if (_isHorizontal)
        {
            exitTransform.position = exitTransform.position + new Vector3(_length, 0, 0);
            if (_length > 0)
                _spawnPos = transform.position + new Vector3(_length - _length + 1, 0, 0);
            else
                _spawnPos = transform.position + new Vector3(_length - _length - 1, 0, 0);
        }
        else
        {
            exitTransform.position = exitTransform.position + new Vector3(0, _length, 0);
            if (_length > 0)
                _spawnPos = transform.position + new Vector3( 0, _length - _length + 1, 0);
            else
                _spawnPos = transform.position + new Vector3(0, _length - _length - 1, 0);
        }

        _prefab = Resources.Load("Prefabs/Enemis/Enemis_list/DeathBallSmall") as GameObject;
    }
    
    private void Update()
    {
        if (!_coIsRunning)
            StartCoroutine(NewBallCo());
    }

    private IEnumerator NewBallCo()
    {
        _coIsRunning = true;
        GameObject InstantiatePrefab = Instantiate(_prefab, _spawnPos, Quaternion.identity);
        InstantiatePrefab.transform.SetParent(transform);

        yield return new WaitForSeconds(_launchTime);
        _coIsRunning = false;
    }
}
