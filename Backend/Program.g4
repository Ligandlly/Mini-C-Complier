grammar Program;

program: funcDecl+ decls funcDefs;

funcDefs : funcDef+;

decls : decl*;

InlineLabel : 'label' Num;
label: InlineLabel ':';
stmt : (label)? quaternary | label;

decl 
    : variableDecl
    | paramDecl
    | globalVariableDecl
    ; 
    
Type 
    : 'int'
    | 'short'
    | 'char'
    | 'void'
    ;
    
funcDecl : 'func_decl' ';' Type ';' Id ';' Num ';';
    
globalVariableDecl
    : 'global' ';' Type  ';' variable ';' ';'         
    | 'global' ';' Type  ';' variable';' Num ';'      
    ;
    
variableDecl 
    : 'decl_var' ';' Type  ';' variable ';' ';'         
    | 'decl_arr' ';' Type  ';' variable';' Num ';'      
    ;

paramDecl
    : 'param_decl' ';' Type ';' variable ';' Num ';' 
    | 'param_decl' ';' Type ';' variable ';' ';'     
    ;    
    
funcTail 
    : 'end_func' ';' ';' ';' ';'
    ;

funcHead
    : 'func' ';' Type ';' Id ';' Num ';'
    ;    

funcDef
    : funcHead  stmt* funcTail
    ;

quaternary: 
    literalAssignment
    | variableAssignment
//    | addOrMinus
//    | multiple
//    | divide
    | binary
    | return
    | jumpEqual
    | end
    | call
    ;
    
literalAssignment: 
    '=' ';' Num ';' ';' variable ';';

variableAssignment: 
    '=' ';' variable ';' ';' variable ';';
    

operand : Num | variable;
BinaryOp : '+' | '-' | '&' | '|' | '^' | '<<' | '>>' | '<' | '>' | '>=' | '<=' | '==' | '!=' | '*' | '/' | '%' ; 
binary 
    : BinaryOp ';' left=operand ';' right=operand ';' rlt=operand ';' 
    ;
//    
//AdditaveOp : '+' | '-' | '&' | '|' | '^' | '<<' | '>>' | '<' | '>' | '>=' | '<=' | '==' | '!='; 
//addOrMinus 
//    : AdditaveOp ';' left=Num ';' right=Num ';' rlt=variable ';'  #digitAddOrMinus
//    | AdditaveOp ';' left=variable  ';' right=Num ';' rlt=variable ';'  #idFirstAddOrMinus
//    | AdditaveOp ';' left=Num ';' right=variable  ';' rlt=variable ';'  #numFirstAddOrMinus
//    | AdditaveOp ';' left=variable  ';' right=variable  ';' rlt=variable ';'  #idAddOrMinus
//    ;
//    
//multiple
//    : '*' ';' left=Num ';' right=Num ';' rlt=variable ';'      #digitMultiple
//    | '*' ';' left=variable  ';' right=Num ';' rlt=variable ';'      #idFirstMultiple
//    | '*' ';' left=Num ';' right=variable  ';' rlt=variable ';'      #numFirstMultiple
//    | '*' ';' left=variable  ';' right=variable  ';' rlt=variable ';'      #idMultiple
//    ;
//
//DivideOp : '/' | '%';
//divide
//    : DivideOp ';' left=Num ';' right=Num ';' rlt=variable ';'      #digitDivide
//    | DivideOp ';' left=variable  ';' right=Num ';' rlt=variable ';'      #idFirstDivide
//    | DivideOp ';' left=Num ';' right=variable  ';' rlt=variable ';'      #numFirstDivide
//    | DivideOp ';' left=variable  ';' right=variable  ';' rlt=variable ';'      #idDivide
//    ;
//    
return
    : 'return' ';' variable ';' ';' ';'       #variableReturn
    | 'return' ';' Num ';' ';' ';'      #literalReturn 
    ;
    
jumpEqual 
    : 'Je' ';' left=operand  ';' right=operand  ';' rlt=InlineLabel ';' 
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

Num: '-'? Digit+;
fragment Digit: [0-9];

Id : [a-zA-Z]Letter*;
fragment Letter: [a-zA-Z0-9];

Whitespace: [ \t]+ -> skip;
Newline: ( '\r' '\n'? | '\n') -> skip;