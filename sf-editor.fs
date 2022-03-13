WORDLIST CONSTANT EDITOR-WL
: EDITOR  GET-ORDER NIP EDITOR-WL SWAP SET-ORDER ;
ALSO EDITOR  EDITOR-WL SET-CURRENT
SYNONYM FORTH FORTH

VARIABLE CHR

: -ROT ( x1 x2 x3 - x3 x1 x2)  ROT ROT ;
: HUH ( a n)  TYPE  [CHAR] ? EMIT  ABORT ;
: CHOOSE ( ? x1 x2 - x1|x2)  ROT IF SWAP THEN NIP ;
: TIB@ ( a)  SOURCE DROP ;  : #TIB ( n)  SOURCE NIP ;

: N  1 SCR +! ;
: B  -1 SCR +! ;
: L  PAGE  SCR @ LIST ;

: LIN ( - n)  CHR @ 6 RSHIFT  15 MIN ;
: ^LIN ( - n)  LIN 6 LSHIFT ;
: LIN^ ( - n)  LIN 1+ 6 LSHIFT ;
: PRI? ( c - ?)  32 128 WITHIN ;
: SCR@ ( n - c)  SCR @ BLOCK + C@ ;
: PRI ( n1 n2)  ?DO I SCR@ DUP PRI? SWAP BL CHOOSE LOOP ;
: TYP  CR  ^LIN CHR @ PRI  [CHAR] ^ EMIT  CHR @ LIN^ PRI
       SPACE LIN . ;
: T ( n)  6 LSHIFT  CHR !  TYP ;
: TRAIL ( - a n)  CHR @ SCR@  LIN^ CHR @ - ;
: -FOUND  'FND #FND @ HUH ;
: FND ( a1 n)  TRAIL 2SWAP SEARCH NIP
               IF ( FIXME -- start of string is wrong, should be end) 0 SCR@ - CHR !  ELSE -FOUND THEN ;
: BLANK ( a n)  0 ?DO BL DUP I + C! LOOP 2DROP ;
: END  #TIB >IN ! ;
: END?  #TIB >IN @ = ;
: >>> ( - a n ?)  TIB@ >IN @ +  #TIB >IN @ -  END? 0=  END ;
: >FND'  >>> IF DUP #FND ! 'FND SWAP MOVE ELSE 2DROP THEN ;
: >INS'  >>> IF DUP #INS ! 'INS SWAP MOVE ELSE 2DROP THEN ;
: >FND ( - a n)  >FND'  'FND #FND @ ;
: >INS ( - a n)  >INS'  'INS #INS @ ;
: F  >FND FND ;
: R  E I ;
: I'  >INS INS ;
: I  I' TYP ;
: P  ^LIN DUP CHR !  SCR@ 64 BLANK  UPDATE  I' ;

PREVIOUS  FORTH-WORDLIST SET-CURRENT
