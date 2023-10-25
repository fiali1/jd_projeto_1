using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            GameObject textContainer = new()
            {
                name = "TextContainer",
            };

            textContainer.transform.parent = transform;

            // Generate sprite from texture and apply it to a container image
            UnityEngine.UI.Image containerImage = gameObject.AddComponent<UnityEngine.UI.Image>();
            Texture2D texture2D = Resources.Load("Textura Botao") as Texture2D;
            Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * 0.5f);
            containerImage.sprite = sprite;

            // Generate info message for the player
            TextMeshProUGUI emptyMessage = textContainer.AddComponent<TextMeshProUGUI>();
            emptyMessage.text = "Ainda não há placares disponíveis!";
            emptyMessage.fontSize = 20;
            emptyMessage.font = Resources.Load("HVD Fonts  MikadoRegular SDF") as TMP_FontAsset;
            emptyMessage.color = new Color32(79, 48, 0, 255);
            emptyMessage.alignment = TextAlignmentOptions.Top;
            
            // Setup message alignement and positioning
            VerticalLayoutGroup layout = gameObject.GetComponent<VerticalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleCenter;
            emptyMessage.transform.position = transform.position;

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

            if (i <= 2) 
            {
                rankingPosition.fontStyle = rankingName.fontStyle = rankingPoints.fontStyle = FontStyles.Bold;
            }
            
            Color32 inRankingsColor = new (79, 48, 0, 255);
            rankingPosition.color = rankingName.color = rankingPoints.color = inRankingsColor;
    

            // Setup ranking component positioning
            float yPosition = (float)(transform.position.y - (i * 25));
            rankingContainer.transform.position = new Vector3(
                0,
                yPosition,
                transform.position.z
            );
        }        
    }

    public void TelaInicial(){
        SceneManager.LoadScene("TelaInicial");
    }

    public void Sair(){
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        print("Game is exiting");
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
