using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static 
        Vector3 GetPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);

        Vector3 ab_bc = Vector3.Lerp(ab, bc, t);
        Vector3 bc_cd = Vector3.Lerp(bc, cd, t);

        Vector3 abBc_bcCd = Vector3.Lerp(ab_bc, bc_cd, t);
        return abBc_bcCd;
    }
}
