using UnityEngine;

// Esta es una clase CONCRETA que Unity sí puede usar como componente.
// Se encarga de conectar los datos de MediaPipe con nuestro sistema.
public class MediaPipeHandBridge : MonoBehaviour, IHandProvider
{
    // Aquí es donde se recibirían los datos reales de MediaPipe
    private HandData _leftHand;
    private HandData _rightHand;

    public HandData GetLeftHand() => _leftHand;
    public HandData GetRightHand() => _rightHand;

    // Esta función la llamaría el plugin de MediaPipe para actualizar los datos
    public void UpdateHandData(HandData left, HandData right)
    {
        _leftHand = left;
        _rightHand = right;
    }
}
