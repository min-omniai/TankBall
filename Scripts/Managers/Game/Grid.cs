using UnityEngine;

public class Grid : MonoBehaviour
{
    private Define.ColorType _colorType;
    private SpriteRenderer _spriteRenderer = null;

    public void Init(Define.ColorType colorType)
    {
        _colorType = colorType;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetGridSprite();
    }

    private void SetGridSprite()
    {
        switch (_colorType)
        {
            case Define.ColorType.Red:
                _spriteRenderer.sprite = Managers.Resource.Load<Sprite>("RedBlock");
                break;

            case Define.ColorType.Blue:
                _spriteRenderer.sprite = Managers.Resource.Load<Sprite>("BlueBlock");
                break;
        }
    }

    public bool CheckOccupation(Define.ColorType colorType)
    {
        if (_colorType == colorType)
            return false;
        else
        {
            _colorType = colorType;
            SetGridSprite();
            return true;
        }
    }
}
