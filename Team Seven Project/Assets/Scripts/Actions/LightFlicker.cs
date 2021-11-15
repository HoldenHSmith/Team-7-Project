using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
	[SerializeField] private float _flickerTimeMin = 0.02f;
	[SerializeField] private float _flickerTimeMax = 0.25f;
	[SerializeField] private float _timeOnMin = 0.05f;
	[SerializeField] private float _timeOnMax = 0.25f;

	private float _flickerTimer;
	private float _flickerOnTimer;
	private Light _lightSource;
	// Start is called before the first frame update
	void Start()
	{
		_lightSource = GetComponent<Light>();
		SetOnTime();
	}

	// Update is called once per frame
	void Update()
	{
		if (_flickerOnTimer <= 0)
		{
			_flickerTimer -= Time.deltaTime;

			_lightSource.enabled = false;
			if (_flickerTimer <= 0)
			{
				SetOnTime();
			}
			else
			{
			}
		}
		else
		{
			SetFlicker();
			_flickerOnTimer -= Time.deltaTime;
			_lightSource.enabled = true;
		}
	}

	private void SetFlicker()
	{
		_flickerTimer = Random.Range(_flickerTimeMin, _flickerTimeMax);
	}

	private void SetOnTime()
	{
		_flickerOnTimer = Random.Range(_timeOnMin, _timeOnMax);

	}
}
