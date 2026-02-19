namespace TemplateApi.Tests.Utility;

using TemplateApi.Utility;

public class AgainstNullOrWhiteSpaceTests
{
    [Fact]
    public void NullThrowsArgumentException() => Assert.Throws<ArgumentException>(() => Guard.AgainstNullOrWhiteSpace(null, "param"));

    [Fact]
    public void EmptyThrowsArgumentException() => Assert.Throws<ArgumentException>(() => Guard.AgainstNullOrWhiteSpace(string.Empty, "param"));

    [Fact]
    public void WhiteSpaceThrowsArgumentException() => Assert.Throws<ArgumentException>(() => Guard.AgainstNullOrWhiteSpace("   ", "param"));

    [Fact]
    public void ValidDoesNotThrow() => Guard.AgainstNullOrWhiteSpace("value", "param");
}
