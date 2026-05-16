using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Tiferet.Avalonia.Contexts;
using Tiferet.Contexts;
using Tiferet.Events;

namespace Tiferet.Avalonia.Examples.Calculator.Contexts;

// *** contexts

// ** context: calculator_view_context
/// <summary>
/// View context for the Avalonia calculator. Bridges Tiferet domain events
/// (calc.add, calc.subtract, etc.) to bindable properties and commands.
/// </summary>
public class CalculatorViewContext : ViewContext
{
    // * attribute: app_context
    private readonly AppInterfaceContext _app;

    // * attribute: backing fields
    private string _display = "0";
    private string? _currentOperator;
    private string? _operandA;
    private bool _awaitingOperandB;
    private string? _errorMessage;

    // *** properties

    // ** property: display
    public string Display
    {
        get => _display;
        private set => SetProperty(ref _display, value);
    }

    // ** property: error_message
    public string? ErrorMessage
    {
        get => _errorMessage;
        private set => SetProperty(ref _errorMessage, value);
    }

    // ** property: history
    public ObservableCollection<string> History { get; } = new();

    // ** property: commands
    public ICommand DigitCommand { get; }
    public ICommand OperatorCommand { get; }
    public ICommand EqualsCommand { get; }
    public ICommand ClearCommand { get; }

    // * init
    public CalculatorViewContext(AppInterfaceContext app)
    {
        _app = app;
        DigitCommand = new RelayCommand<string>(OnDigit);
        OperatorCommand = new RelayCommand<string>(OnOperator);
        EqualsCommand = new RelayCommand(OnEquals);
        ClearCommand = new RelayCommand(OnClear);
    }

    // *** methods

    // ** method: on_digit
    private void OnDigit(string? digit)
    {
        if (digit is null) return;
        ErrorMessage = null;

        if (_awaitingOperandB || Display == "0")
        {
            Display = digit;
            _awaitingOperandB = false;
        }
        else
        {
            Display += digit;
        }
    }

    // ** method: on_operator
    private void OnOperator(string? op)
    {
        if (op is null) return;
        ErrorMessage = null;

        // If there's a pending operation, evaluate it first.
        if (_operandA is not null && !_awaitingOperandB)
            OnEquals();

        _operandA = Display;
        _currentOperator = op;
        _awaitingOperandB = true;
    }

    // ** method: on_equals
    private void OnEquals()
    {
        if (_currentOperator is null || _operandA is null) return;
        ErrorMessage = null;

        // Map operator symbol to feature ID.
        var featureId = _currentOperator switch
        {
            "+" => "calc.add",
            "-" => "calc.subtract",
            "×" => "calc.multiply",
            "÷" => "calc.divide",
            "^" => "calc.exp",
            "√" => "calc.sqrt",
            _ => null,
        };

        if (featureId is null) return;

        try
        {
            var data = new Dictionary<string, object?>
            {
                ["A"] = _operandA,
                ["B"] = _currentOperator == "√" ? "0.5" : Display,
            };

            var result = _app.Run(featureId, data: data);
            var resultStr = Convert.ToDouble(result).ToString(CultureInfo.InvariantCulture);

            // Add to history.
            var expression = _currentOperator == "√"
                ? $"√{_operandA} = {resultStr}"
                : $"{_operandA} {_currentOperator} {Display} = {resultStr}";
            History.Insert(0, expression);

            Display = resultStr;
            _operandA = resultStr;
            _currentOperator = null;
            _awaitingOperandB = false;
        }
        catch (TiferetApiException ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    // ** method: on_clear
    private void OnClear()
    {
        Display = "0";
        _operandA = null;
        _currentOperator = null;
        _awaitingOperandB = false;
        ErrorMessage = null;
    }
}

// *** helpers

// ** helper: relay_command
/// <summary>
/// Minimal ICommand implementation for view context commands.
/// </summary>
internal class RelayCommand : ICommand
{
    private readonly Action _execute;

    public RelayCommand(Action execute) => _execute = execute;

#pragma warning disable CS0067
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _execute();
}

// ** helper: relay_command_t
internal class RelayCommand<T> : ICommand
{
    private readonly Action<T?> _execute;

    public RelayCommand(Action<T?> execute) => _execute = execute;

#pragma warning disable CS0067
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _execute(parameter is T t ? t : default);
}
