using UnityEngine;

[CreateAssetMenu(fileName = "New AnimationArray", menuName = "AnimationArray/New AnimationArray")]
public class AnimationArray : ScriptableObject
{
    public string[] AnimationName;
    public AnimationType[] AnimationType;
    public string[] AnimationParameter;
    public string[] WhenToPlay;
}

public enum AnimationType
{
    Float,
    Int,
    Bool,
    Trigger
}
