using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;


    private Queue<string> _sentences;
    private bool _isTalking;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
        _isTalking = false;
    }

    void Update()
    {
        if (_isTalking)
        {
            if (InputManager.AButton())
                DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _isTalking = true;
        animator.SetBool("isOpen", _isTalking);
        nameText.text = dialogue.name;
        _sentences.Clear();

        foreach (string sentence in dialogue.sentences)
            _sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        _isTalking = false;
        animator.SetBool("isOpen", _isTalking);
    }
}
