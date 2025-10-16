Wisej-Extensions
====

Repository for __Wisej - Full Stack Web Development__ extensions.

__Wisej__ is designed to build, debug, deploy and manage web applications of any complexity in Visual Studio or SharpDevelop. It’s a highly specialized framework for Real Time Web Applications. Developers can focus on their projects and don’t worry about HTML, state management, backend services, callbacks, ajax panels, synchronization, DOM, CSS, JavaScript, security, authentication, concurrency...

This repository groups the content of all extensions found on [Wisej site](https://wisej.com/extensions/) and some new extensions. This repository is updated more frequently than the source code at Wisej site.

Developers new to Wisej should install [Wisej trial version](https://wisej.com/#buy).

## Extending Wisej

Wisej was designed to be extensible and open to all sorts of technologies from the start. The extensions published are a fraction of what is possible and what will be available during the product’s lifetime.

Note that most published extensions are also already precompiled and included in the installer and are automatically added to Visual Studio’s toolbox.

## News

| Extension | Change&nbsp;Date | Description |
| :--- | :---: | :--- |
| Wisej.Web.Ext.ChartJs | 16 Aug 2018 | Added support for SteppedLine. Set colours and style on each individual point. Upgraded to chartjs 2.7.2. |
| Wisej.Web.Ext.FullCalendar | 12 Aug 2018 | Added support for the Scheduler plug in, timeline views and BusinessHours. Upgraded to fullcalendar 3.9.0. |
| Wisej.Web.Ext.RibbonBar | 12 Aug 2018 | Added a slidebar to scroll tab buttons and tab pages that exceed the width of the container. |
| Wisej.Web.Ext.RibbonBar | 07 Aug 2018 | Items are removed when disposed. |
| Wisej.Web.Ext.GoogleMaps | 07 Aug 2018 | Prevent capturing clicks on overlays on mobile. |
| Wisej.Web.Ext.GoogleMaps | 31 Jul 2018 | Added Reverse Geocode support including awaitable methods. |
| Wisej.Ext.Geolocation | 29 Jul 2018 | Added awaitable methods. |
| Wisej.Web.Ext.Html2Canvas | 29 Jul 2018 | Added awaitable methods. |
| Wisej.Web.Ext.SmoothieChart | 29 Jul 2018 | Added awaitable methods. |

License
-------
Wisej is Copyright Ice Tea Group LLC, 2018

## GitHub Workflow Usage

The repository includes a **Build & Pack Wisej.NET Standard Extensions** workflow that restores, builds, and optionally packs the NuGet specifications.

To queue a manual run from the GitHub UI:

1. Navigate to **Actions** in this repository.
2. Select **Build & Pack Wisej.NET Standard Extensions**.
3. Click **Run workflow** and fill in the inputs:
   * `pack` &ndash; set to `true` when you want `.nupkg` packages produced and published.
   * `assembly_version` &ndash; optional override for the assembly version baked into the packages.
   * `package_channel` &ndash; choose `test`, `preview`, or `release` to control the package filename suffix.
4. Confirm by pressing **Run workflow**.

You can also trigger the same workflow from the command line with the GitHub CLI:

```bash
gh workflow run "Build & Pack Wisej.NET Standard Extensions" \
  --field pack=true \
  --field assembly_version=1.2.3.4 \
  --field package_channel=preview
```

If automation is required, issue a `workflow_dispatch` call with `curl`:

```bash
curl -X POST \
  -H "Authorization: Bearer <GH_TOKEN_WITH_WORKFLOW_SCOPE>" \
  -H "Accept: application/vnd.github+json" \
  https://api.github.com/repos/iceteagroup/Wisej.NET-Standard-Extensions/actions/workflows/build-and-test.yml/dispatches \
  -d '{
    "ref": "main",
    "inputs": {
      "pack": "true",
      "assembly_version": "1.2.3.4",
      "package_channel": "preview"
    }
  }'
```

Replace `<GH_TOKEN_WITH_WORKFLOW_SCOPE>` and `ref` with your token and branch respectively. The downstream QA workflow will trigger automatically after the build job completes.
