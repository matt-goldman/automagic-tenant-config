using Xunit;

namespace MedMan.Application.UnitTests
{
    [CollectionDefinition(nameof(QueryCollection))]
    public class QueryCollection
        : ICollectionFixture<TestFixture>
    { }
}