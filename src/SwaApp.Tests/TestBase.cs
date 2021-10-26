using System;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace SwaApp.Tests
{
    // SOURCE: https://bunit.dev/
    public abstract class TestBase<T> : IDisposable where T : IComponent
    {
        private readonly Lazy<IRenderedComponent<T>> LazyComponent;

        protected TestBase()
        {
            Context = new TestContext();
            LazyComponent = new Lazy<IRenderedComponent<T>>(() => Context.RenderComponent<T>());
        }

        protected TestContext Context { get; }
        protected IRenderedComponent<T> Component => LazyComponent.Value;

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}