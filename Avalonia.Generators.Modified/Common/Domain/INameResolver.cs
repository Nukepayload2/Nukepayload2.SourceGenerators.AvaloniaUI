using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using XamlX.Ast;

namespace Avalonia.Generators.Common.Domain;

internal enum NamedFieldModifier
{
    Public = 0,
    Private = 1,
    Internal = 2,
    Protected = 3,
}

internal interface INameResolver
{
    IReadOnlyList<ResolvedName> ResolveNames(XamlDocument xaml);
}

internal class ResolvedName
{
    private readonly Lazy<string> _typeName;

    public ResolvedName(XamlX.TypeSystem.IXamlType clrType, string name, string fieldModifier)
    {
        _typeName = new Lazy<string>(() =>
        {
            var typeName = $@"{clrType.Namespace}.{clrType.Name}";
            var typeAgs = clrType.GenericArguments.Select(arg => arg.FullName).ToImmutableList();
            var genericTypeName = typeAgs.Count == 0
                ? $"global::{typeName}"
                : $@"global::{typeName}<{string.Join(", ", typeAgs.Select(arg => $"global::{arg}"))}>";
            return genericTypeName;
        });
        XamlType = clrType;
        Name = name;
        CsFieldModifier = fieldModifier;
    }

    // VB generator doesn't need this. Defer init.
    public string CsTypeName => _typeName.Value;
    public XamlX.TypeSystem.IXamlType XamlType { get; }
    public string Name { get; }
    // Convertible to VB.
    public string CsFieldModifier { get; }
} // End Class ' ResolvedName