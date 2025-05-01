using UnityEngine;

public class BossRoomTriggerHandler : MonoBehaviour
{
    [Header("Objects to Disable When Entering a Boss Room")]
    public GameObject[] objectsToDisable;

    [Header("Settings")]
    public string bossRoomTag = "BossRoom";
    public bool disableOnlyOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered && disableOnlyOnce)
            return;

        if (other.CompareTag(bossRoomTag))
        {
            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                    obj.SetActive(false); ;
            }

            hasTriggered = true;
        }
    }
}
