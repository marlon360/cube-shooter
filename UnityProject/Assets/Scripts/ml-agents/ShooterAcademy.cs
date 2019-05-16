using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class ShooterAcademy : Academy {
    public override void InitializeAcademy () {
        Physics.defaultSolverIterations = 12;
        Physics.defaultSolverVelocityIterations = 12;
        Time.fixedDeltaTime = 0.01333f; // (75fps). default is .2 (60fps)
        Time.maximumDeltaTime = .15f; // Default is .33
    }
}