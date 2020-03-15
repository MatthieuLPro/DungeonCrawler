using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private string _detectorType;

    private bool                _isDetected;
    private GameObject          _detectedObject;
    private CircleCollider2D    _colliderCircle = null;
    private BoxCollider2D       _colliderBox = null;

    void Start() {
        if (gameObject.GetComponent<CircleCollider2D>() != null)
            _colliderCircle = gameObject.GetComponent<CircleCollider2D>();
        else
            _colliderBox = gameObject.GetComponent<BoxCollider2D>();
    }

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */
    public string DetectorType {
        get { return _detectorType; }
        set { _detectorType = value; }
    }

    public bool IsDetected {
        get { return _isDetected; }
        set { _isDetected = value; }
    }

    public GameObject DetectedObject {
        get { return _detectedObject; }
        set { _detectedObject = value; }
    }

    /* ************************************************ */
    /* Detection */
    /* ************************************************ */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(DetectorType))
            return;

        IsDetected = true;
        DetectedObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(DetectorType))
            return;

        IsDetected = false;
        DetectedObject = null;
        StartCoroutine(_RefreshColliderCo());
    }

    private IEnumerator _RefreshColliderCo() {
        if (_colliderCircle)
            _colliderCircle.enabled = false;
        else
            _colliderBox.enabled = false;
        yield return new WaitForSeconds(0.1f);
        if (_colliderCircle)            
            _colliderCircle.enabled = true;
        else
            _colliderBox.enabled = false;
    }
}
