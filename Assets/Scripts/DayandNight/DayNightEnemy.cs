using UnityEngine;

public class DayNightEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRAnge = 1f;
    public int attackDamage = 1;
    public float attackCooldown = 3f; //wait 3 secs before attacking again;
    public float stunDuration = 2f; //AFTER being hit stun time
    
    
    public LayerMask wallLayer; //layer with obstacles
    private Transform dayNightplayer; //reference the player
    private float attackTimer = 0f;
    private float stunTimer = 0f; //hw long enemy stays stunned
    // private DayNightManager dayNightManager;

        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            dayNightplayer = playerObject.transform;
        }
        // dayNightManager = FindAnyObjectByType<DayNightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!DayNight.isNight) //in the day dont do NADA!, only work at night
        {
            return;
        }
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            return;
        }
        if (dayNightplayer == null)
        {
            return;
        }
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        float distance = Vector2.Distance(transform.position, dayNightplayer.position);
        if (distance <= detectionRange)
        {
            if (IsLineOfSight())
            {
                if (distance <= attackRAnge)
                {
                    AttackPlayer();
                }
                else
                {
                    ChasePlayer();
                }
            }
            else
            {
                {
                    return;
                }
            }
        }

        bool IsLineOfSight() //from unityhelp page
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, dayNightplayer.position, wallLayer); //is there a wall blocking enemy view

            if(hit.collider != null)
            {
                return false; //if teh kine hit a wall, then hide the playerr
            }
            return true; //nothing is blocking teh player
        }
    }
    void ChasePlayer()
    {
        Vector2 direction = (dayNightplayer.position - transform.position).normalized; //find direction and go 
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime); //move toward player
    }


    void AttackPlayer()
    {
        if (attackTimer > 0)
        {
            return;
        }

        DayNightPlayer dayNightPlayer = dayNightplayer.GetComponent<DayNightPlayer>(); //find player health

        if(dayNightPlayer != null)
        {
            dayNightPlayer.TakeDamage(attackDamage);
        }

        attackTimer = attackCooldown;
    }

    public void Stun()
    {
        //Add enemy health and they die after a couple hits?
        stunTimer = stunDuration; //stop enemy for a little
    }
}
