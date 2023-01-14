using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ParcelStack3D : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _parcelPrefabs = new List<GameObject>();
    /*
    *   Prefab list
    *   0 = Normal parcel
    *   1 = Uncommon parcel
    *   2 = Rare Parcel
    */

    [SerializeField]
    private List<Transform> _stackPositions = new List<Transform>();
    [SerializeField]
    private List<TextMeshPro> _playerNameText = new List<TextMeshPro>();

    private List<PlayerInfo> _playerList = new List<PlayerInfo>();


    [SerializeField]
    private float yOffset = 5;  // Y axis offset when spawning parcels

    [SerializeField]
    private float xOffset = 5;  // X axis offset

    private DatabaseController databaseController;

    void Awake()
    {
        databaseController = GameObject.Find("Managers")?.GetComponent<DatabaseInitializer>()?.dbc;
        //  StartCoroutine(PopulateStacks(_playerList));
    }

    private void OnEnable()
    {
        databaseController.GetAllPlayers((allPlayers) => OnEnableCallback(allPlayers));
        databaseController.GetPlayerInformation((exception, player) => OnEnableCallbackPlayer(player));
    }

    private void OnEnableCallback(List<PlayerInfo> allPlayers)
    {
        if (allPlayers == null) //no connection
        {
            Debug.Log("No Connection to db");
        }
        else //playerlist recieved from the database
        {
            _playerList = allPlayers;
            for (int i = 0; i < 3; ++i)
            {
                _playerNameText[i + 1].text = $"{i + 1}. {_playerList[i].Name}";
            }

        }
    }

    private void OnEnableCallbackPlayer(PlayerInfo player)
    {
        _playerNameText[0].text = player.Name;
        StartCoroutine(SpawnStack(player, _stackPositions[0]));
    }

    public void SpawnStack(int i)   // Parameter is what the number of player you want to spawn. 1 == Number 1 player, 2 Number 2 etx
    {
        StartCoroutine(SpawnStack(_playerList[i - 1], _stackPositions[i]));
    }

    public IEnumerator SpawnStack(PlayerInfo player, Transform spawnPos)
    {
        List<Rarity> rarities = new List<Rarity>();
        CreateParcelList(rarities, player);

        int iterations = 0;
        int xOffsetAmount = ((-rarities.Count - 1) / 5) + 2;

        foreach (var rarity in rarities)
        {
            Vector3 offsetSpawnPos = new Vector3(spawnPos.position.x + xOffset * xOffsetAmount,
                                           spawnPos.position.y + yOffset * iterations,
                                           spawnPos.position.z);
            switch (rarity)
            {
                case Rarity.Common:
                    Instantiate(_parcelPrefabs[0], offsetSpawnPos, Quaternion.identity, spawnPos);
                    break;
                case Rarity.Uncommon:
                    Instantiate(_parcelPrefabs[1], offsetSpawnPos, Quaternion.identity, spawnPos);
                    break;
                case Rarity.Rare:
                    Instantiate(_parcelPrefabs[2], offsetSpawnPos, Quaternion.identity, spawnPos);
                    break;
            }
            ++iterations;
            if (iterations == 5)
            {
                iterations = 0;
                ++xOffsetAmount;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CreateParcelList(List<Rarity> rarityList, PlayerInfo playerInfo)
    {
        int commonAmount = playerInfo.CommonParcels;
        int uncommonAmount = playerInfo.UncommonParcels;
        int rareAmount = playerInfo.RareParcels;

        for (int j = 0; j < commonAmount; j++)
        {
            rarityList.Add(Rarity.Common);
        }
        for (int j = 0; j < uncommonAmount; j++)
        {
            rarityList.Add(Rarity.Uncommon);
        }
        for (int j = 0; j < rareAmount; j++)
        {
            rarityList.Add(Rarity.Rare);
        }
        rarityList.Shuffle();
    }
}

static class ShuffleExtension   // Shuffle list with Fisher–Yates 
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
