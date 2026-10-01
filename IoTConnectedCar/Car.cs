using System;
using CarSimulator.Models;

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

        private void UpdateRpm()
        {
            if (_currentSpeed == 0)
            {
                _currentRpm = IdleRpm;
                return;
            }

            double targetRpm = IdleRpm + (_currentSpeed / MaxSpeed) * (MaxRpm - IdleRpm);

            int variation = _random.Next(-200, 201);

            _currentRpm = targetRpm + variation;

            _currentRpm = Math.Clamp(
                _currentRpm,
                IdleRpm,
                MaxRpm
            );
        }

        private void UpdateFuelLevel()
        {
            double fuelConsumption;

            if (_currentSpeed == 0)
            {
                fuelConsumption = 0.01;
            }
            else
            {
                fuelConsumption = 0.02 + (_currentRpm / MaxRpm) * 0.05;
            }

            _currentFuelLevel -= fuelConsumption;

            _currentFuelLevel = Math.Clamp(
                _currentFuelLevel,
                0,
                100
            );
        }

        private void UpdateEngineTemperature()
        {
            double targetTemperature;

            if (_currentSpeed == 0)
            {
                targetTemperature = 80.0;
            }
            else
            {
                targetTemperature = 80.0 + (_currentRpm / MaxRpm) * 20.0;
            }

            double temperatureChange =
                (targetTemperature - _currentEngineTemperature) * 0.1;

            _currentEngineTemperature += temperatureChange;

            _currentEngineTemperature = Math.Clamp(
                _currentEngineTemperature,
                80.0,
                100.0
            );
        }

        public TelemetryData GenerateTelemetry()
        {
            UpdateSpeed();
            UpdateRpm();
            UpdateFuelLevel();
            UpdateEngineTemperature();

            return new TelemetryData
            {
                Speed = _currentSpeed,
                Rpm = _currentRpm,
                FuelLevel = _currentFuelLevel,
                EngineTemperature = _currentEngineTemperature
            };
        }
    }
}