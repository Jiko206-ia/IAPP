using UnityEngine;
using System;

public class GestureManager : MonoBehaviour
{
    public MediaPipeHandBridge handProvider; 

    [Header("Ajustes de Rendimiento ZTE / Gama Media")]
    [Tooltip("Intervalo de actualización en segundos para limitar consumo de CPU/GPU (ej. 0.033s = ~30 FPS)")]
    public float gestureCheckInterval = 0.033f;

    public Action<HandData> OnPinchStart;
    public Action<HandData> OnPinchEnd;

    private bool _wasPinchingLeft;
    private bool _wasPinchingRight;
    private float _timer;

    void Update()
    {
        if (handProvider == null) return;

        _timer += Time.deltaTime;
        if (_timer >= gestureCheckInterval)
        {
            _timer = 0f;
            CheckHand(handProvider.GetLeftHand(), ref _wasPinchingLeft);
            CheckHand(handProvider.GetRightHand(), ref _wasPinchingRight);
        }
    }

    private void CheckHand(HandData hand, ref bool wasPinching)
    {
        if (!hand.isTracked)
        {
            if (wasPinching)
            {
                wasPinching = false;
                OnPinchEnd?.Invoke(hand);
            }
            return;
        }

        if (hand.isPinching && !wasPinching)
        {
            OnPinchStart?.Invoke(hand);
        }
        else if (!hand.isPinching && wasPinching)
        {
            OnPinchEnd?.Invoke(hand);
        }

        wasPinching = hand.isPinching;
    }
}
