grammar Program;

//program: stmt+;

decl 
    : variableDecl
    | funcDef
    ; 
    
Type 
    : 'int'
    | 'short'
    | 'char'
    | 'void'
    ;
    
variableDecl 
    : 'global' ';'  Type ';' Id ';' ';'             #globalVarDecl
    | 'decl_val' ';' Type  ';' Id ';' ';'           #localVarDecl
    | 'global_arr' ';' Type  ';' Id ';' Num ';'     #globalArrDecl
    | 'decl_arr' ';' Type  ';' Id ';' Num ';'       #localVarDecl
    ;

paramDecl
    : 'param_decl' ';' Type ';' Id ';' Num ';'   #arrayParam
    | 'param_decl' ';' Type ';' Id ';' ';'      #variableParam
    ;    
    
funcTail 
    : 'end_func' ';' ';' ';' ';'
    ;
    
funcDef
    : paramDecl* 'func' ';' Type ';' Id ';' Num ';'  quaternary* funcTail
    ;

quaternary: 
    literalAssignment
    | variableAssignment
    | addOrMinus
    | multiple
    | divide
    | return
    ;
    
literalAssignment: 
    '=' ';' Num ';' ';' Id ';';

variableAssignment: 
    '=' ';' Id ';' ';' Id ';';
    
AdditaveOp : '+' | '-' | '&' | '|' | '^' | '<<' | '>>'; 
addOrMinus 
    : AdditaveOp ';' Num ';' Num ';' Id ';' #digitAddOrMinus
    | AdditaveOp ';' Id ';' Num ';' Id ';'  #hybridAddOrMinus
    | AdditaveOp ';' Num ';' Id ';' Id ';'  #hybridAddOrMinus
    | AdditaveOp ';' Id ';' Id ';' Id ';'   #variableAddOrMinus
    ;
    
multiple
    : '*' ';' Num ';' Num ';' Id ';'    #digitMultiple
    | '*' ';' Id ';' Num ';' Id ';'     #hybridMultiple
    | '*' ';' Num ';' Id ';' Id ';'     #hybridMultiple
    | '*' ';' Id ';' Id ';' Id ';'      #variableMultiple
    ;

DivideOp : '/' | '%';
divide
    : DivideOp ';' Num ';' Num ';' Id ';'    #digitDivide
    | DivideOp ';' Id ';' Num ';' Id ';'     #hybridDivide
    | DivideOp ';' Num ';' Id ';' Id ';'     #hybridDivide
    | DivideOp ';' Id ';' Id ';' Id ';'      #variableDivide
    ;
    
return
    : 'return' ';' Id ';' ';' ';'       #variableReturn
    | 'return' ';' Num ';' ';' ';'      #literalReturn 
    ;

Num: Digit+;
fragment Digit: [0-9];

Id : Letter+;
fragment Letter: [a-zA-Z0-9];

Whitespace: [ \t]+ -> skip;
Newline: ( '\r' '\n'? | '\n') -> skip;