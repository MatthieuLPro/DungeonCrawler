using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ************************************************ */
/* Class descritption */
/* ************************************************ */
// This class instance bullet
// Bullet instance can have 2 types of direction
// - Hunt   => Target player position
// - Linear => Direction is horizontal or vertical

public class EnemyShoot : MonoBehaviour
{
    /* Parent components */
    private GameObject      _parent;
    private EnemyTest       _enemyScript;

    /* Detector component */
    private ObjectDetector _detectorScript;

    /* Sound component */
    private AudioManager _audioScript;

    [Header("Range attack params")]
    [SerializeField]
    private float _bulletFrequency = .0f;
    [SerializeField]
    private string _bulletType = "hunt";

    private bool _bulletIsLaunched;
    private GameObject _myPrefab;
    private GameObject _target;

    /* ************************************************ */
    /* Main Functions */
    /* ************************************************ */
    void Start()
    {
        _parent         = transform.parent.gameObject;
        _enemyScript    = _parent.GetComponent<EnemyTest>();
        _detectorScript = _parent.transform.GetChild(0).GetComponent<ObjectDetector>();
        _audioScript    = _parent.transform.GetChild(2).GetComponent<AudioManager>();

        _myPrefab = GetAmmoType();
    }

    void Update(){
        if (Target != _detectorScript.DetectedObject)
            Target = _detectorScript.DetectedObject;

        if (!BulletIsLaunched && _detectorScript.IsDetected)
            StartCoroutine(_LaunchBullet());
    }

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */
    public float BulletFrequency {
        get { return _bulletFrequency; }
        set { _bulletFrequency = value; }
    }

    public bool BulletIsLaunched {
        get { return _bulletIsLaunched; }
        set { _bulletIsLaunched = value; }
    }

    public GameObject Target {
        get { return _target; }
        set { _target = value; }
    }

    /* ************************************************ */
    /* Instance bullets */
    /* ************************************************ */
    private GameObject GetAmmoType(){
        GameObject bullet = Resources.Load("Prefabs/Enemis/Traps/TrapStatic_range_ammo") as GameObject;

        return bullet;
    }

    private void _InstantiateBullet(){
        GameObject bullet = Instantiate(_myPrefab, transform.position, Quaternion.identity);

        _SetLayers(bullet);
        _findDirection(bullet);
        _SetParent(bullet);
    }

    private void _SetLayers(GameObject bullet){
        SpriteRenderer sprite   = bullet.GetComponent<SpriteRenderer>();

        bullet.layer = _parent.layer;
        sprite.sortingLayerName = _enemyScript.GetSortingLayerPlayerName();
        sprite.sortingOrder = -1;
    }

    private void _SetParent(GameObject bullet){
        bullet.transform.SetParent(transform);
    }

    /* ************************************************ */
    /* Bullet directions */
    /* ************************************************ */
    private void _findDirection(GameObject bullet) {
        if (_bulletType == "linear")
            _setLinearDirection(bullet);
        else
            _SetHuntDirection(bullet);
    }

    private void _setLinearDirection(GameObject bullet) {
        DeterminateSingleDirections direction = bullet.transform.GetChild(0).transform.GetChild(0).GetComponent<DeterminateSingleDirections>();
        Vector2 targetPosition                = Target.transform.position;

        if ((targetPosition.x - _parent.transform.position.x) > 0) {
            if ((targetPosition.y - _parent.transform.position.y) > 0) {
                direction.DirectionX = 0;
                direction.DirectionY = 1;
            }
            else {
                direction.DirectionX = 1;
                direction.DirectionY = 0;   
            }   
        }
        else {
            if ((targetPosition.y - _parent.transform.position.y) > 0) {
                direction.DirectionX = -1;
                direction.DirectionY = 0;
            }
            else {
                direction.DirectionX = 0;
                direction.DirectionY = -1;   
            }   
        }

        direction.GetDirection();
    }


    private void _SetHuntDirection(GameObject bullet){
        DeterminateSingleDirections direction = bullet.transform.GetChild(0).transform.GetChild(0).GetComponent<DeterminateSingleDirections>();
        Vector2 targetPosition                = Target.transform.position;

        direction.DirectionX = targetPosition.x - _parent.transform.position.x;
        direction.DirectionY = targetPosition.y - _parent.transform.position.y;

        direction.GetDirection();
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    private IEnumerator _LaunchBullet(){
        BulletIsLaunched = true;

        _InstantiateBullet();
        _audioScript.CallAudio("bullet");
        yield return new WaitForSeconds(BulletFrequency);

        BulletIsLaunched = false;
    }
}
