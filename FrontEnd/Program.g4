grammar Program;

// 程序由函数定义和全局变量声明组成
program: (decl)+;
decl: var_decl | func_def;

var_decl: (type_spec id ';')            # var_declHasId
	| (type_spec id '[' num ']' ';')    # var_declHasArr; // 变量声明

// 变量声明
primary_expr: 
    '(' expr ')'    # primary_exprHasExpr
    | id            # primary_exprHasId
    | num           # primary_exprHasNum ;
postfix_expr:
	primary_expr                                # postfix_exprHasPrimary_expr
	| primary_expr '[' expr ']'                 # postfix_exprHasgetitem
	| id '(' ')'                      # postfix_exprHasEmptyCall
	| id '(' argument_expr_list ')'   # postfix_exprHasCall;

argument_expr_list: expr (',' expr)*;
unary_expr:
	postfix_expr        # unary_exprHasPostfix_expr
	| prefix='++' unary_expr   # unary_exprHasInc
	| prefix='--' unary_expr   # unary_exprHasDec
	| prefix='$' unary_expr    # unary_exprHasDol
	| prefix='!' unary_expr    # unary_exprHasLNot
	| prefix='~' unary_expr    # unary_exprHasNot;

assignmentExpr:
    binaryExpr                                      # assignmentExprHasBinary
	| unary_expr '=' assignmentExpr                 # assignmentExprHasAssign ;

binaryExpr :
	  left=binaryExpr op='*'    right=binaryExpr        
	| left=binaryExpr op='%'    right=binaryExpr        
	| left=binaryExpr op='/'    right=binaryExpr        
	| left=binaryExpr op='+'    right=binaryExpr        
	| left=binaryExpr op='-'    right=binaryExpr        
	| left=binaryExpr op='<<'   right=binaryExpr       
	| left=binaryExpr op='>>'   right=binaryExpr       
	| left=binaryExpr op='<='   right=binaryExpr       
	| left=binaryExpr op='>='   right=binaryExpr       
	| left=binaryExpr op='<'    right=binaryExpr        
	| left=binaryExpr op='>'    right=binaryExpr        
	| left=binaryExpr op='=='   right=binaryExpr       
	| left=binaryExpr op='!='   right=binaryExpr       
	| left=binaryExpr op='&'    right=binaryExpr        
	| left=binaryExpr op='^'    right=binaryExpr        
	| left=binaryExpr op='|'    right=binaryExpr        
	| left=binaryExpr op='&&'   right=binaryExpr       
	| left=binaryExpr op='||'   right=binaryExpr
    | unary_expr
    | num;
	
//assignmentOperator: '=';

expr: assignmentExpr;

func_def: (type_spec | VOID) id param_list compound_stmt; // 函数定义

compound_stmt: '{' '}'          # compound_stmtHasEmpty
    | '{' block_item_list '}'   # compound_stmtHasBody; // 块
    
block_item_list: block_item+;
block_item: (var_decl) | (stmt); // 块中只能有变量声明和statement
param_list: '(' (VOID)? ')'         # param_listHasEmpty
    | '(' param (',' param)* ')'    # param_listHasBody;
    
param: type_spec id                 #paramHasInt
    | type_spec id '[' num? ']'     #paramHasArr;    

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

return_stmt: RETURN expr? ';';

expr_stmt: expr ';';

selection_stmt:
	IF '(' expr ')' ifStmt=stmt ELSE elseStmt=stmt      # selection_stmtHasElse
	| IF '(' expr ')' stmt              # selection_stmtHasEmpty;
	
iteration_stmt: WHILE '(' expr ')' stmt;

num: Num;
id: Id;

Whitespace: [ \t]+ -> skip;
Newline: ( '\r' '\n'? | '\n') -> skip;
BlockComment: '/*' .*? '*/' -> skip;
LineComment: '//' ~[\r\n]* -> skip;

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

fragment Nondigit: [a-zA-Z];

fragment Digit: [0-9];

fragment DecimalConstant: Digit+;

fragment HexadecimalConstant:
	HexadecimalPrefix HexadecimalDigit+;

fragment HexadecimalPrefix: '0' [xX];

fragment NonzeroDigit: [1-9];

fragment OctalDigit: [0-7];

fragment HexadecimalDigit: [0-9a-fA-F];
