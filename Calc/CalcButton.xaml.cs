using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calc
{
    /// <summary>
    /// Interaction logic for CalcButton.xaml
    /// </summary>
    public partial class CalcButton : UserControl
    {
        public CalcButton()
        {
            InitializeComponent();
            //this.DataContext = this;
            grid.DataContext = this;

            PreviewMouseLeftButtonDown += (s, e) => Command?.Execute(CommandParameter);
        }        
        new public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }                
        
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }                
        
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }                
        
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }                
    }

    // Statics
    public partial class CalcButton : UserControl
    {
        new public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(CalcButton), new PropertyMetadata(null));

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(CalcButton), new PropertyMetadata(new Thickness(10)));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CalcButton), new PropertyMetadata(null));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CalcButton), new PropertyMetadata(null));
    }
}
