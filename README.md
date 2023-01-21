# satsolver
brute-force way to solve boolean satisfiability (SAT) problem.
format of the input string must be in CNF form.    
example 1
  (a|b)&(-a|b)&(a|-b)&(-a|-b)
  
example 2
   (a|b)&(-a|b)&(-a|-b)

a , b etc represent variables,
"-" before variable means negation of variable,
"|" is or,
"&" is and.
