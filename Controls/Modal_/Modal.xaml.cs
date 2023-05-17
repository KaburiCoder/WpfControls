using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfControls.Utilites;

namespace WpfControls.Controls
{
  /// <summary>
  /// Modal.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class Modal : UserControl
  {
    private Lazy<AnimationManager> _popupAnimationLazy;
    private Border? _bodyBorder;

    private AnimationManager PopupAnimation => _popupAnimationLazy.Value;

    public new static readonly DependencyProperty BackgroundProperty =
        DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(Modal), new PropertyMetadata(Brushes.White));
    public static readonly DependencyProperty ShadowColorProperty =
        DependencyProperty.Register("ShadowColor", typeof(SolidColorBrush), typeof(Modal), new PropertyMetadata(Brushes.Gray));
    public static readonly DependencyProperty IsOpenProperty =
    DependencyProperty.Register("IsOpen", typeof(bool), typeof(Modal), new PropertyMetadata(false, IsOpenChanged));

    private static void IsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is Modal modal)
      {
        bool isOpen = (bool)e.NewValue;
        if (isOpen)
        {
          modal.BringToFront();
          modal.PopupAnimation.SetMarginProperty(from: new Thickness(600), to: new Thickness(8), 400);
          modal.PopupAnimation.Begin();
          modal.Visibility = Visibility.Visible;
        }
        else
        {
          modal.Visibility = Visibility.Collapsed;
        }
      }
    }
    private void opacityGrid_MouseUp(object sender, MouseButtonEventArgs e)
    {
      IsOpen = false;
    }

    public Modal()
    {
      InitializeComponent();
      _popupAnimationLazy = new Lazy<AnimationManager>(() => new AnimationManager(_bodyBorder!));
      Visibility = Visibility.Hidden;
    }

    public new SolidColorBrush Background
    {
      get { return (SolidColorBrush)GetValue(BackgroundProperty); }
      set { SetValue(BackgroundProperty, value); }
    }

    public bool IsOpen
    {
      get { return (bool)GetValue(IsOpenProperty); }
      set { SetValue(IsOpenProperty, value); }
    }

    public SolidColorBrush ShadowColor
    {
      get { return (SolidColorBrush)GetValue(ShadowColorProperty); }
      set { SetValue(ShadowColorProperty, value); }
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _bodyBorder = (Border)GetTemplateChild("bodyBorder");
    }
  }
}
