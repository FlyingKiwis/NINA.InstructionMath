using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// [MANDATORY] The following GUID is used as a unique identifier of the plugin. Generate a fresh one for your plugin!
[assembly: Guid("0b7be7c9-de8f-490f-830c-cbb5229c2c1a")]

// [MANDATORY] The assembly versioning
//Should be incremented for each new release build of a plugin
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]

// [MANDATORY] The name of your plugin
[assembly: AssemblyTitle("Instruction Math")]
// [MANDATORY] A short description of your plugin
[assembly: AssemblyDescription("Adds instructions which evaluate a math expression")]

// The following attributes are not required for the plugin per se, but are required by the official manifest meta data

// Your name
[assembly: AssemblyCompany("Drew McDermott")]
// The product name that this plugin is part of
[assembly: AssemblyProduct("Instruction Math")]
[assembly: AssemblyCopyright("Copyright © 2022 Drew McDermott")]

// The minimum Version of N.I.N.A. that this plugin is compatible with
[assembly: AssemblyMetadata("MinimumApplicationVersion", "2.0.0.9001")]

// The license your plugin code is using
[assembly: AssemblyMetadata("License", "MPL-2.0")]
// The url to the license
[assembly: AssemblyMetadata("LicenseURL", "https://www.mozilla.org/en-US/MPL/2.0/")]
// The repository where your pluggin is hosted
[assembly: AssemblyMetadata("Repository", "https://github.com/FlyingKiwis/NINA.InstructionMath")]

// The following attributes are optional for the official manifest meta data

//[Optional] Your plugin homepage URL - omit if not applicaple
[assembly: AssemblyMetadata("Homepage", "https://github.com/FlyingKiwis/NINA.InstructionMath")]

//[Optional] Common tags that quickly describe your plugin
[assembly: AssemblyMetadata("Tags", "Math, Sequence, Loop, Calculate, Instruction")]

//[Optional] A link that will show a log of all changes in between your plugin's versions
[assembly: AssemblyMetadata("ChangelogURL", "https://github.com/FlyingKiwis/NINA.InstructionMath/blob/master/CHANGELOG.md")]

//[Optional] The url to a featured logo that will be displayed in the plugin list next to the name
[assembly: AssemblyMetadata("FeaturedImageURL", "")]
//[Optional] A url to an example screenshot of your plugin in action
[assembly: AssemblyMetadata("ScreenshotURL", "")]
//[Optional] An additional url to an example example screenshot of your plugin in action
[assembly: AssemblyMetadata("AltScreenshotURL", "")]
//[Optional] An in-depth description of your plugin
[assembly: AssemblyMetadata("LongDescription", @"# Instruction Math

Named based on Pixel Math in PixInsight this plugin's purpose is to provide users with NINA instructions that operate based on a math equation

## Expressions

Each instruction is controlled by a math expressions.  This plugin uses mXparser to evaluate the expressions.

- [Supported built-in functions of mXparser can be found here](https://mathparser.org/mxparser-math-collection/)

## Instructions

Instructions added by this plugin

### Math Loop

This will loop while the expression evaluates to true

- The expression is compared against a target value to determine truthfulness
- Available comparisons are >, <, >= (greater than or equal), <=, =, != (not equal)

## Keywords *or* Constants, Variables & Functions

In addition to the built in constants and variables this plugin adds some useful ones:

Note: all times are represented as [milliseconds since epoch](https://en.wikipedia.org/wiki/Unix_time)

- **[count]** - The number of times an instruction has been executed
- **[time]** - the current time
- **[astro_dawn]** - The next astronomical dawn
- **[astro_dusk]** - The next astronomical dusk

## Future

The next steps of this plugin are to add more Keywords so that users can come up with more advanced expressions

## Suggestions / Pull Requests

If you would like for me to add a feature [please create an issue with an enhancement label](https://github.com/FlyingKiwis/NINA.InstructionMath/issues)

I'm also open to pull requests if you want to work on new features as well.")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
// [Unused]
[assembly: AssemblyConfiguration("")]
// [Unused]
[assembly: AssemblyTrademark("")]
// [Unused]
[assembly: AssemblyCulture("")]