using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;
using System.Collections;

public class XRSessionManager : MonoBehaviour
{
    public ARSession arSession;
    public ARCameraManager arCameraManager;
    public GameObject cardboardRig; // A standard stereoscopic rig for VR
    public GameObject arRig;        // The AR Session Origin / XR Origin

    public enum XRMode { AR, VR }
    public XRMode currentMode = XRMode.AR;

    void Start()
    {
        // Default to AR on start if not specified
        SwitchToMode(currentMode);
    }

    public void SwitchToMode(XRMode mode)
    {
        currentMode = mode;
        if (mode == XRMode.AR)
        {
            EnableAR(true);
            EnableVR(false);
        }
        else
        {
            EnableAR(false);
            EnableVR(true);
        }
    }

    private void EnableAR(bool enabled)
    {
        if (arRig != null) arRig.SetActive(enabled);
        if (arSession != null) arSession.enabled = enabled;
        
        if (enabled)
        {
            // Reset AR Session to start fresh tracking
            if (arSession != null) arSession.Reset();
        }
    }

    private void EnableVR(bool enabled)
    {
        if (cardboardRig != null) cardboardRig.SetActive(enabled);
        
        // When in VR/Cardboard mode, we might still want the camera background
        // for "Passthrough" if the user requested it. 
        // This is handled by the CameraConfiguration or a specific Passthrough script.
    }

    public void ToggleMode()
    {
        SwitchToMode(currentMode == XRMode.AR ? XRMode.VR : XRMode.AR);
    }
}
