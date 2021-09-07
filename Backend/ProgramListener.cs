//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\Program.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Backend {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="ProgramParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface IProgramListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] ProgramParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] ProgramParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.funcDefs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncDefs([NotNull] ProgramParser.FuncDefsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.funcDefs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncDefs([NotNull] ProgramParser.FuncDefsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.decls"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecls([NotNull] ProgramParser.DeclsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.decls"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecls([NotNull] ProgramParser.DeclsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLabel([NotNull] ProgramParser.LabelContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLabel([NotNull] ProgramParser.LabelContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmt([NotNull] ProgramParser.StmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmt([NotNull] ProgramParser.StmtContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecl([NotNull] ProgramParser.DeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecl([NotNull] ProgramParser.DeclContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.funcDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncDecl([NotNull] ProgramParser.FuncDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.funcDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncDecl([NotNull] ProgramParser.FuncDeclContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.globalVariableDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGlobalVariableDecl([NotNull] ProgramParser.GlobalVariableDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.globalVariableDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGlobalVariableDecl([NotNull] ProgramParser.GlobalVariableDeclContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.variableDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariableDecl([NotNull] ProgramParser.VariableDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.variableDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariableDecl([NotNull] ProgramParser.VariableDeclContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.funcTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncTail([NotNull] ProgramParser.FuncTailContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.funcTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncTail([NotNull] ProgramParser.FuncTailContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.funcHead"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncHead([NotNull] ProgramParser.FuncHeadContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.funcHead"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncHead([NotNull] ProgramParser.FuncHeadContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.funcDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncDef([NotNull] ProgramParser.FuncDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.funcDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncDef([NotNull] ProgramParser.FuncDefContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.quaternary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQuaternary([NotNull] ProgramParser.QuaternaryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.quaternary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQuaternary([NotNull] ProgramParser.QuaternaryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperand([NotNull] ProgramParser.OperandContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperand([NotNull] ProgramParser.OperandContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.unary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnary([NotNull] ProgramParser.UnaryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.unary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnary([NotNull] ProgramParser.UnaryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.binary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinary([NotNull] ProgramParser.BinaryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.binary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinary([NotNull] ProgramParser.BinaryContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>variableReturn</c>
	/// labeled alternative in <see cref="ProgramParser.return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariableReturn([NotNull] ProgramParser.VariableReturnContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>variableReturn</c>
	/// labeled alternative in <see cref="ProgramParser.return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariableReturn([NotNull] ProgramParser.VariableReturnContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>literalReturn</c>
	/// labeled alternative in <see cref="ProgramParser.return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralReturn([NotNull] ProgramParser.LiteralReturnContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>literalReturn</c>
	/// labeled alternative in <see cref="ProgramParser.return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralReturn([NotNull] ProgramParser.LiteralReturnContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.jumpEqual"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterJumpEqual([NotNull] ProgramParser.JumpEqualContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.jumpEqual"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitJumpEqual([NotNull] ProgramParser.JumpEqualContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParam([NotNull] ProgramParser.ParamContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParam([NotNull] ProgramParser.ParamContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCall([NotNull] ProgramParser.CallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCall([NotNull] ProgramParser.CallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.end"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEnd([NotNull] ProgramParser.EndContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.end"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEnd([NotNull] ProgramParser.EndContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable([NotNull] ProgramParser.VariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable([NotNull] ProgramParser.VariableContext context);
}
} // namespace Backend