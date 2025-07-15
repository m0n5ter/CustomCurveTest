using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using CustomCurve.InputRotation.ViewModels;

namespace CustomCurve.InputRotation.Views;

public partial class InputRotationView : ReactiveUserControl<InputRotationViewModel>
{
    public InputRotationView()
    {
        InitializeComponent();
        DetectedLine.StartPoint = new Point(double.NaN, double.NaN);
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (sender is not Visual visual) return;
        DetectedLine.StartPoint = e.GetPosition(visual);
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (ViewModel is not { } vm || sender is not Visual visual) return;
        
        DetectedLine.EndPoint = e.GetPosition(visual);
        vm.DetectedAngle = 180 * Math.Atan2(DetectedLine.EndPoint.Y - DetectedLine.StartPoint.Y, DetectedLine.EndPoint.X - DetectedLine.StartPoint.X) / Math.PI;
    }
}