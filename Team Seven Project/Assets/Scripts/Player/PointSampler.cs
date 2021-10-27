using System.Collections.Generic;
using UnityEngine;

public class PointSampler : MonoBehaviour
{
	[SerializeField] private List<Transform> _samplePoints = new List<Transform>();
	[SerializeField] private bool _hideSamplePoints = true;

	private void Awake()
	{
		if (_hideSamplePoints)
		{
			foreach (Transform samplePoint in _samplePoints)
			{
				samplePoint.GetComponent<MeshRenderer>().enabled = false;
			}
		}
	}

	public List<Transform> SamplePoints { get => _samplePoints; }
}
