using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using SwaApp.Pages;
using Xunit;

namespace SwaApp.Tests
{
    public class CounterTest : TestBase<Counter>
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            Context.AddTestAuthorization().SetAuthorized("User");

            // Act
            Component.ClickIncrement();

            // Assert
            Component.Paragraph().MarkupMatches("<p>Current count: 1</p>");
        }
    }

    public static class CounterComponentExtensions
    {
        public static void ClickIncrement(this IRenderedComponent<Counter> component)
        {
            component.Find("button").Click();
        }

        public static IElement Paragraph(this IRenderedComponent<Counter> component)
        {
            return component.Find("p");
        }
    }
}
