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
		T__17=18, InlineLabel=19, Type=20, BinaryOp=21, Num=22, Id=23, Whitespace=24, 
		Newline=25;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "InlineLabel", "Type", "BinaryOp", "Num", "Digit", "Id", "Letter", 
		"Whitespace", "Newline"
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
		"'param_decl'", "'end_func'", "'func'", "'='", "'return'", "'Je'", "'param'", 
		"'call'", "'end'", "'@'", "'['", "']'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, "InlineLabel", "Type", "BinaryOp", 
		"Num", "Id", "Whitespace", "Newline"
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
		'\x5964', '\x2', '\x1B', '\xE8', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x3', '\x2', '\x3', 
		'\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', 
		'\a', '\x3', '\a', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', 
		'\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', 
		'\b', '\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', 
		'\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', 
		'\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\v', 
		'\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', 
		'\f', '\x3', '\f', '\x3', '\f', '\x3', '\r', '\x3', '\r', '\x3', '\r', 
		'\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', 
		'\x3', '\xE', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', 
		'\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', 
		'\x3', '\x11', '\x3', '\x11', '\x3', '\x12', '\x3', '\x12', '\x3', '\x13', 
		'\x3', '\x13', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', 
		'\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', 
		'\x5', '\x15', '\xB3', '\n', '\x15', '\x3', '\x16', '\x3', '\x16', '\x3', 
		'\x16', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', '\x3', 
		'\x16', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', '\x3', 
		'\x16', '\x3', '\x16', '\x3', '\x16', '\x5', '\x16', '\xC4', '\n', '\x16', 
		'\x3', '\x17', '\x5', '\x17', '\xC7', '\n', '\x17', '\x3', '\x17', '\x6', 
		'\x17', '\xCA', '\n', '\x17', '\r', '\x17', '\xE', '\x17', '\xCB', '\x3', 
		'\x18', '\x3', '\x18', '\x3', '\x19', '\x3', '\x19', '\a', '\x19', '\xD2', 
		'\n', '\x19', '\f', '\x19', '\xE', '\x19', '\xD5', '\v', '\x19', '\x3', 
		'\x1A', '\x3', '\x1A', '\x3', '\x1B', '\x6', '\x1B', '\xDA', '\n', '\x1B', 
		'\r', '\x1B', '\xE', '\x1B', '\xDB', '\x3', '\x1B', '\x3', '\x1B', '\x3', 
		'\x1C', '\x3', '\x1C', '\x5', '\x1C', '\xE2', '\n', '\x1C', '\x3', '\x1C', 
		'\x5', '\x1C', '\xE5', '\n', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x2', 
		'\x2', '\x1D', '\x3', '\x3', '\x5', '\x4', '\a', '\x5', '\t', '\x6', '\v', 
		'\a', '\r', '\b', '\xF', '\t', '\x11', '\n', '\x13', '\v', '\x15', '\f', 
		'\x17', '\r', '\x19', '\xE', '\x1B', '\xF', '\x1D', '\x10', '\x1F', '\x11', 
		'!', '\x12', '#', '\x13', '%', '\x14', '\'', '\x15', ')', '\x16', '+', 
		'\x17', '-', '\x18', '/', '\x2', '\x31', '\x19', '\x33', '\x2', '\x35', 
		'\x1A', '\x37', '\x1B', '\x3', '\x2', '\t', '\a', '\x2', '(', '(', '-', 
		'-', '/', '/', '`', '`', '~', '~', '\x4', '\x2', '>', '>', '@', '@', '\x5', 
		'\x2', '\'', '\'', ',', ',', '\x31', '\x31', '\x3', '\x2', '\x32', ';', 
		'\x4', '\x2', '\x43', '\\', '\x63', '|', '\x5', '\x2', '\x32', ';', '\x43', 
		'\\', '\x63', '|', '\x4', '\x2', '\v', '\v', '\"', '\"', '\x2', '\xF6', 
		'\x2', '\x3', '\x3', '\x2', '\x2', '\x2', '\x2', '\x5', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', 
		')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', '\x31', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', '\x37', '\x3', 
		'\x2', '\x2', '\x2', '\x3', '\x39', '\x3', '\x2', '\x2', '\x2', '\x5', 
		';', '\x3', '\x2', '\x2', '\x2', '\a', '\x45', '\x3', '\x2', '\x2', '\x2', 
		'\t', 'G', '\x3', '\x2', '\x2', '\x2', '\v', 'N', '\x3', '\x2', '\x2', 
		'\x2', '\r', 'W', '\x3', '\x2', '\x2', '\x2', '\xF', '`', '\x3', '\x2', 
		'\x2', '\x2', '\x11', 'k', '\x3', '\x2', '\x2', '\x2', '\x13', 't', '\x3', 
		'\x2', '\x2', '\x2', '\x15', 'y', '\x3', '\x2', '\x2', '\x2', '\x17', 
		'{', '\x3', '\x2', '\x2', '\x2', '\x19', '\x82', '\x3', '\x2', '\x2', 
		'\x2', '\x1B', '\x85', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x8B', '\x3', 
		'\x2', '\x2', '\x2', '\x1F', '\x90', '\x3', '\x2', '\x2', '\x2', '!', 
		'\x94', '\x3', '\x2', '\x2', '\x2', '#', '\x96', '\x3', '\x2', '\x2', 
		'\x2', '%', '\x98', '\x3', '\x2', '\x2', '\x2', '\'', '\x9A', '\x3', '\x2', 
		'\x2', '\x2', ')', '\xB2', '\x3', '\x2', '\x2', '\x2', '+', '\xC3', '\x3', 
		'\x2', '\x2', '\x2', '-', '\xC6', '\x3', '\x2', '\x2', '\x2', '/', '\xCD', 
		'\x3', '\x2', '\x2', '\x2', '\x31', '\xCF', '\x3', '\x2', '\x2', '\x2', 
		'\x33', '\xD6', '\x3', '\x2', '\x2', '\x2', '\x35', '\xD9', '\x3', '\x2', 
		'\x2', '\x2', '\x37', '\xE4', '\x3', '\x2', '\x2', '\x2', '\x39', ':', 
		'\a', '<', '\x2', '\x2', ':', '\x4', '\x3', '\x2', '\x2', '\x2', ';', 
		'<', '\a', 'h', '\x2', '\x2', '<', '=', '\a', 'w', '\x2', '\x2', '=', 
		'>', '\a', 'p', '\x2', '\x2', '>', '?', '\a', '\x65', '\x2', '\x2', '?', 
		'@', '\a', '\x61', '\x2', '\x2', '@', '\x41', '\a', '\x66', '\x2', '\x2', 
		'\x41', '\x42', '\a', 'g', '\x2', '\x2', '\x42', '\x43', '\a', '\x65', 
		'\x2', '\x2', '\x43', '\x44', '\a', 'n', '\x2', '\x2', '\x44', '\x6', 
		'\x3', '\x2', '\x2', '\x2', '\x45', '\x46', '\a', '=', '\x2', '\x2', '\x46', 
		'\b', '\x3', '\x2', '\x2', '\x2', 'G', 'H', '\a', 'i', '\x2', '\x2', 'H', 
		'I', '\a', 'n', '\x2', '\x2', 'I', 'J', '\a', 'q', '\x2', '\x2', 'J', 
		'K', '\a', '\x64', '\x2', '\x2', 'K', 'L', '\a', '\x63', '\x2', '\x2', 
		'L', 'M', '\a', 'n', '\x2', '\x2', 'M', '\n', '\x3', '\x2', '\x2', '\x2', 
		'N', 'O', '\a', '\x66', '\x2', '\x2', 'O', 'P', '\a', 'g', '\x2', '\x2', 
		'P', 'Q', '\a', '\x65', '\x2', '\x2', 'Q', 'R', '\a', 'n', '\x2', '\x2', 
		'R', 'S', '\a', '\x61', '\x2', '\x2', 'S', 'T', '\a', 'x', '\x2', '\x2', 
		'T', 'U', '\a', '\x63', '\x2', '\x2', 'U', 'V', '\a', 't', '\x2', '\x2', 
		'V', '\f', '\x3', '\x2', '\x2', '\x2', 'W', 'X', '\a', '\x66', '\x2', 
		'\x2', 'X', 'Y', '\a', 'g', '\x2', '\x2', 'Y', 'Z', '\a', '\x65', '\x2', 
		'\x2', 'Z', '[', '\a', 'n', '\x2', '\x2', '[', '\\', '\a', '\x61', '\x2', 
		'\x2', '\\', ']', '\a', '\x63', '\x2', '\x2', ']', '^', '\a', 't', '\x2', 
		'\x2', '^', '_', '\a', 't', '\x2', '\x2', '_', '\xE', '\x3', '\x2', '\x2', 
		'\x2', '`', '\x61', '\a', 'r', '\x2', '\x2', '\x61', '\x62', '\a', '\x63', 
		'\x2', '\x2', '\x62', '\x63', '\a', 't', '\x2', '\x2', '\x63', '\x64', 
		'\a', '\x63', '\x2', '\x2', '\x64', '\x65', '\a', 'o', '\x2', '\x2', '\x65', 
		'\x66', '\a', '\x61', '\x2', '\x2', '\x66', 'g', '\a', '\x66', '\x2', 
		'\x2', 'g', 'h', '\a', 'g', '\x2', '\x2', 'h', 'i', '\a', '\x65', '\x2', 
		'\x2', 'i', 'j', '\a', 'n', '\x2', '\x2', 'j', '\x10', '\x3', '\x2', '\x2', 
		'\x2', 'k', 'l', '\a', 'g', '\x2', '\x2', 'l', 'm', '\a', 'p', '\x2', 
		'\x2', 'm', 'n', '\a', '\x66', '\x2', '\x2', 'n', 'o', '\a', '\x61', '\x2', 
		'\x2', 'o', 'p', '\a', 'h', '\x2', '\x2', 'p', 'q', '\a', 'w', '\x2', 
		'\x2', 'q', 'r', '\a', 'p', '\x2', '\x2', 'r', 's', '\a', '\x65', '\x2', 
		'\x2', 's', '\x12', '\x3', '\x2', '\x2', '\x2', 't', 'u', '\a', 'h', '\x2', 
		'\x2', 'u', 'v', '\a', 'w', '\x2', '\x2', 'v', 'w', '\a', 'p', '\x2', 
		'\x2', 'w', 'x', '\a', '\x65', '\x2', '\x2', 'x', '\x14', '\x3', '\x2', 
		'\x2', '\x2', 'y', 'z', '\a', '?', '\x2', '\x2', 'z', '\x16', '\x3', '\x2', 
		'\x2', '\x2', '{', '|', '\a', 't', '\x2', '\x2', '|', '}', '\a', 'g', 
		'\x2', '\x2', '}', '~', '\a', 'v', '\x2', '\x2', '~', '\x7F', '\a', 'w', 
		'\x2', '\x2', '\x7F', '\x80', '\a', 't', '\x2', '\x2', '\x80', '\x81', 
		'\a', 'p', '\x2', '\x2', '\x81', '\x18', '\x3', '\x2', '\x2', '\x2', '\x82', 
		'\x83', '\a', 'L', '\x2', '\x2', '\x83', '\x84', '\a', 'g', '\x2', '\x2', 
		'\x84', '\x1A', '\x3', '\x2', '\x2', '\x2', '\x85', '\x86', '\a', 'r', 
		'\x2', '\x2', '\x86', '\x87', '\a', '\x63', '\x2', '\x2', '\x87', '\x88', 
		'\a', 't', '\x2', '\x2', '\x88', '\x89', '\a', '\x63', '\x2', '\x2', '\x89', 
		'\x8A', '\a', 'o', '\x2', '\x2', '\x8A', '\x1C', '\x3', '\x2', '\x2', 
		'\x2', '\x8B', '\x8C', '\a', '\x65', '\x2', '\x2', '\x8C', '\x8D', '\a', 
		'\x63', '\x2', '\x2', '\x8D', '\x8E', '\a', 'n', '\x2', '\x2', '\x8E', 
		'\x8F', '\a', 'n', '\x2', '\x2', '\x8F', '\x1E', '\x3', '\x2', '\x2', 
		'\x2', '\x90', '\x91', '\a', 'g', '\x2', '\x2', '\x91', '\x92', '\a', 
		'p', '\x2', '\x2', '\x92', '\x93', '\a', '\x66', '\x2', '\x2', '\x93', 
		' ', '\x3', '\x2', '\x2', '\x2', '\x94', '\x95', '\a', '\x42', '\x2', 
		'\x2', '\x95', '\"', '\x3', '\x2', '\x2', '\x2', '\x96', '\x97', '\a', 
		']', '\x2', '\x2', '\x97', '$', '\x3', '\x2', '\x2', '\x2', '\x98', '\x99', 
		'\a', '_', '\x2', '\x2', '\x99', '&', '\x3', '\x2', '\x2', '\x2', '\x9A', 
		'\x9B', '\a', 'n', '\x2', '\x2', '\x9B', '\x9C', '\a', '\x63', '\x2', 
		'\x2', '\x9C', '\x9D', '\a', '\x64', '\x2', '\x2', '\x9D', '\x9E', '\a', 
		'g', '\x2', '\x2', '\x9E', '\x9F', '\a', 'n', '\x2', '\x2', '\x9F', '\xA0', 
		'\x3', '\x2', '\x2', '\x2', '\xA0', '\xA1', '\x5', '-', '\x17', '\x2', 
		'\xA1', '(', '\x3', '\x2', '\x2', '\x2', '\xA2', '\xA3', '\a', 'k', '\x2', 
		'\x2', '\xA3', '\xA4', '\a', 'p', '\x2', '\x2', '\xA4', '\xB3', '\a', 
		'v', '\x2', '\x2', '\xA5', '\xA6', '\a', 'u', '\x2', '\x2', '\xA6', '\xA7', 
		'\a', 'j', '\x2', '\x2', '\xA7', '\xA8', '\a', 'q', '\x2', '\x2', '\xA8', 
		'\xA9', '\a', 't', '\x2', '\x2', '\xA9', '\xB3', '\a', 'v', '\x2', '\x2', 
		'\xAA', '\xAB', '\a', '\x65', '\x2', '\x2', '\xAB', '\xAC', '\a', 'j', 
		'\x2', '\x2', '\xAC', '\xAD', '\a', '\x63', '\x2', '\x2', '\xAD', '\xB3', 
		'\a', 't', '\x2', '\x2', '\xAE', '\xAF', '\a', 'x', '\x2', '\x2', '\xAF', 
		'\xB0', '\a', 'q', '\x2', '\x2', '\xB0', '\xB1', '\a', 'k', '\x2', '\x2', 
		'\xB1', '\xB3', '\a', '\x66', '\x2', '\x2', '\xB2', '\xA2', '\x3', '\x2', 
		'\x2', '\x2', '\xB2', '\xA5', '\x3', '\x2', '\x2', '\x2', '\xB2', '\xAA', 
		'\x3', '\x2', '\x2', '\x2', '\xB2', '\xAE', '\x3', '\x2', '\x2', '\x2', 
		'\xB3', '*', '\x3', '\x2', '\x2', '\x2', '\xB4', '\xC4', '\t', '\x2', 
		'\x2', '\x2', '\xB5', '\xB6', '\a', '>', '\x2', '\x2', '\xB6', '\xC4', 
		'\a', '>', '\x2', '\x2', '\xB7', '\xB8', '\a', '@', '\x2', '\x2', '\xB8', 
		'\xC4', '\a', '@', '\x2', '\x2', '\xB9', '\xC4', '\t', '\x3', '\x2', '\x2', 
		'\xBA', '\xBB', '\a', '@', '\x2', '\x2', '\xBB', '\xC4', '\a', '?', '\x2', 
		'\x2', '\xBC', '\xBD', '\a', '>', '\x2', '\x2', '\xBD', '\xC4', '\a', 
		'?', '\x2', '\x2', '\xBE', '\xBF', '\a', '?', '\x2', '\x2', '\xBF', '\xC4', 
		'\a', '?', '\x2', '\x2', '\xC0', '\xC1', '\a', '#', '\x2', '\x2', '\xC1', 
		'\xC4', '\a', '?', '\x2', '\x2', '\xC2', '\xC4', '\t', '\x4', '\x2', '\x2', 
		'\xC3', '\xB4', '\x3', '\x2', '\x2', '\x2', '\xC3', '\xB5', '\x3', '\x2', 
		'\x2', '\x2', '\xC3', '\xB7', '\x3', '\x2', '\x2', '\x2', '\xC3', '\xB9', 
		'\x3', '\x2', '\x2', '\x2', '\xC3', '\xBA', '\x3', '\x2', '\x2', '\x2', 
		'\xC3', '\xBC', '\x3', '\x2', '\x2', '\x2', '\xC3', '\xBE', '\x3', '\x2', 
		'\x2', '\x2', '\xC3', '\xC0', '\x3', '\x2', '\x2', '\x2', '\xC3', '\xC2', 
		'\x3', '\x2', '\x2', '\x2', '\xC4', ',', '\x3', '\x2', '\x2', '\x2', '\xC5', 
		'\xC7', '\a', '/', '\x2', '\x2', '\xC6', '\xC5', '\x3', '\x2', '\x2', 
		'\x2', '\xC6', '\xC7', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xC9', '\x3', 
		'\x2', '\x2', '\x2', '\xC8', '\xCA', '\x5', '/', '\x18', '\x2', '\xC9', 
		'\xC8', '\x3', '\x2', '\x2', '\x2', '\xCA', '\xCB', '\x3', '\x2', '\x2', 
		'\x2', '\xCB', '\xC9', '\x3', '\x2', '\x2', '\x2', '\xCB', '\xCC', '\x3', 
		'\x2', '\x2', '\x2', '\xCC', '.', '\x3', '\x2', '\x2', '\x2', '\xCD', 
		'\xCE', '\t', '\x5', '\x2', '\x2', '\xCE', '\x30', '\x3', '\x2', '\x2', 
		'\x2', '\xCF', '\xD3', '\t', '\x6', '\x2', '\x2', '\xD0', '\xD2', '\x5', 
		'\x33', '\x1A', '\x2', '\xD1', '\xD0', '\x3', '\x2', '\x2', '\x2', '\xD2', 
		'\xD5', '\x3', '\x2', '\x2', '\x2', '\xD3', '\xD1', '\x3', '\x2', '\x2', 
		'\x2', '\xD3', '\xD4', '\x3', '\x2', '\x2', '\x2', '\xD4', '\x32', '\x3', 
		'\x2', '\x2', '\x2', '\xD5', '\xD3', '\x3', '\x2', '\x2', '\x2', '\xD6', 
		'\xD7', '\t', '\a', '\x2', '\x2', '\xD7', '\x34', '\x3', '\x2', '\x2', 
		'\x2', '\xD8', '\xDA', '\t', '\b', '\x2', '\x2', '\xD9', '\xD8', '\x3', 
		'\x2', '\x2', '\x2', '\xDA', '\xDB', '\x3', '\x2', '\x2', '\x2', '\xDB', 
		'\xD9', '\x3', '\x2', '\x2', '\x2', '\xDB', '\xDC', '\x3', '\x2', '\x2', 
		'\x2', '\xDC', '\xDD', '\x3', '\x2', '\x2', '\x2', '\xDD', '\xDE', '\b', 
		'\x1B', '\x2', '\x2', '\xDE', '\x36', '\x3', '\x2', '\x2', '\x2', '\xDF', 
		'\xE1', '\a', '\xF', '\x2', '\x2', '\xE0', '\xE2', '\a', '\f', '\x2', 
		'\x2', '\xE1', '\xE0', '\x3', '\x2', '\x2', '\x2', '\xE1', '\xE2', '\x3', 
		'\x2', '\x2', '\x2', '\xE2', '\xE5', '\x3', '\x2', '\x2', '\x2', '\xE3', 
		'\xE5', '\a', '\f', '\x2', '\x2', '\xE4', '\xDF', '\x3', '\x2', '\x2', 
		'\x2', '\xE4', '\xE3', '\x3', '\x2', '\x2', '\x2', '\xE5', '\xE6', '\x3', 
		'\x2', '\x2', '\x2', '\xE6', '\xE7', '\b', '\x1C', '\x2', '\x2', '\xE7', 
		'\x38', '\x3', '\x2', '\x2', '\x2', '\v', '\x2', '\xB2', '\xC3', '\xC6', 
		'\xCB', '\xD3', '\xDB', '\xE1', '\xE4', '\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Backend
