using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{
    public Vector3 direction;
    void Start()
    {
        Destroy(this.gameObject, 5.000001f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
            transform.position += direction * Time.deltaTime * 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().RestarVida();
            gameObject.SetActive(false);
        }
    }
}
