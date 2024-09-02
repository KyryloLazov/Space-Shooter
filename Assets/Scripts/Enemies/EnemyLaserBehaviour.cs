using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    void Update()
    {    
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
       
        if (transform.position.y <= -8f)
        {
            if (transform.parent != null)   
            { 
                Destroy(transform.parent.gameObject);    
            }   
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.Damage();
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
