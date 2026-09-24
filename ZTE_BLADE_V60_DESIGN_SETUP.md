# Guía de Configuración y Compilación para ZTE Blade V60 Design

Esta guía detalla los pasos y configuraciones específicas necesarias en Unity para compilar y ejecutar esta aplicación XR de forma óptima en un teléfono **ZTE Blade V60 Design** (Procesador Unisoc T606, GPU Mali-G57, Android 13/14).

---

## 1. Características Técnicas del ZTE Blade V60 Design y Estrategia XR

* **Procesador:** Unisoc T606 (Octa-core 1.6GHz)
* **GPU:** Mali-G57 MP1
* **Compatibilidad ARCore:** **No soportado oficialmente por Google ARCore**.
* **Estrategia Implementada:**
  - La aplicación detecta automáticamente la falta de compatibilidad con ARCore al iniciar.
  - Se activa el modo **Fallback de Cámara Normal (`WebCamTexture`)** a 1280x720 @ 30 FPS.
  - Soporta modo **Cardboard/VR** con acelerómetro/giroscopio.
  - Seguimiento de manos optimizado mediante **MediaPipe Unity Plugin** con suavizado de interpolación (Lerp) y límite de tasa de refresco a ~30 FPS para evitar sobrecalentamiento y mantener alta fluidez.

---

## 2. Configuración en Unity (Player Settings)

Abre **Project Settings > Player > Android Settings**:

### **Identification & Other Settings**
* **Minimum API Level:** `Android 7.0 'Nougat' (API Level 24)` o superior (Android 13/14 en ZTE Blade V60 es API 33/34).
* **Target API Level:** `Automatic (highest installed)` o `API Level 33/34`.
* **Scripting Backend:** `IL2CPP` (Recomendado) o `Mono`.
* **Target Architectures:** Activar **ARM64** y **ARMv7**.
* **Graphics APIs:**
  1. `OpenGLES3` (Principal para máxima compatibilidad con Unisoc T606).
  2. `Vulkan` (Opcional).

### **XR Plug-in Management**
1. En **Project Settings > XR Plug-in Management > Android Tab**:
   - Puedes mantener activado **Google ARCore** (el código del proyecto se encargará de hacer el Fallback automático si la librería ARCore retorna `Unsupported`).
   - Si no deseas incluir la librería de ARCore en el APK final, desmarca Google ARCore.

---

## 3. Permisos en `AndroidManifest.xml`

Asegúrate de tener otorgados los siguientes permisos de cámara y sensores en `Assets/Plugins/Android/AndroidManifest.xml` (el código C# los solicitará automáticamente al iniciar la app):

```xml
<uses-permission android:name="android.permission.CAMERA" />
<uses-feature android:name="android.hardware.camera" android:required="true" />
<uses-feature android:name="android.hardware.camera.autofocus" android:required="false" />
<uses-feature android:name="android.hardware.sensor.accelerometer" android:required="true" />
<uses-feature android:name="android.hardware.sensor.gyroscope" android:required="false" />
```

---

## 4. Instrucciones para Compilar (Build)

1. En Unity, abre **File > Build Settings**.
2. Selecciona la plataforma **Android** y haz clic en **Switch Platform** si aún no está seleccionada.
3. Asegúrate de añadir las escenas del proyecto a la lista **Scenes In Build** (ej. `SampleScene`).
4. Conecta tu ZTE Blade V60 Design vía USB con la **Depuración por USB (USB Debugging)** activada en Opciones de Desarrollador.
5. Haz clic en **Build and Run**.

---

## 5. Verificación de Funcionamiento en el Dispositivo

* **Al abrir la app:** La app solicitará permiso de uso de la cámara.
* **Modo Cámara (Fallback AR):** Se activará la transmisión de la cámara trasera con textura HD 720p.
* **Seguimiento de Manos:** MediaPipe procesará los gestos en tiempo real con suavizado de movimiento.
* **Captura de Notas y UI:** Puedes interactuar con la UI Espacial y el Teclado Virtual QWERTY 3D.
