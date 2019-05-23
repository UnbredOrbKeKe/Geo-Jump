using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsGraphWindow : MonoBehaviour
{
    // Components for the Graph Window
    [SerializeField] GameObject graphLinePrefab = null;
    [SerializeField] Text framePerSecondText = null;
    [SerializeField] RectTransform graphContainer = null;
    [SerializeField] float graphHeightMultiplier = 5f;
    [SerializeField] float graphLineDistance = 5f;

    // A queue for all the graph lines
    Queue<RectTransform> graphLines = new Queue<RectTransform>();

    const float fpsCheckRate = 0.25f;
    int currentFrameCount = 0;
    float timeSinceLastCheck = 0f;

    
    /// <summary>
    /// Update counts the frames since the last Check, and updates the frame per 
    /// second text every check rate.
    /// </summary>
    void Update()
    {
        // Calculate the frames per second
        currentFrameCount++;
        timeSinceLastCheck += Time.unscaledDeltaTime;
        if (timeSinceLastCheck > fpsCheckRate)
        {
            float currentFPS = currentFrameCount / timeSinceLastCheck;
            currentFrameCount = 0;
            timeSinceLastCheck = 0;

            // Update the window parts
            SetFpsText(currentFPS);
            AddLine(currentFPS * graphHeightMultiplier);
        }
    }


    /// <summary>
    /// Updates the FPS Text with the current frame per seconds.
    /// </summary>
    /// <param name="fps">The current FPS.</param>
    void SetFpsText(float fps)
    {
        framePerSecondText.text = ("FPS: " + fps);
    }


    /// <summary>
    /// Adds a new line to the end of the graph with a specified height.
    /// </summary>
    /// <param name="height">The height of the graph line.</param>
    void AddLine(float height)
    {
        bool isFull = IsGraphFull();
        RectTransform lineTransform = null;

        // If the graph is full, remove the left-most line and shift the rest.
        if (isFull)
        {
            lineTransform = graphLines.Dequeue();

            MoveGraph();
        }
        else // If the graph is not full, just add a new line.
        {
            lineTransform = CreateLine();
        }

        lineTransform.sizeDelta = new Vector2(lineTransform.sizeDelta.x, height);

        graphLines.Enqueue(lineTransform);
    }

    
    /// <summary>
    /// Creates a new GraphLine GameObject with the correct RectTransform.
    /// </summary>
    /// <returns>GameObject shaped like a GraphLine.</returns>
    RectTransform CreateLine()
    {
        GameObject lineObject = Instantiate(graphLinePrefab, graphContainer);
        RectTransform lineTransform = lineObject.GetComponent<RectTransform>();

        lineTransform.localPosition = new Vector3(graphLineDistance * graphLines.Count, 0);
        return lineTransform;
    }


    /// <summary>
    /// Shifts every line in the Graph as far to the left as possible.
    /// </summary>
    void MoveGraph()
    {
        float currentX = 0;

        foreach (RectTransform line in graphLines)
        {
            line.localPosition = new Vector3(currentX, 0);

            currentX += graphLineDistance;
        }
    }


    /// <summary>
    /// Checks whether the graph is full or not.
    /// </summary>
    /// <returns>Returns true if full, or false if there is still space left.</returns>
    bool IsGraphFull()
    {
        return (graphLineDistance * graphLines.Count >= graphContainer.sizeDelta.x);
    }
}
