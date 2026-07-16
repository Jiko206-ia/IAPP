using UnityEngine;
using System;

public class GestureManager : MonoBehaviour
{
    // Cambiamos el tipo de IHandProvider a MediaPipeHandBridge para que aparezca en el Inspector de Unity
    public MediaPipeHandBridge handProvider; 
    
    public Action<HandData> OnPinchStart;
    public Action<HandData> OnPinchEnd;
    
    private bool _wasPinchingLeft;
    private bool _wasPinchingRight;

    void Update()
    {
        if (handProvider == null) return;

        CheckHand(handProvider.GetLeftHand(), ref _wasPinchingLeft);
        CheckHand(handProvider.GetRightHand(), ref _wasPinchingRight);
    }

    private void CheckHand(HandData hand, ref bool wasPinching)
    {
        if (!hand.isTracked) return;

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
