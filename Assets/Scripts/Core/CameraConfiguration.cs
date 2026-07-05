using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraConfiguration : MonoBehaviour
{
    public ARCameraBackground arBackground;
    public Camera mainCamera;

    public void SetPassthroughEnabled(bool enabled)
    {
        if (arBackground != null)
        {
            arBackground.enabled = enabled;
        }

        if (enabled)
        {
            mainCamera.clearFlags = CameraClearFlags.Color;
            mainCamera.backgroundColor = new Color(0, 0, 0, 0); // Transparent for AR
        }
        else
        {
            mainCamera.clearFlags = CameraClearFlags.Skybox;
        }
    }
}
