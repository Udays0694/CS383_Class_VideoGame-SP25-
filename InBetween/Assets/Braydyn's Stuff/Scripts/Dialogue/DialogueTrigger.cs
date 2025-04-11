using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;
    public bool triggerOnStart = false;
    public bool requireButtonPress = true;
    public bool interactOnce = false;

    private bool playerInRange = false;
    private bool hasTriggered = false;
    private bool canInteract = true;

    void Start()
    {
        if (triggerOnStart && !hasTriggered)
        {
            TriggerDialogue();
            if (interactOnce)
                hasTriggered = true;
        }
    }

    void Update()
    {
        if (!canInteract) return;

        if (requireButtonPress && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
        else if (!requireButtonPress && playerInRange && !hasTriggered)
        {
            TriggerDialogue();
            if (interactOnce)
                hasTriggered = true;
        }
    }

    void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);

        if (interactOnce)
        {
            hasTriggered = true;
            canInteract = false;
        }
        else
        {
            StartCoroutine(ReenableInteraction());
        }
    }

    IEnumerator ReenableInteraction()
    {
        canInteract = false;
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
