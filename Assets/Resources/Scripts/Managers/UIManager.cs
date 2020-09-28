
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum menuActivate { main, charSelect, levelMap, play }
    public static UIManager Instance;


    [Tooltip("map, shop, ranking, legends, milestones, social, dailyQuests")] 
    [SerializeField] menuActivate _activeMenu;
    [SerializeField] AudioClip _music;

    [Header("Main Menu")]
    [SerializeField] GameObject[] _menuObjects;
    [Tooltip("char select, level select")]
    [SerializeField] Button[] _menuButtons;


    [Header("Character Selection")]
    [SerializeField] GameObject _charButtonParent;
    [SerializeField] GameObject _charDescriptionParent;
    [SerializeField] Button _selectButton;
    [SerializeField] Button[] _charButtons;
    [Tooltip("name, description, basic, special, passive")]
    [SerializeField] TextMeshProUGUI[] _charTexts;
    [Tooltip("basic, special, passive, select")]
    [SerializeField] Image[] _abilityIcons;

    private void Awake()
    {
        Instance = GetComponent<UIManager>();
        StartUpLists();
    }

    private void OnEnable()
    {
        AudioManager.Instance.PlayMusic(_music);
    }




    //====================================================== Menu Change
    public void ChangeMenu(int id)
    {
        foreach (GameObject g in _menuObjects)
            g.SetActive(false);

        _menuObjects[id].SetActive(true);
    }





    //====================================================== Character Selection
    public void ChangeCharacterPreview(int id)
    {
        SOCharacter _char = GameManager.Instance.characterDictionary.characters[id];

        _charTexts[0].text = _char.name;
        _charTexts[1].text = _char.descrition;
        _charTexts[2].text = _char.spellDescriptions[0];
        _charTexts[3].text = _char.spellDescriptions[1];
        _charTexts[4].text = _char.spellDescriptions[2];

        // change icons
        if (_char.icons.Length >= 3)
        {
            for (int i = 0; i < _abilityIcons.Length; i++)
                _abilityIcons[i].sprite = _char.icons[i];
        }

        // check if char is already selected
        _selectButton.onClick.RemoveAllListeners();

        if (id != GameManager.Instance.charId)
        {
            _charTexts[5].text = "Select";
            _selectButton.interactable = true;
            _selectButton.onClick.AddListener(() => GameManager.Instance.SetCharacterId(id));
        }

        else
        {
            _charTexts[5].text = "Selected";
            _selectButton.interactable = false;
        }
    }





    //====================================================== Start Up
    void StartUpLists()
    {
        // get character buttons
        int _charLength = _charButtonParent.transform.childCount;
        _charButtons = new Button[_charLength];

        for (int i = 0; i < _charLength; i++)
        {
            _charButtons[i] = _charButtonParent.transform.GetChild(i).GetComponent<Button>();
            //_charButtons[i].onClick.AddListener(() => ChangeCharacterPreview(i));
        }
    }
}
