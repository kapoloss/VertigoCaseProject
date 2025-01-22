namespace VertigoCaseProject.Helpers
{
    /// <summary>
    /// Calculates the angle for spinning the wheel to a specific slice.
    /// </summary>
    public class WheelSpinCalculator
    {
        /// <summary>
        /// Calculates the total spin angle required to land on a specific slice.
        /// </summary>
        /// <param name="sliceIndex">The index of the target slice.</param>
        /// <param name="totalSlices">The total number of slices on the wheel.</param>
        /// <param name="defaultSpinRound">The default number of full rotations before stopping.</param>
        /// <param name="currentZRotation">The current Z-axis rotation of the wheel.</param>
        /// <returns>The total angle the wheel needs to spin.</returns>
        public float CalculateSpinAngle(int sliceIndex, int totalSlices, int defaultSpinRound, float currentZRotation)
        {
            float degreeForPrize = 360f / totalSlices;
            float turnDegree = (360 * defaultSpinRound) 
                               + (sliceIndex * degreeForPrize) 
                               - currentZRotation;

            return turnDegree;
        }
    }
}