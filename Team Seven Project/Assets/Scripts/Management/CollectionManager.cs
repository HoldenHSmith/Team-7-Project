using System.Collections.Generic;
using UnityEngine;

public sealed class CollectionManager
{
	private static readonly CollectionManager _instance = new CollectionManager();

	private Dictionary<AreaType, bool> _keysCollected;
	private Dictionary<PaperNote, bool> _notesCollected;
	private Dictionary<MiniKeycard, bool> _miniKeycardsCollected;

	private OverlayHandler _overlayHandler;

	public CollectionManager()
	{
		_keysCollected = new Dictionary<AreaType, bool>();
		_notesCollected = new Dictionary<PaperNote, bool>();

		for (int i = 0; i < (int)AreaType.Count; i++)
		{
			_keysCollected.Add((AreaType)i, false);
		}

	}

	public void SetKeyValue(AreaType area, bool value)
	{
		_keysCollected[area] = value;
		if (value)
			Debug.Log($"Key Collected: {area}");
	}

	public void SetNoteValue(PaperNote note, bool value)
	{
		_notesCollected[note] = value;
		if (value)
		{
			_overlayHandler.ReadNote(note);
			Debug.Log($"Note Collected!");
		}
	}

	public void SetMiniKeycardValue(MiniKeycard mini, bool value)
	{
		_miniKeycardsCollected[mini] = value;
		if (value)
			Debug.Log($"Mini Keycard Collected!");
	}

	public bool CheckKeyCollected(AreaType area)
	{
		return _keysCollected[area];
	}

	public static CollectionManager Instance { get => _instance; }
	public Dictionary<AreaType, bool> KeysCollected { get => _keysCollected; }
	public Dictionary<PaperNote, bool> NotesCollected { get => _notesCollected; }

	public OverlayHandler OverlayHandler { get => _overlayHandler; set => _overlayHandler = value; }

}
