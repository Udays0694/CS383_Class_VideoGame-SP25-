using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image portraitImage;
    public GameObject continueArrow;

    [Header("Audio")]
    public AudioSource voiceSource;
    public AudioClip voiceClip;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;

    private bool sentenceDone = false;
    private bool isDialogueActive = false;
    private PlayerScript playerScriptToDisable;
    private Coroutine pulseCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        // Safely check all required UI fields to avoid null exceptions in tests
        if (dialogueBox == null || nameText == null || dialogueText == null || continueArrow == null)
        {
            Debug.LogWarning("DialogueManager: Missing UI references during Awake (likely test context).");
            return;
        }

        dialogueBox.SetActive(false);
        continueArrow.SetActive(false);
    }


    public void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        sentenceDone = false;
        isDialogueActive = true;

        DisablePlayerScript();

        dialogueBox.SetActive(true);
        DisplayCurrentLine();
    }

    void Update()
    {
        if (!isDialogueActive) return;

        if (dialogueBox.activeSelf && sentenceDone && Input.GetKeyDown(KeyCode.E))
        {
            DialogueLine line = currentDialogue.lines[currentLineIndex];

            currentLineIndex = (line.nextLineIndex >= 0)
                ? line.nextLineIndex
                : currentLineIndex + 1;

            DisplayCurrentLine();
        }
    }

    void DisplayCurrentLine()
    {
        continueArrow.SetActive(false);

        if (currentLineIndex >= currentDialogue.lines.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentDialogue.lines[currentLineIndex];
        nameText.text = line.speakerName;
        portraitImage.sprite = line.portrait;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    public IEnumerator TypeSentence(DialogueLine line)
    {
        sentenceDone = false;
        dialogueText.text = "";

        int charIndex = 0;

        foreach (char letter in line.sentence)
        {
            dialogueText.text += letter;

            // Play every 2 characters
            if (charIndex % 4 == 0 && !char.IsWhiteSpace(letter) && voiceClip != null && voiceSource != null)
            {
                voiceSource.pitch = Random.Range(line.minPitch, line.maxPitch);
                voiceSource.PlayOneShot(voiceClip, 0.7f);
            }

            charIndex++;
            yield return new WaitForSeconds(0.015f);
        }


        sentenceDone = true;
        continueArrow.SetActive(true);
        pulseCoroutine = StartCoroutine(PulseArrow());
    }

    IEnumerator PulseArrow()
    {
        float pulseDuration = 1f;
        Vector3 originalScale = continueArrow.transform.localScale;
        Vector3 targetScale = originalScale * 1.1f;

        while (true)
        {
            float timer = 0f;
            while (timer < pulseDuration / 2)
            {
                continueArrow.transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / (pulseDuration / 2));
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            while (timer < pulseDuration / 2)
            {
                continueArrow.transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / (pulseDuration / 2));
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        continueArrow.SetActive(false);

        if (pulseCoroutine != null)
            StopCoroutine(pulseCoroutine);

        continueArrow.transform.localScale = Vector3.one;

        EnablePlayerScript();
    }

    void DisablePlayerScript()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScriptToDisable = player.GetComponent<PlayerScript>();
            if (playerScriptToDisable != null)
                playerScriptToDisable.enabled = false;
        }
    }

    void EnablePlayerScript()
    {
        if (playerScriptToDisable != null)
            playerScriptToDisable.enabled = true;
    }

    public bool IsDialogueActive() => isDialogueActive;
    public int GetCurrentLineIndex() => currentLineIndex;

}
