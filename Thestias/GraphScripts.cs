using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading;
using System.Threading.Tasks;

namespace Thestias
{
    public partial class Graph
    {
        private readonly Dictionary<string, Script> Scripts = new();

        public bool AddScript(Script script)
        {
            script.Compile();

            if (IsValidScript(script))
            {
                Compilation compilation = script.GetCompilation();

                SyntaxTree syntaxTree = compilation.SyntaxTrees.First();
                CompilationUnitSyntax syntaxRootNode = syntaxTree.GetRoot() as CompilationUnitSyntax;
                SemanticModel semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees.First());

                MethodDeclarationSyntax method = null;
                foreach (MemberDeclarationSyntax member in syntaxRootNode.Members)
                {
                    if (member is MethodDeclarationSyntax) method = member as MethodDeclarationSyntax;
                }

                Scripts.Add(/*semanticModel.GetSymbolInfo(method).Symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)*/"square", script);
                return true;
            }

            return false;
        }

        public bool AddScript(string code, ScriptOptions options = null)
        {
            return this.AddScript(CSharpScript.Create(code, ScriptOptions.Default));
        }

        public async Task<object> GetResultFromScriptAsync(Script script)
        {
            if ((from script1 in Scripts.Values where script1 == script select script1).Count() == 0)
            {
                throw new ArgumentException();
            }

            try
            {
                var result = await script.RunAsync();
                return result.ReturnValue;
            }
            catch
            {
                return null;
            }
        }

        public async Task<object> GetResultFromScriptAsync(string name)
        {
            return await this.GetResultFromScriptAsync(this.Scripts[name]);
        }

        private static bool IsValidScript(Script script)
        {
            Compilation compilation = script.GetCompilation();

            SyntaxTree syntaxTree = compilation.SyntaxTrees.First();
            CompilationUnitSyntax syntaxRootNode = syntaxTree.GetRoot() as CompilationUnitSyntax;
            SemanticModel semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees.First());

            foreach (UsingDirectiveSyntax usingDirectiveSyntax in syntaxRootNode.Usings)
            {
                if (IsIllegalAssemblyName(semanticModel.GetSymbolInfo(usingDirectiveSyntax.Name).Symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)))
                {
                    return false;
                }
            }

            bool IsIllegalAssemblyName(string assemblyName)
            {
                return assemblyName switch
                {
                    "System.Console" => true,
                    "System.Diagnostics.Debug" => true,
                    "System.IO" => true,
                    "System.IO.FileSystem" => true,
                    "System.IO.FileSystem.Primitives" => true,
                    "System.Reflection" => true,
                    "System.Reflection.Extensions" => true,
                    "System.Runtime" => true,
                    "System.Runtime.Extensions" => true,
                    "System.Runtime.InteropServices" => true,
                    "System.Diagnostics.Tracing" => true,
                    "System.Runtime.CompilerServices.Unsafe" => true,
                    "System.Memory" => true,
                    "System.Reflection.Emit.ILGeneration" => true,
                    "System.Diagnostics.Tools" => true,
                    "System.Reflection.Metadata" => true,
                    "System.IO.Compression" => true,
                    "System.IO.MemoryMappedFiles" => true,
                    "System.Diagnostics.FileVersionInfo" => true,
                    "Microsoft.Win32.Registry" => true,
                    "System.Security.AccessControl" => true,
                    _ => false,
                };
            }

            return true;
        }
    }
}