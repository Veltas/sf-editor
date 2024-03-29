\ Copyright 2022-2023 Christopher Leonard, MIT Licence
\ Line-based block text editor

DECIMAL

WORDLIST CONSTANT EDITOR-WL

: EDITOR        GET-ORDER NIP EDITOR-WL SWAP SET-ORDER ;

ALSO EDITOR DEFINITIONS

: FORTH         FORTH ;
: LIST          LIST ;
: FLUSH         FLUSH ;

CREATE CHR 0 ,
CREATE 'FND 64 ALLOT
CREATE #FND 0 ,
CREATE 'INS 64 ALLOT
CREATE #INS 0 ,

: UMIN ( u1 u2 - u3)
                2DUP U< IF SWAP THEN NIP ;
: UMAX ( u1 u2 - u3)
                2DUP U< IF SWAP THEN DROP ;
: SWAP! ( a1 a2)
                2DUP @ SWAP @ ROT ! SWAP ! ;
: CSWAP! ( a1 a2)
                2DUP C@ SWAP C@ ROT C! SWAP C! ;
: LINE ( n - a)
                6 LSHIFT  SCR @ BLOCK + ;
: #LIN ( - n)   CHR @ 6 RSHIFT  15 MIN ;
: 'LIN ( - a)   #LIN LINE ;
: 'ELIN ( - a)  'LIN 64 + ;
: 'CHR ( - a)   CHR @  SCR @ BLOCK + ;
: PRI' ( c)     DUP 32 127 WITHIN
                IF EMIT ELSE DROP SPACE THEN ;
: PRI ( a1 a2)  SWAP ?DO I C@ PRI' LOOP ;
: TYPE-LINE'    'LIN 'CHR PRI  [CHAR] ^ EMIT  'CHR 'ELIN PRI ;
: TYPE-LINE     CR  TYPE-LINE'  SPACE #LIN . ;
: TRAIL ( - a n)
                'CHR  0 LINE 1024 + OVER - ;
: -FOUND        'FND #FND @ TYPE  ."  NONE"  ABORT ;
: FND           TRAIL 'FND #FND @ SEARCH NIP
                IF 0 LINE - #FND @ + CHR !  ELSE -FOUND THEN ;
: BUDGE         'CHR  DUP #INS @ + 'ELIN UMIN  'ELIN OVER -
                UPDATE CMOVE> ;
: TRANS         'INS  'CHR  #INS @ 'ELIN 'CHR - MIN
                UPDATE MOVE ;
: INS           BUDGE  TRANS  #INS @ CHR +! ;
: BLANK ( a n)  UPDATE  0 ?DO BL OVER C! 1+ LOOP DROP ;
: 'FOUND ( - a)
                'CHR #FND @ - 'LIN UMAX ;
: COLLAPSE      'CHR  'FOUND  'ELIN 'CHR -  UPDATE CMOVE ;
: BLANK-END     'ELIN #FND @ -  'ELIN OVER -  BLANK ;
: ERS           COLLAPSE  'FOUND 0 LINE - CHR !  BLANK-END ;
: SQUEEZE       'LIN  DUP 64 +  0 LINE 1024 + OVER -
                UPDATE CMOVE> ;
: UNSQUEEZE     'LIN 64 +  'LIN  0 LINE 1024 + 'LIN 64 + -
                UPDATE CMOVE  15 LINE 64 BLANK ;

: >>> ( - a n ?)
                [CHAR] ^ PARSE 64 MIN DUP 0<> ;
: >FND          >>> IF DUP #FND ! 'FND SWAP MOVE
                    ELSE 2DROP THEN ;
: >INS          >>> IF DUP #INS ! 'INS SWAP MOVE
                    ELSE 2DROP THEN ;

: WIPE          0 LINE 1024 BLANK ;
: N             1 SCR +! ;
: B             -1 SCR +! ;
: L             SCR @ LIST  EDITOR ;
: T ( n)        6 LSHIFT CHR !  TYPE-LINE  EDITOR ;
: F             >FND FND  TYPE-LINE ;
: I             >INS INS  TYPE-LINE ;
: E             ERS  TYPE-LINE ;
: D             >FND FND  E ;
: R             ERS I ;
: P             #LIN 6 LSHIFT CHR !  'LIN 64 BLANK  >INS INS ;
: U             >INS  #LIN 1+ 6 LSHIFT CHR !  1024 CHR @ <>
                IF  SQUEEZE  'LIN 64 BLANK  INS  THEN ;
: X             'LIN 'INS 64 MOVE  64 #INS !  UNSQUEEZE
                'LIN 0 LINE - CHR ! ;
: K             64. ?DO 'INS I + 'FND I + CSWAP! LOOP
                #INS #FND SWAP! ;

FORTH-WORDLIST SET-CURRENT

: L             L ;
: T             T ;
: WIPE          WIPE ;

PREVIOUS
