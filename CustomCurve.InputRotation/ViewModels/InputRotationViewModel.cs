using ReactiveUI;
using System;

namespace CustomCurve.InputRotation.ViewModels;

public class InputRotationViewModel: ViewModelBase
{
    private double _angle = 10;
    private double _detectedAngle;
    private bool _autoApply;

    public double Angle
    {
        get => _angle;
        set
        {
            _angle = Math.Clamp(value, -180, 180);
            this.RaisePropertyChanged();
        }
    }

    public double DetectedAngle
    {
        get => _detectedAngle;
        set
        {
            if (Math.Abs(value) > 90) value -= Math.Sign(value) * 180;
            if (AutoApply) Angle = value;
            this.RaiseAndSetIfChanged(ref _detectedAngle, value);
        }
    }

    public bool AutoApply
    {
        get => _autoApply;
        set => this.RaiseAndSetIfChanged(ref _autoApply, value);
    }
}