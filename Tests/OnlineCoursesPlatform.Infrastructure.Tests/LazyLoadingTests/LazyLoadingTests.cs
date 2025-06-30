using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Tests.LazyLoadingTests
{
    public class LazyLoadingTagsTests : IClassFixture<YourTestFixture>
    {
        private readonly AppDbContext _context;

        public LazyLoadingTagsTests(YourTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task LazyLoading_Tags_ShouldLoadTagsForCategory()
        {
            // Arrange
            var category = await _context.Categories.FirstOrDefaultAsync();
            Assert.NotNull(category);

            // Act
            var tagsCount = category.Tags.Count;

            // Assert
            Assert.True(tagsCount > 0, "Lazy loading failed: no tags loaded for category.");
        }
    }
}
