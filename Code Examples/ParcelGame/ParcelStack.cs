using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParcelStack : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _parcelPrefabs = new List<GameObject>();

    [SerializeField]
    private List<Transform> _stackPositions = new List<Transform>();

    private List<PlayerInfo> _playerList = new List<PlayerInfo>();

    [SerializeField]
    private float parcelVerticalOffset = 175f;
    [SerializeField]
    private float parcelHorizontalOffset = 175f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PopulateStacks(_playerList));
    }



    public IEnumerator PopulateStacks(List<PlayerInfo> playerList)
    {
        int x = 5;
        int horizontalOffsetCount = 0;
        int stackSize = 0;
        Vector3 spawnPos;
        for (int k = 0; k < 5; ++k)
        {
            spawnPos = _stackPositions[k].GetComponent<RectTransform>().position;
            stackSize = 0;
            horizontalOffsetCount = 0;
            for (int i = 0; i < x; i++) // Normal
            {
                if (stackSize == 5)
                {
                    stackSize = 0;
                    ++horizontalOffsetCount;
                }
                ++stackSize;
                GameObject temp = Instantiate(_parcelPrefabs[0], spawnPos, Quaternion.identity, _stackPositions[k]);
                temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    horizontalOffsetCount * -parcelHorizontalOffset + Random.Range(-25f, 25f),
                    i * parcelVerticalOffset);
                yield return new WaitForSeconds(0.1f);
            }

            for (int i = 0; i < x; i++) // Uncommon
            {

                if (stackSize == 5)
                {
                    stackSize = 0;
                    ++horizontalOffsetCount;
                }
                ++stackSize;
                GameObject temp = Instantiate(_parcelPrefabs[1], spawnPos, Quaternion.identity, _stackPositions[k]);
                temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    horizontalOffsetCount * -parcelHorizontalOffset + Random.Range(-25f, 25f),
                    i * parcelVerticalOffset);

                yield return new WaitForSeconds(0.1f);
            }

            for (int i = 0; i < x; i++) // Rare
            {

                if (stackSize == 5)
                {
                    stackSize = 0;
                    ++horizontalOffsetCount;
                }
                ++stackSize;
                GameObject temp = Instantiate(_parcelPrefabs[2], spawnPos, Quaternion.identity, _stackPositions[k]);
                temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    horizontalOffsetCount * -parcelHorizontalOffset + Random.Range(-25f, 25f),
                    i * parcelVerticalOffset);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }


}
