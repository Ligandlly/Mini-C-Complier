grammar Program;

program: decl* | stmt+;


Label : 'label' Num ':';
stmt : Label* quaternary;

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
    : 'decl_var' ';' Type  ';' variable ';' ';'           #localVarDecl
    | 'decl_arr' ';' Type  ';' variable';' Num ';'       #localArrDecl
    ;

paramDecl
    : 'param_decl' ';' Type ';' variable ';' Num ';'   #arrayParam
    | 'param_decl' ';' Type ';' variable ';' ';'      #variableParam
    ;    
    
funcTail 
    : 'end_func' ';' ';' ';' ';'
    ;

funcHead
    : paramDecl* 'func' ';' Type ';' Id ';' Num ';'
    ;    

funcDef
    : funcHead  quaternary* funcTail
    ;

quaternary: 
    literalAssignment
    | variableAssignment
    | addOrMinus
    | multiple
    | divide
    | return
    | jumpEqual
    | end
    | call
    ;
    
literalAssignment: 
    '=' ';' Num ';' ';' variable ';';

variableAssignment: 
    '=' ';' variable ';' ';' variable ';';
    
AdditaveOp : '+' | '-' | '&' | '|' | '^' | '<<' | '>>'; 
addOrMinus 
    : AdditaveOp ';' left=Num ';' right=Num ';' rlt=variable ';'  #digitAddOrMinus
    | AdditaveOp ';' left=variable  ';' right=Num ';' rlt=variable ';'  #idFirstAddOrMinus
    | AdditaveOp ';' left=Num ';' right=variable  ';' rlt=variable ';'  #numFirstAddOrMinus
    | AdditaveOp ';' left=variable  ';' right=variable  ';' rlt=variable ';'  #idAddOrMinus
    ;
    
multiple
    : '*' ';' left=Num ';' right=Num ';' rlt=variable ';'      #digitMultiple
    | '*' ';' left=variable  ';' right=Num ';' rlt=variable ';'      #idFirstMultiple
    | '*' ';' left=Num ';' right=variable  ';' rlt=variable ';'      #numFirstMultiple
    | '*' ';' left=variable  ';' right=variable  ';' rlt=variable ';'      #idMultiple
    ;

DivideOp : '/' | '%';
divide
    : DivideOp ';' left=Num ';' right=Num ';' rlt=variable ';'      #digitDivide
    | DivideOp ';' left=variable  ';' right=Num ';' rlt=variable ';'      #idFirstDivide
    | DivideOp ';' left=Num ';' right=variable  ';' rlt=variable ';'      #numFirstDivide
    | DivideOp ';' left=variable  ';' right=variable  ';' rlt=variable ';'      #idDivide
    ;
    
return
    : 'return' ';' variable ';' ';' ';'       #variableReturn
    | 'return' ';' Num ';' ';' ';'      #literalReturn 
    ;
    
jumpEqual 
    : 'Je' ';' left=Num ';' right=Num ';' rlt=Num ';'   #digitJumpEqual
    | 'Je' ';' left=variable  ';' right=Num ';' rlt=Num ';'   #idFirstJumpEqual
    | 'Je' ';' left=Num ';' right=variable  ';' rlt=Num ';'   #numFirstJumpEqual
    | 'Je' ';' left=variable  ';' right=variable  ';' rlt=Num ';'   #idJumpEqual 
    ;
    
param
    : 'param' ';' variable ';' ';' ';'
    ;
    
call
    : param* 'call' ';' Id ';' Num ';' rlt=variable ';'
    ;
    
end : 'end' ';' ';' ';' ';';

variable 
    : name=Id '@' scope=Id '[' offset=Num ']' '@' Id 
    | name=Id '@' scope=Id
    ;

Num: Digit+;
fragment Digit: [0-9];

Id : [a-zA-Z]Letter*;
fragment Letter: [a-zA-Z0-9];

Whitespace: [ \t]+ -> skip;
Newline: ( '\r' '\n'? | '\n') -> skip;