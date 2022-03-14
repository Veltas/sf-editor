DECIMAL
WORDLIST CONSTANT EDITOR-WL
: EDITOR  GET-ORDER NIP EDITOR-WL SWAP SET-ORDER ;
ALSO EDITOR DEFINITIONS
: FORTH  FORTH ;

VARIABLE CHR
CREATE 'FND 64 ALLOT  VARIABLE #FND
CREATE 'INS 64 ALLOT  VARIABLE #INS

: -ROT ( x1 x2 x3 - x3 x1 x2)  ROT ROT ;
: HUH ( a n)  TYPE  [CHAR] ? EMIT  ABORT ;
: CHOOSE ( ? x1 x2 - x1|x2)  ROT IF SWAP THEN NIP ;

BL WORD TIB FIND NIP 0=  BL WORD #TIB FIND NIP 0=  OR [IF]
: TIB ( a)  SOURCE DROP ;  : #TIB@ ( n)  SOURCE NIP ;
[ELSE]
DUP 0= ?END  : #TIB@ ( - a)  #TIB @ ;
[THEN]

: N  1 SCR +! ;
: B  -1 SCR +! ;
: L  PAGE  SCR @ LIST ;

: LINE ( n - a)  6 LSHIFT  SCR @ BLOCK + ;
: #LIN ( - n)  CHR @ 6 RSHIFT  15 MIN ;
: 'LIN ( - a)  #LIN LINE ;
: 'CHR ( - a)  CHR @  SCR @ BLOCK + ;
: PRI' ( c)  DUP 32 127 WITHIN IF EMIT ELSE DROP SPACE THEN ;
: PRI ( a1 a2)  ?DO I C@ PRI' LOOP ;
: TYPE-LINE  CR  'LIN 'CHR PRI  [CHAR] ^ EMIT
             'CHR 'LIN 64 + PRI  SPACE #LIN . ;
: T ( n)  DUP  6 LSHIFT  CHR !  TYPE-LINE ;
: TRAIL ( - a n)  'CHR  'LIN 64 + 'CHR - ;
: -FOUND  'FND #FND @ HUH ;
: FND ( a n)  TRAIL 2SWAP SEARCH NIP
              IF 0 SCR+ - CHR !  ELSE -FOUND THEN ; ( FIXME -- start of string is wrong, should be end)
: BLANK ( a n)  0 ?DO BL DUP I + C! LOOP 2DROP ;
: END  #TIB@ >IN ! ;
: END? ( - ?)  #TIB@ >IN @ = ;
: >>> ( - a n ?)  TIB >IN @ +  #TIB@ >IN @ -  END? 0=  END ;
: >FND'  >>> IF DUP #FND ! 'FND SWAP MOVE ELSE 2DROP THEN ;
: >INS'  >>> IF DUP #INS ! 'INS SWAP MOVE ELSE 2DROP THEN ;
: >FND ( - a n)  >FND'  'FND #FND @  CR ;
: >INS ( - a n)  >INS'  'INS #INS @  CR ;
: F  >FND FND ;
: I'  >INS INS ;
: I  I' TYP ;
: R  E I ;
: P  ^LIN DUP CHR !  SCR+ 64 BLANK  UPDATE  I' ;

PREVIOUS  FORTH-WORDLIST SET-CURRENT
