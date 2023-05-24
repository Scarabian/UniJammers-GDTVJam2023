using uj.GameManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tip", menuName = "Tips/Tip")]
public class PlayerTIPSO : ScriptableObject
{
    public string title;
    [TextArea(3, 10)]
    public string tip;
    public TriggerCondition triggerCondition;

    public bool IsTriggered()
    {
        return triggerCondition.CheckCondition();
    }
}

[System.Serializable]
public abstract class TriggerCondition
{
    public abstract bool CheckCondition();
}

[System.Serializable]
public class TimeElapsedCondition : TriggerCondition
{
    public float requiredTime;

    public override bool CheckCondition()
    {
        return Time.time >= requiredTime;
    }
}

/*[System.Serializable]
public class ScoreCondition : TriggerCondition
{
    public int requiredScore;

    public override bool CheckCondition()
    {
       // int currentScore = GameManager.Instance.GetCurrentScore();
        // return currentScore >= requiredScore;
    }
}*/
