using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleActivateObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects = new List<GameObject>();
    [SerializeField] private bool _startActive = false;
    private bool _active;
    private void Awake()
    {
        _active = _startActive;
        foreach (GameObject obj in _objects)
        {
            obj.SetActive(_startActive);
        }
    }

    public void Activate()
    {
        foreach (GameObject obj in _objects)
        {
            obj.SetActive(true);
        }
    }

    public void Deactivate()
    {
        foreach (GameObject obj in _objects)
        {
            obj.SetActive(false);
        }
    }

    public void Toggle()
    {
        _active = !_active;
        foreach (GameObject obj in _objects)
        {
            obj.SetActive(_active);
        }
    }
}
