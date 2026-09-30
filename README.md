<div align="center">

<a href="https://wisej.com"><img src="https://wisej.com/wp-content/uploads/2022/05/wisej-dotnet-logotype-main-nav-203-05312022.png" alt="Wisej.NET" width="203"></a>

# Wisej.NET Extensions

**Open-source controls, components and services for Wisej.NET: charts, editors, maps, calendars, ribbons, device APIs, icon packs and more.**

<a href="https://docs.wisej.com/extensions/"><img alt="Documentation" src="https://img.shields.io/badge/Documentation-0B6BCB?style=for-the-badge"></a>
<a href="https://docs.wisej.com/extensions/api/"><img alt="API Reference" src="https://img.shields.io/badge/API%20Reference-6E40C9?style=for-the-badge&logo=dotnet&logoColor=white"></a>
<a href="https://www.nuget.org/profiles/IceTeaGroup"><img alt="NuGet" src="https://img.shields.io/badge/NuGet-004880?style=for-the-badge&logo=nuget&logoColor=white"></a>
<a href="https://wisej.com"><img alt="Wisej.NET" src="https://img.shields.io/badge/Wisej.NET-1F2937?style=for-the-badge"></a>

</div>

This repository contains the source code of the standard **Wisej.NET extensions**. Each extension is a regular .NET assembly, published on NuGet, that you add to a Wisej.NET project and program from C# or VB.NET like any other control. Every folder is one extension, with its own README that links to its documentation.

> [!TIP]
> The [Wisej.NET documentation](https://docs.wisej.com/extensions/) describes every extension in detail, with examples in C# and VB.NET, and has a full [API reference](https://docs.wisej.com/extensions/api/) for each assembly. It's the place to start.

## Branches

Each branch matches a Wisej.NET release: `4.1` is the current one, and older branches such as `4.0`, `3.5` and `2.5` are kept for applications on those versions. Some extensions exist only in some branches.

## Catalog

### Charts, gauges and diagrams

| Extension | Description | Links |
| :-- | :-- | :-- |
| **ChartJS** | Line, bar, radar, pie, doughnut, polar area, bubble and scatter charts built on Chart.js 2, configured and fed with data from server code. | [Docs](https://docs.wisej.com/extensions/extensions/chartjs/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.chartjs/) |
| **ChartJS3** | The Chart.js 3 edition of the ChartJS extension, with the same object model and options reorganized the way Chart.js 3 arranges them. | [Docs](https://docs.wisej.com/extensions/extensions/chartjs3/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.chartjs3/) |
| **ChartJS4** | A modernized Chart.js 4 integration for Wisej.NET, with a decoupled object model and System.Text.Json serialization. | [Docs](https://docs.wisej.com/extensions/extensions/chartjs/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.chartjs4/) |
| **ApexCharts** | **Work in progress.** Interactive charts built on ApexCharts, wrapped as a Wisej.NET widget. | [Docs](https://docs.wisej.com/extensions/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.apexcharts/) |
| **Charts** | **Work in progress.** An early, work-in-progress Chart.js 1.0 control that renders a fixed sample bar chart. Kept as a source example; use ChartJS for real charts. | [Docs](https://docs.wisej.com/extensions/extensions/charts/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.charts/) |
| **WebCharts** | Server-rendered charts with the .NET Framework Microsoft Chart Controls (System.Windows.Forms.DataVisualization), shown in the browser as an image. .NET Framework 4.8 only. | [Docs](https://docs.wisej.com/extensions/extensions/webcharts/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.webcharts/) |
| **SmoothieChart** | A real-time scrolling chart for streaming data, fed point by point from a server event. | [Docs](https://docs.wisej.com/extensions/extensions/smoothiechart/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.smoothiechart/) |
| **JustGage** | An animated, resolution-independent gauge for showing one value within a range, with custom color sectors, a donut mode and number formatting. | [Docs](https://docs.wisej.com/extensions/extensions/justgage/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.justgage/) |
| **jQueryKnob** | A touch-friendly dial the user can drag, scroll or type into, based on jQuery Knob, with configurable arc, range, step and colors. | [Docs](https://docs.wisej.com/extensions/extensions/jqueryknob/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.jqueryknob/) |
| **ProgressCircle** | A circular progress indicator drawn from the server on an HTML5 canvas, and a compact example of building a custom control on Wisej.Web.Canvas. | [Docs](https://docs.wisej.com/extensions/extensions/progresscircle/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.progresscircle/) |
| **CountUp** | A label that animates to each new number, counting up or down from the value it currently shows. Based on CountUp.js. | [Docs](https://docs.wisej.com/extensions/extensions/countup/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.countup/) |
| **Odometer** | Numbers that roll to their new value digit by digit, like a mechanical counter, with seven skins from a car dashboard to a slot machine. | [Docs](https://docs.wisej.com/extensions/extensions/odometer/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.odometer/) |
| **CoolClock** | An analog clock drawn on an HTML canvas, with 19 skins, an optional digital readout, logarithmic dials and a fixed time-zone offset. | [Docs](https://docs.wisej.com/extensions/extensions/coolclock/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.coolclock/) |
| **Mermaid** | Render flowcharts, sequence, class, state and Gantt diagrams from Mermaid text, with pan and zoom, click events, validation, and SVG, PNG and PDF export. | [Docs](https://docs.wisej.com/extensions/extensions/mermaid/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.mermaid/) |
| **jSequence** | Draws UML sequence diagrams from a simple text description, based on js-sequence-diagrams, with a click event for the diagram elements. | [Docs](https://docs.wisej.com/extensions/extensions/jsequence/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.jsequence/) |

### Editors and documents

| Extension | Description | Links |
| :-- | :-- | :-- |
| **TinyMCE** | A WYSIWYG HTML editor control built on TinyMCE 4, with a configurable menu bar and toolbar, server-side commands, external plugins and a read-only mode. | [Docs](https://docs.wisej.com/extensions/extensions/tinymce/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.tinymce/) |
| **TinyMCE6** | A WYSIWYG HTML editor control built on TinyMCE 6, the recommended TinyMCE extension for new Wisej.NET projects. | [Docs](https://docs.wisej.com/extensions/extensions/tinymce6/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.tinymce6/) |
| **CKEditor** | A full-featured WYSIWYG HTML editor control built on CKEditor 4, with a configurable toolbar, server-side command and link events, external plugins and a read-only mode. | [Docs](https://docs.wisej.com/extensions/extensions/ckeditor/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.ckeditor/) |
| **QuillJS** | A rich text editor control built on Quill, with HTML, plain text and Delta access to the content, a server-side formatting API and selection events. | [Docs](https://docs.wisej.com/extensions/extensions/quilljs/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.quilljs/) |
| **TinyEditor** | A lightweight, self-contained WYSIWYG HTML editor control with a configurable toolbar, an HTML source view and no external dependencies. | [Docs](https://docs.wisej.com/extensions/extensions/tinyeditor/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.tinyeditor/) |
| **AceEditor** | A code editor control built on Ace, with syntax highlighting for 171 languages, 39 themes, code folding, search and replace, and text synchronized with the server as the user types. | [Docs](https://docs.wisej.com/extensions/extensions/aceeditor/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.aceeditor/) |
| **XTerm** | A terminal control built on xterm.js that collects the lines the user types and lets the server write output, including ANSI colors, back to the screen. | [Docs](https://docs.wisej.com/extensions/extensions/xterm/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.xterm/) |
| **OfficeViewer** | A panel that displays Word, Excel and PowerPoint documents through Microsoft's Office Online viewer, from a URL, an application file or a stream. | [Docs](https://docs.wisej.com/extensions/extensions/officeviewer/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.officeviewer/) |
| **OnlyOffice** | **Preview.** A preview control that hosts the ONLYOFFICE document editor in a Wisej.NET page. It needs an ONLYOFFICE Document Server, and in this release its client configuration is fixed. | [Docs](https://docs.wisej.com/extensions/extensions/onlyoffice/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.onlyoffice/) |
| **PrintPreview** | Previews and prints PrintDocument objects, in PDF or WMF mode, with a ready-made PrintPreviewDialog and a PrintPreviewControl that fits in any container. | [Docs](https://docs.wisej.com/extensions/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.printpreview/) |
| **Signature** | A canvas control where users sign with a mouse, pen or finger, with undo/redo, a read-only mode and export to a server-side image. | [Docs](https://docs.wisej.com/extensions/extensions/signature/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.signature/) |

### Navigation and layout

| Extension | Description | Links |
| :-- | :-- | :-- |
| **NavigationBar** | A responsive vertical navigation bar with a header, nested items, info bubbles, shortcut buttons, a user panel and a compact icon-only mode. | [Docs](https://docs.wisej.com/extensions/extensions/navigationbar/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.navigationbar/) |
| **RibbonBar** | An Office-style command bar that organizes an application's commands into tabbed pages, groups and items, with an application button, drop-down menus and a compact mode. | [Docs](https://docs.wisej.com/extensions/extensions/ribbonbar/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.ribbonbar/) |
| **ToolStrip** | **Work in progress.** A WinForms-compatible ToolStrip container for toolbar buttons, drop-downs, combo boxes and other toolbar items. | [Docs](https://docs.wisej.com/extensions/) |
| **SideButton** | A retractable, animated edge button that toggles a side panel, like the collapsible side bars of modern web apps. | [Docs](https://docs.wisej.com/extensions/extensions/sidebutton/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.sidebutton/) |
| **TourPanel** | Guided, step-by-step tours of a Wisej.NET application that highlight one control at a time and explain it in an HTML callout, with auto-play and full control from code. | [Docs](https://docs.wisej.com/extensions/extensions/tourpanel/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.tourpanel/) |
| **Navigator** | **Work in progress.** A work-in-progress router that maps URL hash paths to Wisej.NET pages, with route parameters, deep links and an optional login page. | [Docs](https://docs.wisej.com/extensions/extensions/navigator/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.navigator/) |
| **ViewBuilder** | Builds Wisej.NET forms, pages and panels at runtime from a JSON definition or an object model, with nested controls, components, event handlers and data bindings. | [Docs](https://docs.wisej.com/extensions/extensions/viewbuilder/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.viewbuilder/) |
| **PullToRefresh** | An extender that adds the mobile pull-to-refresh gesture to any scrollable Wisej.NET container and raises a server-side Refresh event. | [Docs](https://docs.wisej.com/extensions/extensions/pulltorefresh/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.pulltorefresh/) |
| **TaskDialog** | **Work in progress.** A Windows-style task dialog with command links, radio buttons, an expander, a footnote and a progress bar. | [Docs](https://docs.wisej.com/extensions/) |
| **TaskBar** | **Legacy.** Legacy extension that provided a standalone taskbar for minimized floating windows outside a Desktop. Not part of Wisej.NET 4.x. | [Docs](https://docs.wisej.com/extensions/extensions/taskbar/) |

### Data and grids

| Extension | Description | Links |
| :-- | :-- | :-- |
| **ColumnFilter** | An extender that adds Excel-style filter buttons to DataGridView column headers, with a value-list panel, a condition builder, and support for custom filter panels. | [Docs](https://docs.wisej.com/extensions/extensions/columnfilter/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.columnfilter/) |
| **FuzzySort** | Adds type-ahead filtering with highlighted matches to a Wisej.NET ComboBox, using the fuzzysort JavaScript library. | [Docs](https://docs.wisej.com/extensions/extensions/fuzzysort/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.fuzzysort/) |
| **DataGridView Summary Row** | **Legacy.** Legacy extension that added subtotal and summary rows to the DataGridView. The feature has been built into the DataGridView since Wisej 2.5. | [Docs](https://docs.wisej.com/extensions/extensions/datagridviewsummaryrow/) |

### Notifications and communication

| Extension | Description | Links |
| :-- | :-- | :-- |
| **Notification** | Shows native desktop notifications from a Wisej.NET application through the browser's Notification API, with a server-side Click event. | [Docs](https://docs.wisej.com/extensions/extensions/notification/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.notification/) |
| **Bubbles** | An extender that shows numeric notification badges on any Wisej.NET control or toolbar button, in alert, warning, critical or custom styles, with a Click event. | [Docs](https://docs.wisej.com/extensions/extensions/bubbles/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.bubbles/) |
| **Chat Control** | A chat box of message bubbles and an input box, with users and avatars, any Wisej.NET control as a message, lazy messages with a typing indicator, and tools in the input box. | [Docs](https://docs.wisej.com/extensions/extensions/chat-control/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.chatcontrol/) |
| **WebShare** | Open the device's native share sheet from server code to share links, text and files, through the browser's Web Share API. | [Docs](https://docs.wisej.com/extensions/extensions/webshare/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.webshare/) |

### Media, maps and devices

| Extension | Description | Links |
| :-- | :-- | :-- |
| **Camera** | Shows the live stream of the device's camera, takes snapshots as server-side images and records video that is uploaded to the server. | [Docs](https://docs.wisej.com/extensions/extensions/camera/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.camera/) |
| **Barcode** | Generates 1D and 2D barcodes (QR, Code 128, EAN, Data Matrix and more) on the server, and reads them from images or live from the device camera. | [Docs](https://docs.wisej.com/extensions/extensions/barcode/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.barcode/) |
| **Speech** | Text-to-speech and speech-to-text for Wisej.NET applications, built on the browser's Web Speech API, as two extender components. | [Docs](https://docs.wisej.com/extensions/extensions/speech/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.speech/) |
| **ScreenRecorder** | **Preview.** Records a screen, window or browser tab chosen by the user and uploads the video to the server. A preview component, not yet published on NuGet. | [Docs](https://docs.wisej.com/extensions/extensions/screenrecorder/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.screenrecorder/) |
| **Recorder** | **Work in progress.** Audio, camera and screen recorder components that capture media in the browser and upload the recording to the server. | [Docs](https://docs.wisej.com/extensions/) |
| **YouTube** | **Work in progress.** A placeholder for a future YouTube player control. How to embed YouTube videos in Wisej.NET today. | [Docs](https://docs.wisej.com/extensions/extensions/youtube/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.youtube/) |
| **JssorSlider** | **Work in progress.** An unfinished image slider control based on Jssor Slider. Its current state, and what to use instead. | [Docs](https://docs.wisej.com/extensions/extensions/jssorslider/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.jssorslider/) |
| **ImageSlider** | **Work in progress.** An image slider control. | [Docs](https://docs.wisej.com/extensions/) |
| **Pannellum** | An interactive 360° panorama viewer for equirectangular, cube-map and multi-resolution images, with hot spots and virtual tours. | [Docs](https://docs.wisej.com/extensions/extensions/pannellum/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.pannellum/) |
| **GoogleMaps** | A Google Maps control for Wisej.NET with markers, info windows, routes, geocoding and server-side map events. | [Docs](https://docs.wisej.com/extensions/extensions/googlemaps/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.googlemaps/) |
| **Geolocation** | Reads the position of the user's device (latitude, longitude, accuracy, altitude) from the browser, once or continuously. | [Docs](https://docs.wisej.com/extensions/extensions/geolocation/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.geolocation/) |
| **Html2Canvas** | Takes a screenshot of a control or of the whole browser page and returns it to the server as an image. | [Docs](https://docs.wisej.com/extensions/extensions/html2canvas/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.html2canvas/) |
| **FullCalendar** | A full-size drag-and-drop event calendar with month, week, day, list and resource timeline views, based on FullCalendar 3.9. | [Docs](https://docs.wisej.com/extensions/extensions/fullcalendar/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.fullcalendar/) |
| **BingWallpaper** | A component that rotates Bing's images of the day, with cross-fade and zoom, as the background of the Desktop, the main page or any control. | [Docs](https://docs.wisej.com/extensions/extensions/bingwallpaper/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.bingwallpaper/) |
| **CustomWallpaper** | A component that rotates your own images, with cross-fade and zoom, as the background of the Desktop, the main page or any control. | [Docs](https://docs.wisej.com/extensions/extensions/customwallpaper/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.customwallpaper/) |

### Browser APIs

| Extension | Description | Links |
| :-- | :-- | :-- |
| **ClientClipboard** | Read and write text and images on the user's clipboard from server code, and get notified when the user cuts, copies or pastes. | [Docs](https://docs.wisej.com/extensions/extensions/clientclipboard/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.clientclipboard/) |
| **ClientFileSystem** | Open, read, write and save files and folders on the user's own device from server code, through the browser's File System Access API. | [Docs](https://docs.wisej.com/extensions/extensions/clientfilesystem/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.clientfilesystem/) |
| **WebAuthn** | Passwordless sign-in with passkeys, Windows Hello, Touch ID and security keys through the browser's Web Authentication API. | [Docs](https://docs.wisej.com/extensions/extensions/webauthn/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.webauthn/) |
| **WebWorker** | Runs JavaScript in a background Web Worker in the user's browser and exchanges messages with it from server code. | [Docs](https://docs.wisej.com/extensions/extensions/webworker/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.webworker/) |
| **WebARIA** | An extender component that adds WAI-ARIA attributes, such as aria-required, aria-describedby and aria-hidden, to any Wisej.NET control from the designer or from code. | [Docs](https://docs.wisej.com/extensions/extensions/webaria/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.webaria/) |

### Server and integration

| Extension | Description | Links |
| :-- | :-- | :-- |
| **Amazon S3** | A file system provider that exposes an Amazon S3 bucket to the Wisej.NET file dialogs and to any code that works with IFileSystemProvider. | [Docs](https://docs.wisej.com/extensions/extensions/amazon-s3/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.filesystem/) |
| **Brotli** | Brotli compression for Wisej.NET, with a BrotliStream for .NET Framework and a browser-side Brotli decoder for WebSocket traffic. | [Docs](https://docs.wisej.com/extensions/extensions/brotli/) · [API](https://docs.wisej.com/extensions/api/system.io.compression/) |
| **ClearScript** | Server-side scripting for Wisej.NET applications with Google V8 JavaScript, JScript or VBScript, built on Microsoft ClearScript. | [Docs](https://docs.wisej.com/extensions/extensions/clearscript/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.clearscript/) |
| **Tesseract** | Optical character recognition in the browser with Tesseract.js, for images and for a live Camera feed. | [Docs](https://docs.wisej.com/extensions/extensions/tesseract/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.tesseract/) |
| **Translation** | A component that translates text on the server through a pluggable translation provider. | [Docs](https://docs.wisej.com/extensions/extensions/translation/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.translation/) |
| **CognitiveServices** | **Work in progress.** A work-in-progress component for image analysis with the Azure AI Vision (formerly Cognitive Services Computer Vision) REST API. | [Docs](https://docs.wisej.com/extensions/extensions/cognitiveservices/) · [API](https://docs.wisej.com/extensions/api/wisej.ext.cognitiveservices/) |
| **Authentication** | **Work in progress.** OAuth2 single sign-on for Wisej.NET applications, with ready-made GitHub and Google providers built on a common base class. | [Docs](https://docs.wisej.com/extensions/) |
| **AspNetControl** | Host classic ASP.NET Web Forms controls, such as report viewers and designers, inside a Wisej.NET application. | [Docs](https://docs.wisej.com/extensions/extensions/aspnetcontrol/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.aspnetcontrol/) |
| **Polymer** | **Legacy.** Hosts Polymer 1.x web components (paper and iron elements) in Wisej.NET controls. Legacy technology that depends on HTML Imports. | [Docs](https://docs.wisej.com/extensions/extensions/polymer/) · [API](https://docs.wisej.com/extensions/api/wisej.web.ext.polymer/) |

### Icon packs

Fourteen icon libraries as embedded SVG icons, ready to pick in the designer and to use in code: Bootstrap, css.gg, Elegant, Feather, FluentUI (filled and outlined), Font Awesome, Ionicons, Lucide, Material Design, Modern UI, Tabler, Vaadin and Visual Studio. See [Icon Packs](https://docs.wisej.com/extensions/icon-packs/overview/), where each pack has a searchable browser of its icons.

### Premium extensions

Integrations of the commercial DevExtreme, Syncfusion, Kendo UI, Ignite UI and Webix suites are documented in [Premium Extensions](https://docs.wisej.com/extensions/premium-extensions/overview/). Their source code is in a separate repository, available to Technology Partners.

## Installation

The extensions are published on NuGet as `Wisej-{version}-{Name}`, where `{version}` is the major version of Wisej.NET. For example, for Wisej.NET 4:

```
dotnet add package Wisej-4-ChartJS
```

Keep every `Wisej-4-*` extension at the same version as the `Wisej-4` package. In Visual Studio, search for `Wisej-4` in **Manage NuGet Packages** to see them all. See [Installing an extension](https://docs.wisej.com/extensions/#installation).

## Building your own

Wisej.NET was built to be extended, and any extension in this repository can be the starting point for your own. [Extension Types](https://docs.wisej.com/extensions/introduction/extension-types/) explains the kinds of extension (control, widget, component, extender provider and icon pack) and how each one works.

## Contributing

Found a bug or have an improvement that could help others? Open an [issue](https://github.com/iceteagroup/wisej-extensions/issues) or a pull request. For questions about using Wisej.NET, visit the [Wisej.NET forums](https://wisej.com/support/).

## License

Copyright © Ice Tea Group LLC. All rights reserved.
