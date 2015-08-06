using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Fenrisulfr.Exceptions;

namespace Fenrisulfr.FnirsControllerLogic
{
    public class FakeFnirs : IFnirsController
    {
        private ConcurrentQueue<SensorResult> _results = new ConcurrentQueue<SensorResult>();
        private Task _readerThread;
        private Stopwatch _stopwatch = new Stopwatch();
        private bool _stopping = false;
        private FnirsControllerState _state = FnirsControllerState.Stopped;

        public void Reset()
        {
            
        }

        public FnirsControllerState GetState()
        {
            return _state;
        }

        public void Start()
        {
            _state = FnirsControllerState.Running;
            _results = new ConcurrentQueue<SensorResult>();

            if (_readerThread == null)
            {
                _readerThread = Task.Run(() =>
                {
                    _stopwatch.Start();

                    while (!_stopping)
                    {
                        try
                        {
                            DoWork();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception on reader thread: " + ex.Message);
                        }
                    }
                });
            }
            else
            {
                Console.WriteLine("Already started!");
            }
        }

        private void DoWork()
        {
            Thread.Sleep(100);
            _results.Enqueue(new SensorResult()
            {
                Milliseconds = (int)_stopwatch.ElapsedMilliseconds, 
                Read770 = (Math.Sin(_stopwatch.ElapsedMilliseconds)+1) * 5000,
                Read940 = (-Math.Sin(_stopwatch.ElapsedMilliseconds)+1) * 5000
            });
        }

        public void Stop()
        {
            _stopping = true;
            if (_readerThread != null) _readerThread.Wait();
            _stopwatch.Stop();

            _stopping = false;
            _readerThread = null;

            _state = FnirsControllerState.Stopped;
        }

        public int ResultsInQueue
        {
            get { return _results.Count; }
        }

        public SensorResult GetNextResult()
        {
            SensorResult result;

            // Try 200 times, because why not?
            // If it fails 200 times in a row, tough luck.
            for (int i = 0; i < 200; i++)
            {
                if (_results.TryDequeue(out result))
                    return result;

                Thread.Sleep(1);
            }

            throw new ComputerSaysNoException("This is ridiculous, if you aren't going to give me a value then I am outta here. Screw you.");
        }

        public void SetSensorLEDState(ushort address, LEDState state)
        {
            throw new NotImplementedException();
        }
    }
}
