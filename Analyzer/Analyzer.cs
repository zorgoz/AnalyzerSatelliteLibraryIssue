using System;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;

namespace Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor X = new("X0000", "AssemblyPath", "Path is: '{0}' ({1})", "Debug", DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [X];
        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            // context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.PropertyDeclaration, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = context.Node;

            context.ReportDiagnostic(Diagnostic.Create(X, node.GetLocation(), Assembly.GetExecutingAssembly().Location, node.Kind().ToString()));

            if (node.IsKind(SyntaxKind.ClassDeclaration))
            {
                C();
            }
        }

        private static void C()
        { 
            var x = new Ulid();
        }

    }
}
