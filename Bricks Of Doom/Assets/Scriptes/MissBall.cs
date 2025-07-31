using UnityEngine;

public class MissBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Ball")
        {
            FindAnyObjectByType<GameManager>().Miss();
        }
    }
}
