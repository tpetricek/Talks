namespace Movies

open System

type Cast = 
  { Actor : string
    Character : string }

type Movie = 
  { Title : string 
    Released : DateTime 
    Cast : Cast[] }