using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpread;
    [SerializeField] private float speed;

    [Header("Screen Clamp")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private bool bInvulnerable;
    private Vector3 initialPos;




    private void Start()
    {
        bInvulnerable = false;
        initialPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.W))
        {
            float y = transform.position.y + speed * Time.deltaTime;
            y = Mathf.Clamp(y, minY, maxY);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            float x = transform.position.x - speed * Time.deltaTime;
            x = Mathf.Clamp(x, minX, maxX);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float y = transform.position.y - speed * Time.deltaTime;
            y = Mathf.Clamp(y, minY, maxY);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            float x = transform.position.x + speed * Time.deltaTime;
            x = Mathf.Clamp(x, minX, maxX);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.Space) && !bInvulnerable)
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

    private IEnumerator DisableInvulnerable()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        bInvulnerable = false;
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3.0f);
        this.transform.position = initialPos;
        bInvulnerable = true;

        StartCoroutine(DisableInvulnerable());
        StartCoroutine(Blink(0.0f,true));
    }
    private IEnumerator Blink(float time,bool bVisible)
    {
        if(bVisible) gameObject.GetComponent<MeshRenderer>().enabled = true;
        else gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.2f);
        if(time<2.6f)
        {
            StartCoroutine(Blink(time + 0.2f, !bVisible));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( !bInvulnerable && other.CompareTag("Enemy"))
        {
            StartCoroutine("Respawn");
            bInvulnerable = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
