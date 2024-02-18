# Nukepayload2.SourceGenerators.AvaloniaUI
This is a Visual Basic SourceGenerator built for generating strongly-typed references to controls with x:Name (or just Name) attributes declared in XAML (or, in .axaml). The source generator will look for the xaml (or axaml) file with the same name as your partial Visual Basic class that is a subclass of Avalonia.INamed and parses the XAML markup, finds all XAML tags with x:Name attributes and generates the Visual Basic code.

Embedded 3rd-party components:
- https://github.com/AvaloniaUI/Avalonia/tree/master/src/tools/Avalonia.Generators
- https://github.com/kekekeks/XamlX

## Progress
- [x] Convert C# generator to VB
- [ ] Add demo projects
- [ ] Fix bugs exposed by the demo projects
