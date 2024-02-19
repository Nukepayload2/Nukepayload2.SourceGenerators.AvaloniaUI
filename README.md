# Nukepayload2.SourceGenerators.AvaloniaUI
This is a Visual Basic SourceGenerator built for generating strongly-typed references to controls with x:Name (or just Name) attributes declared in XAML (or, in .axaml). The source generator will look for the xaml (or axaml) file with the same name as your partial Visual Basic class that is a subclass of Avalonia.INamed and parses the XAML markup, finds all XAML tags with x:Name attributes and generates the Visual Basic code.

Embedded 3rd-party components:
- https://github.com/AvaloniaUI/Avalonia/tree/master/src/tools/Avalonia.Generators
- https://github.com/kekekeks/XamlX

## Progress
- [x] Convert C# generator to VB
- [x] Basic manual testing
    - [x] `InitializeComponents` is working
    - [x] `DesignerGenerated` (automatic `InitializeComponents` call) is working
    - [x] `x:Name` and `WithEvents` is working
- [ ] Add demo projects
    - [ ] Port [Mvvm](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/MVVM) samples
        - [x] BasicMvvm
        - [x] CommandSample
        - [ ] ValidationSample
        - [ ] ValueConversionSample
    - [ ] Port [CustomControls](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/CustomControls) samples
        - [ ] RatingControlSample
    - [ ] Add VB-specific samples for testing purpose
        - [ ] Generate `WithEvents` by using `x:Name` in XAML
        - [ ] The `InitializeComponents` method can be automatically called by the VB compiler
        - [ ] x:FieldModifier: Make generated element references `Public`, `Friend`, `Protected` or `Private`. `Friend` is the default value.
- [ ] Release NuGet package
    - [ ] Prerelease package when basic manual testing is completed
    - [ ] Publish stable release when all demos are working correctly
