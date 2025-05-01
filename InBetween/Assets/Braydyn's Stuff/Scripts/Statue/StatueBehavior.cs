using UnityEngine;

public class StatueBehavior : MonoBehaviour
{
    public enum StatueAction
    {
        HealPlayer,
        DamagePlayer,
        TeleportPlayer,
        GiveItem,
        PlayAnimation
    }

    [Header("Statue Configuration")]
    public StatueAction action;
    public float value = 10f; // Heal/damage amount
    public Transform teleportTarget;
    public GameObject itemPrefab;

    private PlayerScript player;

    void Start()
    {
        FindPlayer();
    }

    void FindPlayer()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.GetComponent<PlayerScript>();
        }
    }

    public void ActivateStatue()
    {
        FindPlayer(); // in case player wasn't ready at Start
        if (player == null)
        {
            Debug.LogWarning("StatueBehavior: No PlayerScript found.");
            return;
        }

        switch (action)
        {
            case StatueAction.HealPlayer:
                player.HealME((int)value);
                break;

            case StatueAction.DamagePlayer:
                player.TakeDamage((int)value);
                break;

            case StatueAction.TeleportPlayer:
                if (teleportTarget != null)
                    player.transform.position = teleportTarget.position;
                break;

            case StatueAction.GiveItem:
              //do the code for this it would be super cool later!
                break;

            case StatueAction.PlayAnimation:
                Debug.Log("Statue animation triggered (stub).");
                break;
        }
    }
}
