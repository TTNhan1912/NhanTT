using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private bool _isRawImage;


    private Image _img;
    private Material _imgMaterial;

    private RawImage _rawImg;
    [SerializeField] private float _x, _y;

    private void Awake()
    {
        if (_isRawImage)
        {
            _rawImg = this.GetComponent<RawImage>();
        }
        else
        {
            _img = this.GetComponent<Image>();
            _imgMaterial = Instantiate(_img.material);
        }
    }


    private void Update()
    {
        if(_isRawImage)
            _rawImg.uvRect = new Rect(_rawImg.uvRect.position + new Vector2(_x,_y) *Time.deltaTime, _rawImg.uvRect.size);
        else
        {
            _imgMaterial.mainTextureOffset += new Vector2(_x, _y) * Time.deltaTime;
            _img.material = _imgMaterial;
        }
    }
}