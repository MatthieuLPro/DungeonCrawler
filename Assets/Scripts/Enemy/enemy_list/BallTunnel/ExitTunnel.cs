using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTunnel : MonoBehaviour
{
    public Vector3[] _entrancePosition;

    void Start(){
        _entrancePosition = new Vector3[1] { Vector3.zero };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        if (_entrancePosition[0] == Vector3.zero)
            _SetEntranceVectors();

        _SetNewObjectPosition(other.transform);
    }

    private void _SetEntranceVectors(){
        BallTunnel ballTunnel = transform.parent.GetComponent<BallTunnel>();
        if (ballTunnel.GetRandomSpawn())
            _entrancePosition = ballTunnel.GetSpawnVector();
        else
            _entrancePosition[0] = ballTunnel.GetSpawnVector()[0];
    }

    private void _SetNewObjectPosition(Transform objectTransform){
        objectTransform.position = _entrancePosition[Random.Range(0, _entrancePosition.Length)];
    }
}
