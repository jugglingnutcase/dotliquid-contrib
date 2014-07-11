namespace DotLiquid.Contrib.Mustache
{
    using DotLiquid;
    using DotLiquid.FileSystems;
    using N = Nustache.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Renders mustache from a template using the passed data context
    /// </summary>
    /// <example>
    /// Used like: <c>{% mustachetemplate template_location data_context %}</c>
    ///
    /// The markup after the tag should have two things:
    /// 1. A mustache template location
    /// 2. The model context defining which data should render the mustache template
    /// </example>
    public class MustacheTemplateTag : Tag
    {
        private string modelContext = "";
        private string templateName = "";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);

            // There should be two things:
            // 1. A mustache template location
            // 2. The model context saying which data should render the mustache template
            string[] splitMarkup = markup.Split(new char[] { ' ' });
            this.templateName = splitMarkup[0];
            this.modelContext = splitMarkup[1];
        }

        public override void Render(Context context, TextWriter result)
        {
            // Read the mustache template
            IFileSystem fileSystem = context.Registers["file_system"] as IFileSystem ?? Template.FileSystem;
            string mustacheTemplate = fileSystem.ReadTemplateFile(context, this.templateName);

            // Get the appropriate data to use to render the mustache template
            var model = context[this.modelContext];

            // Render the template or an error message
            string output;
            if (model != null)
            {
                output = N.Render.StringToString(mustacheTemplate, model);
            }
            else
            {
                output = "Liquid mustache template issue: Not sure which data to render";
            }

            result.Write(output);
        }
    }
}
