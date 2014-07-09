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
    /// data from the <code>person</code> object, call the mustache block like <code>{% mustache person %}</code>.
    /// </remarks>
    public class MustacheBlock : Block
    {
        private string _modelContext = "";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            // Consider anything within the mustache block as if it were in a raw block
            tokens.Insert(0, @"{% raw %}");
            tokens.Insert(tokens.Count - 1, @"{% endraw %}");

            // Ignore any whitespace in the markup
            _modelContext = markup.Trim();

            base.Initialize(tagName, markup, tokens);
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
