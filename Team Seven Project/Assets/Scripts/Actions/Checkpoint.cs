using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(RecipientHandler))]
public class Checkpoint : MonoBehaviour, IMessageSender
{

	[SerializeField] private Vector3 _offSet = Vector3.zero;
	[SerializeField] private Quaternion _saveRotation = Quaternion.identity;
	[SerializeField] private Mesh _previewMesh = null;
	public BoxCollider _boxCollider;
	private RecipientHandler _recipientHandler;
	private bool _doorsActivated = false;

	private void OnEnable()
	{
		_boxCollider = GetComponent<BoxCollider>();
		_boxCollider.isTrigger = true;
	}

	private void Awake()
	{
		_recipientHandler = GetComponent<RecipientHandler>();
	}

	public void SaveGame()
	{
		SaveManager.Save(transform.position + _offSet, _saveRotation);
	}

	public void SendMessage()
	{
		for (int i = 0; i < _recipientHandler.Recipients.Count; i++)
		{
			MessageDispatcher.Instance.DispatchMessage(0, this, _recipientHandler.Recipients[i], MessageType.Msg_Activate, null);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_doorsActivated && other.tag == "Player")
			SendMessage();

		_doorsActivated = true;
	}

	public Vector3 Offset { get => _offSet; set => _offSet = value; }
	public Quaternion SaveRotation { get => _saveRotation; set => _saveRotation = value; }

#if UNITY_EDITOR
	[Header("Debug Options")]
	public bool DrawBox = true;
	public Color BoxColor;
	public Color BoxOutlineColor;


	private void OnDrawGizmosSelected()
	{
		if (_previewMesh != null)
		{
			Gizmos.color = new Color(0, 1, 0, 0.5f);
			Quaternion meshRotation = _saveRotation;
			meshRotation = Quaternion.Euler(meshRotation.eulerAngles.x, meshRotation.eulerAngles.y + 180, meshRotation.eulerAngles.z);
			Gizmos.DrawWireMesh(_previewMesh, 0, transform.position + _offSet, meshRotation, Vector3.one);
			DrawTriggerBox();
		}
	}

	private void DrawTriggerBox()
	{
		if (_boxCollider != null && DrawBox)
		{
			Vector3 drawBoxScale = new Vector3(transform.lossyScale.x * _boxCollider.size.x, transform.lossyScale.y * _boxCollider.size.y, transform.lossyScale.z * _boxCollider.size.z);
			Vector3 drawBoxPosition = transform.localToWorldMatrix.MultiplyPoint(_boxCollider.center);

			Gizmos.matrix = Matrix4x4.TRS(drawBoxPosition, transform.rotation, drawBoxScale);
			Gizmos.color = BoxColor;
			Gizmos.DrawCube(Vector3.zero, Vector3.one);
			Gizmos.color = BoxOutlineColor;
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
		}
	}

	public void SetOffset(Vector3 position)
	{
		_offSet = position - transform.position;
	}


#endif
}
