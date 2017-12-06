- title : Functional-First Programming with F#
- description : The F# language is nowadays described as a functional-first programming 
    language. Is this just a marketing trick to make you think that F# is somehow special, 
    or does the F# style of programming really differ from other functional languages enough 
    that it is worth using a new functional-first label? I will try to give you an answer 
    in this talk! Expect a mix of practical hands-on code samples that illustrate the F# 
    style of programming, ramblings on the philosophy behind F#, and extracts from case 
    studies based on large projects completed using the F# language.
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************

# _Functional-First_ <br/>Programming with F#

<br />
<br />
<br />
<br />

#### **Tomas Petricek**, Alan Turing Institute + fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)

***************************************************************************************************
 - class : wordcloud

software stacks _trainings_

_mac and linux_ **cross platform** tutorials

## F# Software Foundation

user groups **open source** _Xamarin_

community **[www.fsharp.org](http://www.fsharp.org)** research

support  _contributions_ diversity

***************************************************************************************************

## _FUNCTIONAL_

***************************************************************************************************
- data-background:images/function.jpg

***************************************************************************************************
 
A _function_ from $A$ to $B$ is an object $f$   
such that every $a\in A$ is uniquely associated  
with an object $f(a)\in B$.

***************************************************************************************************
- data-background:images/bauhaus.jpg

***************************************************************************************************

A _function_ is the purpose for which  
something is designed or exists.

***************************************************************************************************

## _Form_ follows _function_

***************************************************************************************************

# _#1 - TYPE PROVIDERS_

***************************************************************************************************

### _SIMPLICITY_

The elimination of ornament

***************************************************************************************************
- data-background:images/decor.jpg

***************************************************************************************************

```csharp
public interface FactoryFactory<T> {
  public Factory<T> getFactory();
}
```

***************************************************************************************************
  
```haskell
newtype StateT s m a =
  StateT { runStateT :: (s -> m (a,s)) }

instance (Monad m) => Monad (StateT s m) where
  return a = StateT $ \s -> return (a,s)
  (StateT x) >>= f = StateT $ \s -> x s >>= \(v,s') ->
    runStateT (f v) s'     
```    

***************************************************************************************************

_Dot-driven_ development

_F# Data_ library for data access

Custom _type providers_

***************************************************************************************************

# _#2 - DOMAIN MODELING_

***************************************************************************************************

### _MATERIALS_

Enough strudiness to be able to build it

***************************************************************************************************
- data-background:images/masp.jpg

***************************************************************************************************

Types to _model the domain_

Behind the scenes of _Elm-style_

***************************************************************************************************

# _#3 - COMPOSITION_

***************************************************************************************************

### _MATERIALS_

Variability to design whatever you need

***************************************************************************************************
- data-background:images/brasilia.jpg

***************************************************************************************************

_Suave_ and _Giraffe_ libraries

_Composing_ web parts from web parts

***************************************************************************************************

## _SUMMARY_

***************************************************************************************************

Modernism is associated with an analytical approach to the _function_
of buildings, a strictly rational use of _new materials_, an openness to 
_structural innovation_ and the _elimination of ornament_.

***************************************************************************************************

## Functional-First Programming

<br />

_Function_ as in purpose

_Form_ follows _function_

_Abstraction_ vs _ornament_

<br />
<br />

#### **Tomas Petricek**, Alan Turing Institute + fsharpWorks <br /> [@tomaspetricek](http://twitter.com/tomaspetricek) | [tomasp.net](http://tomasp.net) | [fsharpworks.com](http://fsharpworks.com)
