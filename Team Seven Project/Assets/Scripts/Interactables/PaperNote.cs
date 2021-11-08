using UnityEngine;

public class PaperNote : MonoBehaviour, ICollectable, IInteractable
{

    [SerializeField] private string _noteText;

    public void OnCollect()
    {
        CollectionManager.Instance.SetNotevalue(this, true);
    }

    public void OnInteract(PlayerCharacter playerCharacter)
    {
        OnCollect();
        this.gameObject.SetActive(false);
        //Open up note on canvas UI and set text
    }
}
