﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem trace = null;

    private Transform parent;

    //총알의 변수
    private float accuracy;
    private float range;
    private float speed;
    private float damage;

    public void SetBullet(float accuracy, float range, float speed, float damage)
    {
        this.accuracy = accuracy;
        this.range = range;
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        parent = gameObject.transform.parent;
        gameObject.SetActive(false);
    }

    private void Vanish()
    {
        gameObject.transform.SetParent(parent);
        gameObject.SetActive(false);
    }

    public void Fire(GameObject dirObject, string owner)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(dirObject.transform.position, dirObject.transform.forward + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), 0), out hitInfo, range))
        {
            if (hitInfo.collider.gameObject.tag == "Player" && owner != "Player")
            {
                hitInfo.collider.gameObject.GetComponent<CharacterBase>().ReceiveDamage(damage, hitInfo.point);
            }

            else if (hitInfo.collider.gameObject.tag == "Boss" && owner != "Boss")
            {
                hitInfo.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(damage, hitInfo.point);
            }

            else if (hitInfo.collider.gameObject.tag == "Enemy" && owner != "Enemy")
            {
                hitInfo.collider.gameObject.GetComponent<EnemyHit>().HitByBullet(damage, hitInfo.point);
            }

            else if (hitInfo.collider.gameObject.tag == "EnemyHead" && owner != "Enemy")
            {
                UiController.instance.HitCritical();
                hitInfo.collider.gameObject.GetComponent<EnemyHit>().HitByBullet(damage * 2, hitInfo.point);
            }

            else if (hitInfo.collider.gameObject.tag == "Dummy")
            {
                hitInfo.collider.gameObject.GetComponent<Dummy>().ReceiveDamage(damage, hitInfo.point);
            }

            else if (hitInfo.collider.gameObject.tag == "Barrel")
            {
                hitInfo.collider.gameObject.GetComponent<Barrel>().Explosion();
            }

            else if (hitInfo.collider.gameObject.tag == "Wall")
            {
                gameObject.transform.SetParent(hitInfo.collider.gameObject.transform);
                trace.transform.position = hitInfo.point;
                trace.Play();
            }
        }
        Invoke("Vanish", 1.0f);
    }
}
