using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class CameraConfiguration : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    public UnityEngine.UI.RawImage backgroundRenderer; // Para proyectar la cámara si no hay ARCore
    public Camera fallbackCamera; // Opcional: Cámara estándar para proyección si no hay AR Foundation

    public bool isCameraRunning => _webCamTexture != null && _webCamTexture.isPlaying;

    public void RequestCameraAndStartFallback()
    {
        StartCoroutine(RequestPermissionAndStart());
    }

    private IEnumerator RequestPermissionAndStart()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Debug.Log("Solicitando permiso de cámara en Android...");
            Permission.RequestUserPermission(Permission.Camera);

            // Esperar un momento a que el usuario acepte el diálogo de permisos
            float timer = 0f;
            while (!Permission.HasUserAuthorizedPermission(Permission.Camera) && timer < 5f)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
#endif
        StartFallbackCamera();
    }

    public void StartFallbackCamera()
    {
        if (backgroundRenderer == null)
        {
            Debug.LogWarning("CameraConfiguration: backgroundRenderer no está asignado.");
            return;
        }

        if (_webCamTexture != null && _webCamTexture.isPlaying)
        {
            return; // Ya está corriendo
        }

        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.LogError("No se encontró ninguna cámara en el dispositivo.");
            return;
        }

        // Buscar cámara trasera por defecto
        string selectedDeviceName = devices[0].name;
        foreach (var device in devices)
        {
            if (!device.isFrontFacing)
            {
                selectedDeviceName = device.name;
                break;
            }
        }

        // Configuración optimizada de resolución y FPS para ZTE Blade V60 Design (Unisoc T606)
        _webCamTexture = new WebCamTexture(selectedDeviceName, 1280, 720, 30);
        backgroundRenderer.texture = _webCamTexture;
        backgroundRenderer.gameObject.SetActive(true);
        _webCamTexture.Play();

        Debug.Log($"Cámara de respaldo (Fallback) encendida con éxito en dispositivo '{selectedDeviceName}'.");
    }

    public void StopFallbackCamera()
    {
        if (_webCamTexture != null && _webCamTexture.isPlaying)
        {
            _webCamTexture.Stop();
        }
        if (backgroundRenderer != null)
        {
            backgroundRenderer.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        StopFallbackCamera();
    }
}
