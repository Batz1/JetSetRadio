using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float projectileSpeed;
    Transform player;
    Vector3 target;
    public Vector2 goTo;
    public float lifeTime;
    public float spawnCooldown;
    [HideInInspector] public float lifeTick;
    [HideInInspector] public Transform parentEnemy;
    MeshRenderer mosR;
    CircleCollider2D cc;
    public ParticleSystem Pcollosion;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.currentPlayer.transform;
        target = new Vector3(player.position.x, player.position.y, 0);
        Pcollosion.GetComponent<ParticleSystem>().enableEmission = false;
        mosR = GetComponent<MeshRenderer>();
        cc = GetComponent<CircleCollider2D>();
        mosR.enabled = false;
        cc.enabled = false;

        goTo = target - transform.position;

        lifeTick = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTick <= lifeTime - spawnCooldown)
        {
            transform.Translate(goTo.normalized * projectileSpeed * Time.deltaTime);
            mosR.enabled = true;
            cc.enabled = true;
            Pcollosion.Play();
        }

        lifeTick -= Time.deltaTime;

        if(lifeTick < 0)
        {
            Destroy(gameObject);
        }

        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Deflect") || other.CompareTag("Range") || other.CompareTag("playerProjectile"))
        {
            Pcollosion.Play();
           
            return;
        }
        else
        {
            Pcollosion.Play();
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
    
}
