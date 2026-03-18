using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    private GameObject[] spawnPointsArr;
    private void Awake()
    {
        if(Instance == null)
        {  
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<PhotonView>();
            Instance = this;
        }
      
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex > 1)
        {
            spawnPointsArr = GameObject.FindGameObjectsWithTag("SpawnPoint");
            Transform spawnPoint = spawnPointsArr[Random.Range(0, spawnPointsArr.Length)].transform;

            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), spawnPoint.position, Quaternion.identity);
        }
    }
}
