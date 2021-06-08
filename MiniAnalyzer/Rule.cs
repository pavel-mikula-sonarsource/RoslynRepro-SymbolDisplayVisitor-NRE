using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MiniAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MiniRule2 : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "XMini";

        private static DiagnosticDescriptor rule = new DiagnosticDescriptor(DiagnosticId, "XXX", "{0}", "MMF-1979", DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationStartAction(
                cc =>
                {
                    foreach(var t in GetAllNamedTypes(cc.Compilation.GlobalNamespace))
                    {
                        var cur = t;
                        while (cur != null)
                        {
                            var thisWillThrow = cur.ToDisplayString();
                            cur = cur.BaseType;
                        }
                    }
                });
        }

        public static IEnumerable<INamedTypeSymbol> GetAllNamedTypes(INamespaceSymbol @namespace)
        {
            if (@namespace == null)
            {
                yield break;
            }

            foreach (var typeMember in @namespace.GetTypeMembers().SelectMany(GetAllNamedTypes))
            {
                yield return typeMember;
            }

            foreach (var typeMember in @namespace.GetNamespaceMembers().SelectMany(GetAllNamedTypes))
            {
                yield return typeMember;
            }
        }

        public static IEnumerable<INamedTypeSymbol> GetAllNamedTypes(INamedTypeSymbol type)
        {
            if (type == null)
            {
                yield break;
            }

            yield return type;

            foreach (var nestedType in type.GetTypeMembers().SelectMany(GetAllNamedTypes))
            {
                yield return nestedType;
            }
        }
    }
}
