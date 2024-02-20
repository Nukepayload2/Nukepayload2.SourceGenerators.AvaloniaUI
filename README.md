# Nukepayload2.SourceGenerators.AvaloniaUI
This is a Visual Basic SourceGenerator built for generating strongly-typed references to controls with x:Name (or just Name) attributes declared in XAML (or, in .axaml). The source generator will look for the xaml (or axaml) file with the same name as your partial Visual Basic class that is a subclass of Avalonia.INamed and parses the XAML markup, finds all XAML tags with x:Name attributes and generates the Visual Basic code.

## VB-specific features
- `WithEvents` members: allows you to attach events or locate event handlers with the drop-down button of Visual Basic code editor.
- `DesignerGenerated` attribute: Let the Visual Basic compiler and editor call `InitializeComponents` for you.

## Progress
- [x] Convert C# generator to VB
- [x] Basic manual testing (tested with 11.0.9)
    - [x] `InitializeComponents` is working
    - [x] `DesignerGenerated` (automatic `InitializeComponents` call) is working
    - [x] `x:Name` -> `WithEvents` is working
- [ ] Add demo projects
    - [x] Port [Mvvm](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/MVVM) samples
        - [x] BasicMvvm
        - [x] CommandSample
        - [x] ValidationSample
        - [x] ValueConversionSample
    - [x] Port [CustomControls](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/CustomControls) samples
        - [x] RatingControlSample
    - [ ] Port [DataTemplates](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/DataTemplates) samples
        - [ ] BasicDataTemplateSample
        - [ ] FuncDataTemplateSample
        - [ ] IDataTemplateSample
    - [x] Port [Drawing](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Drawing) samples
        - [x] BattleCity
    - [ ] Port [Routing](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Routing) samples
        - [ ] BasicViewLocatorSample
    - [ ] Port [ViewInteraction](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/ViewInteraction) samples
        - [ ] DialogManagerSample
        - [ ] MvvmDialogSample
    - [ ] Port [Testing](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Testing) samples
        - [ ] TestableApp
        - [ ] TestableApp.Appium
        - [ ] TestableApp.Headless.NUnit
        - [ ] TestableApp.Headless.XUnit
    - [x] Add VB-specific samples for testing purpose
        - [x] Generate `WithEvents` by using `x:Name` in XAML
        - [x] The `InitializeComponents` method can be automatically called by the VB compiler
        - [x] x:FieldModifier: Make generated element references `Public`, `Friend`, `Protected` or `Private`. `Friend` is the default value.
        - [x] A project that uses the NuGet package of source generator instead of project reference 
- [ ] Release NuGet package
    - [ ] Prerelease package when basic manual testing is completed
    - [ ] Publish stable release when all demos are working correctly

## References
Embedded 3rd-party components:
- https://github.com/AvaloniaUI/Avalonia/tree/master/src/tools/Avalonia.Generators
- https://github.com/kekekeks/XamlX
