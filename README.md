# XR Productivity & Modeler for Android

Este proyecto es una base para una aplicación de Realidad Mixta (XR) al estilo Meta Quest / Apple Vision Pro, diseñada para dispositivos Android. Soporta tanto Realidad Aumentada (AR) como Realidad Virtual (Cardboard) e incluye herramientas de productividad y un modelador 3D básico controlado por seguimiento de manos.

## Requisitos Previos

*   **Unity 2022.3 LTS** (o superior).
*   **Android Build Support** instalado en Unity Hub.
*   **Dispositivo Android** compatible con ARCore.

## Configuración del Proyecto en Unity

Para poner en marcha este proyecto, sigue estos pasos:

### 1. Instalación de Paquetes
Abre el **Package Manager** (Window > Package Manager) e instala:
*   **AR Foundation** (v5.x o superior)
*   **Google ARCore XR Plugin**
*   **XR Interaction Toolkit**
*   **OpenXR Plugin**

### 2. Configuración de XR
Ve a **Project Settings > XR Plug-in Management**:
*   En la pestaña de **Android**, activa **Google ARCore**.

### 3. Integración de MediaPipe Unity (Hand Tracking)
Como los móviles no tienen sensores de profundidad dedicados, usamos MediaPipe para el seguimiento de manos:
1.  Descarga el plugin de [MediaPipe Unity Plugin](https://github.com/homuler/MediaPipeUnityPlugin).
2.  Sigue las instrucciones de instalación del plugin para importar los paquetes necesarios en la carpeta `Packages` o `Assets`.
3.  Asegúrate de que los modelos de MediaPipe estén en la carpeta `StreamingAssets`.

## Estructura de Carpetas
*   `Assets/Scripts/Core`: Gestión de la sesión XR y cambio entre modos AR/VR.
*   `Assets/Scripts/Interactions`: Lógica de seguimiento de manos y gestos.
*   `Assets/Scripts/Modules`: Lógica de las herramientas (Notas, Calendario, Modelador).
*   `Assets/Scripts/UI`: Controladores de la interfaz espacial.
*   `Assets/Prefabs`: Objetos preconfigurados (Ventanas, Herramientas, Formas 3D).

## Cómo Compilar (Build)
1.  Ve a **File > Build Settings**.
2.  Cambia la plataforma a **Android**.
3.  Asegúrate de que las escenas estén añadidas.
4.  En **Player Settings**, configura el **Minimum API Level** a Android 7.0 (API 24) o superior.
5.  Haz clic en **Build and Run**.
