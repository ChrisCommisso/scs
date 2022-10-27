using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.scripts
{
    [Serializable]
    class GameState
    {
        float p1Health;
        float p2Health;
        Move p1Current;
        Move p2Current;
        Vector3 p1Position;
        Vector3 p2Position;
    }
}
