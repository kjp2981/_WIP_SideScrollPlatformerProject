using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> feedbackList;

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach(Feedback f in feedbackList)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach (Feedback f in feedbackList)
        {
            f.CompletePrevFeedback();
        }
    }
}
