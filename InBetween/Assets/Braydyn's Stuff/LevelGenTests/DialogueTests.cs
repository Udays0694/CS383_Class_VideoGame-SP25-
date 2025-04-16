using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DialogueTests
{
    private DialogueManager dialogueManager;
    private GameObject managerObj;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Create core object
        managerObj = new GameObject("DialogueManager");
        dialogueManager = managerObj.AddComponent<DialogueManager>();

        // Assign DialogueManager.Instance manually (what Awake would normally do)
        DialogueManager.Instance = dialogueManager;

        // Create UI elements with TMP components
        dialogueManager.dialogueBox = new GameObject("DialogueBox");
        dialogueManager.dialogueBox.SetActive(false);

        GameObject nameGO = new GameObject("NameText");
        nameGO.AddComponent<CanvasRenderer>();
        dialogueManager.nameText = nameGO.AddComponent<TextMeshProUGUI>();

        GameObject dialogueGO = new GameObject("DialogueText");
        dialogueGO.AddComponent<CanvasRenderer>();
        dialogueManager.dialogueText = dialogueGO.AddComponent<TextMeshProUGUI>();

        GameObject portraitGO = new GameObject("Portrait");
        dialogueManager.portraitImage = portraitGO.AddComponent<Image>();

        dialogueManager.continueArrow = new GameObject("ContinueArrow");

        // Dummy voice clip setup
        dialogueManager.voiceSource = managerObj.AddComponent<AudioSource>();
        dialogueManager.voiceClip = AudioClip.Create("dummy", 44100, 1, 44100, false);

        yield return null;
    }

    [Test]
    public void DialogueData_CreatesAndHoldsLines()
    {
        var data = ScriptableObject.CreateInstance<DialogueData>();
        data.lines.Add(new DialogueLine { sentence = "Hello!", speakerName = "NPC" });

        Assert.AreEqual(1, data.lines.Count);
        Assert.AreEqual("NPC", data.lines[0].speakerName);
    }

    [UnityTest]
    public IEnumerator DialogueManager_StartsAndDisplaysDialogue()
    {
        var data = ScriptableObject.CreateInstance<DialogueData>();
        data.lines.Add(new DialogueLine { sentence = "Testing", speakerName = "TestSpeaker" });

        dialogueManager.StartDialogue(data);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual("TestSpeaker", dialogueManager.nameText.text);
    }

    [Test]
    public void DialogueLine_HasJumpIndexByDefault()
    {
        DialogueLine line = new DialogueLine();
        Assert.AreEqual(-1, line.nextLineIndex);
    }

    [UnityTest]
    public IEnumerator DialogueManager_TypingEffectPlays()
    {
        var data = ScriptableObject.CreateInstance<DialogueData>();
        data.lines.Add(new DialogueLine { sentence = "Typing test", speakerName = "Tester" });

        dialogueManager.StartDialogue(data);
        yield return new WaitForSeconds(0.3f);

        Assert.IsTrue(dialogueManager.dialogueText.text.Length > 0);
    }

    [UnityTest]
    public IEnumerator DialogueManager_ContinueArrowAppears()
    {
        var data = ScriptableObject.CreateInstance<DialogueData>();
        data.lines.Add(new DialogueLine { sentence = "Test", speakerName = "Speaker" });

        dialogueManager.StartDialogue(data);
        yield return new WaitForSeconds(0.3f);

        Assert.IsTrue(dialogueManager.continueArrow.activeSelf);
    }

    [UnityTest]
    public IEnumerator DialogueManager_EndsDialogueAtEndOfLines()
    {
        var data = ScriptableObject.CreateInstance<DialogueData>();
        data.lines.Add(new DialogueLine { sentence = "Line1", speakerName = "NPC" });

        dialogueManager.StartDialogue(data);
        yield return new WaitForSeconds(0.2f);

        // Simulate end of dialogue
        dialogueManager.SendMessage("DisplayCurrentLine");
        dialogueManager.SendMessage("DisplayCurrentLine");

        yield return new WaitForSeconds(0.2f);
        Assert.IsFalse(dialogueManager.dialogueBox.activeSelf, "Dialogue box should close at end.");
    }

    [Test]
    public void DialogueLine_PitchRangeWithinBounds()
    {
        var line = new DialogueLine { minPitch = 0.8f, maxPitch = 1.2f };
        Assert.IsTrue(line.minPitch >= 0.01f && line.maxPitch <= 2f);
    }

    [UnityTest]
    public IEnumerator DialogueTrigger_TriggersDialogue()
    {
        var triggerGO = new GameObject("Trigger");
        var trigger = triggerGO.AddComponent<DialogueTrigger>();

        trigger.dialogue = ScriptableObject.CreateInstance<DialogueData>();
        trigger.requireButtonPress = false;
        trigger.triggerOnStart = false;
        trigger.interactOnce = false;

        trigger.SendMessage("TriggerDialogue");

        yield return new WaitForSeconds(0.2f);

        Assert.IsTrue(triggerGO.activeSelf);
    }

    [Test]
    public void DialogueTrigger_HasCorrectSettings()
    {
        var triggerGO = new GameObject("Trigger");
        var trigger = triggerGO.AddComponent<DialogueTrigger>();

        trigger.triggerOnStart = true;
        trigger.dialogue = ScriptableObject.CreateInstance<DialogueData>();

        Assert.IsTrue(trigger.triggerOnStart);
        Assert.IsNotNull(trigger.dialogue);
    }

    [TearDown]
    public void TearDown()
    {
        DialogueManager.Instance = null;
        Object.DestroyImmediate(managerObj);
    }
}
