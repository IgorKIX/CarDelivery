using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private bool isUploadToCloud = false;
    [SerializeField] private string url = "http://localhost:3000/";



    private Dictionary<string, string> gameData;
    private FileDataHandler fileDataHandler;
    private CloudDataHandler cloudDataHandler;
    private List<IDataPersistance> dataPersistancesObject;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private async void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.cloudDataHandler = new CloudDataHandler(this.url);
        this.dataPersistancesObject = FindAllDataPersistanceObject();
        if (isUploadToCloud)
        {
            await Task.Run(() => synchronizeSaveGameData());
        }
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new Dictionary<string, string>();
    }

    public void LoadGame()
    {
        // Load any saved data form a file using the data handler
        this.gameData = fileDataHandler.Load();
        // if no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found.");
            this.gameData = new Dictionary<string, string>();
        }

        // Push the loaded data to all other scripts that need it
        foreach (IDataPersistance dataPersistanceObj in dataPersistancesObject)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts, to be updated
        foreach (IDataPersistance dataPersistanceObj in dataPersistancesObject)
        {
            dataPersistanceObj.SaveData(gameData);
        }
        // save that data to a file using data handler
        fileDataHandler.Save(gameData);

        if (isUploadToCloud)
        {
            // save that data to a cloud using data handler
            cloudDataHandler.Save(gameData);
        }
    }

    private List<IDataPersistance> FindAllDataPersistanceObject()
    {
        IEnumerable<IDataPersistance> dataPersistancesObject = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistancesObject);
    }

    private async void synchronizeSaveGameData()
    {
        Dictionary<string, string> cloudSaveGame = await cloudDataHandler.Load();
        Dictionary<string, string> fileSaveGame = fileDataHandler.Load();

        // If there is a diference, replace file save game with this from cloud
        if (cloudSaveGame != null && !compareDictionaries(cloudSaveGame, fileSaveGame))
        {
            fileDataHandler.Save(cloudSaveGame);
        }
    }

    private bool compareDictionaries(Dictionary<string, string> dic1, Dictionary<string, string> dic2)
    {
        // early-exit checks
        if (null == dic2)
            return null == dic1;
        if (null == dic1)
            return false;
        if (object.ReferenceEquals(dic1, dic2))
            return true;
        if (dic1.Count != dic2.Count)
            return false;

        // check keys are the same
        foreach (string k in dic1.Keys)
            if (!dic2.ContainsKey(k))
                return false;

        // check values are the same
        foreach (string k in dic1.Keys)
            if (!dic1[k].Equals(dic2[k]))
                return false;

        return true;
    }
}
