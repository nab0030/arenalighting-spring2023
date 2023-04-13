using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionManager : MonoBehaviour
{
    [SerializeField] private InputField searchInputField;
    [SerializeField] private RectTransform dropdownContent;
    [SerializeField] private GameObject togglePrefab;

    [SerializeField] private GameObject Section1;
    [SerializeField] private GameObject Section2;


    private Dictionary<string, GameObject> sections;
    private Dictionary<string, Toggle> toggles;


    // Start is called before the first frame update
    void Start()
    {
        sections = new Dictionary<string, GameObject>();
        toggles = new Dictionary<string, Toggle>();

        AddSection("Section 1", Section1);
        AddSection("Section 2", Section2);

        searchInputField.onValueChanged.AddListener(OnSearchValueChanged);
    }

    private void AddSection(string sectionName, GameObject section)
    {
        sections.Add(sectionName, section);

        GameObject toggleObject = Instantiate(togglePrefab, dropdownContent);
        Toggle toggleComponent = toggleObject.GetComponent<Toggle>();
        toggleComponent.isOn = false;
        toggleComponent.name = sectionName;
        toggleComponent.GetComponentInChildren<Text>().text = sectionName;

        toggles.Add(sectionName, toggleComponent);
    }


    private void OnSearchValueChanged(string searchText)
    {
        foreach (var toggle in toggles)
        {
            toggle.Value.gameObject.SetActive(string.IsNullOrEmpty(searchText) || toggle.Key.ToLower().Contains(searchText.ToLower()));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
