using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superArrowController : MonoBehaviour
{
    [Header("Tower Scriptible")]
    private towerController _towerController;

    [Header("Elements")]
    particleManager _paricleManager;
    gameManager _gameManager;

    [Header("Settings")]
    private Transform target;
    public int arrowDamage;
    public Rigidbody2D rb;
    public float shootPower;

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("gameManager").GetComponent<gameManager>();
        _paricleManager = GameObject.FindWithTag("particleManager").GetComponent<particleManager>();
        _towerController = GameObject.FindGameObjectWithTag("tower").GetComponent<towerController>();

        FindClosestEnemy();

        Destroy(gameObject, _towerController.towerGameInRange);
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * _towerController.towerGameInSpeed * Time.deltaTime);
        }

    }


    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy1");
        if (enemies.Length == 0)
        {
            Debug.LogWarning("Hedef düþman bulunamadý!");
            return;
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnDestroy()
    {
        // Eðer obje yok edildiyse, diðer objelerin hedefini güncelle
        superArrowController[] objectControllers = FindObjectsOfType<superArrowController>();
        foreach (superArrowController controller in objectControllers)
        {
            if (controller != this)
            {
                controller.FindClosestEnemy();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy1"))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _paricleManager.bloodParticle(newPos);


            collision.GetComponent<enemyController>().enemyHealtDamage(_towerController.towerGameInDamage);

            Destroy(gameObject);



        }
    }

}
