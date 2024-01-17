using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPerception : MonoBehaviour
{
    [SerializeField] private string[] tagName;
    [SerializeField] private float distance = 1;
    [SerializeField] private float maxAngle = 45;
    [SerializeField] private LayerMask layerMask = Physics.AllLayers;

    public string[] TagName { get { return tagName; } }
    public float Distance { get { return distance; } }
    public float MaxAngle { get {  return maxAngle; } }
    public LayerMask LayerMask { get { return layerMask; } }

    public abstract GameObject[] GetGameObjects();
}
