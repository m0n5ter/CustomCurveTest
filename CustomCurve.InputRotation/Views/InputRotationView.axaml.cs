using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using CustomCurve.InputRotation.ViewModels;

namespace CustomCurve.InputRotation.Views;

public partial class InputRotationView : ReactiveUserControl<InputRotationViewModel>
{
    private bool _entered;

    public InputRotationView()
    {
        InitializeComponent();
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (sender is not Visual visual || !e.KeyModifiers.HasFlag(KeyModifiers.Shift)) return;
        DetectedLine.StartPoint = e.GetPosition(visual);
        _entered = true;
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_entered || sender is not Visual visual || !e.KeyModifiers.HasFlag(KeyModifiers.Shift)) return;
        DetectedLine.EndPoint = e.GetPosition(visual);
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (!_entered || !e.KeyModifiers.HasFlag(KeyModifiers.Shift) || ViewModel is not { } vm) return;
        OnPointerMoved(sender, e);
        vm.DetectedAngle = 180 * Math.Atan2(DetectedLine.EndPoint.Y - DetectedLine.StartPoint.Y, DetectedLine.EndPoint.X - DetectedLine.StartPoint.X) / Math.PI;
        _entered = false;
    }

    private void OnDetectedAngleClick(object? sender, PointerPressedEventArgs e)
    {
        if (ViewModel is not { } vm) return;
        vm.Angle = vm.DetectedAngle;
    }
}