using UnityEngine;
using UnityEngine.UI; // For standard InputField
using TMPro;          // For TextMeshPro InputField

public class SpatialKeyboard : MonoBehaviour
{
    public NoteService noteService;
    public TMP_InputField targetInputField; // If they are using TextMeshPro InputField
    public InputField standardInputField;  // If they are using Standard UI InputField

    public void TypeKey(string key)
    {
        if (noteService == null) return;

        // Append key
        noteService.tempNoteContent += key;

        // Update TextMeshPro input field if assigned
        if (targetInputField != null)
        {
            targetInputField.text = noteService.tempNoteContent;
        }

        // Update Standard input field if assigned
        if (standardInputField != null)
        {
            standardInputField.text = noteService.tempNoteContent;
        }

        Debug.Log("Typed: " + key + " | Current content: " + noteService.tempNoteContent);
    }

    public void Backspace()
    {
        if (noteService == null || string.IsNullOrEmpty(noteService.tempNoteContent)) return;

        noteService.tempNoteContent = noteService.tempNoteContent.Substring(0, noteService.tempNoteContent.Length - 1);

        if (targetInputField != null)
        {
            targetInputField.text = noteService.tempNoteContent;
        }

        if (standardInputField != null)
        {
            standardInputField.text = noteService.tempNoteContent;
        }
    }

    public void Clear()
    {
        if (noteService == null) return;

        noteService.tempNoteContent = "";

        if (targetInputField != null)
        {
            targetInputField.text = "";
        }

        if (standardInputField != null)
        {
            standardInputField.text = "";
        }
    }
}
