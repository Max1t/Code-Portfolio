using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MatchTextEffect : MonoBehaviour
{

    public GameObject text; // Prefab which holds the text.
    public List<GameObject> textPool;
    public int poolAmount;
    public Transform textParent;
    public bool expand;
    public Camera mainCamera;

    public Canvas canvas;

    public static MatchTextEffect instance;

    public Color Red;
    public Color Blue;
    public Color Green;
    public Color Black;
    public Color White;
    public Color Yellow;
    public Color Violet;

    public List<List<GameObject>> DestroyLists = new List<List<GameObject>>();

    public bool spawnCoroutineRunningNumber = false;
    public bool spawnCoroutineRunningOther = false;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        textPool = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject instance = Instantiate(text, Vector3.zero, Quaternion.identity, textParent);
            text.SetActive(false);
            textPool.Add(instance);
        }
    }

    /// <summary>
    /// Get text object from pool
    /// </summary>
    public GameObject GetFromPool()
    {
        foreach (GameObject text in textPool)
        {
            if (!text.activeInHierarchy)
            {
                return text;
            }
        }
        if (expand)
        {
            GameObject instance = Instantiate(text, Vector3.zero, Quaternion.identity, textParent);
            text.SetActive(false);
            textPool.Add(instance);
            return instance;
        }
        else return null;
    }


    public void SpawnText(int count, string tag, Vector3 worldPos, bool number) // true if spawing the amount of blocks destoyed, false if spawning other text
    {
        GameObject instance = GetFromPool();
        if (number) SetTextNumber(instance, count, tag);
        if (!number) SetTextFlavor(instance, count, tag);
        instance.transform.position = MatchTextEffect.instance.worldToUISpace(worldPos); // Get The position of the block in canvas space
        instance.SetActive(true);
        MatchTextEffect.instance.StartCoroutine(MatchTextEffect.instance.FadeUp(instance));
        // yield return new WaitForEndOfFrame();
    }


    public void SpawnText(int count, string tag, Vector3 worldPos) // true if spawing the amount of blocks destoyed, false if spawning other text
    {
        GameObject instance = GetFromPool();
        SetTextNumber(instance, count, tag);
        instance.transform.position = MatchTextEffect.instance.worldToUISpace(worldPos); // Get The position of the block in canvas space
        instance.SetActive(true);
        MatchTextEffect.instance.StartCoroutine(MatchTextEffect.instance.FadeUp(instance));
        // yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Collect a list to be parsed later
    /// </summary>
    /// <param name="list"> The list to be collected </param>
    public void CollectList(List<GameObject> list)
    {
        DestroyLists.Add(list);
    }

    /// <summary>
    /// Go trough all lists collected by CollectList().
    /// Merge lists which contain any amount of duplicates
    /// and call SpawnText() with the appropriate lists
    /// </summary>
    public void ResolveLists()
    {
        int listsCombined = 0;
        List<List<GameObject>> resolvedLists = new List<List<GameObject>>();
        List<List<GameObject>> checkList = new List<List<GameObject>>();
        if (DestroyLists.Count > 1)
        {
            foreach (List<GameObject> list1 in DestroyLists)
            {
                if (checkList.Contains(list1)) continue;
                foreach (List<GameObject> list2 in DestroyLists)
                {
                    if (list1 == list2) continue;
                    if (checkList.Contains(list2)) continue;
                    if (list1.Any(x => list2.Any(y => x.Equals(y))))    // Checks if the lists contain any items in the other list
                    {                                                   // If duplicate items are found merge lists so correct text can be made
                        Debug.Log("Merge List");
                        listsCombined++;
                        var merge = list1.Union(list2).ToList();
                        resolvedLists.Add(merge);
                        checkList.Add(list1);
                        checkList.Add(list2);
                        continue;
                    }
                }
                if (!checkList.Contains(list1))
                {
                    resolvedLists.Add(list1);
                    checkList.Add(list1);
                }
            }
        }
        else
        {
            resolvedLists.Add(DestroyLists[0]);
        }

        SpawnText(resolvedLists, DestroyLists);

        DestroyLists = new List<List<GameObject>>();
        resolvedLists = new List<List<GameObject>>();
        checkList = new List<List<GameObject>>();
    }

    void SpawnText(List<List<GameObject>> resolvedLists, List<List<GameObject>> allLists)
    {
        foreach (var list in resolvedLists)
        {
            MatchTextEffect.instance.SpawnText(list.Count, list[0].tag, list[0].transform.position, true); // Spawn text indicating how many were matched
        }
        foreach (var list in allLists)
        {
            if (list.Count > 3)
                MatchTextEffect.instance.SpawnText(list.Count, list[0].tag, list[0].transform.TransformPoint(Vector3.up * 0.5f), false);
        }
    }



    /// <summary>
    /// Fades the desired Gameobect while moving it up, Changes texts color depending on the tag
    /// </summary>
    IEnumerator FadeUp(GameObject obj)
    {
        TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
        Color color = text.color;
        float startOpacity = color.a;
        float t = 0f;
        while (t < 1.5f)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / 1f);
            color.a = Mathf.Lerp(startOpacity, 0f, blend);
            text.color = color;
            obj.transform.Translate(Vector2.up);
            yield return null;
        }
        obj.SetActive(false);
        color.a = startOpacity;
        text.color = color;
        yield return null;
    }

    /// <summary>
    /// Get canvas position from a world position
    /// </summary>
    /// <param name="worldPos"> World position of the object you want the canvas postion from</param>
    public Vector3 worldToUISpace(Vector3 worldPos)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out movePos);
        return canvas.transform.TransformPoint(movePos);
    }

    /// <summary>
    /// Set the apppropriate text for the parameter text gameobject
    /// </summary>
    /// <param name="obj">Text GameObject</param>
    /// <param name="amount"> Amount of blocks destroyed</param>
    /// <param name="tag"> Tag of the destroyed blocks to set color</param>
    /// <param name="number"> If setting text for a number or an other text. True = normal number text</param>
    public void SetTextNumber(GameObject obj, int amount, string tag)
    {
        Color textColor = Color.black;
        switch (tag)
        {
            case "Blue":
                {
                    textColor = Blue;
                    break;
                }
            case "Red":
                {
                    textColor = Red;
                    break;
                }
            case "Green":

                {
                    textColor = Green;
                    break;
                }
            case "White":
                {
                    textColor = White;
                    break;
                }
            case "Violet":
                {
                    textColor = Violet;
                    break;
                }
            case "Yellow":
                {
                    textColor = Yellow;
                    break;
                }
            case "Damage":
                {
                    textColor = Black;
                    break;
                }
        }
        TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
        text.color = textColor;
        text.text = "+" + amount;

    }



    public void SetTextFlavor(GameObject obj, int amount, string tag)
    {
        Color textColor = Color.black;
        switch (tag)
        {
            case "Blue":
                {
                    textColor = Blue;
                    break;
                }
            case "Red":
                {
                    textColor = Red;
                    break;
                }
            case "Green":

                {
                    textColor = Green;
                    break;
                }
            case "White":
                {
                    textColor = White;
                    break;
                }
            case "Violet":
                {
                    textColor = Violet;
                    break;
                }
            case "Yellow":
                {
                    textColor = Yellow;
                    break;
                }
            case "Damage":
                {
                    textColor = Black;
                    break;
                }
        }
        TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
        text.color = textColor;
        switch (amount)
        {
            case 4:
                {
                    text.text = "4 " + "Of A Kind";
                    break;
                }
            case 5:
                {
                    text.text = "5 " + "In A Row";
                    break;
                }
            case 6:
                {
                    text.text = "6 " + "In A Row";
                    break;
                }
            case 7:
                {
                    text.text = "7 " + "In A Row";
                    break;
                }
            case 8:
                {
                    text.text = "8 " + "In A Row";
                    break;
                }
        }
    }
}



