grammar Program;

program: funcDecl+ decls funcDefs;

funcDefs: funcDef+;

decls: decl*;

InlineLabel: 'label' Num;
label: InlineLabel ':';
stmt: (label)? quaternary | label;

decl: variableDecl | globalVariableDecl;

Type: 'int' | 'short' | 'char' | 'void';

funcDecl: 'func_decl' ';' Type ';' Id ';' Num ';';

globalVariableDecl:
	'global' ';' Type ';' variable ';' ';'
	| 'global' ';' Type ';' variable ';' Num ';';

variableDecl:
	'decl_var' ';' Type ';' variable ';' ';'
	| 'decl_arr' ';' Type ';' variable ';' Num ';'
	| 'param_decl' ';' Type ';' variable ';' Num ';'
	| 'param_decl' ';' Type ';' variable ';' ';';

funcTail: 'end_func' ';' ';' ';' ';';

funcHead: 'func' ';' Type ';' Id ';' Num ';';

funcDef: funcHead stmt* funcTail;

quaternary: unary | binary | return | jumpEqual | end | call;

operand: Num | variable;

UnaryOp: '$' | '!' | '~' | '=';

unary: UnaryOp ';' src = operand ';' ';' rlt = operand ';';

BinaryOp:
	'+'
	| '-'
	| '&'
	| '|'
	| '^'
	| '<<'
	| '>>'
	| '<'
	| '>'
	| '>='
	| '<='
	| '=='
	| '!='
	| '*'
	| '/'
	| '%';
binary:
	BinaryOp ';' left = operand ';' right = operand ';' rlt = operand ';';

return:
	'return' ';' variable ';' ';' ';'	# variableReturn
	| 'return' ';' Num ';' ';' ';'		# literalReturn;

jumpEqual:
	'Je' ';' left = operand ';' right = operand ';' rlt = InlineLabel ';';

param: 'param' ';' (variable | Num) ';' ';' ';';

call: param* 'call' ';' Id ';' Num ';' rlt = variable ';';

end: 'end' ';' ';' ';' ';';

variable:
	name = Id '@' scope = Id '[' offset = Num ']' '@' Id
	| name = Id '@' scope = Id;

Num: '-'? Digit+;
fragment Digit: [0-9];

Id: [a-zA-Z]Letter*;
fragment Letter: [a-zA-Z0-9];

Whitespace: [ \t]+ -> skip;
Newline: ( '\r' '\n'? | '\n') -> skip;
