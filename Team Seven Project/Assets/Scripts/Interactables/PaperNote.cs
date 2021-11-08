using UnityEngine;

public class PaperNote : MonoBehaviour, ICollectable, IInteractable
{

    [SerializeField] private string _noteTitle;
    [SerializeField] private string _noteBody;

    private OverlayHandler _overlayHandler;
    private void Awake()
    {
        //Change this because its bad
     
    }
    public void OnCollect()
    {
        CollectionManager.Instance.SetNoteValue(this, true);
    }

    public void OnInteract(PlayerCharacter playerCharacter)
    {
        OnCollect();
        this.gameObject.SetActive(false);
        //Open up note on canvas UI and set text
    }

    public string Title { get => _noteTitle; }
    public string Body { get => _noteBody; }
}
