using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            Debug.Log($"Collision with: {collision.gameObject.name}");
            this.gameObject.SetActive(false);


        }
    }
}