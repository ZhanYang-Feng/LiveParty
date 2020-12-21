using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
* 模块名: ProcessData.cs
* 功能描述：过程中所需的struct等声明
***********************************************/

namespace ProcessData
{
    /// <summary>
    /// 地图的状态
    /// </summary>
    public enum MapState
    {
        NotHasBeen,               //没去过
        HasBeenButNotOn,          //去过但现在不在
        OnTheMap                  //现在玩家在
    }

    /// <summary>
    /// 角色的六维属性
    /// </summary>
    public enum Property
    {
        Strength,           //力量 Strength、量度肌肉力量
        Dexterity,          //敏捷 Dexterity、量度靈敏性
        Constitution,       //體格 Constitution、量度持久力
        Intelligence,       //智力 Intelligence、量度推理和記憶
        Wisdom,             //感知 Wisdom、量度觀察和洞察力
        Charisma            //魅力 Charisma、量度個人風采
    }



    
}
