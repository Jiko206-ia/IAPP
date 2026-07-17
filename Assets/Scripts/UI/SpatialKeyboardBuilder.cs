using UnityEngine;
using UnityEngine.UI;

# if UNITY_EDITOR
using UnityEditor;
# endif

public class SpatialKeyboardBuilder : MonoBehaviour
{
    public SpatialKeyboard keyboardScript;
    public GameObject buttonPrefab; // Opcional: si el usuario tiene un diseño de botón personalizado

    [ContextMenu("Generar Teclado Completo")]
    public void BuildKeyboard()
    {
        if (keyboardScript == null)
        {
            keyboardScript = GetComponent<SpatialKeyboard>();
        }

        if (keyboardScript == null)
        {
            Debug.LogError("Por favor, añade el script SpatialKeyboard a este objeto antes de construir.");
            return;
        }

        // Alfabeto estándar de un teclado QWERTY
        string[] rows = new string[]
        {
            "Q W E R T Y U I O P",
            "A S D F G H J K L",
            "Z X C V B N M",
            "SPACE BACKSPACE CLEAR"
        };

        float startY = 100f;
        float rowHeight = 45f;
        float keyWidth = 40f;
        float keyHeight = 40f;

        for (int r = 0; r < rows.Length; r++)
        {
            string[] keys = rows[r].Split(' ');
            float totalWidth = keys.Length * keyWidth;
            float startX = -totalWidth / 2f + keyWidth / 2f;

            for (int k = 0; k < keys.Length; k++)
            {
                string keyLabel = keys[k];
                
                // Crear objeto de botón
                GameObject keyObj = new GameObject("Key_" + keyLabel);
                keyObj.transform.SetParent(this.transform, false);

                // Configurar RectTransform para UI
                RectTransform rt = keyObj.AddComponent<RectTransform>();
                rt.sizeDelta = new Vector2(keyLabel == "SPACE" ? keyWidth * 3 : keyWidth, keyHeight);
                rt.anchoredPosition = new Vector2(startX + (keyLabel == "SPACE" ? keyWidth : 0), startY - (r * rowHeight));
                
                if (keyLabel == "SPACE") startX += keyWidth * 2; // Compensar espacio
                startX += keyWidth;

                // Añadir imagen de fondo del botón
                Image img = keyObj.AddComponent<Image>();
                img.color = new Color(0.2f, 0.2f, 0.2f, 0.8f); // Fondo gris oscuro semitransparente

                // Añadir componente Button
                Button btn = keyObj.AddComponent<Button>();

                // Añadir Texto
                GameObject textObj = new GameObject("Text");
                textObj.transform.SetParent(keyObj.transform, false);
                Text txt = textObj.AddComponent<Text>();
                txt.text = keyLabel;
                txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                txt.fontSize = 14;
                txt.alignment = TextAnchor.MiddleCenter;
                txt.color = Color.white;

                // Ajustar tamaño del texto al botón
                RectTransform textRt = textObj.GetComponent<RectTransform>();
                textRt.anchorMin = Vector2.zero;
                textRt.anchorMax = Vector2.one;
                textRt.sizeDelta = Vector2.zero;

                // Configurar el evento OnClick usando el script SpatialKeyboard
                string currentKey = keyLabel;
                if (currentKey == "SPACE") currentKey = " ";
                
                btn.onClick.AddListener(() =>
                {
                    if (currentKey == "BACKSPACE")
                    {
                        keyboardScript.Backspace();
                    }
                    else if (currentKey == "CLEAR")
                    {
                        keyboardScript.Clear();
                    }
                    else
                    {
                        keyboardScript.TypeKey(currentKey);
                    }
                });
            }
        }

        Debug.Log("¡Teclado QWERTY Generado con éxito!");
    }
}
