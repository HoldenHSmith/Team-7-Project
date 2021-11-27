using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuTextMaterialBlock : MonoBehaviour
{
	public MenuButtonType MenuButton;
	private Renderer _renderer;
	private MaterialPropertyBlock _matPropertyBlock;

	private Camera _camera;
	private bool _selected = false;
	private bool _canSelect = true;
	private bool _buttonsDeactivated = true;
	[SerializeField] private MainMenuHandler _menuHandler = null;
	[SerializeField] private AudioClip _hoverClip = null;
	[SerializeField] private AudioClip _clickedClip = null;
	private AudioSource _audioSource;

	// Start is called before the first frame update
	void Awake()
	{
		_matPropertyBlock = new MaterialPropertyBlock();
		_renderer = GetComponent<Renderer>();
		_camera = Camera.main;
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!_canSelect || _buttonsDeactivated)
			return;
		//Check if mouse is over object
		RaycastHit hit;
		Vector3 currentMousePos = Vector3.zero;
		Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform == this.transform)
			{
				if (!_selected)
					PlayHoverSound();

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
		{
			switch (MenuButton)
			{
				case MenuButtonType.Play:
					_menuHandler.LoadGame();
					break;
				case MenuButtonType.New:
					_menuHandler.NewGameClicked();
					break;
				case MenuButtonType.Settings:
					_menuHandler.SettingsClicked();
					break;
				case MenuButtonType.Quit:
					_menuHandler.QuitGame();
					break;
				default:
					break;
			}

			PlayClickedSound();
		}


		if (!_selected)
			SetProperties(0.04f);
	}

	public void SetProperties(float emissive)
	{
		_renderer.GetPropertyBlock(_matPropertyBlock);
		_matPropertyBlock.SetFloat("_Emissive", emissive);
		_renderer.SetPropertyBlock(_matPropertyBlock);
	}

	public enum MenuButtonType
	{
		Play,
		New,
		Settings,
		Quit
	}

	public void ActivateButton()
	{
		_buttonsDeactivated = false;
	}

	public void PlayHoverSound()
	{
		RandomPitch();
		_audioSource.PlayOneShot(_hoverClip);
	}

	public void PlayClickedSound()
	{
		RandomPitch();
		_audioSource.PlayOneShot(_clickedClip);
	}

	private void RandomPitch()
	{
		_audioSource.pitch = Random.Range(0.9f, 1.1f);
	}

	public bool CanSelect { get => _canSelect; set => _canSelect = value; }

}
