using UnityEngine;

public class TargetInteractable : MonoBehaviour
{
    public enum InteractableType
    {
        Collectable,
        Trap
    }

    public InteractableType type;
    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Trigger()
    {
        if(type == InteractableType.Collectable)
        {
            gameManager.TargetCollected();
        }
        gameObject.SetActive(false);
    }
}
