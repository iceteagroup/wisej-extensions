# Wisej.Web.Ext.Mermaid

Mermaid (https://mermaid.js.org/) integration for Wisej.NET.

## Usage

Add a `Wisej.Web.Ext.Mermaid.Mermaid` control to a page and set `Diagram` to any Mermaid definition.

## Loading Mermaid JS Locally

By default the widget loads Mermaid from an embedded resource. To use a locally hosted Mermaid script instead, set either:

- Per control: `MermaidScriptSource = "/js/mermaid.min.js"` (or any URL/relative URL your app serves).
- Globally (static): set `Wisej.Web.Ext.Mermaid.Mermaid.Source` early at app startup:

```csharp
Wisej.Web.Ext.Mermaid.Mermaid.Source = "/js/mermaid.min.js";
```

## Font

Use the control's `Font` property to set the diagram font family and size (mapped to Mermaid theme variables).

## Validation Errors

The control fires `Mermaid.Error` when Mermaid parsing/validation fails in the browser (e.g. invalid syntax).

## Validate Without Rendering

Call `ValidateAsync()` to validate Mermaid text in the browser without rendering it:

```csharp
var result = await mermaid1.ValidateAsync("flowchart LR\nA -->");
if (!result.Ok)
	Alert.Show(result.Message);
```

## Hand-Drawn Look

Set the global Mermaid config (passed to `mermaid.initialize(...)`) to use the `handDrawn` look:

```js
mermaid.initialize({
  theme: "forest",
  look: "handDrawn",
});
```

In Wisej, set it through the control config:

```csharp
mermaid1.UpdateConfig(c => c.Look = "handDrawn");
```
