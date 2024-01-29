using UnityEngine;
using UnityEngine.UI;

public class GarbageView : MonoBehaviour
{
    [HideInInspector] public GarbageEnum GarbageEnum;
    
    [SerializeField] private Image _iconImage;

    [SerializeField] private Sprite _beerIcon;
    [SerializeField] private Sprite _whiskeyIcon;
    [SerializeField] private Sprite _pastaIcon;
    
    private void Start()
    {
        GarbageEnum = GarbageEnum.None;
    }

    private void Update()
    {
        if (GarbageEnum == GarbageEnum.Beer)
        {
            _iconImage.sprite = _beerIcon;
        }
        else if (GarbageEnum == GarbageEnum.Whiskey)
        {
            _iconImage.sprite = _whiskeyIcon;
        
        }
        else if (GarbageEnum == GarbageEnum.Pasta)
        {
            _iconImage.sprite = _pastaIcon;
        }
    }
}
