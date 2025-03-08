using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteKey("BestScore"); // Supprime le meilleur score
        PlayerPrefs.Save(); // Sauvegarde pour valider
        Debug.Log("BestScore r�initialis� !"); // Message dans la console
    }
}
