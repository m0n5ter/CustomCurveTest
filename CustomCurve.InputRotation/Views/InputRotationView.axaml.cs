using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using CustomCurve.InputRotation.Utils;
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
        
        Point? startPoint = DetectedLine.StartPoint;
        Point? endPoint = e.GetPosition(visual);
        
        GeometryUtils.ClipOrExtendToRectangle(new Rect(DetectionPanel.Bounds.Size), ref startPoint, ref endPoint);

        if (startPoint == null || endPoint == null)
        {
            DetectedLine.IsVisible = false;
            return;
        }

        DetectedLine.IsVisible = true;
        DetectedLine.StartPoint = startPoint.Value;
        DetectedLine.EndPoint = endPoint.Value;
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (!_entered || !e.KeyModifiers.HasFlag(KeyModifiers.Shift) || ViewModel is not { } vm) return;
        
        OnPointerMoved(sender, e);
        
        if (!DetectedLine.IsVisible) return;

        vm.DetectedAngle = 180 * Math.Atan2(DetectedLine.EndPoint.Y - DetectedLine.StartPoint.Y, DetectedLine.EndPoint.X - DetectedLine.StartPoint.X) / Math.PI;
        _entered = false;
    }

    private void OnDetectedAngleClick(object? sender, PointerPressedEventArgs e)
    {
        if (ViewModel is not { } vm) return;
        vm.Angle = vm.DetectedAngle;
    }
}