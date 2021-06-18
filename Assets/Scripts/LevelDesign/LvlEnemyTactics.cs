using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LvlEnemyTactics
{
    public static StatsStruct slime = new StatsStruct(1, 10, 0, 0, 1, 1, 10, 0);
    public static StatsStruct turtle = new StatsStruct(1, 50, 2, 2, 1, 1, 0, 0);

    public static StatsStruct MultiplyKoef(StatsStruct _source, float _koef)
    {
        StatsStruct result = new StatsStruct(1, _source.Health * _koef, _source.Atack * _koef, _source.Speed * _koef, _source.AtackSpeed * _koef, 1, _koef*_source.MoneyMining, 0);
        result.Atack = _source.Atack * _koef;
        result.AtackSpeed = _source.AtackSpeed * _koef;
        result.Health = _source.Health * _koef;
        result.Speed = _source.Speed * _koef;
        return result;
    }
   
}
