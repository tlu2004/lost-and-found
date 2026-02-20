using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLoadScene : MonoBehaviour
{
    public string dwnstrs;   // exact name in Build Settings

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("scene load");
        SceneManager.LoadScene(dwnstrs);
    }
}