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

namespace Frontend {

using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IProgramListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class ProgramBaseListener : IProgramListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgram([NotNull] ProgramParser.ProgramContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgram([NotNull] ProgramParser.ProgramContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDecl([NotNull] ProgramParser.DeclContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDecl([NotNull] ProgramParser.DeclContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>var_declHasId</c>
	/// labeled alternative in <see cref="ProgramParser.var_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterVar_declHasId([NotNull] ProgramParser.Var_declHasIdContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>var_declHasId</c>
	/// labeled alternative in <see cref="ProgramParser.var_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitVar_declHasId([NotNull] ProgramParser.Var_declHasIdContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>var_declHasArr</c>
	/// labeled alternative in <see cref="ProgramParser.var_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterVar_declHasArr([NotNull] ProgramParser.Var_declHasArrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>var_declHasArr</c>
	/// labeled alternative in <see cref="ProgramParser.var_decl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitVar_declHasArr([NotNull] ProgramParser.Var_declHasArrContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>primary_exprHasExpr</c>
	/// labeled alternative in <see cref="ProgramParser.primary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPrimary_exprHasExpr([NotNull] ProgramParser.Primary_exprHasExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>primary_exprHasExpr</c>
	/// labeled alternative in <see cref="ProgramParser.primary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPrimary_exprHasExpr([NotNull] ProgramParser.Primary_exprHasExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>primary_exprHasId</c>
	/// labeled alternative in <see cref="ProgramParser.primary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPrimary_exprHasId([NotNull] ProgramParser.Primary_exprHasIdContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>primary_exprHasId</c>
	/// labeled alternative in <see cref="ProgramParser.primary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPrimary_exprHasId([NotNull] ProgramParser.Primary_exprHasIdContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>primary_exprHasNum</c>
	/// labeled alternative in <see cref="ProgramParser.primary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPrimary_exprHasNum([NotNull] ProgramParser.Primary_exprHasNumContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>primary_exprHasNum</c>
	/// labeled alternative in <see cref="ProgramParser.primary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPrimary_exprHasNum([NotNull] ProgramParser.Primary_exprHasNumContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>postfix_exprHasPrimary_expr</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPostfix_exprHasPrimary_expr([NotNull] ProgramParser.Postfix_exprHasPrimary_exprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>postfix_exprHasPrimary_expr</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPostfix_exprHasPrimary_expr([NotNull] ProgramParser.Postfix_exprHasPrimary_exprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>postfix_exprHasgetitem</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPostfix_exprHasgetitem([NotNull] ProgramParser.Postfix_exprHasgetitemContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>postfix_exprHasgetitem</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPostfix_exprHasgetitem([NotNull] ProgramParser.Postfix_exprHasgetitemContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>postfix_exprHasEmptyCall</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPostfix_exprHasEmptyCall([NotNull] ProgramParser.Postfix_exprHasEmptyCallContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>postfix_exprHasEmptyCall</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPostfix_exprHasEmptyCall([NotNull] ProgramParser.Postfix_exprHasEmptyCallContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>postfix_exprHasCall</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPostfix_exprHasCall([NotNull] ProgramParser.Postfix_exprHasCallContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>postfix_exprHasCall</c>
	/// labeled alternative in <see cref="ProgramParser.postfix_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPostfix_exprHasCall([NotNull] ProgramParser.Postfix_exprHasCallContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.argument_expr_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArgument_expr_list([NotNull] ProgramParser.Argument_expr_listContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.argument_expr_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArgument_expr_list([NotNull] ProgramParser.Argument_expr_listContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>unary_exprHasPostfix_expr</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_exprHasPostfix_expr([NotNull] ProgramParser.Unary_exprHasPostfix_exprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>unary_exprHasPostfix_expr</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_exprHasPostfix_expr([NotNull] ProgramParser.Unary_exprHasPostfix_exprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>unary_exprHasInc</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_exprHasInc([NotNull] ProgramParser.Unary_exprHasIncContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>unary_exprHasInc</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_exprHasInc([NotNull] ProgramParser.Unary_exprHasIncContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>unary_exprHasDec</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_exprHasDec([NotNull] ProgramParser.Unary_exprHasDecContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>unary_exprHasDec</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_exprHasDec([NotNull] ProgramParser.Unary_exprHasDecContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>unary_exprHasDol</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_exprHasDol([NotNull] ProgramParser.Unary_exprHasDolContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>unary_exprHasDol</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_exprHasDol([NotNull] ProgramParser.Unary_exprHasDolContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>unary_exprHasLNot</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_exprHasLNot([NotNull] ProgramParser.Unary_exprHasLNotContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>unary_exprHasLNot</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_exprHasLNot([NotNull] ProgramParser.Unary_exprHasLNotContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>unary_exprHasNot</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary_exprHasNot([NotNull] ProgramParser.Unary_exprHasNotContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>unary_exprHasNot</c>
	/// labeled alternative in <see cref="ProgramParser.unary_expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary_exprHasNot([NotNull] ProgramParser.Unary_exprHasNotContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>assignmentExprHasBinary</c>
	/// labeled alternative in <see cref="ProgramParser.assignmentExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssignmentExprHasBinary([NotNull] ProgramParser.AssignmentExprHasBinaryContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>assignmentExprHasBinary</c>
	/// labeled alternative in <see cref="ProgramParser.assignmentExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssignmentExprHasBinary([NotNull] ProgramParser.AssignmentExprHasBinaryContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>assignmentExprHasAssign</c>
	/// labeled alternative in <see cref="ProgramParser.assignmentExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssignmentExprHasAssign([NotNull] ProgramParser.AssignmentExprHasAssignContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>assignmentExprHasAssign</c>
	/// labeled alternative in <see cref="ProgramParser.assignmentExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssignmentExprHasAssign([NotNull] ProgramParser.AssignmentExprHasAssignContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.binaryExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinaryExpr([NotNull] ProgramParser.BinaryExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.binaryExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinaryExpr([NotNull] ProgramParser.BinaryExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr([NotNull] ProgramParser.ExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr([NotNull] ProgramParser.ExprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.func_def"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunc_def([NotNull] ProgramParser.Func_defContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.func_def"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunc_def([NotNull] ProgramParser.Func_defContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>compound_stmtHasEmpty</c>
	/// labeled alternative in <see cref="ProgramParser.compound_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompound_stmtHasEmpty([NotNull] ProgramParser.Compound_stmtHasEmptyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>compound_stmtHasEmpty</c>
	/// labeled alternative in <see cref="ProgramParser.compound_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompound_stmtHasEmpty([NotNull] ProgramParser.Compound_stmtHasEmptyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>compound_stmtHasBody</c>
	/// labeled alternative in <see cref="ProgramParser.compound_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompound_stmtHasBody([NotNull] ProgramParser.Compound_stmtHasBodyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>compound_stmtHasBody</c>
	/// labeled alternative in <see cref="ProgramParser.compound_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompound_stmtHasBody([NotNull] ProgramParser.Compound_stmtHasBodyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.block_item_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlock_item_list([NotNull] ProgramParser.Block_item_listContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.block_item_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlock_item_list([NotNull] ProgramParser.Block_item_listContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.block_item"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlock_item([NotNull] ProgramParser.Block_itemContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.block_item"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlock_item([NotNull] ProgramParser.Block_itemContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>param_listHasEmpty</c>
	/// labeled alternative in <see cref="ProgramParser.param_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParam_listHasEmpty([NotNull] ProgramParser.Param_listHasEmptyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>param_listHasEmpty</c>
	/// labeled alternative in <see cref="ProgramParser.param_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParam_listHasEmpty([NotNull] ProgramParser.Param_listHasEmptyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>param_listHasBody</c>
	/// labeled alternative in <see cref="ProgramParser.param_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParam_listHasBody([NotNull] ProgramParser.Param_listHasBodyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>param_listHasBody</c>
	/// labeled alternative in <see cref="ProgramParser.param_list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParam_listHasBody([NotNull] ProgramParser.Param_listHasBodyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>paramHasInt</c>
	/// labeled alternative in <see cref="ProgramParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParamHasInt([NotNull] ProgramParser.ParamHasIntContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>paramHasInt</c>
	/// labeled alternative in <see cref="ProgramParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParamHasInt([NotNull] ProgramParser.ParamHasIntContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>paramHasArr</c>
	/// labeled alternative in <see cref="ProgramParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParamHasArr([NotNull] ProgramParser.ParamHasArrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>paramHasArr</c>
	/// labeled alternative in <see cref="ProgramParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParamHasArr([NotNull] ProgramParser.ParamHasArrContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStmt([NotNull] ProgramParser.StmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStmt([NotNull] ProgramParser.StmtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.continue_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterContinue_stmt([NotNull] ProgramParser.Continue_stmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.continue_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitContinue_stmt([NotNull] ProgramParser.Continue_stmtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.break_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBreak_stmt([NotNull] ProgramParser.Break_stmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.break_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBreak_stmt([NotNull] ProgramParser.Break_stmtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.return_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReturn_stmt([NotNull] ProgramParser.Return_stmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.return_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReturn_stmt([NotNull] ProgramParser.Return_stmtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.expr_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_stmt([NotNull] ProgramParser.Expr_stmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.expr_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_stmt([NotNull] ProgramParser.Expr_stmtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>selection_stmtHasElse</c>
	/// labeled alternative in <see cref="ProgramParser.selection_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSelection_stmtHasElse([NotNull] ProgramParser.Selection_stmtHasElseContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>selection_stmtHasElse</c>
	/// labeled alternative in <see cref="ProgramParser.selection_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSelection_stmtHasElse([NotNull] ProgramParser.Selection_stmtHasElseContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>selection_stmtHasEmpty</c>
	/// labeled alternative in <see cref="ProgramParser.selection_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSelection_stmtHasEmpty([NotNull] ProgramParser.Selection_stmtHasEmptyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>selection_stmtHasEmpty</c>
	/// labeled alternative in <see cref="ProgramParser.selection_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSelection_stmtHasEmpty([NotNull] ProgramParser.Selection_stmtHasEmptyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.iteration_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIteration_stmt([NotNull] ProgramParser.Iteration_stmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.iteration_stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIteration_stmt([NotNull] ProgramParser.Iteration_stmtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.num"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNum([NotNull] ProgramParser.NumContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.num"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNum([NotNull] ProgramParser.NumContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterId([NotNull] ProgramParser.IdContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.id"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitId([NotNull] ProgramParser.IdContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="ProgramParser.type_spec"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterType_spec([NotNull] ProgramParser.Type_specContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ProgramParser.type_spec"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitType_spec([NotNull] ProgramParser.Type_specContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace Frontend
