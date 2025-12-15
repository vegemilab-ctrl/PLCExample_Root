using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class MagneticSensor_Ex : MonoBehaviour
{
    public string layerName = "Sensor";
    public UnityEvent<bool> onChangeDetected;

    private bool _hasDetectedPrev = false;
    private bool _hasDetected = false;
    public List<Collider> _detectedList;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    private void OnTriggerEnter(Collider other)
    {
        _detectedList.Add(other);
        _hasDetected = _detectedList.Count > 0;

        if (_hasDetectedPrev != _hasDetected)        
            onChangeDetected?.Invoke(_hasDetected);

        _hasDetectedPrev = _hasDetected;
    }

    private void OnTriggerExit(Collider other)
    {
        _detectedList.Remove(other);
        _hasDetected = _detectedList.Count > 0;

        if (_hasDetectedPrev != _hasDetected)
            onChangeDetected?.Invoke(_hasDetected);

        _hasDetectedPrev = _hasDetected;
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
