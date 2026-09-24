using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;
using System.Collections;

public class XRSessionManager : MonoBehaviour
{
    public ARSession arSession;
    public ARCameraManager arCameraManager;
    public CameraConfiguration cameraConfiguration;
    public GameObject cardboardRig; // A standard stereoscopic rig for VR
    public GameObject arRig;        // The AR Session Origin / XR Origin

    public enum XRMode { AR, VR, Fallback }
    public XRMode currentMode = XRMode.AR;

    public bool autoFallbackOnUnsupported = true;
    public bool isFallbackActive { get; private set; } = false;

    void Start()
    {
        StartCoroutine(InitializeXRSession());
    }

    private IEnumerator InitializeXRSession()
    {
        if (autoFallbackOnUnsupported)
        {
            // Comprobar compatibilidad con ARCore
            if (ARSession.state == ARSessionState.None || ARSession.state == ARSessionState.CheckingAvailability)
            {
                yield return ARSession.CheckAvailability();
            }

            if (ARSession.state == ARSessionState.Unsupported)
            {
                Debug.LogWarning("ARCore no está soportado en este dispositivo (ej. ZTE Blade V60 Design). Activando modo Fallback WebCam.");
                EnableFallbackMode();
                yield break;
            }
        }

        // Si es soportado o no se forzó el chequeo, procedemos con el modo por defecto
        SwitchToMode(currentMode);
    }

    public void SwitchToMode(XRMode mode)
    {
        currentMode = mode;
        if (mode == XRMode.AR)
        {
            isFallbackActive = false;
            if (cameraConfiguration != null) cameraConfiguration.StopFallbackCamera();
            EnableAR(true);
            EnableVR(false);
        }
        else if (mode == XRMode.VR)
        {
            isFallbackActive = false;
            if (cameraConfiguration != null) cameraConfiguration.StopFallbackCamera();
            EnableAR(false);
            EnableVR(true);
        }
        else if (mode == XRMode.Fallback)
        {
            EnableFallbackMode();
        }
    }

    public void EnableFallbackMode()
    {
        currentMode = XRMode.Fallback;
        isFallbackActive = true;

        EnableAR(false);
        EnableVR(false);

        if (cameraConfiguration != null)
        {
            cameraConfiguration.RequestCameraAndStartFallback();
        }
        else
        {
            Debug.LogError("XRSessionManager: CameraConfiguration no asignado para el modo Fallback.");
        }
    }

    private void EnableAR(bool enabled)
    {
        if (arRig != null) arRig.SetActive(enabled);
        if (arSession != null) arSession.enabled = enabled;
        
        if (enabled && arSession != null)
        {
            arSession.Reset();
        }
    }

    private void EnableVR(bool enabled)
    {
        if (cardboardRig != null) cardboardRig.SetActive(enabled);
    }

    public void ToggleMode()
    {
        if (isFallbackActive)
        {
            SwitchToMode(XRMode.VR);
        }
        else
        {
            SwitchToMode(currentMode == XRMode.AR ? XRMode.VR : XRMode.AR);
        }
    }
}
