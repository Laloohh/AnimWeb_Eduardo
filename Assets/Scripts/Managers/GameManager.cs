using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isPlayerAlive;
    public static int nextlLevel = 0;
    // Start is called before the first frame update

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        isPlayerAlive = true; 
    }

    // Update is called once per frame
    void Update() {
        if (!isPlayerAlive) {
            SceneManager.LoadScene("GameOver");
        }

        switch (nextlLevel) {
            case 1:
                SceneManager.LoadScene("Level1");
                nextlLevel = 0;
                    break;
            case 2:
            SceneManager.LoadScene("Level2");
                nextlLevel = 0;
                break;
            case 3:
            SceneManager.LoadScene("Level3");
                nextlLevel = 0;
                break;
            case 4:
                SceneManager.LoadScene("Level4");
                nextlLevel = 0;
                break;
            case 5:
                SceneManager.LoadScene("Level5");
                nextlLevel = 0;
                break;
            case 6:
                SceneManager.LoadScene("Congratulations");
                nextlLevel = 0;
                break;
        }
    }
}
