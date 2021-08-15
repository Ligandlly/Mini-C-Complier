grammar Program;

// 程序由函数定义和全局变量声明组成
program: (decl)+;
decl: var_decl | func_def;

var_decl: (type_spec id ';')            # var_declHasId
	| (type_spec id '[' num ']' ';')    # var_declHasArr; // 变量声明

// 变量声明
primary_expr: '(' expr ')' | id | num;
postfix_expr:
	primary_expr                                # postfix_exprHasPrimary_expr
	| primary_expr '[' expr ']'                 # postfix_exprHasgetitem
	| primary_expr '(' ')'                      # postfix_exprHasEmptyCall
	| primary_expr '(' argument_expr_list ')'   # postfix_exprHasCall
	| primary_expr '++'                         # postfix_exprHasInc
	| primary_expr '--'                         # postfix_exprHasDec;

argument_expr_list: expr (',' expr)*;
unary_expr:
	postfix_expr        # unary_exprHasPostfix_expr
	| '++' unary_expr   # unary_exprHasInc
	| '--' unary_expr   # unary_exprHasDec
	| '$' unary_expr    # unary_exprHasDol
	| '!' unary_expr    # unary_exprHasLNot
	| '~' unary_expr    # unary_exprHasNot;

assignmentExpr:
	assignmentExpr '*' assignmentExpr           # assignmentExprHasMul
	| assignmentExpr '%' assignmentExpr         # assignmentExprHasMod
	| assignmentExpr '/' assignmentExpr         # assignmentExprHasDiv
	| assignmentExpr '+' assignmentExpr         # assignmentExprHasAdd
	| assignmentExpr '-' assignmentExpr         # assignmentExprHasMin
	| assignmentExpr '<<' assignmentExpr        # assignmentExprHasLsft
	| assignmentExpr '>>' assignmentExpr        # assignmentExprHasRsft
	| assignmentExpr '<=' assignmentExpr        # assignmentExprHasLe
	| assignmentExpr '>=' assignmentExpr        # assignmentExprHasGe
	| assignmentExpr '<' assignmentExpr         # assignmentExprHasLt
	| assignmentExpr '>' assignmentExpr         # assignmentExprHasGt
	| assignmentExpr '==' assignmentExpr        # assignmentExprHasEq
	| assignmentExpr '!=' assignmentExpr        # assignmentExprHasNe
	| assignmentExpr '&' assignmentExpr         # assignmentExprHasAnd
	| assignmentExpr '^' assignmentExpr         # assignmentExprHasXor
	| assignmentExpr '|' assignmentExpr         # assignmentExprHasOr
	| assignmentExpr '&&' assignmentExpr        # assignmentExprHasLAnd
	| assignmentExpr '||' assignmentExpr        # assignmentExprHasLOr
	| unary_expr '=' assignmentExpr             # assignmentExprHasAssign
	| unary_expr                                # assignmentExprHasUnary_expr
	| num                                       # assignmentExprHasNum ;

//assignmentOperator: '=';

expr: assignmentExpr;

func_def: (type_spec | VOID) id param_list compound_stmt; // 函数定义

compound_stmt: '{' '}'          # compound_stmtHasEmpty
    | '{' block_item_list '}'   # compound_stmtHasBody; // 块
    
block_item_list: block_item+;
block_item: (var_decl) | (stmt); // 块中只能有变量声明和statement
param_list: '(' (VOID)? ')'         # param_listHasEmpty
    | '(' param (',' param)* ')'    # param_listHasBody;
param: type_spec id | type_spec id '[' num? ']';

stmt:
	selection_stmt
	| iteration_stmt
	| return_stmt
	| continue_stmt
	| break_stmt
	| compound_stmt
	| expr_stmt; // break and continue are not included in stmt
	
continue_stmt: CONTINUE ';';

break_stmt: BREAK ';';

return_stmt: RETURN expr ';';

expr_stmt: expr ';';

selection_stmt:
	IF '(' expr ')' stmt ELSE stmt      # selection_stmtHasElse
	| IF '(' expr ')' stmt              # selection_stmtHasEmpty;
	
iteration_stmt: WHILE '(' expr ')' stmt;

num: Num;
id: Id;

Whitespace: [ \t]+ -> skip;
Newline: ( '\r' '\n'? | '\n') -> skip;
BlockComment: '/*' .*? '*/' -> skip;
LineComment: '//' ~[\r\n]* -> skip;

// LSFT: '<<'; // Operators RSFT: '>>'; LOR: '||'; LAND: '&&'; EQ: '=='; NE: '!='; LE: '<='; GE:
// '>='; LT: '<'; GT: '>'; MUL: '*'; DIV: '/'; MOD: '%'; OR: '|'; AND: '&'; XOR: '^'; NOT: '~';
// LNOT: '!'; ASSIGN: '='; DOL: '$'; INC: '++'; DEC: '--'; MIN: '-'; ADD: '+';
INT: 'int';
SHORT: 'short';
CHAR: 'char';
VOID: 'void';
type_spec: INT | SHORT | CHAR;
IF: 'if';
ELSE: 'else';
WHILE: 'while';
BREAK: 'break';
CONTINUE: 'continue';
RETURN: 'return';

Id: IdentifierNondigit ( IdentifierNondigit | Digit)*;

Num: HexadecimalConstant | DecimalConstant;

fragment IdentifierNondigit:
	Nondigit; //|   // other implementation-defined characters...

fragment Nondigit: [a-zA-Z_];

fragment Digit: [0-9];

fragment DecimalConstant: Digit+;

fragment HexadecimalConstant:
	HexadecimalPrefix HexadecimalDigit+;

fragment HexadecimalPrefix: '0' [xX];

fragment NonzeroDigit: [1-9];

fragment OctalDigit: [0-7];

fragment HexadecimalDigit: [0-9a-fA-F];


// unary_operator: ADD | MIN | NOT | LNOT | INC | DEC | DOL; binary_op: ADD | MIN | MUL | DIV | MOD
// | LAND | LOR | OR | AND | XOR | RSFT | LSFT | GE | GT | LE | LT | NE | EQ;

// unaryOperator: '&' | '*' | '+' | '-' | '~' | '!';

// multiplicative_expr: unary_expr (('*' | '/' | '%') unary_expr)*;

// additive_expr: multiplicative_expr (('+' | '-') multiplicative_expr)*;

// sft_expr: additive_expr (('<<' | '>>') additive_expr)*;

// relational_expr: sft_expr (('<' | '>' | '<=' | '>=') sft_expr)*;

// equalityExpression: relational_expr (('==' | '!=') relational_expr)*;

// andExpression: equalityExpression ( '&' equalityExpression)*;

// exclusiveOrExpression: andExpression ('^' andExpression)*;

// inclusiveOrExpression: exclusiveOrExpression ('|' exclusiveOrExpression)*;

// logicalAndExpression: inclusiveOrExpression ('&&' inclusiveOrExpression)*;

// logicalOrExpression: logicalAndExpression ('||' logicalAndExpression)*;

// conditionalExpression: logicalOrExpression ('?' expr ':' conditionalExpression)?;

// assignmentExpression: conditionalExpression | unary_expr assignmentOperator assignmentExpression
// | num;