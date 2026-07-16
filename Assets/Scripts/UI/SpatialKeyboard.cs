using UnityEngine;
using TMPro;

public class SpatialKeyboard : MonoBehaviour
{
    public NoteService noteService;
    public TMP_InputField targetInputField; // If they are looking at it on screen

    public void TypeKey(string key)
    {
        if (noteService == null) return;

        // Append key
        noteService.tempNoteContent += key;

        // If we have an input field UI on screen, show it there too
        if (targetInputField != null)
        {
            targetInputField.text = noteService.tempNoteContent;
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
    }

    public void Clear()
    {
        if (noteService == null) return;

        noteService.tempNoteContent = "";

        if (targetInputField != null)
        {
            targetInputField.text = "";
        }
    }
}
