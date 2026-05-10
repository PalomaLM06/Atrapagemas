using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager Instance;

    public GameObject selectedCharacterPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectCharacter(GameObject prefab)
    {
        selectedCharacterPrefab = prefab;
        Debug.Log("Seleccionado: " + prefab.name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tablero");//Nombre de escena de donde sale los personajes
    }
}