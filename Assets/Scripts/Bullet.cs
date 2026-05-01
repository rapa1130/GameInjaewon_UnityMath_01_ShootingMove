//using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction
{
    LEFT,RIGHT
}
public class Bullet : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float yDistance;
    [SerializeField] protected float yBackDistance;
    [SerializeField] protected float xDistance;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletSpeedDeviation;

    public Direction BulletXDirection
    {
        get; set;
    }

    private float accumulateTime;
    private Vector3 initialPosition;
    private float bulletSpeedXRandomed;
    private float bulletSpeedYRandomed;


    private void Start()
    {
        initialPosition= transform.position;
        target = null;
        accumulateTime = 0;
        bulletSpeedXRandomed = Random.Range(bulletSpeed - bulletSpeedDeviation, bulletSpeed + bulletSpeedDeviation);
        bulletSpeedYRandomed = Random.Range(bulletSpeed - bulletSpeedDeviation, bulletSpeed + bulletSpeedDeviation);
    }
    private void FixedUpdate()
    {
        Detect();
        Move();
        
    }
    public virtual void Detect()
    {
        Vector3 center = transform.position + Vector3.up * yDistance / 2;
        Vector3 extent = new Vector3(xDistance, yDistance, 1);
        RaycastHit[] hits = Physics.BoxCastAll(center,extent,Vector3.up,transform.rotation);

        if (hits.Length == 0)
        {
            target = null;
            return;
        }

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "Enemy")
            {
                if(target == null)
                {
                    target = hits[i].collider.transform;
                }
                else if (target.position.y> hits[i].collider.transform.position.y)
                {
                    target = hits[i].collider.transform;
                }
            }
        }
    }
    public virtual void Move()
    {
        accumulateTime += Time.deltaTime;
        float totalYDistance = yBackDistance + yDistance;
        float goalX; 
        if(BulletXDirection == Direction.LEFT)
        {
            goalX = initialPosition.x - xDistance;
        }
        else
        {
            goalX = initialPosition.x + xDistance;
        }

        float x = LerpFloatNonLinearX(initialPosition.x, goalX, accumulateTime * bulletSpeedXRandomed);
        float goalY = initialPosition.y + yDistance;
        float y = LerpFloatNonLinearY(initialPosition.y, goalY, accumulateTime * bulletSpeedYRandomed);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    private float LerpFloatNonLinearX(float a,float b,float t)
    {
        return a + (b - a) * (1.0f * t * t * t - 4.0f * t);
    }
    private float LerpFloatNonLinearY(float a,float b,float t)
    {
        return a + (b - a) * (4.0f * t * t * t - 4.0f *t);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

}
