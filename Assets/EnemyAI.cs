using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform target;
    public LayerMask isGround, isPlayer;

    public Vector3 walkPoint;
    bool walkPointset;
    public float walkPointRange;

    public GameObject endgamemenu;
    public GameObject inGameUI; 
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    public TextMeshProUGUI itemFound;

    public float attackCooldown;
    public static int numberFound;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) GameOver();
    }

    private void Patrolling()
    {
        if (!walkPointset) SearchWalkPoint();

        if (walkPointset)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointset = false;

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointset = true;
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= sightRange)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //Attack Target
                FaceTarget();

            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void GameOver()
    {
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        numberFound = GameScript.itemfound;
        itemFound.text = numberFound.ToString() + "/20";
    }


}   


