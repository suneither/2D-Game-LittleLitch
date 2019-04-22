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

    public float startBasicBoltDamage = 15f;
    public float startUltimateBoltDamage = 15f;
    public float startMelleAttackDamage = 15f;

    private float melleAttackDistance = 2f;
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

    private float delay = 1;
    private void Start()
    {
        enemyMovementScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        startBasicBoltDamage = basicBoltDamage;
        startUltimateBoltDamage = ultimateBoltDamage;
        startMelleAttackDamage = melleAttackDamage;
}
    void Update()
    {

        /*if (Time.time > timeToFireBasicBolt && enemyMovementScript.distanceBeetweenPlayer < basicBoltDistance)
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
        }*/

        if(delay < Time.time)
        {
            delay = Time.time + 1;
            KNN();
        }
       
    }

    void Shoot(GameObject bolty)
    {
          Instantiate(bolty, firePoint.transform.position, Quaternion.identity);
    }

    void MelleAttack()
    {
        if (Time.time > timeToAttackMelleAttack && enemyMovementScript.distanceBeetweenPlayer < melleAttackDistance)
        {
            timeToAttackMelleAttack = Time.time + 1 / melleAttackRate;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(firePoint.position, melleAttackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<PlayerMovement>().health -= melleAttackDamage;
            }
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
        float knnTimebasic = timeToAttackMelleAttack - Time.time;
        float knnTimebolt = timeToFireBasicBolt - Time.time;
        float knnTimeboltult = timeToFireUltimateBolt - Time.time;

        if (knnTimebasic < 0)
        {
            knnTimebasic = 0;
        }
        if(knnTimebolt < 0)
        {
            knnTimebolt = 0;
        }
        if (knnTimeboltult < 0)
        {
            knnTimeboltult = 0;
        }

        float knnDistanceOfMelle = Mathf.Sqrt(
            Mathf.Pow(playerHealth - melleAttackDamage, 2) +
            Mathf.Pow(knnTimebasic, 2) +
            Mathf.Pow(distanceBeetweenPlayer - melleAttackDistance, 2))/1000;

        float knnDistanceOfBasicBolt = Mathf.Sqrt(
            Mathf.Pow(playerHealth - basicBoltDamage, 2) +
            Mathf.Pow(knnTimebolt, 2) +
            Mathf.Pow(distanceBeetweenPlayer - basicBoltDistance, 2))/1000;

        float knnDistanceOfUltimateBolt = Mathf.Sqrt(
            Mathf.Pow(playerHealth - ultimateBoltDamage, 2) +
            Mathf.Pow(knnTimeboltult, 2) +
            Mathf.Pow(distanceBeetweenPlayer - ultimateBoltDistance, 2))/1000;

        print(knnDistanceOfMelle + "sss" + knnDistanceOfBasicBolt + "sss" + knnDistanceOfUltimateBolt + "sss");
        print(melleAttackDamage + " sss" + basicBoltDamage + "sss" + ultimateBoltDamage);
        if(knnDistanceOfMelle < knnDistanceOfBasicBolt && knnDistanceOfMelle < knnDistanceOfUltimateBolt)
        {
            ultimateBoltDamage += ultimateBoltDamage * 0.001f * Random.Range(10,50);
            basicBoltDamage += basicBoltDamage * 0.001f * Random.Range(10, 50);

            melleAttackDamage -= melleAttackDamage * 0.001f * Random.Range(10, 50);
            MelleAttack();
        }
        if(knnDistanceOfBasicBolt < knnDistanceOfMelle && knnDistanceOfBasicBolt < knnDistanceOfUltimateBolt)
        {
            melleAttackDamage += melleAttackDamage * 0.001f * Random.Range(10, 50);
            ultimateBoltDamage += ultimateBoltDamage * 0.001f * Random.Range(10, 50);

            basicBoltDamage -= basicBoltDamage * 0.001f * Random.Range(10, 50);
            if (Time.time > timeToFireBasicBolt && enemyMovementScript.distanceBeetweenPlayer < basicBoltDistance)
            {
               
                timeToFireBasicBolt = Time.time + 1 / basicBoltFireRate;
                Shoot(basicBolt);
            }
        }
        if(knnDistanceOfUltimateBolt < knnDistanceOfMelle && knnDistanceOfUltimateBolt < knnDistanceOfBasicBolt)
        {
            melleAttackDamage += melleAttackDamage * 0.001f * Random.Range(10, 50);
            basicBoltDamage += basicBoltDamage * 0.001f * Random.Range(10, 50);

            ultimateBoltDamage -= ultimateBoltDamage * 0.001f * Random.Range(10, 50);
            if (Time.time > timeToFireUltimateBolt && enemyMovementScript.distanceBeetweenPlayer < ultimateBoltDistance)
            {
               
                timeToFireUltimateBolt = Time.time + 1 / ultimateBoltFireRate;
                Shoot(ultimateBolt);
            }
        }
    }
}
