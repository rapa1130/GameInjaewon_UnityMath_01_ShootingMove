using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed;

    private void FixedUpdate()
    {
        Move();
    }
    public virtual void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);
    }
}
