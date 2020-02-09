using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionLineMultiples : RandomDirectionLines
{
    override protected Vector2 _GetVariation(){
        return new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }
}
