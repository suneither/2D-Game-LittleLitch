using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float basicBoltFireRate = 0f;
    public float ultimateBoltFireRate = 0f;
    public float melleAttackRate = 0f;

    public float basicBoltDamage = 15f;
    public float ultimateBoltDamage = 15f;
    public float melleAttackDamage = 15f;

    private float melleAttackDistance = 1f;
    private float basicBoltDistance = 7f;
    private float ultimateBoltDistance = 5f;

    private float timeToFireBasicBolt = 0f;
    private float timeToFireUltimateBolt = 0f;
    private float timeToAttackMelleAttack = 0f;

    public Transform firePoint;
    public GameObject basicBolt;
    public GameObject ultimateBolt;

    public LayerMask whatIsEnemies;
    public float melleAttackRange;
    EnemyMovement enemyMovementScript;
    PlayerMovement playerMovement;
    private void Start()
    {
        enemyMovementScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {

        if (Time.time > timeToFireBasicBolt && enemyMovementScript.distanceBeetweenPlayer < basicBoltDistance)
        {
            timeToFireBasicBolt = Time.time + 1 / basicBoltFireRate;
            Shoot(basicBolt);
        }
        if(Time.time > timeToFireUltimateBolt && enemyMovementScript.distanceBeetweenPlayer < ultimateBoltDistance)
        {
            
            timeToFireUltimateBolt = Time.time + 1 / ultimateBoltFireRate;
            Shoot(ultimateBolt);
        }
        if(Time.time > timeToAttackMelleAttack && enemyMovementScript.distanceBeetweenPlayer < melleAttackDistance)
        {
            timeToAttackMelleAttack = Time.time + 1 / melleAttackRate;
            MelleAttack();
        }
    }

    void Shoot(GameObject bolty)
    {
        Instantiate(bolty, firePoint.transform.position, Quaternion.identity);
    }

    void MelleAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(firePoint.position, melleAttackRange, whatIsEnemies);
        for(int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<PlayerMovement>().health -= melleAttackDamage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, melleAttackRange);
    }

    private void KNN()
    {
        float playerHealth = playerMovement.health;
        float distanceBeetweenPlayer = enemyMovementScript.distanceBeetweenPlayer;
        basicBoltFireRate = 0f;
        ultimateBoltFireRate = 0f;
        melleAttackRate = 0f;

        melleAttackDamage = 30f;
        ultimateBoltDamage = 50f;
        melleAttackDamage = 15f;

        melleAttackDistance = 1f;
        basicBoltDistance = 7f;
        ultimateBoltDistance = 5f;

       // float knnDistanceOfMelle = playerHealth - melleAttackDamage

}
}
