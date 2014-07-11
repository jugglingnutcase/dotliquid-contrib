using DotLiquid;
using DotLiquid.FileSystems;
using DotLiquid.Contrib.Mustache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotLiquid.Contrib.Mustache.Tests
{
    public class MustacheTemplateTagTests
    {
        [Fact]
        public void Mustache_template_renders_included_mustache_template()
        {
            // Given
            string name = "Artemis";
            Template.FileSystem = new TestFileSystem("template", "Hi {{ name }}!");
            Template.RegisterTag<MustacheTemplateTag>("mustachetemplate");
            Template template = Template.Parse("{% mustachetemplate template data %}");
            Hash data = new Hash();
            data.Add(new KeyValuePair<string, object>("data", new { name = name }));

            // When
            var result = template.Render(data);

            // Then
            Assert.Equal(string.Format("Hi {0}!", name).Trim(), result.Trim());
        }

        [Fact]
        public void Mustache_template_renders_error_when_data_context_not_found()
        {
            // Given
            string name = "Artemis";
            Template.FileSystem = new TestFileSystem("template", "Hi {{ name }}");
            Template.RegisterTag<MustacheTemplateTag>("mustachetemplate");
            Template template = Template.Parse("{% mustachetemplate template ARROWS %}");
            Hash data = new Hash();
            data.Add(new KeyValuePair<string, object>("data", new { name = name }));

            // When
            var result = template.Render(data);

            // Then
            Assert.Contains("Liquid mustache template issue", result);
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
