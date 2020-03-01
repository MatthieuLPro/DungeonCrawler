﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private string _detectorType;

    private bool _isDetected;
    private GameObject _detectedObject;


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
    }


}
