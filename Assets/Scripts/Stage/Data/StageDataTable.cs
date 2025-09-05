using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Stages", menuName = "Stage Data/Create New Table")]
public class StageDataTable : ScriptableObject
{
    public List<StageData> stages;
}
