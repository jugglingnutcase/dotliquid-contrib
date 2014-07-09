using DotLiquid;
using DotLiquid.Contrib.Mustache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotLiquid.Contrib.Mustache.Tests
{
    public class MustacheBlockTests
    {
        [Fact]
        public void Mustache_block_renders_template()
        {
            // Given
            Template.RegisterTag<MustacheBlock>("mustache");
            Template template = Template.Parse("{% mustache data %}Hi {{ name }}!{% endmustache %}");

            Hash data = new Hash();
            data.Add(new KeyValuePair<string, object>("data", new { name = "Mustache" }));

            // When
            var result = template.Render(data);

            // Then
            Assert.Equal("Hi Mustache!".Trim(), result.Trim());
        }

        [Fact]
        public void Mustache_block_renders_template_without_automatic_raw_insertion()
        {
            // Given
            Template.RegisterTag<MustacheBlock>("mustache");
            Template template = Template.Parse("{% mustache data no_raw %}{% raw %}Hi {{ name }}!{% endraw %}{% endmustache %}");

            Hash data = new Hash();
            data.Add(new KeyValuePair<string, object>("data", new { name = "Mustache" }));

            // When
            var result = template.Render(data);

            // Then
            Assert.Equal("Hi Mustache!".Trim(), result.Trim());
        }
    }
}
