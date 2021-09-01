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
    : 'decl_var' ';' Type  ';' name=Id '@' scope=Id ';' ';'           #localVarDecl
    | 'decl_arr' ';' Type  ';' name=Id '@' scope=Id ';' Num ';'       #localArrDecl
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
    | jumpEqual
    | end
    ;
    
literalAssignment: 
    '=' ';' Num ';' ';' Id ';';

variableAssignment: 
    '=' ';' Id ';' ';' Id ';';
    
AdditaveOp : '+' | '-' | '&' | '|' | '^' | '<<' | '>>'; 
addOrMinus 
    : AdditaveOp ';' left=Num ';' right=Num ';' rlt=Id ';'  #digitAddOrMinus
    | AdditaveOp ';' left=Id  ';' right=Num ';' rlt=Id ';'  #idFirstAddOrMinus
    | AdditaveOp ';' left=Num ';' right=Id  ';' rlt=Id ';'  #numFirstAddOrMinus
    | AdditaveOp ';' left=Id  ';' right=Id  ';' rlt=Id ';'  #idAddOrMinus
    ;
    
multiple
    : '*' ';' left=Num ';' right=Num ';' rlt=Id ';'      #digitMultiple
    | '*' ';' left=Id  ';' right=Num ';' rlt=Id ';'      #idFirstMultiple
    | '*' ';' left=Num ';' right=Id  ';' rlt=Id ';'      #numFirstMultiple
    | '*' ';' left=Id  ';' right=Id  ';' rlt=Id ';'      #idMultiple
    ;

DivideOp : '/' | '%';
divide
    : DivideOp ';' left=Num ';' right=Num ';' rlt=Id ';'      #digitDivide
    | DivideOp ';' left=Id  ';' right=Num ';' rlt=Id ';'      #idFirstDivide
    | DivideOp ';' left=Num ';' right=Id  ';' rlt=Id ';'      #numFirstDivide
    | DivideOp ';' left=Id  ';' right=Id  ';' rlt=Id ';'      #idDivide
    ;
    
return
    : 'return' ';' Id ';' ';' ';'       #variableReturn
    | 'return' ';' Num ';' ';' ';'      #literalReturn 
    ;
    
jumpEqual 
    : 'Je' ';' left=Num ';' right=Num ';' rlt=Num ';'   #digitJumpEqual
    | 'Je' ';' left=Id  ';' right=Num ';' rlt=Num ';'   #idFirstJumpEqual
    | 'Je' ';' left=Num ';' right=Id  ';' rlt=Num ';'   #numFirstJumpEqual
    | 'Je' ';' left=Id  ';' right=Id  ';' rlt=Num ';'   #idJumpEqual 
    ;
    
end : 'end' ';' ';' ';' ';';

Num: Digit+;
fragment Digit: [0-9];

Id : [a-zA-Z]Letter*;
fragment Letter: [a-zA-Z0-9];

Whitespace: [ \t]+ -> skip;
Newline: ( '\r' '\n'? | '\n') -> skip;