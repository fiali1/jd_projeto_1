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
            SceneManager.LoadScene("Game");
            PlayerPrefs.SetString("PlayerName", input);
        }
        else {
            print("Falta preencher o nome");
        }
    }

    public void TelaCreditos(){
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
        Debug.Log(input);
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
