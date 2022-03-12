: HUH ( a n)  TYPE [CHAR] ? EMIT ABORT ;
: CHOOSE ( ? x1 x2 - x1|x2)  ROT IF SWAP THEN NIP ;
: LIN ( - n)  CHR @ 6 RSHIFT  15 MIN ;
: ^LIN ( - n)  LIN 6 LSHIFT ;  : LIN^ ( - n)  LIN 1+ 6 LSHIFT ;
: PRI? ( c - ?)  32 128 WITHIN ;
: SCR@ ( n - c)  SCR @ BLOCK + C@ ;
: PRI ( n1 n2)  ?DO I SCR@ DUP PRI? SWAP BL CHOOSE LOOP ;
: LIN.  LIN 0 .R  9 EMIT ;
: TYP  CR LIN.  ^LIN CHR @ PRI  [CHAR] ^ EMIT  CHR @ LIN^ PRI ;
: L  PAGE  SCR @ LIST ;
: T ( n)  64 * DUP  CHR !  TYP ;
: TRAIL ( - a n)  CHR @ SCR@  LIN^ CHR @ - ;
: -FOUND  'FI #FI @ HUH ;
: FND ( a1 n)  TRAIL 2SWAP SEARCH NIP
               IF ( FIXME -- start of string is wrong, should be end) 0 SCR@ - CHR !  ELSE -FOUND THEN ;
: F  >FI FND ;
: R  E I ;
