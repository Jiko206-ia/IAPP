using UnityEngine;

// Conecta los datos de MediaPipe con el sistema de interacciones XR.
// Incluye suavizado (smoothing) y soporte de optimización para GPUs de gama de entrada como Mali-G57 (ZTE Blade V60 Design).
public class MediaPipeHandBridge : MonoBehaviour, IHandProvider
{
    private HandData _leftHand;
    private HandData _rightHand;

    [Header("Optimización y Rendimiento")]
    [Tooltip("Suavizar el movimiento de las manos para evitar tirones a FPS más bajos")]
    public bool enableSmoothing = true;
    [Range(1f, 30f)]
    public float smoothingFactor = 15f;

    [Header("Modo Simulación / Fallback")]
    public bool enableMouseSimulation = false;

    public HandData GetLeftHand() => _leftHand;
    public HandData GetRightHand() => _rightHand;

    // Actualiza los datos de las manos (llamado por el plugin de MediaPipe)
    public void UpdateHandData(HandData left, HandData right)
    {
        if (enableSmoothing)
        {
            _leftHand = SmoothHandData(_leftHand, left);
            _rightHand = SmoothHandData(_rightHand, right);
        }
        else
        {
            _leftHand = left;
            _rightHand = right;
        }
    }

    private void Update()
    {
        // Si la simulación con ratón está activa, simular la mano derecha con la posición de la pantalla
        if (enableMouseSimulation && Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPos = ray.GetPoint(1.0f);

            HandData simRight = new HandData
            {
                isTracked = true,
                palmPosition = targetPos,
                indexTipPosition = targetPos,
                thumbTipPosition = targetPos + Vector3.right * 0.02f,
                pinchStrength = Input.GetMouseButton(0) ? 1.0f : 0.0f
            };

            _rightHand = enableSmoothing ? SmoothHandData(_rightHand, simRight) : simRight;
        }
    }

    private HandData SmoothHandData(HandData current, HandData target)
    {
        if (!target.isTracked) return target;

        float dt = Time.deltaTime * smoothingFactor;
        HandData smoothed = target;

        if (current.isTracked)
        {
            smoothed.palmPosition = Vector3.Lerp(current.palmPosition, target.palmPosition, dt);
            smoothed.indexTipPosition = Vector3.Lerp(current.indexTipPosition, target.indexTipPosition, dt);
            smoothed.thumbTipPosition = Vector3.Lerp(current.thumbTipPosition, target.thumbTipPosition, dt);
            smoothed.palmRotation = Quaternion.Slerp(current.palmRotation, target.palmRotation, dt);
            smoothed.pinchStrength = Mathf.Lerp(current.pinchStrength, target.pinchStrength, dt);
        }

        return smoothed;
    }
}
