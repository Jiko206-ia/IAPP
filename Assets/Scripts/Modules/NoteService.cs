using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class Note
{
    public string id;
    public string title;
    public string content;
    public string date;
}

public class NoteService : MonoBehaviour
{
    private string _savePath;
    private List<Note> _notes = new List<Note>();

    void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "notes.json");
        LoadNotes();
    }

    public void SaveNote(string title, string content)
    {
        Note newNote = new Note {
            id = System.Guid.NewGuid().ToString(),
            title = title,
            content = content,
            date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm")
        };
        _notes.Add(newNote);
        SyncToDisk();
    }

    public List<Note> GetAllNotes() => _notes;

    private void SyncToDisk()
    {
        string json = JsonUtility.ToJson(new SerializationWrapper<Note>(_notes));
        File.WriteAllText(_savePath, json);
    }

    private void LoadNotes()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            _notes = JsonUtility.FromJson<SerializationWrapper<Note>>(json).items;
        }
    }
}

[System.Serializable]
public class SerializationWrapper<T>
{
    public List<T> items;
    public SerializationWrapper(List<T> items) => this.items = items;
}
