using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Utils
{
    public static class LayerUtils
    {
        public static bool IsLayerInMask(this LayerMask mask, int layer)
        {
            return (mask & (1 << layer)) != 0;
        }
    }
}