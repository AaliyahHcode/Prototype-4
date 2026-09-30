using TMPro;
using UnityEngine;

public class DayNightPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private Vector2 lastDirection = Vector2.down; //for the rock to know where to be tossed

    public int maxHealth = 5;
    public int health = 5;
    public TMP_Text healthText;

    // FOR THE ROCKS
    public int rocks = 0;
    public GameObject rockPrefab;
    public TMP_Text rockText;

    public float throwDistance = 1f; //how far away the rock appears
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement != Vector2.zero) //remeber the last direction teh player moved
        {
            lastDirection = movement;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowRock();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health < 0)
        {
            health = 0;
        }

        UpdateUI();
    }
    public void TakeDamage(int amount) //when enemy attacks teh player
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
        UpdateUI(); //update teh health display

        if (health <= 0) //did player lose all tehri health
        {
            DayNightManager dayNightManager = FindFirstObjectByType<DayNightManager>();
            if (dayNightManager != null)
            {
                dayNightManager.GameOver();
            }

            enabled = false; //stop moving
        }
    }
        

    public void AddRock()
    {
        rocks++;
        UpdateUI();
    }

    void ThrowRock()
    {
        if (rocks <= 0)
        {
            return; //dont throw anything if no rocks
        }

        //create a rock by the player
        Vector3 spawnPosition = transform.position + (Vector3)(lastDirection * throwDistance);
        GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        RockProjectile projectile = rock.GetComponent<RockProjectile>(); //tell the rock what direction to go
        if (projectile != null)
        {
            projectile.direction = lastDirection;
        }

        rocks--; //use a rock
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //did the player touch the berry?
        if (other.CompareTag("Berry"))
        {
            Heal(1);
            Destroy(other.gameObject);
        }
        //did the player touch the rock?
        if (other.CompareTag("Rock"))
        {
            AddRock();
            Destroy(other.gameObject);
        }
    }
    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health + "/" + maxHealth;
        }
        if (rockText != null)
        {
            rockText.text = "Rocks: " + rocks;
        }
    }
}
