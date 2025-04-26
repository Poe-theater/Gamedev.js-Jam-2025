using UnityEngine;

public class HandlerBlockAttack : MonoBehaviour
{
    public bool collideWithEnemy = false;

    private void OnTriggerEnter(Collider other)
    {
        collideWithEnemy = true;
    }

    private void OnTriggerStay(Collider other)
    {
        collideWithEnemy = false;

    }

    private void OnTriggerExit(Collider other)
    {
        collideWithEnemy = false;
    }
}
