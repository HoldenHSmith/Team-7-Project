using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGuardDistract : MonoBehaviour
{
	public float evaluation;
	public float speed;
	private Renderer _renderer;
	private MaterialPropertyBlock _propBlock;

    // Start is called before the first frame update
    void Awake()
    {
		_propBlock = new MaterialPropertyBlock();
		_renderer = GetComponent<Renderer>();
    }

	private void Update()
	{
		//Get the current value of the material properties in the renderer.
		_renderer.GetPropertyBlock(_propBlock);
		//Assign our new value
		_propBlock.SetFloat("_Evaluation", evaluation);
		_propBlock.SetFloat("_Speed", speed);
		_renderer.SetPropertyBlock(_propBlock);
	}

}
