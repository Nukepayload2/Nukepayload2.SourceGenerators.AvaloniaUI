# Nukepayload2.SourceGenerators.AvaloniaUI
This is a Visual Basic SourceGenerator built for generating strongly-typed references to controls with x:Name (or just Name) attributes declared in XAML (or, in .axaml). The source generator will look for the xaml (or axaml) file with the same name as your partial Visual Basic class that is a subclass of Avalonia.INamed and parses the XAML markup, finds all XAML tags with x:Name attributes and generates the Visual Basic code.

[Get on NuGet](https://www.nuget.org/packages/Nukepayload2.SourceGenerators.AvaloniaUI)

[![NuGet version (Nukepayload2.SourceGenerators.AvaloniaUI)](https://img.shields.io/nuget/v/Nukepayload2.SourceGenerators.AvaloniaUI.svg?style=flat-square)](https://www.nuget.org/packages/Nukepayload2.SourceGenerators.AvaloniaUI/)

The [VB template pack](https://github.com/Nukepayload2/avalonia-vbnet-templates) has been released! It uses this source generator in project templates.

## VB-specific features
- `WithEvents` members: allows you to attach events or locate event handlers with the drop-down button of Visual Basic code editor.
- `DesignerGenerated` attribute: Let the Visual Basic compiler and editor call `InitializeComponents` for you.

## System requirements
### Minimum requirements
- Visual Studio 16.9 or later
- .NET SDK 6.0 or later

### Highest tested environment
- Visual Studio 17.9.0
- .NET SDK 8.0
- Windows 11 23H2

## Avalonia compatibility
### Package 1.0.0 and its prerelease versions
It's guaranteed to be compatible with `11.0.9`. Other versions may work, but they are not tested.

## How to use it
Install the `Nukepayload2.SourceGenerators.AvaloniaUI` package to a VB Avalonia project.

You can use [demo projects](https://github.com/Nukepayload2/Nukepayload2.SourceGenerators.AvaloniaUI/tree/master/Demos) as your project template collection. If you need to create a new project, just copy a demo project to your solution, then install the `Nukepayload2.SourceGenerators.AvaloniaUI` package.

## Progress
- [x] Convert C# generator to VB
- [x] Basic manual testing (tested with 11.0.9)
    - [x] `InitializeComponents` is working
    - [x] `DesignerGenerated` (automatic `InitializeComponents` call) is working
    - [x] `x:Name` -> `WithEvents` is working
- [x] Add demo projects
    - [x] Port [Mvvm](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/MVVM) samples
        - [x] BasicMvvm
        - [x] CommandSample
        - [x] ValidationSample
        - [x] ValueConversionSample
    - [x] Port [CustomControls](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/CustomControls) samples
        - [x] RatingControlSample
    - [x] Port [DataTemplates](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/DataTemplates) samples
        - [x] BasicDataTemplateSample
        - [x] FuncDataTemplateSample
        - [x] IDataTemplateSample
    - [x] Port [Drawing](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Drawing) samples
        - [x] BattleCity
    - [x] Port [Routing](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Routing) samples
        - [x] BasicViewLocatorSample
    - [x] Port [ViewInteraction](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/ViewInteraction) samples
        - [x] DialogManagerSample
        - [x] MvvmDialogSample
    - [x] Port [Testing](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Testing) samples
        - [x] TestableApp
        - [x] TestableApp.Appium
        - [x] TestableApp.Headless.NUnit
        - [x] TestableApp.Headless.XUnit
    - [x] Add VB-specific samples for testing purpose
        - [x] Generate `WithEvents` by using `x:Name` in XAML
        - [x] The `InitializeComponents` method can be automatically called by the VB compiler
        - [x] x:FieldModifier: Make generated element references `Public`, `Friend`, `Protected` or `Private`. `Friend` is the default value.
        - [x] A project that uses the NuGet package of source generator instead of project reference 
- [x] Release NuGet package
    - [x] Prerelease package when basic manual testing is completed
    - [x] Publish stable release when all demos are working correctly

## References
Embedded 3rd-party components:
- https://github.com/AvaloniaUI/Avalonia/tree/master/src/tools/Avalonia.Generators
- https://github.com/kekekeks/XamlX
