using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordFormerLeft : MonoBehaviour
{
    public string Word;
    public string Letter;
    public ArrayOfStrings aos;
    public Transform mainCanvas;

    public GameObject letterToTheLeft;

    public static Stack<GameObject> stackOfLeftObjs = new Stack<GameObject>();

    bool wordFound = false;

    int superI = 0;

    private void Start()
    {
        aos = GameObject.Find("SelectedLetterManager").GetComponent<ArrayOfStrings>();
        Word = null;
        letterToTheLeft = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !wordFound)
        {
            if (other.gameObject.GetComponentInChildren<WordFormerLeft>().Word != null)
            {
                Word = other.gameObject.GetComponentInChildren<WordFormerLeft>().Word + Letter;
                letterToTheLeft = other.gameObject;
            }
            else
            {
                Word = other.gameObject.GetComponentInChildren<WordFormerLeft>().Letter + Letter;
                letterToTheLeft = other.gameObject;
            }
            Debug.Log(Word);

            int i = 0;
            foreach (string abc in aos.ArrayofWords)
            {
                if (string.Compare(abc, Word, true) == 0)
                {
                    Debug.Log("Word Found");
                    wordFound = true;
                    stackOfLeftObjs.Push(transform.parent.gameObject);
                    superI = i;
                    StartCoroutine("wordFoundSequence");
                    //aos.ArrayofBlanks[i].enabled = false;
                    //aos.ArrayofTexts[i].gameObject.SetActive(true);
                }
                i++;
            }
        }
    }

    public void objToLeft()
    {
        if (letterToTheLeft != null)
        {
            stackOfLeftObjs.Push(letterToTheLeft);
            letterToTheLeft.GetComponentInChildren<WordFormerLeft>().objToLeft();
        }
        sizeOfStack = stackOfLeftObjs.Count;
    }

    int i = 0;
    int sizeOfStack = stackOfLeftObjs.Count;
    IEnumerator wordFoundSequence()//this is executing twice
    {
        yield return new WaitForSeconds(0.1f);
        transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        objToLeft();
        while (i < sizeOfStack)
        {
            GameObject obj = stackOfLeftObjs.Pop();
            RectTransform gameObjectsLetter = obj.GetComponent<UIFollow>().nameLabel;
            GameObject letterDuplicate = Instantiate(gameObjectsLetter.gameObject, gameObjectsLetter.transform.position, gameObjectsLetter.transform.rotation);
            letterDuplicate.transform.SetParent(mainCanvas);
            letterDuplicate.transform.position = Vector3.Lerp(letterDuplicate.transform.position, aos.ArrayofBlanks[superI].transform.position,0.1f);
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 0.5f, obj.transform.position.z);
            obj.transform.localScale *= 1.5f;
            yield return new WaitForSeconds(0.12f);
            obj.transform.localScale /= 1.5f;
            i++;
        }
        yield return new WaitForSeconds(0.25f);
        i = 0;
        ResetPosition.reset = true;
        wordFound = false;
        Word = null;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("here");
        Word = null;
        letterToTheLeft = null;
    }
}