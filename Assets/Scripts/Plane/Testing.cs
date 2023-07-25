using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private Transform b_Pf;
    [SerializeField] private Transform spawnPos1;
    [SerializeField] private Transform spawnPos2;

    private float timer;
    [SerializeField] private float spawnRate;


    private void Update()
    {

        if (Time.time > timer)
        {
            Bullet b1 = Bullet.Create(b_Pf, spawnPos1.position, transform.up);
            b1.OnCollision += OnCollision;
            Bullet b2 = Bullet.Create(b_Pf, spawnPos2.position, transform.up);
            b2.OnCollision += OnCollision;
            timer = Time.time + spawnRate;
        }



    }

    public void OnCollision(object sender, Bullet.OnCollisionEventArgs e)
    {
        if (e.collider2D.transform.TryGetComponent(out IDamagable iDamagable))
        {
            int damage = Random.Range(1, 50);
            iDamagable.Damage(damage);
            DamagePoppup.CreatePopPup(e.collider2D.transform.position, damage);

        }
    }



}
