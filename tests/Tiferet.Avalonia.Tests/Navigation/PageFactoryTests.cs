using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Tiferet.Avalonia.Navigation;

namespace Tiferet.Avalonia.Tests.Navigation;

// *** tests

public class PageFactoryTests
{
    // ** test: create_generic_resolves_from_di
    [Fact]
    public void Create_Generic_ResolvesFromDI()
    {
        var services = new ServiceCollection();
        services.AddTransient<StubPage>();
        var sp = services.BuildServiceProvider();
        var factory = new PageFactory(sp);

        var page = factory.Create<StubPage>();

        Assert.NotNull(page);
        Assert.IsType<StubPage>(page);
    }

    // ** test: create_by_type_resolves_from_di
    [Fact]
    public void Create_ByType_ResolvesFromDI()
    {
        var services = new ServiceCollection();
        services.AddTransient<StubPage>();
        var sp = services.BuildServiceProvider();
        var factory = new PageFactory(sp);

        var page = factory.Create(typeof(StubPage));

        Assert.NotNull(page);
        Assert.IsType<StubPage>(page);
    }

    // ** test: create_by_type_throws_for_non_inpc
    [Fact]
    public void Create_ByType_ThrowsForNonINPC()
    {
        var services = new ServiceCollection();
        services.AddTransient<NonInpcService>();
        var sp = services.BuildServiceProvider();
        var factory = new PageFactory(sp);

        Assert.Throws<InvalidOperationException>(() => factory.Create(typeof(NonInpcService)));
    }

    // ** test: create_generic_throws_for_unregistered
    [Fact]
    public void Create_Generic_ThrowsForUnregistered()
    {
        var services = new ServiceCollection();
        var sp = services.BuildServiceProvider();
        var factory = new PageFactory(sp);

        Assert.Throws<InvalidOperationException>(() => factory.Create<StubPage>());
    }

    private class NonInpcService { }
}
