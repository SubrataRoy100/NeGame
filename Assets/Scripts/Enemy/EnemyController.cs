using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{

    public static EnemyController Create(Transform pf, Vector3 spawnPos)
    {
        Transform e_trasmform = Instantiate(pf, spawnPos, Quaternion.identity);

        EnemyController e = e_trasmform.GetComponent<EnemyController>();
        if (e != null)
        {
            return e;
        }
        else
        {
            return null;
        }
    }

    [SerializeField] private int currentHealth;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform pf_Bullet;
    [SerializeField] private Transform pf_BulletSpawnPos;
    [SerializeField] private float fireRate;
    private Transform target;
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float timerMax;

    public enum State
    {
        Roaming, Chase, Attack
    }

    public State state;


    private void Awake()
    {

    }
    private void Start()
    {
        if (FindObjectOfType<PlaneController>() != null)
        {
            target = FindObjectOfType<PlaneController>().transform;
        }
        state = State.Roaming;
        timerMax = 0.1f;
        startingPosition = transform.position;
        roamingPosition = GetRandomPositon();

    }
    private void Update()
    {


        float distance;
        switch (state)
        {
            case State.Roaming:
                Move(roamingPosition);
                distance = Vector3.Distance(transform.position, roamingPosition);
                if (distance < 1f)
                {
                    roamingPosition = GetRandomPositon();
                }
                if (target != null)
                {
                    if (distance < chaseRange)
                    {
                        state = State.Chase;
                    }
                }
                break;
            case State.Chase: //    CHASE STATE
                if (target == null)
                {
                    state = State.Roaming;
                    return;
                }

                distance = Vector3.Distance(transform.position, target.position); ;
                Move(target.position);

                if (distance < attackRange)
                {
                    state = State.Attack;
                }
                if (distance > chaseRange)
                {
                    state = State.Roaming;
                }


                break;
            case State.Attack: // ATTACK STATE
                if (target == null)
                {
                    state = State.Roaming;
                    return;
                }
                distance = Vector3.Distance(transform.position, target.position);
                Vector3 dir = (target.position - transform.position).normalized;
                float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(targetAngle - 90f, Vector3.forward), turnSpeed * Time.deltaTime);
                if (distance > attackRange && distance < chaseRange)
                {
                    state = State.Chase;
                }

                if (Time.time > timerMax)
                {
                    Bullet b = Bullet.Create(pf_Bullet, pf_BulletSpawnPos.position, dir);
                    b.OnCollision += OnCollision;
                    timerMax = Time.time + fireRate;
                }

                break;
        }
    }

    private Vector3 GetRandomPositon()
    {
        Vector3 r_pos = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized;
        return startingPosition + r_pos * UnityEngine.Random.Range(5f, 5f);
    }

    private void Move(Vector3 dir)
    {
        Vector3 direction = (dir - transform.position).normalized;
        transform.position += direction * playerSpeed * Time.deltaTime;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(targetAngle - 90f, Vector3.forward), turnSpeed * Time.deltaTime);

    }



    public void OnCollision(object sender, Bullet.OnCollisionEventArgs e)
    {
        if (e.collider2D.transform.TryGetComponent(out PlaneHealth player))
        {
            int damage = Random.Range(50, 100);
            player.GetHealthSystem().TakeDamage(damage);
            DamagePoppup.CreatePopPup(e.collider2D.transform.position, damage);
        }
    }

    public void Damage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth < 0)
        {
            Destroy(this.gameObject);
            GameManager.I.enemies.Remove(this.transform);
            Debug.Log("Dead");
        }
    }

    private void OnApplicationQuit()
    {

        PlayerPrefs.SetFloat("EnemyX", transform.position.x);
        PlayerPrefs.SetFloat("EnemyY", transform.position.y);
        Debug.Log(transform.position.x + "Enemy");




    }


}



