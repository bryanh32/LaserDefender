using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;
    [Header ("Enemy Firing")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float laserSpeed = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip laserSound;
    [Header ("Enemy Death")]
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] [Range(0, 1)] float laserVolume;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathVolume = 0.5f;
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -laserSpeed);
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject shipExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(shipExplosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
    }
}
