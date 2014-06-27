# Mustache block

This DotLiquid block renders mustache code within the block using the data
object under the passed key.

## Example

Given data like this under the key of `person` in the liquid context:

```c#
var data = {Name = "James"};
```

and a liquid template like:

```
{% mustache 'person' %}
  {% raw %}
  Hi {{ Name }}!
  {% endraw %}
{% endmustache %}
```

you will get `Hi James!` back.

The mustache block renders everything within it just like it would render a
normal DotLiquid template and then whatever results is processed
as a mustache template.

## Comments

You can argue that this block is unnecessary and over-engineering *and you'd be
right*! i (james) wanted a quick way to share templates between client and
server for a project i'm working on.
