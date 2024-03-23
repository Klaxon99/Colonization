using UnityEngine;
using TMPro;

public class ResourceCountView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ResourceCounter _resourceCounter;

    private void Awake()
    {
        _text.text = _resourceCounter.Count.ToString();
    }

    private void OnEnable()
    {
        _resourceCounter.ResourceCountChange += ChangeText;
    }

    private void OnDisable()
    {
        _resourceCounter.ResourceCountChange -= ChangeText;
    }

    private void ChangeText(int count)
    {
        _text.text = count.ToString();
    }
}