using UnityEngine;

public class SimpleSave : MonoBehaviour
{
	public void SaveGame()
	{
		SaveManager.Save(GameManager.Instance.Player.transform.position);
	}
}
