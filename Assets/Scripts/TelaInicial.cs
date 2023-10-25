using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{
    private string input;
    private bool preencheuNome = false;

    public void TelaJogo(){
        if (preencheuNome) {
            preencheuNome = false;
            PlayerPrefs.SetString("PlayerName", input);
            Destroy(GameObject.Find("AudioManager"));
            SceneManager.LoadScene("Game");
        }
        else {
            print("Falta preencher o nome");
        }
    }

    public void TelaCreditos(){
        GameObject.Find("AudioManager").GetComponent<AudioManager>().StopSound("GameMusic");
        SceneManager.LoadScene("Creditos");
    }

    public void TelaRanking(){
        SceneManager.LoadScene("Rankings");
    }

    public void Sair(){
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        print("Game is exiting");
    }
    
    public void ReadStringInput(string s){
        preencheuNome = true;
        input = s;
    }
    
    void Awake() {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
        if (!audioManager.IsPlaying("GameMusic")) 
        {
            audioManager.PlaySound("GameMusic");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
