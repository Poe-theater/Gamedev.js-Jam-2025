using UnityEngine;

public class IslandManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
    }

    private void Instance_OnGameStateChange(object sender, GameState e)
    {
        if (e == GameState.Playing)
        {
            animator.enabled = false;
            Vector3 newPos = transform.position;
            newPos.y = 0;
            transform.position = newPos;
        }
        else if (e == GameState.MainMenu)
        {
            Vector3 newPos = transform.position;
            newPos.y = 0;
            transform.position = newPos;
            animator.enabled = true;
        }
    }
}
