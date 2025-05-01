using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueData dialogue;
    public bool triggerOnStart = false;
    public bool requireButtonPress = true;
    public bool interactOnce = false;

    [Header("Events")]
    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    [Header("Interaction Cooldown")]
    public float cooldownAfterDialogue = 1.0f;


    [System.Serializable]
    public class DialogueLineEvent
    {
        public int lineIndex;
        public UnityEvent onLineReached;
    }

    [Header("Per-Line Events")]
    public List<DialogueLineEvent> perLineEvents = new List<DialogueLineEvent>();

    private bool playerInRange = false;
    private bool hasTriggered = false;
    private bool canInteract = true;
    private int lastLineIndex = -1;

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
        if (!canInteract || DialogueManager.Instance == null) return;

        if (DialogueManager.Instance.IsDialogueActive())
        {
            int currentLine = DialogueManager.Instance.GetCurrentLineIndex();
            if (currentLine != lastLineIndex)
            {
                lastLineIndex = currentLine;
                foreach (var entry in perLineEvents)
                {
                    if (entry.lineIndex == currentLine)
                        entry.onLineReached?.Invoke();
                }
            }
        }

        if (!DialogueManager.Instance.IsDialogueActive())
        {
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

    }

    void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        onDialogueStart?.Invoke();
        StartCoroutine(WaitForDialogueEnd());

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

    IEnumerator WaitForDialogueEnd()
    {
        while (DialogueManager.Instance.IsDialogueActive())
            yield return null;

        onDialogueEnd?.Invoke();

        canInteract = false;
        yield return new WaitForSeconds(cooldownAfterDialogue);
        canInteract = true;
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
