using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Rankings : MonoBehaviour
{
    public GameObject rankingPrefab;
    private readonly List<Ranking> rankingsList = new();

    void LoadRankings() 
    {
        // Generate sample rankings list
        for (int i = 0; i < 10; i++) 
        {
            Ranking newRanking = new("ABC", 100 * (i + 1));
            rankingsList.Add(newRanking);
        }
    }

    void RenderRankingsList() {
        for (int i = 0; i < rankingsList.Count; i++)
        {
            // Instantiate and setup each rank components set via a prefab
            GameObject rankingContainer = Instantiate(rankingPrefab, transform);

            TextMeshProUGUI rankingPosition = rankingContainer.transform.Find("Position").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rankingName = rankingContainer.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rankingPoints = rankingContainer.transform.Find("Points").GetComponent<TextMeshProUGUI>();

            rankingPosition.name = rankingPosition.name += i.ToString();
            rankingName.name = rankingName.name += i.ToString();
            rankingPoints.name = rankingPoints.name += i.ToString();

            rankingPosition.text = (i + 1).ToString();
            rankingName.text = rankingsList[i].name;
            rankingPoints.text = rankingsList[i].points.ToString();

            // Setup ranking component positioning
            float yPosition = (float)(transform.position.y - (i * 25));
            rankingContainer.transform.position = new Vector3(
                0,
                yPosition,
                transform.position.z
            );
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadRankings();
        RenderRankingsList();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
