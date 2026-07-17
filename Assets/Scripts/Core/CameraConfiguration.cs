using UnityEngine;

public class CameraConfiguration : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    public UnityEngine.UI.RawImage backgroundRenderer; // Para proyectar la cámara si no hay ARCore

    public void StartFallbackCamera()
    {
        if (backgroundRenderer == null) return;

        // Si el móvil no soporta ARCore, encendemos la cámara normal de fotos como fondo
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            _webCamTexture = new WebCamTexture(devices[0].name, 1280, 720, 30);
            backgroundRenderer.texture = _webCamTexture;
            _webCamTexture.Play();
            Debug.Log("Cámara de respaldo (Fallback) encendida con éxito.");
        }
        else
        {
            Debug.LogError("No se encontró ninguna cámara en el dispositivo.");
        }
    }

    void OnDestroy()
    {
        if (_webCamTexture != null && _webCamTexture.isPlaying)
        {
            _webCamTexture.Stop();
        }
    }
}
