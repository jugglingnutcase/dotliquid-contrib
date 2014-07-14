using DotLiquid.Contrib.Mustache;
using DotLiquid.FileSystems;
using System.Collections.Generic;
using Xunit;

namespace DotLiquid.Contrib.MustacheBlock.Tests
{
    public class IncludeRawTagTests
    {
        [Fact]
        public void IncludeRaw_tag_returns_only_the_template()
        {
            Template.FileSystem = new TestFileSystem("TEMPLATE", "THE TEMPLATE");
            Template.RegisterTag<IncludeRawTag>("includeraw");
            Template template = Template.Parse("{% includeraw TEMPLATE %}");

            var result = template.Render();

            Assert.Contains("THE TEMPLATE", result);
        }
    }

    public class TestFileSystem : IFileSystem
    {
        string templateName;
        string template;
        public TestFileSystem(string templateName, string template)
        {
            this.templateName = templateName;
            this.template = template;
        }

        public string ReadTemplateFile(Context context, string templateName)
        {
            if (this.templateName == templateName)
            {
                return this.template;
            }
            return "FAIL!";
        }
    }
}
