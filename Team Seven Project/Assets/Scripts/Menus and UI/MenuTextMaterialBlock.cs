using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuTextMaterialBlock : MonoBehaviour
{
	private Renderer _renderer;
	private MaterialPropertyBlock _matPropertyBlock;

	private Camera _camera;
	private bool _selected = false;

	[SerializeField] private MainMenuHandler _menuHandler = null;

	// Start is called before the first frame update
	void Awake()
	{
		_matPropertyBlock = new MaterialPropertyBlock();
		_renderer = GetComponent<Renderer>();
		_camera = Camera.main;
	}

	private void Update()
	{

		//Check if mouse is over object
		RaycastHit hit;
		Vector3 currentMousePos = Vector3.zero;
		Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform == this.transform)
			{
				SetProperties(1);
				_selected = true;
			}
			else
			{
				SetProperties(0);
				_selected = false;
			}
		}
		else
		{
			SetProperties(0);
			_selected = false;
		}

		if (_selected && Mouse.current.leftButton.wasReleasedThisFrame)
			_menuHandler.NewGameClicked();
	}

	public void SetProperties(float emissive)
	{
		_renderer.GetPropertyBlock(_matPropertyBlock);
		_matPropertyBlock.SetFloat("_Emissive", emissive);
		_renderer.SetPropertyBlock(_matPropertyBlock);
	}


}
