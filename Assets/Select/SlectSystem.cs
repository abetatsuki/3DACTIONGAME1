using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlectSystem : MonoBehaviour
{
 
    [SerializeField,StringLengthLimit(10)] private string[] _strings;
    [SerializeField] private float _fontSize = 36f;
    [SerializeField] private float _lineSpacing = 80f;
    [SerializeField] private RectTransform _viewArea; // ← Mask付きの表示範囲
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.yellow;

    private RectTransform _thisRectTransform;
    private int _currentIndex = 0;
    private TextMeshProUGUI[] _textList;

    private void Start()
    {
        _thisRectTransform = GetComponent<RectTransform>();
        GenerateTexts();
        UpdateHighlight();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentIndex = Mathf.Max(0, _currentIndex - 1);
            UpdateHighlight();
            ScrollToCurrent();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentIndex = Mathf.Min(_strings.Length - 1, _currentIndex + 1);
            UpdateHighlight();
            ScrollToCurrent();
        }
    }

    private void GenerateTexts()
    {
        _textList = new TextMeshProUGUI[_strings.Length];

        for (int i = 0; i < _strings.Length; i++)
        {
            GameObject textObject = new GameObject($"Text_{i}", typeof(TextMeshProUGUI));
            textObject.transform.SetParent(_thisRectTransform, false);

            TextMeshProUGUI tmp = textObject.GetComponent<TextMeshProUGUI>();
            tmp.text = _strings[i];
            tmp.fontSize = _fontSize;
            tmp.enableAutoSizing = true; //文字が入らない場合にサイズを変えてはいるようにしてくれる
            tmp.color = _normalColor;
            tmp.alignment = TextAlignmentOptions.Center; // ← 中央揃え！

            RectTransform rect = tmp.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1);  // 中央上に固定 原点設定
            rect.anchorMax = new Vector2(0.5f, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.anchoredPosition = new Vector2(0, -i * _lineSpacing); // ← 上から下へ
            _textList[i] = tmp;
        }
    }

    private void UpdateHighlight()
    {
        for (int i = 0; i < _textList.Length; i++)
        {
            _textList[i].color = (i == _currentIndex) ? _selectedColor : _normalColor;
        }
    }

    private void ScrollToCurrent()
    {
        if (_viewArea == null) return;

        float targetY = _currentIndex * _lineSpacing;
        Vector2 pos = _thisRectTransform.anchoredPosition;
        pos.y = Mathf.Clamp(targetY - _lineSpacing, 0, (_strings.Length - 1) * _lineSpacing);
        _thisRectTransform.anchoredPosition = pos;
    }
}
