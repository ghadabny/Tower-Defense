using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteKey("BestScore"); // Supprime le meilleur score
        PlayerPrefs.Save(); // Sauvegarde pour valider
        Debug.Log("BestScore réinitialisé !"); // Message dans la console
    }
}
