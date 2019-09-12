using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private GameObject camera;
    [SerializeField] Material serverMaterial;
    [SerializeField] GameObject capsule;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSp1;
    [SerializeField] Transform bulletSp2;
    int currentGun = 0;

    private int vida = 100;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {//Set Cursor to not be visible
        Cursor.visible = false;
        if (isLocalPlayer)
        {
            camera.SetActive(true);
            capsule.GetComponent<MeshRenderer>().material = serverMaterial;            
        }
       
    }


    void Update()
    {
        if (isLocalPlayer) {
            if (Input.GetAxis("Horizontal") != 0)
            {
                transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 4;
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 4;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Cmd_Shoot(transform.forward);
            }
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(0f, yaw, 0.0f);
        }
    }

    [Command]
    private void Cmd_Shoot(Vector3 direction)
    {
        GameObject newBullet;
        if (currentGun == 0)
        {
            newBullet = Instantiate(bullet, bulletSp1.position, new Quaternion());
            currentGun++;
        }
        else
        {
            newBullet = Instantiate(bullet, bulletSp2.position, new Quaternion());
            currentGun = 0;
        }
        
        newBullet.GetComponent<BulletController>().direction = direction;
        NetworkServer.SpawnWithClientAuthority(newBullet, gameObject);
    }

    public void RestarVida()
    {
        vida -= 10;
        if(vida <= 0)
        {
            Destroy(this.gameObject);
        }
        Debug.Log("VIDA DE ALGUIEN: " + vida);
    }
    

}
