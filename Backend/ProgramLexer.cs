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
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public partial class ProgramLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, InlineLabel=19, Type=20, UnaryOp=21, BinaryOp=22, Num=23, Id=24, 
		Whitespace=25, Newline=26;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "InlineLabel", "Type", "UnaryOp", "BinaryOp", "Num", "Digit", 
		"Id", "Letter", "Whitespace", "Newline"
	};


	public ProgramLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public ProgramLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "':'", "'func_decl'", "';'", "'global'", "'decl_var'", "'decl_arr'", 
		"'param_decl'", "'end_func'", "'func'", "'return'", "'Je'", "'J'", "'param'", 
		"'call'", "'end'", "'@'", "'['", "']'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, "InlineLabel", "Type", "UnaryOp", 
		"BinaryOp", "Num", "Id", "Whitespace", "Newline"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Program.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static ProgramLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x1C', '\xEC', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', 
		'\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', 
		'\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\t', '\x3', 
		'\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', 
		'\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', 
		'\n', '\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\v', 
		'\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', 
		'\f', '\x3', '\r', '\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', 
		'\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', '\xF', 
		'\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\x10', '\x3', '\x10', 
		'\x3', '\x10', '\x3', '\x10', '\x3', '\x11', '\x3', '\x11', '\x3', '\x12', 
		'\x3', '\x12', '\x3', '\x13', '\x3', '\x13', '\x3', '\x14', '\x3', '\x14', 
		'\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', 
		'\x3', '\x14', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x5', '\x15', '\xB5', '\n', '\x15', '\x3', 
		'\x16', '\x3', '\x16', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', 
		'\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', 
		'\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', 
		'\x17', '\x3', '\x17', '\x5', '\x17', '\xC8', '\n', '\x17', '\x3', '\x18', 
		'\x5', '\x18', '\xCB', '\n', '\x18', '\x3', '\x18', '\x6', '\x18', '\xCE', 
		'\n', '\x18', '\r', '\x18', '\xE', '\x18', '\xCF', '\x3', '\x19', '\x3', 
		'\x19', '\x3', '\x1A', '\x3', '\x1A', '\a', '\x1A', '\xD6', '\n', '\x1A', 
		'\f', '\x1A', '\xE', '\x1A', '\xD9', '\v', '\x1A', '\x3', '\x1B', '\x3', 
		'\x1B', '\x3', '\x1C', '\x6', '\x1C', '\xDE', '\n', '\x1C', '\r', '\x1C', 
		'\xE', '\x1C', '\xDF', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1D', '\x3', 
		'\x1D', '\x5', '\x1D', '\xE6', '\n', '\x1D', '\x3', '\x1D', '\x5', '\x1D', 
		'\xE9', '\n', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x2', '\x2', '\x1E', 
		'\x3', '\x3', '\x5', '\x4', '\a', '\x5', '\t', '\x6', '\v', '\a', '\r', 
		'\b', '\xF', '\t', '\x11', '\n', '\x13', '\v', '\x15', '\f', '\x17', '\r', 
		'\x19', '\xE', '\x1B', '\xF', '\x1D', '\x10', '\x1F', '\x11', '!', '\x12', 
		'#', '\x13', '%', '\x14', '\'', '\x15', ')', '\x16', '+', '\x17', '-', 
		'\x18', '/', '\x19', '\x31', '\x2', '\x33', '\x1A', '\x35', '\x2', '\x37', 
		'\x1B', '\x39', '\x1C', '\x3', '\x2', '\n', '\x6', '\x2', '#', '#', '&', 
		'&', '?', '?', '\x80', '\x80', '\a', '\x2', '(', '(', '-', '-', '/', '/', 
		'`', '`', '~', '~', '\x4', '\x2', '>', '>', '@', '@', '\x5', '\x2', '\'', 
		'\'', ',', ',', '\x31', '\x31', '\x3', '\x2', '\x32', ';', '\x4', '\x2', 
		'\x43', '\\', '\x63', '|', '\x5', '\x2', '\x32', ';', '\x43', '\\', '\x63', 
		'|', '\x4', '\x2', '\v', '\v', '\"', '\"', '\x2', '\xFA', '\x2', '\x3', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x5', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', '\x2', '\x11', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1B', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', ')', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', '\x2', '-', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x33', '\x3', '\x2', '\x2', '\x2', '\x2', '\x37', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x39', '\x3', '\x2', '\x2', '\x2', '\x3', ';', '\x3', '\x2', 
		'\x2', '\x2', '\x5', '=', '\x3', '\x2', '\x2', '\x2', '\a', 'G', '\x3', 
		'\x2', '\x2', '\x2', '\t', 'I', '\x3', '\x2', '\x2', '\x2', '\v', 'P', 
		'\x3', '\x2', '\x2', '\x2', '\r', 'Y', '\x3', '\x2', '\x2', '\x2', '\xF', 
		'\x62', '\x3', '\x2', '\x2', '\x2', '\x11', 'm', '\x3', '\x2', '\x2', 
		'\x2', '\x13', 'v', '\x3', '\x2', '\x2', '\x2', '\x15', '{', '\x3', '\x2', 
		'\x2', '\x2', '\x17', '\x82', '\x3', '\x2', '\x2', '\x2', '\x19', '\x85', 
		'\x3', '\x2', '\x2', '\x2', '\x1B', '\x87', '\x3', '\x2', '\x2', '\x2', 
		'\x1D', '\x8D', '\x3', '\x2', '\x2', '\x2', '\x1F', '\x92', '\x3', '\x2', 
		'\x2', '\x2', '!', '\x96', '\x3', '\x2', '\x2', '\x2', '#', '\x98', '\x3', 
		'\x2', '\x2', '\x2', '%', '\x9A', '\x3', '\x2', '\x2', '\x2', '\'', '\x9C', 
		'\x3', '\x2', '\x2', '\x2', ')', '\xB4', '\x3', '\x2', '\x2', '\x2', '+', 
		'\xB6', '\x3', '\x2', '\x2', '\x2', '-', '\xC7', '\x3', '\x2', '\x2', 
		'\x2', '/', '\xCA', '\x3', '\x2', '\x2', '\x2', '\x31', '\xD1', '\x3', 
		'\x2', '\x2', '\x2', '\x33', '\xD3', '\x3', '\x2', '\x2', '\x2', '\x35', 
		'\xDA', '\x3', '\x2', '\x2', '\x2', '\x37', '\xDD', '\x3', '\x2', '\x2', 
		'\x2', '\x39', '\xE8', '\x3', '\x2', '\x2', '\x2', ';', '<', '\a', '<', 
		'\x2', '\x2', '<', '\x4', '\x3', '\x2', '\x2', '\x2', '=', '>', '\a', 
		'h', '\x2', '\x2', '>', '?', '\a', 'w', '\x2', '\x2', '?', '@', '\a', 
		'p', '\x2', '\x2', '@', '\x41', '\a', '\x65', '\x2', '\x2', '\x41', '\x42', 
		'\a', '\x61', '\x2', '\x2', '\x42', '\x43', '\a', '\x66', '\x2', '\x2', 
		'\x43', '\x44', '\a', 'g', '\x2', '\x2', '\x44', '\x45', '\a', '\x65', 
		'\x2', '\x2', '\x45', '\x46', '\a', 'n', '\x2', '\x2', '\x46', '\x6', 
		'\x3', '\x2', '\x2', '\x2', 'G', 'H', '\a', '=', '\x2', '\x2', 'H', '\b', 
		'\x3', '\x2', '\x2', '\x2', 'I', 'J', '\a', 'i', '\x2', '\x2', 'J', 'K', 
		'\a', 'n', '\x2', '\x2', 'K', 'L', '\a', 'q', '\x2', '\x2', 'L', 'M', 
		'\a', '\x64', '\x2', '\x2', 'M', 'N', '\a', '\x63', '\x2', '\x2', 'N', 
		'O', '\a', 'n', '\x2', '\x2', 'O', '\n', '\x3', '\x2', '\x2', '\x2', 'P', 
		'Q', '\a', '\x66', '\x2', '\x2', 'Q', 'R', '\a', 'g', '\x2', '\x2', 'R', 
		'S', '\a', '\x65', '\x2', '\x2', 'S', 'T', '\a', 'n', '\x2', '\x2', 'T', 
		'U', '\a', '\x61', '\x2', '\x2', 'U', 'V', '\a', 'x', '\x2', '\x2', 'V', 
		'W', '\a', '\x63', '\x2', '\x2', 'W', 'X', '\a', 't', '\x2', '\x2', 'X', 
		'\f', '\x3', '\x2', '\x2', '\x2', 'Y', 'Z', '\a', '\x66', '\x2', '\x2', 
		'Z', '[', '\a', 'g', '\x2', '\x2', '[', '\\', '\a', '\x65', '\x2', '\x2', 
		'\\', ']', '\a', 'n', '\x2', '\x2', ']', '^', '\a', '\x61', '\x2', '\x2', 
		'^', '_', '\a', '\x63', '\x2', '\x2', '_', '`', '\a', 't', '\x2', '\x2', 
		'`', '\x61', '\a', 't', '\x2', '\x2', '\x61', '\xE', '\x3', '\x2', '\x2', 
		'\x2', '\x62', '\x63', '\a', 'r', '\x2', '\x2', '\x63', '\x64', '\a', 
		'\x63', '\x2', '\x2', '\x64', '\x65', '\a', 't', '\x2', '\x2', '\x65', 
		'\x66', '\a', '\x63', '\x2', '\x2', '\x66', 'g', '\a', 'o', '\x2', '\x2', 
		'g', 'h', '\a', '\x61', '\x2', '\x2', 'h', 'i', '\a', '\x66', '\x2', '\x2', 
		'i', 'j', '\a', 'g', '\x2', '\x2', 'j', 'k', '\a', '\x65', '\x2', '\x2', 
		'k', 'l', '\a', 'n', '\x2', '\x2', 'l', '\x10', '\x3', '\x2', '\x2', '\x2', 
		'm', 'n', '\a', 'g', '\x2', '\x2', 'n', 'o', '\a', 'p', '\x2', '\x2', 
		'o', 'p', '\a', '\x66', '\x2', '\x2', 'p', 'q', '\a', '\x61', '\x2', '\x2', 
		'q', 'r', '\a', 'h', '\x2', '\x2', 'r', 's', '\a', 'w', '\x2', '\x2', 
		's', 't', '\a', 'p', '\x2', '\x2', 't', 'u', '\a', '\x65', '\x2', '\x2', 
		'u', '\x12', '\x3', '\x2', '\x2', '\x2', 'v', 'w', '\a', 'h', '\x2', '\x2', 
		'w', 'x', '\a', 'w', '\x2', '\x2', 'x', 'y', '\a', 'p', '\x2', '\x2', 
		'y', 'z', '\a', '\x65', '\x2', '\x2', 'z', '\x14', '\x3', '\x2', '\x2', 
		'\x2', '{', '|', '\a', 't', '\x2', '\x2', '|', '}', '\a', 'g', '\x2', 
		'\x2', '}', '~', '\a', 'v', '\x2', '\x2', '~', '\x7F', '\a', 'w', '\x2', 
		'\x2', '\x7F', '\x80', '\a', 't', '\x2', '\x2', '\x80', '\x81', '\a', 
		'p', '\x2', '\x2', '\x81', '\x16', '\x3', '\x2', '\x2', '\x2', '\x82', 
		'\x83', '\a', 'L', '\x2', '\x2', '\x83', '\x84', '\a', 'g', '\x2', '\x2', 
		'\x84', '\x18', '\x3', '\x2', '\x2', '\x2', '\x85', '\x86', '\a', 'L', 
		'\x2', '\x2', '\x86', '\x1A', '\x3', '\x2', '\x2', '\x2', '\x87', '\x88', 
		'\a', 'r', '\x2', '\x2', '\x88', '\x89', '\a', '\x63', '\x2', '\x2', '\x89', 
		'\x8A', '\a', 't', '\x2', '\x2', '\x8A', '\x8B', '\a', '\x63', '\x2', 
		'\x2', '\x8B', '\x8C', '\a', 'o', '\x2', '\x2', '\x8C', '\x1C', '\x3', 
		'\x2', '\x2', '\x2', '\x8D', '\x8E', '\a', '\x65', '\x2', '\x2', '\x8E', 
		'\x8F', '\a', '\x63', '\x2', '\x2', '\x8F', '\x90', '\a', 'n', '\x2', 
		'\x2', '\x90', '\x91', '\a', 'n', '\x2', '\x2', '\x91', '\x1E', '\x3', 
		'\x2', '\x2', '\x2', '\x92', '\x93', '\a', 'g', '\x2', '\x2', '\x93', 
		'\x94', '\a', 'p', '\x2', '\x2', '\x94', '\x95', '\a', '\x66', '\x2', 
		'\x2', '\x95', ' ', '\x3', '\x2', '\x2', '\x2', '\x96', '\x97', '\a', 
		'\x42', '\x2', '\x2', '\x97', '\"', '\x3', '\x2', '\x2', '\x2', '\x98', 
		'\x99', '\a', ']', '\x2', '\x2', '\x99', '$', '\x3', '\x2', '\x2', '\x2', 
		'\x9A', '\x9B', '\a', '_', '\x2', '\x2', '\x9B', '&', '\x3', '\x2', '\x2', 
		'\x2', '\x9C', '\x9D', '\a', 'n', '\x2', '\x2', '\x9D', '\x9E', '\a', 
		'\x63', '\x2', '\x2', '\x9E', '\x9F', '\a', '\x64', '\x2', '\x2', '\x9F', 
		'\xA0', '\a', 'g', '\x2', '\x2', '\xA0', '\xA1', '\a', 'n', '\x2', '\x2', 
		'\xA1', '\xA2', '\x3', '\x2', '\x2', '\x2', '\xA2', '\xA3', '\x5', '/', 
		'\x18', '\x2', '\xA3', '(', '\x3', '\x2', '\x2', '\x2', '\xA4', '\xA5', 
		'\a', 'k', '\x2', '\x2', '\xA5', '\xA6', '\a', 'p', '\x2', '\x2', '\xA6', 
		'\xB5', '\a', 'v', '\x2', '\x2', '\xA7', '\xA8', '\a', 'u', '\x2', '\x2', 
		'\xA8', '\xA9', '\a', 'j', '\x2', '\x2', '\xA9', '\xAA', '\a', 'q', '\x2', 
		'\x2', '\xAA', '\xAB', '\a', 't', '\x2', '\x2', '\xAB', '\xB5', '\a', 
		'v', '\x2', '\x2', '\xAC', '\xAD', '\a', '\x65', '\x2', '\x2', '\xAD', 
		'\xAE', '\a', 'j', '\x2', '\x2', '\xAE', '\xAF', '\a', '\x63', '\x2', 
		'\x2', '\xAF', '\xB5', '\a', 't', '\x2', '\x2', '\xB0', '\xB1', '\a', 
		'x', '\x2', '\x2', '\xB1', '\xB2', '\a', 'q', '\x2', '\x2', '\xB2', '\xB3', 
		'\a', 'k', '\x2', '\x2', '\xB3', '\xB5', '\a', '\x66', '\x2', '\x2', '\xB4', 
		'\xA4', '\x3', '\x2', '\x2', '\x2', '\xB4', '\xA7', '\x3', '\x2', '\x2', 
		'\x2', '\xB4', '\xAC', '\x3', '\x2', '\x2', '\x2', '\xB4', '\xB0', '\x3', 
		'\x2', '\x2', '\x2', '\xB5', '*', '\x3', '\x2', '\x2', '\x2', '\xB6', 
		'\xB7', '\t', '\x2', '\x2', '\x2', '\xB7', ',', '\x3', '\x2', '\x2', '\x2', 
		'\xB8', '\xC8', '\t', '\x3', '\x2', '\x2', '\xB9', '\xBA', '\a', '>', 
		'\x2', '\x2', '\xBA', '\xC8', '\a', '>', '\x2', '\x2', '\xBB', '\xBC', 
		'\a', '@', '\x2', '\x2', '\xBC', '\xC8', '\a', '@', '\x2', '\x2', '\xBD', 
		'\xC8', '\t', '\x4', '\x2', '\x2', '\xBE', '\xBF', '\a', '@', '\x2', '\x2', 
		'\xBF', '\xC8', '\a', '?', '\x2', '\x2', '\xC0', '\xC1', '\a', '>', '\x2', 
		'\x2', '\xC1', '\xC8', '\a', '?', '\x2', '\x2', '\xC2', '\xC3', '\a', 
		'?', '\x2', '\x2', '\xC3', '\xC8', '\a', '?', '\x2', '\x2', '\xC4', '\xC5', 
		'\a', '#', '\x2', '\x2', '\xC5', '\xC8', '\a', '?', '\x2', '\x2', '\xC6', 
		'\xC8', '\t', '\x5', '\x2', '\x2', '\xC7', '\xB8', '\x3', '\x2', '\x2', 
		'\x2', '\xC7', '\xB9', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xBB', '\x3', 
		'\x2', '\x2', '\x2', '\xC7', '\xBD', '\x3', '\x2', '\x2', '\x2', '\xC7', 
		'\xBE', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xC0', '\x3', '\x2', '\x2', 
		'\x2', '\xC7', '\xC2', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xC4', '\x3', 
		'\x2', '\x2', '\x2', '\xC7', '\xC6', '\x3', '\x2', '\x2', '\x2', '\xC8', 
		'.', '\x3', '\x2', '\x2', '\x2', '\xC9', '\xCB', '\a', '/', '\x2', '\x2', 
		'\xCA', '\xC9', '\x3', '\x2', '\x2', '\x2', '\xCA', '\xCB', '\x3', '\x2', 
		'\x2', '\x2', '\xCB', '\xCD', '\x3', '\x2', '\x2', '\x2', '\xCC', '\xCE', 
		'\x5', '\x31', '\x19', '\x2', '\xCD', '\xCC', '\x3', '\x2', '\x2', '\x2', 
		'\xCE', '\xCF', '\x3', '\x2', '\x2', '\x2', '\xCF', '\xCD', '\x3', '\x2', 
		'\x2', '\x2', '\xCF', '\xD0', '\x3', '\x2', '\x2', '\x2', '\xD0', '\x30', 
		'\x3', '\x2', '\x2', '\x2', '\xD1', '\xD2', '\t', '\x6', '\x2', '\x2', 
		'\xD2', '\x32', '\x3', '\x2', '\x2', '\x2', '\xD3', '\xD7', '\t', '\a', 
		'\x2', '\x2', '\xD4', '\xD6', '\x5', '\x35', '\x1B', '\x2', '\xD5', '\xD4', 
		'\x3', '\x2', '\x2', '\x2', '\xD6', '\xD9', '\x3', '\x2', '\x2', '\x2', 
		'\xD7', '\xD5', '\x3', '\x2', '\x2', '\x2', '\xD7', '\xD8', '\x3', '\x2', 
		'\x2', '\x2', '\xD8', '\x34', '\x3', '\x2', '\x2', '\x2', '\xD9', '\xD7', 
		'\x3', '\x2', '\x2', '\x2', '\xDA', '\xDB', '\t', '\b', '\x2', '\x2', 
		'\xDB', '\x36', '\x3', '\x2', '\x2', '\x2', '\xDC', '\xDE', '\t', '\t', 
		'\x2', '\x2', '\xDD', '\xDC', '\x3', '\x2', '\x2', '\x2', '\xDE', '\xDF', 
		'\x3', '\x2', '\x2', '\x2', '\xDF', '\xDD', '\x3', '\x2', '\x2', '\x2', 
		'\xDF', '\xE0', '\x3', '\x2', '\x2', '\x2', '\xE0', '\xE1', '\x3', '\x2', 
		'\x2', '\x2', '\xE1', '\xE2', '\b', '\x1C', '\x2', '\x2', '\xE2', '\x38', 
		'\x3', '\x2', '\x2', '\x2', '\xE3', '\xE5', '\a', '\xF', '\x2', '\x2', 
		'\xE4', '\xE6', '\a', '\f', '\x2', '\x2', '\xE5', '\xE4', '\x3', '\x2', 
		'\x2', '\x2', '\xE5', '\xE6', '\x3', '\x2', '\x2', '\x2', '\xE6', '\xE9', 
		'\x3', '\x2', '\x2', '\x2', '\xE7', '\xE9', '\a', '\f', '\x2', '\x2', 
		'\xE8', '\xE3', '\x3', '\x2', '\x2', '\x2', '\xE8', '\xE7', '\x3', '\x2', 
		'\x2', '\x2', '\xE9', '\xEA', '\x3', '\x2', '\x2', '\x2', '\xEA', '\xEB', 
		'\b', '\x1D', '\x2', '\x2', '\xEB', ':', '\x3', '\x2', '\x2', '\x2', '\v', 
		'\x2', '\xB4', '\xC7', '\xCA', '\xCF', '\xD7', '\xDF', '\xE5', '\xE8', 
		'\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Backend
