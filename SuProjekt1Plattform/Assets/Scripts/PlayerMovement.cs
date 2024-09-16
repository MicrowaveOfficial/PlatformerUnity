using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    private float horizontalValue;
    private bool isGrounded;
    private bool canMove;
    private float rayDistance = 0.3f;

    private int startingHealth = 5;
    private int currentHealth = 0;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthColor;
    [SerializeField] private Color greenHealth, yellowHealth, redHealth;
    [SerializeField] private AudioClip pickupSound, takeDamage, death;
    [SerializeField] private AudioClip[] jumpSounds;

    public int redGemsCollected = 0;
    [SerializeField] private TMP_Text redGemText;
    [SerializeField] private GameObject gemParticles, dustParticles;

    private Rigidbody2D RigBody;
    private SpriteRenderer sprRend;
    private Animator anim;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        redGemText.text = "" + redGemsCollected;
        canMove = true;

        RigBody = GetComponent<Rigidbody2D>();
        sprRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if(horizontalValue < 0)
        {
            FlipSprite(true);
        }
        else if(horizontalValue > 0) 
        {
            FlipSprite(false);
        }

        if (Input.GetButtonDown("Jump") && groundCheck() == true) 
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(RigBody.velocity.x));
        anim.SetFloat("VerticalSpeed", RigBody.velocity.y);
        anim.SetBool("IsGrounded", groundCheck());
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

            RigBody.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, RigBody.velocity.y);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RedGem"))
        {
            Destroy(other.gameObject);
            redGemsCollected++;
            redGemText.text = "" + redGemsCollected;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupSound, 1f);
            Instantiate(gemParticles, other.transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }
    }

    private void FlipSprite(bool direction)
    {
        sprRend.flipX = direction;
    }

    private void Jump()
    {
        RigBody.AddForce(new Vector2(0, jumpForce));
        int randomValue = Random.Range(0, jumpSounds.Length);
        audioSource.PlayOneShot(jumpSounds[randomValue], 0.4f);
        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            audioSource.PlayOneShot(death, 1f);
            Respawn();
        }

        else
        {
            audioSource.PlayOneShot(takeDamage, 1f);
        }
    }

    public void TakeKnockback(float knockForce, float upwards)
    {
        canMove = false;
        RigBody.AddForce(new Vector2(knockForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = spawnPosition.position;
        RigBody.velocity = Vector2.zero;
    }

    private void UpdateHealthBar()
    {

        healthSlider.value = currentHealth;

        if(currentHealth >= 4)
        {
            healthColor.color = Color.green;
        }
        else if(currentHealth == 3)
        {
            healthColor.color = yellowHealth;
        }
        else
        {
            healthColor.color = redHealth;
        }
    }

    private void RestoreHealth(GameObject healthPickup)
    {
        if (currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            audioSource.PlayOneShot(pickupSound, 1f);
            int healthToRestore = healthPickup.GetComponent<HealthPickup>().healthAmount;
            currentHealth += healthToRestore;
            UpdateHealthBar();
            Destroy(healthPickup);

            if(currentHealth > startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }

    private bool groundCheck()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);
        //Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
