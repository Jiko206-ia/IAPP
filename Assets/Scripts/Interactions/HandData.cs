using UnityEngine;

public struct HandData
{
    public bool isTracked;
    public Vector3 indexTipPosition;
    public Vector3 thumbTipPosition;
    public Vector3 palmPosition;
    public Quaternion palmRotation;
    
    public float pinchStrength; // 0 to 1
    public bool isPinching => pinchStrength > 0.8f;
}
