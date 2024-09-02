using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    private AudioSource _explosion;

    void Start()
    {
        _explosion = GetComponent<AudioSource>();

        if( _explosion == null)
        {
            Debug.LogError("Explosion is empty");
        }
        _explosion.Play();
        Destroy(this.gameObject, 3f);
    }

    void Update()
    {
        
    }
}
