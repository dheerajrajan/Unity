using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] states;

    public int health { get; private set; }
    public int points = 100;
    public bool unbreakable;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on Brick GameObject.");
        }
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);

        if (!unbreakable)
        {
            if (states == null || states.Length == 0)
            {
                Debug.LogWarning("Brick states array is not assigned or empty.");
                return;
            }

            health = states.Length;
            spriteRenderer.sprite = states[health - 1];
        }
    }

    public void Hit()
    {
        if (unbreakable)
            return;

        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (states != null && health - 1 >= 0 && health - 1 < states.Length)
            {
                spriteRenderer.sprite = states[health - 1];
            }
            else
            {
                Debug.LogWarning("Trying to set sprite for invalid health index.");
            }
        }

        GameManager gm = FindAnyObjectByType<GameManager>();
        if (gm != null)
        {
            gm.Hit(this);
        }
        else
        {
            Debug.LogError("GameManager not found in scene.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
