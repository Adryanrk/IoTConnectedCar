using System;

namespace CarSimulator
{
    public class Car
    {
        private const int IdleRpm = 800;
        private const int MaxRpm = 6000;
        private const int MinSpeed = 0;
        private const int MaxSpeed = 120;

        private readonly Random _random = new Random();

        private double _currentSpeed = 0;
        private double _currentRpm = IdleRpm;
        private double _currentFuelLevel = 100.0;
        private double _currentEngineTemperature = 80.0;

        private void UpdateSpeed()
        {
            int speedChange = _random.Next(-5, 11);

            _currentSpeed += speedChange;

            _currentSpeed = Math.Clamp(
                _currentSpeed,
                MinSpeed,
                MaxSpeed
            );
        }
    }
}