grammar IR;

statement: (Label ':')*  ;

fragment Word: ;
fragment Letter : [_a-zA-Z];
fragment Digit: [0-9];
fragment Label: 'label_' Letter;
fragment TmpVar: '@' Letter+;
