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
    [HideInInspector] public float lifeTick;
    [HideInInspector] public Transform parentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.currentPlayer.transform;
        target = new Vector3(player.position.x, player.position.y, 0);

        goTo = target - transform.position;

        lifeTick = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(goTo.normalized * projectileSpeed * Time.deltaTime);

        lifeTick -= Time.deltaTime;

        if(lifeTick < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Deflect") || other.CompareTag("Range") || other.CompareTag("playerProjectile"))
        {
            return;
        }
        else
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
