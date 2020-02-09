using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionLineSingles : RandomDirectionLines
{
    override protected Vector2 _GetVariation()
    {
        if (Random.Range(0, 2) == 0)
            return new Vector2(Random.Range(-1.0f, 1.0f), changePos.y);
        else
            return new Vector2(changePos.x, Random.Range(-1.0f, 1.0f));
    }
}
