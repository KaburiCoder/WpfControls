using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfControls.Utilites
{
  public class AnimationManager
  {
    private readonly FrameworkElement _targetElement;
    private readonly Lazy<Storyboard> _storyboardLazy = new(() => new Storyboard());
    private readonly Lazy<DoubleAnimation> _doubleAnimationLazy = new(() => new DoubleAnimation());

    public Storyboard Storyboard => _storyboardLazy.Value;
    public DoubleAnimation DoubleAnimation => _doubleAnimationLazy.Value;

    public AnimationManager(FrameworkElement targetElement)
    {
      _targetElement = targetElement;
    }

    public void SetLeftProperty(double from, double to, double milliseconds)
    {
      Storyboard.Children.Clear();

      DoubleAnimation.From = from;
      DoubleAnimation.To = to;
      DoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(milliseconds));

      Storyboard.Children.Add(DoubleAnimation);

      Storyboard.SetTarget(DoubleAnimation, _targetElement);
      Storyboard.SetTargetProperty(DoubleAnimation, new PropertyPath(Canvas.LeftProperty));
    }

    public void Begin()
    {
      Storyboard.Begin();
    }
  }
}
