namespace Fenrisulfr.FnirsControllerLogic
{
    public interface IFnirsController
    {
        void Reset();
        FnirsControllerState GetState();
        void Start();
        void Stop();
        int ResultsInQueue { get; }

        /// <summary>
        /// This special method throws an exception if you call it without starting the controller. That is its main purpose.
        /// It might also return you sensor values if you ask nicely.
        /// </summary>
        /// <returns></returns>
        SensorResult GetNextResult();

        void SetSensorLEDState(ushort address, LEDState state);
    }
}