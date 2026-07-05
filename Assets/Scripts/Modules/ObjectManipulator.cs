using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    private GameObject _selectedObject;
    private Vector3 _initialGrabPosition;
    private Vector3 _initialObjectPosition;

    public void SelectObject(GameObject obj)
    {
        _selectedObject = obj;
    }

    public void StartMove(Vector3 grabPos)
    {
        if (_selectedObject == null) return;
        _initialGrabPosition = grabPos;
        _initialObjectPosition = _selectedObject.transform.position;
    }

    public void UpdateMove(Vector3 currentGrabPos)
    {
        if (_selectedObject == null) return;
        Vector3 delta = currentGrabPos - _initialGrabPosition;
        _selectedObject.transform.position = _initialObjectPosition + delta;
    }

    public void ScaleObject(float factor)
    {
        if (_selectedObject == null) return;
        _selectedObject.transform.localScale *= factor;
    }
}
