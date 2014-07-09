# Mustache block

This DotLiquid block renders mustache code within the block using the data
object under the passed key.

## Example

Given data like this under the key of `person` in the liquid context:

```c#
var data = { Name = "James" };
```

and a liquid template like:

```
{% mustache person %}
Hi {{ Name }}!
{% endmustache %}
```

you will get `Hi James!` back.

The mustache block considers everything within it as a mustache template.

### Optionally use liquid markup inside the mustache block

If you want to render liquid markup within the `mustache` block, like an include
for example, use `no_raw` in the mustache block's markup to prevent treating the
markup inside the block as a mustache template:

```
{% mustache person no_raw %}
{% include person_template %}
{% endmustache %}
```

Remember to enclose any mustache template code in a `raw` block so it isn't
processed as liquid markup.

## Comments

You can argue that this block is unnecessary and a result of over-engineering
*and you'd be right*! i (@jugglingnutcase) wanted a quick way to share templates
between client and server for a project i'm working on.
