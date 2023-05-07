using UnityEngine;

public class Stage : MonoBehaviour
{
    private int _redGridCount = 0;
    public int RedGridCount => _redGridCount;
    private int _blueGridCount = 0;
    public int BlueGridCount => _blueGridCount;

    public bool RedTeamWin => _blueGridCount <= 0;
    public bool BlueTeamWin => _redGridCount <= 0;

    public void Init()
    {
        _redGridCount = 0;
        _blueGridCount = 0;

        Grid[] grid = transform.GetComponentsInChildren<Grid>();
        foreach (Grid g in grid)
        {
            if (g.GetComponent<SpriteRenderer>().sprite.name == "RedBlock")
            {
                _redGridCount += 1;
                g.Init(Define.ColorType.Red);
            }
            else if (g.GetComponent<SpriteRenderer>().sprite.name == "BlueBlock")
            {
                _blueGridCount += 1;
                g.Init(Define.ColorType.Blue);
            }
        }

        Block[] block = transform.GetComponentsInChildren<Block>();
        foreach (Block b in block)
        {
            b.Init();
        }

        Cannon[] cannon = transform.GetComponentsInChildren<Cannon>();
        foreach (Cannon c in cannon)
        {
            c.Init();
        }
    }

    public void SetGridCount(Define.ColorType colorType)
    {
        switch (colorType)
        {
            case Define.ColorType.Red:
                _redGridCount += 1;
                _blueGridCount -= 1;

                if (RedTeamWin)
                    Managers.Resource.Instantiate("UI_PopupVictory", Managers.Game.GameSceneUI.transform);
                break;

            case Define.ColorType.Blue:
                _redGridCount -= 1;
                _blueGridCount += 1;

                if (BlueTeamWin)
                    Managers.Resource.Instantiate("UI_PopupDefeat", Managers.Game.GameSceneUI.transform);
                break;
        }
    }
}
