using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 4f;
    float shootBullet;
    bool bulletCooldown;
    public float bulletFireRate = 0.5f;
    public GameObject bulletPrefab;
    Vector2 shotDir = Vector2.right;
    Animator animator;
    public int health = 5;
    public HealthBar healthBar;


    private void Start()
    {
        // Get the Animator component from this GameObject
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(health);
    }
    private void FixedUpdate()
    {
        Vector2 motion = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
        transform.Translate(motion.normalized * speed * Time.deltaTime);
        shootBullet = Input.GetAxisRaw("Fire2");
        if (motion == Vector2.zero)
        {
            //idle animation
            animator.SetInteger("speed", 0);
        }
        else
        {
            // Update shot direction based on movement
            shotDir = motion.normalized;

            // Flip the character based on movement direction
            if (motion.x != 0)
            {
                animator.SetInteger("speed", 1);
                // Flip the character horizontally based on movement direction
                Vector3 localScale = transform.localScale;
                localScale.x = motion.x > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
                transform.localScale = localScale;
            }
            else if (motion.y != 0)
            {
                animator.SetInteger("speed", 2);
                Vector3 localScale = transform.localScale;
                localScale.y = motion.y > 0 ? Mathf.Abs(localScale.y) : -Mathf.Abs(localScale.y);
                transform.localScale = localScale;
            }
        }
        if (shootBullet != 0 && !bulletCooldown)
        {
            StartCoroutine(FireShot2());
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.SetHealth(health);
    }

    IEnumerator FireShot2()
    {
        bulletCooldown = true;
        GameObject prefab = Instantiate(bulletPrefab);
        prefab.transform.position = transform.position;
        prefab.GetComponent<bullet2>().moveDirection = shotDir;
        yield return new WaitForSeconds(bulletFireRate);
        bulletCooldown = false;
    }
}