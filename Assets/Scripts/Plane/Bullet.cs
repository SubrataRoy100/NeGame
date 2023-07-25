using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ISavable
{

    public event EventHandler<OnCollisionEventArgs> OnCollision;
    public class OnCollisionEventArgs : EventArgs
    {
        public Collider2D collider2D;
    }

    public static Bullet Create(Transform pfBullet, Vector3 spawnPos, Vector3 dir)
    {
        Transform b_transform = Instantiate(pfBullet, spawnPos, Quaternion.identity);
        Bullet bullet = b_transform.GetComponent<Bullet>();
        bullet.SetUp(dir);
        return bullet;
    }


    private Vector3 direction;
    private Vector3 cPosition;
    private Quaternion c_rotation;
    public void SetUp(Vector3 dir)
    {
        this.direction = dir;



    }
    private void Start()
    {
        cPosition = transform.position;
        c_rotation = transform.rotation;
    }
    private void Update()
    {
        float speed = 10f;
        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        cPosition = transform.position;
        c_rotation = transform.rotation;
        Destroy(gameObject, 2);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {
            OnCollision?.Invoke(this, new OnCollisionEventArgs
            {
                collider2D = collider
            });

        }
    }

    public void Save(SaveData saveData)
    {

    }

    public void Load(ref SaveData saveData)
    {


    }
}
