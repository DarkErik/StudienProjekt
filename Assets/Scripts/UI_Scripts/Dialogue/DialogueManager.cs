using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance{ get; private set; }
    private static Dialogue currentDialogue = null;
    

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialgueText;
    public Animator animator;

    private Queue<string> sentences;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && animator.GetBool("IsOpen"))
            DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(currentDialogue == dialogue) return;

        PlayerMovement.Shutdown();
        currentDialogue = dialogue;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            animator.SetBool("IsOpen", false);
            currentDialogue = null;
            PlayerMovement.WakeUp();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeLetters(sentence));

    }

    IEnumerator TypeLetters(string sentence)
    {
        dialgueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialgueText.text += letter;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

}
