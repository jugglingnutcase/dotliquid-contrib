namespace DotLiquid.Contrib.Mustache
{
    using DotLiquid;
    using N = Nustache.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Renders a mustache template enclosed by the block using the supplied parameter as a reference the data used in rendering the template.
    /// </summary>
    /// <remarks>
    /// Make sure to pass the key of the data object that the mustache template should render as markup. For example, if you want to send the template
    /// data from the <code>Person</code> object, call the mustache block like <code>{% mustache "Person" %}</code>. Quotes are optional.
    /// The block accepts double or single quotes, or no quotes at all.
    /// </remarks>
    public class MustacheBlock : Block
    {
        private string _modelContext = "";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);

            // Ignore any quotes or whitespace in the markup
            _modelContext = markup.Replace(@"""", @"").Replace(@"'", "").Trim();
        }

        public override void Render(Context context, TextWriter result)
        {
            using (TextWriter tempWriter = new StringWriter())
            {
                RenderAll(NodeList, context, tempWriter);
                try
                {
                    // Get the appropriate data to use to render the mustache template
                    var model = context[_modelContext];
                    if (model != null)
                    {
                        //' Render the mustache template
                        string mustachioed = N.Render.StringToString(tempWriter.ToString(), model);
                        result.Write(mustachioed);
                    }
                    else
                    {
                        result.Write(tempWriter.ToString());
                    }
                }

                catch (Exception ex)
                {
                    result.Write("Rendering Mustache template failed: " + ex.Message);
                }
            }
        }
    }
}
