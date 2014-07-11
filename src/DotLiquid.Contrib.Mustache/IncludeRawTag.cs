namespace DotLiquid.Contrib.Mustache
{
    using DotLiquid;
    using DotLiquid.FileSystems;
    using N = Nustache.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Includes a template, but does not Render it through liquid.
    /// </summary>
    public class IncludeRawTag : Tag
    {
        private string templateName = "";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);
            this.templateName = markup.Trim();
        }

        public override void Render(Context context, TextWriter result)
        {
            // Read the mustache template
            IFileSystem fileSystem = context.Registers["file_system"] as IFileSystem ?? Template.FileSystem;
            result.Write(fileSystem.ReadTemplateFile(context, this.templateName));
        }
    }
}
