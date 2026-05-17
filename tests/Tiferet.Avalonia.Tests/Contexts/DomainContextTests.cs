using System.ComponentModel;
using Tiferet.Avalonia.Contexts;
using Tiferet.Domain;

namespace Tiferet.Avalonia.Tests.Contexts;

// *** fixtures

// ** fixture: test domain model
public sealed record TestModel(string Name) : DomainObject;

// ** fixture: concrete domain view model for testing
public class TestDomainViewModel : DomainViewModel<TestModel>
{
    public TestDomainViewModel() : base(null) { }

    public void SetTestState(TestModel model) => State = model;

    public void CallExecuteFeature(string featureId, Dictionary<string, object?> data)
        => ExecuteFeature(featureId, data);

    public Task<object?> CallExecuteFeatureAsync(string featureId, Dictionary<string, object?> data)
        => ExecuteFeatureAsync(featureId, data);
}

// ** fixture: concrete domain view context for testing
public class TestDomainViewContext : DomainViewContext<TestModel>
{
    public TestDomainViewContext() : base(null) { }

    public void SetTestState(TestModel model) => State = model;

    public void CallExecuteFeature(string featureId, Dictionary<string, object?> data)
        => ExecuteFeature(featureId, data);

    public Task<object?> CallExecuteFeatureAsync(string featureId, Dictionary<string, object?> data)
        => ExecuteFeatureAsync(featureId, data);
}

// *** tests

public class DomainViewModelTests
{
    // ** test: state_initially_null
    [Fact]
    public void State_InitiallyNull()
    {
        var vm = new TestDomainViewModel();

        Assert.Null(vm.State);
    }

    // ** test: state_raises_property_changed
    [Fact]
    public void State_RaisesPropertyChanged()
    {
        var vm = new TestDomainViewModel();
        var changed = new List<string>();
        vm.PropertyChanged += (_, e) => changed.Add(e.PropertyName!);

        vm.SetTestState(new TestModel("Test"));

        Assert.Contains("State", changed);
        Assert.Equal("Test", vm.State!.Name);
    }

    // ** test: execute_feature_throws_without_context
    [Fact]
    public void ExecuteFeature_ThrowsWithoutContext()
    {
        var vm = new TestDomainViewModel();

        var ex = Assert.Throws<InvalidOperationException>(
            () => vm.CallExecuteFeature("calc.add", new()));

        Assert.Contains("AppInterfaceContext", ex.Message);
    }

    // ** test: execute_feature_async_throws_without_context
    [Fact]
    public async Task ExecuteFeatureAsync_ThrowsWithoutContext()
    {
        var vm = new TestDomainViewModel();

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => vm.CallExecuteFeatureAsync("calc.add", new()));

        Assert.Contains("AppInterfaceContext", ex.Message);
    }
}

public class DomainViewContextTests
{
    // ** test: state_initially_null
    [Fact]
    public void State_InitiallyNull()
    {
        var ctx = new TestDomainViewContext();

        Assert.Null(ctx.State);
    }

    // ** test: state_raises_property_changed
    [Fact]
    public void State_RaisesPropertyChanged()
    {
        var ctx = new TestDomainViewContext();
        var changed = new List<string>();
        ctx.PropertyChanged += (_, e) => changed.Add(e.PropertyName!);

        ctx.SetTestState(new TestModel("Hello"));

        Assert.Contains("State", changed);
        Assert.Equal("Hello", ctx.State!.Name);
    }

    // ** test: execute_feature_throws_without_context
    [Fact]
    public void ExecuteFeature_ThrowsWithoutContext()
    {
        var ctx = new TestDomainViewContext();

        var ex = Assert.Throws<InvalidOperationException>(
            () => ctx.CallExecuteFeature("calc.add", new()));

        Assert.Contains("AppInterfaceContext", ex.Message);
    }

    // ** test: execute_feature_async_throws_without_context
    [Fact]
    public async Task ExecuteFeatureAsync_ThrowsWithoutContext()
    {
        var ctx = new TestDomainViewContext();

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => ctx.CallExecuteFeatureAsync("calc.add", new()));

        Assert.Contains("AppInterfaceContext", ex.Message);
    }
}
