using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMatching : MonoBehaviour
{
    //public Button settingBtn;
    //public Button settingCloseBtn;
    //public Button ExitBtn;
    //public Button shopBtn;
    //public Button shopCloseBtn;
    public Button RunBtn;
    //public Button TutoBtn;
    //public Button TutoCloseBtn;
    [SerializeField] private GameObject[] tabs = new GameObject[0];
    [SerializeField] private GameObject SetNickNamePanel;

    public TextMeshProUGUI ramCounter;
    public TextMeshProUGUI dateCounter;

    //private void Awake()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    var um = UIManager.Instance;
    //    settingBtn.onClick.AddListener(um.OnSettingPopup);
    //    ExitBtn.onClick.AddListener(um.OnExitButton);
    //    shopBtn.onClick.AddListener(um.OnShopButton);
    //    TutoBtn.onClick.AddListener(um.OnTutoPopup);
    //    RunBtn.onClick.AddListener(um.OnStartButton);
    //    settingCloseBtn.onClick.AddListener(um.CloseSettingPopup);
    //    shopCloseBtn.onClick.AddListener(um.CloseShoppopup);
    //    TutoCloseBtn.onClick.AddListener(um.CloseTutoPopup);
    //    UpdateRamCounter();
    //}

    private void Start()
    {
        var um = UIManager.Instance;
        //settingBtn.onClick.AddListener(um.OnSettingPopup);
        //ExitBtn.onClick.AddListener(um.OnExitButton);
        //shopBtn.onClick.AddListener(um.OnShopButton);
        //TutoBtn.onClick.AddListener(um.OnTutoPopup);
        RunBtn.onClick.AddListener(um.OnStartButton);
        //settingCloseBtn.onClick.AddListener(um.CloseSettingPopup);
        //shopCloseBtn.onClick.AddListener(um.CloseShoppopup);
        //TutoCloseBtn.onClick.AddListener(um.CloseTutoPopup);
        UpdateRamCounter();
        SetNickNamePanel.SetActive(!GameManager.Instance.isNickNameSetted);
    }

    public void UpdateRamCounter()
    {
        //ramCounter.text = $"Ram : {GameManager.Instance.RamCount}MB";
    }

    private void Update()
    {
        //dateCounter.text = DateTime.Now.ToString("hh:mm");
        
        //if(Input.GetKeyDown(KeyCode.F9))
        //{ 
        //    GameManager.Instance.RamCount += 100;
        //    UpdateRamCounter();
        //}
        //if(Input.GetKeyDown(KeyCode.F2))
        //{
        //    Debug.Log("Change MoveMode");
        //    GameManager.Instance.ToggleMoveOption();
        //}

        if(Input.GetKeyDown(KeyCode.Escape)) 
        { 
            foreach(var tab in tabs) 
            {
                if (tab.activeSelf == true)
                {
                    return;
                }
            }
            Application.Quit();
        }
    }
}
