using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrollTexto : MonoBehaviour
{
	public float scrollSpeed = 20;
	public float limiteLoopingY = 2500;

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
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 pos = transform.position;

		Vector3 local = transform.TransformDirection(0,1,0);

		pos += local * scrollSpeed * Time.deltaTime;
		transform.position = pos;

			if(pos.y >= limiteLoopingY){
				transform.position = new Vector3(transform.position.x, -540, transform.position.z) ;
			}
    }
	
}
