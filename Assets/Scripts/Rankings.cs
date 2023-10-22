using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[System.Serializable]
public class RankingList
{
    public List<Ranking> rankings;
}

public class Rankings : MonoBehaviour
{
    public GameObject rankingPrefab;
    private List<Ranking> rankingsList = new ();

    void LoadRankings() 
    {
        if (File.Exists("playerRankings.json")) {
            string rankingsJsonData = File.ReadAllText("playerRankings.json");
            RankingList storedRankings = JsonUtility.FromJson<RankingList>(rankingsJsonData);

            // Load player rankings list
            foreach (Ranking ranking in storedRankings.rankings) 
            {
                rankingsList.Add(ranking);
            }

            rankingsList = rankingsList.OrderByDescending(item => item.points).ToList();
        }
    }

    void RenderRankingsList() {
        if (rankingsList.Count == 0) 
        {            
            TextMeshProUGUI emptyMessage = gameObject.AddComponent<TextMeshProUGUI>();

            emptyMessage.text = "Ainda não há placares disponíveis";
            emptyMessage.fontSize = 20;
            emptyMessage.font = Resources.Load("HVD Fonts  MikadoRegular SDF") as TMP_FontAsset;
            emptyMessage.alignment = TextAlignmentOptions.Top;

            emptyMessage.transform.position = new Vector3(
                0,
                transform.position.y - 25,
                transform.position.z
            );

            return;
        }

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

            if (i == 0) 
            {
                Color32 firstPlaceColor = new (241, 255, 74, 255);
                rankingPosition.color = rankingName.color = rankingPoints.color = firstPlaceColor;
                rankingPosition.fontStyle = rankingName.fontStyle = rankingPoints.fontStyle = FontStyles.Bold;
            }

            else if (i == 1) 
            {
                Color32 secondPlaceColor = new (237, 237, 237, 255);
                rankingPosition.color = rankingName.color = rankingPoints.color = secondPlaceColor;
                rankingPosition.fontStyle = rankingName.fontStyle = rankingPoints.fontStyle = FontStyles.Bold;
            }

            else if (i == 2)
            {
                Color32 thirdPlaceColor = new (207, 177, 70, 255);
                rankingPosition.color = rankingName.color = rankingPoints.color = thirdPlaceColor;
                rankingPosition.fontStyle = rankingName.fontStyle = rankingPoints.fontStyle = FontStyles.Bold;
            }

            else 
            {
                 Color32 inRankingsColor = new (255, 187, 232, 255);
                rankingPosition.color = rankingName.color = rankingPoints.color = inRankingsColor;
            }

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
