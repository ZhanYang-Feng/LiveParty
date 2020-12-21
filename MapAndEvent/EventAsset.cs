using ProcessData;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Timeline;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/**********************************************
* 模块名: EventAsset.cs
* 功能描述：与判定有关的Event的参数有关的配置文件类
***********************************************/

[CreateAssetMenu(menuName = "EventAsset", fileName = "eventAsset", order = 1)]
public class EventAsset : SerializedScriptableObject
{
    public string eventName;

    [Header("事件类型 （例如 主线、支线1等）")]
    public string eventType;

    [Header("在事件类型中此事件的顺序")]
    public int orderInType;

    [Header("事件内容描述")]
    public string eventIntroduction;

    [FoldoutGroup("事件判定")]
    [Header("几个骰子")]
    public int numberOfDice;

    [FoldoutGroup("事件判定")]
    [Header("多少面的骰子")]
    public int numberOfFace;

    [FoldoutGroup("事件判定")]
    [Header("判定所需阈值")]
    [ShowInInspector]
    [DictionaryDrawerSettings(KeyLabel = "属性", ValueLabel = "阈值")]
    public Dictionary<Property, float> judgementThresholds = new Dictionary<Property, float>();


    [FoldoutGroup("判定成功")]
    [Header("属性点增加")]
    public Dictionary<Property, float> addStat = new Dictionary<Property, float>();        

    [FoldoutGroup("判定成功")]
    [Header("事件判定成功后播放的Timeline动画")]
    [XmlIgnore]
    public TimelineAsset winTimelineAsset;      


    [FoldoutGroup("判定失败")]
    [Header("属性点减少")]
    public Dictionary<Property, float> minusStat = new Dictionary<Property, float>();       

    [FoldoutGroup("判定失败")]
    [Header("事件判定失败后播放的Timeline动画")]
    [XmlIgnore]
    public TimelineAsset loseTimelineAsset;

    
}




