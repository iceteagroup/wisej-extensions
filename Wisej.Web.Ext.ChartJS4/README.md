# Wisej.Web.Ext.ChartJS4

A modernized, refactored Chart.js 4.x integration for Wisej.NET with improved serialization and maintainability.

## Architecture Overview

### Key Improvements

1. **Modern JSON Serialization**
   - Uses `System.Text.Json` instead of custom serialization patterns
   - Eliminates the need for `ShouldSerialize*()` methods
   - Leverages `JsonExtensionData` for unlimited flexibility

2. **Decoupled Design**
   - No tight coupling between components
   - Options and datasets are standalone POCOs with optional owner back-references
   - Owner references are used to auto-refresh the chart when nested options or data change

3. **Better Maintainability**
   - Clear separation of concerns (Models, Core, Serialization)
   - Strongly-typed with proper null handling
   - Modern C# features (nullable reference types, records, etc.)

4. **Extensibility**
   - `JsonExtensionData` property on all model classes
   - Allows adding any Chart.js option without code changes
   - Full Chart.js API surface available

### Project Structure

```
Wisej.Web.Ext.ChartJS4/
├── Core/
│   └── ChartJS4.cs              # Main widget control
├── Models/
│   ├── ChartType.cs               # Chart type enumeration
│   ├── ChartDataSet.cs            # Data set models (LineDataSet, BarDataSet, etc.)
│   └── ChartOptions.cs            # Options models (ChartOptions, PluginsOptions, etc.)
├── Serialization/
│   └── ColorJsonConverter.cs      # Custom JSON converter for Color objects
└── JavaScript/
    ├── chart-4.4.9.min.js         # Chart.js library
    ├── chartjs-plugin-datalabels.js
    ├── chartjs-adapter-date-fns-3.0.0.bundle.min.js
    └── startup.js                 # Widget initialization script
```

## Usage Example

```csharp
using Wisej.Web.Ext.ChartJS4;
using Wisej.Web.Ext.ChartJS4.Models;

var chart = new ChartJS4
{
    ChartType = ChartType.Line,
    Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun" }
};

// Add datasets
chart.DataSets.Add(new LineDataSet
{
    Label = "Sales",
    Data = new object[] { 12, 19, 3, 5, 2, 3 },
    BorderColor = Color.Blue,
    BackgroundColor = Color.FromArgb(100, Color.Blue),
    Tension = 0.4,
    Fill = true
});

// Configure options
chart.ChartOptions.Responsive = true;
chart.ChartOptions.MaintainAspectRatio = false;
chart.ChartOptions.Plugins = new PluginsOptions
{
    Legend = new LegendOptions { Display = true, Position = "top" },
    Title = new TitleOptions { Display = true, Text = "Monthly Sales" }
};
```

## Chart.js API Methods

ChartJS4 provides a complete implementation of the Chart.js API, allowing full control over chart behavior:

### Update and Rendering

```csharp
// Update the entire chart with animation
chart.UpdateChart();

// Update with specific mode
chart.UpdateChart("resize");

// Update data only (faster)
chart.UpdateData(300); // 300ms animation

// Trigger a redraw without animation
chart.Render();

// Reset to initial state
chart.Reset();

// Clear the canvas
chart.Clear();
```

### Animation Control

```csharp
// Stop all running animations
chart.Stop();
```

### Sizing

```csharp
// Auto-resize from container
chart.Resize();

// Resize to specific dimensions
chart.Resize(800, 600);
```

### Dataset Visibility

```csharp
// Show/hide entire dataset
chart.Hide(0); // Hide first dataset
chart.Show(0); // Show first dataset

// Hide specific data element
chart.Hide(0, 2); // Hide third element in first dataset

// Set visibility explicitly
chart.SetDatasetVisibility(0, false);

// Check if dataset is visible
chart.IsDatasetVisible(0, (isVisible) => {
    Console.WriteLine($"Dataset visible: {isVisible}");
});

// Toggle data visibility
chart.ToggleDataVisibility(2);

// Get data visibility state
chart.GetDataVisibility(2, (isVisible) => {
    Console.WriteLine($"Data visible: {isVisible}");
});

// Get count of visible datasets
chart.GetVisibleDatasetCount((count) => {
    Console.WriteLine($"Visible datasets: {count}");
});
```

### Active Elements

```csharp
// Set active (hovered) elements
chart.SetActiveElements(new object[] {
    new { datasetIndex = 0, index = 2 }
});

// Get currently active elements
chart.GetActiveElements((elements) => {
    foreach (var element in elements) {
        // Process active elements
    }
});
```

### Export

```csharp
// Get chart as PNG image
chart.GetImage((image) => {
    if (image != null) {
        image.Save("chart.png");
    }
});

// Get chart as base64 PNG
chart.ToBase64Image((base64) => {
    Console.WriteLine(base64);
});

// Get chart as JPEG with quality
chart.ToBase64Image("image/jpeg", 0.8, (base64) => {
    Console.WriteLine(base64);
});

// Generate HTML legend
chart.GenerateLegend((html) => {
    Console.WriteLine(html);
});
```

### Lifecycle

```csharp
// Destroy the chart instance
chart.Destroy();
```

## Using JsonExtensionData for Custom Options

One of the most powerful features is the ability to add any Chart.js option without modifying the library:

```csharp
// Add custom plugin options
chart.ChartOptions.Plugins.ExtensionData = new Dictionary<string, object>
{
    ["customPlugin"] = new
    {
        enabled = true,
        color = "red"
    }
};

// Add custom dataset properties
var dataset = new LineDataSet
{
    Label = "Data",
    Data = new object[] { 1, 2, 3 }
};
dataset.ExtensionData = new Dictionary<string, object>
{
    ["borderDash"] = new[] { 5, 5 },
    ["pointRadius"] = 5
};
chart.DataSets.Add(dataset);
```

## Migration from ChartJS4

### Key Differences

1. **Control Name**: `ChartJS4` → `ChartJS4`
2. **Namespace**: `Wisej.Web.Ext.ChartJS4` → `Wisej.Web.Ext.ChartJS4`
3. **Options Property**: `chart.Options` → `chart.ChartOptions`
4. **No ShouldSerialize Methods**: Serialization is handled automatically by System.Text.Json

### Migration Steps

1. **Update Namespaces**
   ```csharp
   // Old
   using Wisej.Web.Ext.ChartJS4;
   
   // New
   using Wisej.Web.Ext.ChartJS4;
   using Wisej.Web.Ext.ChartJS4.Models;
   ```

2. **Update Control References**
   ```csharp
   // Old
   var chart = new ChartJS4();
   
   // New
   var chart = new ChartJS4();
   ```

3. **Update Options Property**
   ```csharp
   // Old
   chart.Options.Responsive = true;
   
   // New
   chart.ChartOptions.Responsive = true;
   ```

4. **Update DataSets**
   ```csharp
   // Old (DataSetCollection)
   chart.DataSets.Add(...);
    
   // New (observable DataSetCollection)
   chart.DataSets.Add(...);  // Same API
   ```

### Benefits of Migration

- **Cleaner Code**: No more complex serialization logic
- **Better IntelliSense**: Strongly-typed models with full documentation
- **Easier Customization**: Add any Chart.js option via ExtensionData
- **Future-Proof**: Modern architecture ready for new Chart.js versions
- **Better Performance**: Efficient JSON serialization

## Comprehensive Chart.js Options Coverage

ChartJS4 provides extensive options coverage with 100+ strongly-typed properties organized across:

### Chart Options
- **Core**: Responsive, AspectRatio, DevicePixelRatio, Locale, ResizeDelay
- **Layout**: Padding configuration
- **Animation**: Duration, Easing, Delay, Loop
- **Transitions**: Mode-specific transition configurations
- **Elements**: Point, Line, Bar, Arc default styling

### Plugins
- **Legend**: Position, Alignment, Labels, MaxHeight/Width, Reverse
- **Title/Subtitle**: Text, Position, Alignment, Color, Font, Padding  
- **Tooltip**: Mode, Intersect, Colors, Fonts, Padding, Caret styling
- **Data Labels**: Display, Anchor, Align, Offset, Colors, Fonts, Border
- **Decimation**: Enabled, Algorithm, Samples
- **Filler**: Propagate, DrawTime

### Scales & Axes
- **Axis Configuration**: Type, Display, Position, Stack, Weight, Stacked, Reverse, Offset
- **Range**: Min, Max, SuggestedMin, SuggestedMax
- **Grid**: Display, Color, LineWidth, Circular, DrawBorder
- **Border**: Display, Color, Width
- **Title**: Display, Text, Color, Font, Padding
- **Ticks**: Display, Color, Font, Rotation, Mirror, Padding

### Datasets (All Types)
- **Common**: Label, Data, Type, Hidden, BackgroundColor, BorderColor, BorderWidth
- **Line**: Tension, Fill, CapStyle, Dash patterns, Join style, Point styling (30+ properties)
- **Bar**: Base, BarThickness, CategoryPercentage, BarPercentage
- **Pie/Doughnut**: Weight, Offset, Spacing
- **Bubble/Scatter**: Point radius, style, colors
- **Radar/Polar**: Radial configurations

### Interaction
- Mode, Intersect, Axis, IncludeInvisible

### Font Configuration
- Family, Size, Style, Weight, LineHeight

All options support **JsonExtensionData** - add any Chart.js option not explicitly defined:

```csharp
// Example: Using advanced Chart.js options
chart.ChartOptions.ExtensionData = new Dictionary<string, object>
{
    ["onHover"] = "function(event, elements) { /* custom hover logic */ }",
    ["parsing"] = new { xAxisKey = "date", yAxisKey = "value" },
    ["normalized"] = true
};

// Advanced scale configuration
chart.ChartOptions.Scales.ExtensionData = new Dictionary<string, object>
{
    ["r"] = new  // Radial axis for radar charts
    {
        type = "radialLinear",
        angleLines = new { display = true },
        pointLabels = new { display = true }
    }
};
```

This architecture provides:
- ✅ **Comprehensive coverage** of common options (100+ properties)
- ✅ **Unlimited extensibility** via JsonExtensionData
- ✅ **Type safety** for standard options
- ✅ **IntelliSense support** for defined properties
- ✅ **Full Chart.js compatibility** - any option can be added

## System Requirements

- .NET Framework 4.8+
- .NET 8.0+
- .NET 9.0+
- Wisej-4 (version 4.0 or later)

## License

Copyright © 2021-2025 Ice Tea Group LLC. All Rights Reserved.

## Support

For issues or questions, please refer to the Wisej.NET documentation or support channels.
