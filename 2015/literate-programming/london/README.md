[![Issue Stats](http://issuestats.com/github/fsprojects/FsReveal/badge/issue)](http://issuestats.com/github/fsprojects/FsReveal)
[![Issue Stats](http://issuestats.com/github/fsprojects/FsReveal/badge/pr)](http://issuestats.com/github/fsprojects/FsReveal)

# FsReveal [![NuGet Status](http://img.shields.io/nuget/v/FsReveal.svg?style=flat)](https://www.nuget.org/packages/FsReveal/)

FsReveal allows you to write beautiful slides in [Markdown](http://daringfireball.net/projects/markdown/syntax)
and brings F# to the [reveal.js][revealjs] web presentation framework.

## Features

- Write your slides in [Markdown](http://daringfireball.net/projects/markdown/syntax) or .fsx files
- Syntax highlighting for most programming languages including C#, F# and LaTeX
- Speaker notes; Shows the current slide, next slide, elapsed time and current time
- Built in themes
- Horizontal and vertical slides
- Built in slide transitions using CSS 3D transforms
- Slide overview
- Works on mobile browsers. Swipe your way through the presentation.

## Getting Started

Clone or [download](https://github.com/fsprojects/FsReveal/archive/master.zip) the [FsReveal repository](https://github.com/fsprojects/FsReveal). Check out GitHub's links to the right if you need some help.

### Prerequisites

If you are using Linux or OS X, [you need to install Mono](http://www.mono-project.com/download/ "Install Mono").

Windows users don't need to install anything.

### Create

Open `slides/input.md` in your favourite text editor.

This is the source for your entire presentation. Inside you will find an example presentation that demonstrates how to use FsReveal.

#### Images

Put images into the slides/images folder.

### Styling

Create a custom stylesheet in `slides/custom.css`.

### Build

Open a console/terminal in the FsReveal folder.

If you're using Windows run 
    
    build.cmd
    
If you're using a Mac run

    ./build.sh
    
This will download all of the packages that FsReveal needs to create your slides.

Your slides are then generated and saved to the `output` folder.

A web server will start automatically and your presentation will be opened in your browser.

FsReveal will detect changes to your slides and generate them again for you; you just need to refresh your browser.

> We're working on auto refreshing the browser for you.

### Use

- Use the arrow keys to navigate left, right, up and down
- Press `Esc` to see an overview
- Press `f` to view in fullscreen
- Press `s` to see speaker notes.

## Examples

Check out what others have created. Submit a PR if you have something to add to the list.

- [Markdown example][md-example] by [@kimsk][kimsk-twitter] [(source)][md-example-source]
- [.fsx example][fsx-example] by [@kimsk][kimsk-twitter] [(source)][fsx-example-source]
- [RPG F# Workshop][rpg-fsharp-workshop] by [@troykershaw][troykershaw-twitter] [(source)][rpg-fsharp-workshop-source]
- [F# on the Web - 0 to production in 12 weeks][fsharp-on-the-web] by [@panesofglass][panesofglass-twitter] [(source)][fsharp-on-the-web-source]


[revealjs]: https://github.com/hakimel/reveal.js/ "reveal.js | HTML presentations made easy"

[kimsk-twitter]: https://twitter.com/kimsk "@kimsk on Twitter"
[troykershaw-twitter]: https://twitter.com/troykershaw "@troykershaw on Twitter"
[panesofglass-twitter]: https://twitter.com/panesofglass "@panesofglass on Twitter"

[fsx-example]: http://kimsk.github.io/fsreveal-sample-fsx/FsReveal.html#/ ".fsx example"
[fsx-example-source]: https://github.com/kimsk/fsreveal-sample-fsx/blob/master/slides/FsReveal.fsx ".fsx example source"

[md-example]: http://kimsk.github.io/fsreveal-sample-md/FsReveal.html#/ "Markdown example"
[md-example-source]: https://github.com/kimsk/fsreveal-sample-md/blob/master/slides/FsReveal.md "Markdown example source"

[rpg-fsharp-workshop]: http://troykershaw.github.io/RpgFsharpWorkshop "RPG F# Workshop" 
[rpg-fsharp-workshop-source]: https://github.com/troykershaw/RpgFsharpWorkshop "RPG F# Workshop source"

[fsharp-on-the-web]: http://panesofglass.github.io/TodoBackendFSharp "F# on the Web - 0 to production in 12 weeks"
[fsharp-on-the-web-source]: https://github.com/panesofglass/TodoBackendFSharp "F# on the Web source"

### Maintainer(s)

- [@kimsk](https://github.com/kimsk)
- [@forki](https://github.com/forki)
- [@troykershaw](https://github.com/troykershaw)
- [@agross](https://github.com/agross)
- [@shishkin](https://github.com/shishkin)
- [@ilkerde](https://github.com/ilkerde)

The default maintainer account for projects under "fsprojects" is [@fsgit](https://github.com/fsgit) - F# Community Project Incubation Space (repo management)
