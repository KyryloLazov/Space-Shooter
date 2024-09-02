using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private GameObject _explosion;

    private SpawnBehaviour _spawnBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        _spawnBehaviour = GameObject.Find("SpawnManager").GetComponent<SpawnBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            GameObject explode = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _spawnBehaviour.StartSpawn();
            Destroy(this.gameObject);
        }
    }
}
