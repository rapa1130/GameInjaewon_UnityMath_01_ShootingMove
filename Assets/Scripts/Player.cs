using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpread;
    [SerializeField] private float speed;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
           
            float y = transform.position.y + speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            float x = transform.position.x - speed * Time.deltaTime;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float y = transform.position.y - speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            float x = transform.position.x + speed * Time.deltaTime;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Bullet bulletLeft = Instantiate(bulletPrefab);
            bulletLeft.transform.position = transform.position;
            //bulletLeft.transform.Rotate(0, 0, -bulletSpread);
            bulletLeft.BulletXDirection = Direction.LEFT;

            Bullet bulletRight = Instantiate(bulletPrefab);
            bulletRight.transform.position = transform.position;
            //bulletRight.transform.Rotate(0, 0, bulletSpread);
            bulletLeft.BulletXDirection = Direction.RIGHT;
        }

    }
}
