(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
FSketch
======================

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      The FSketch library can be <a href="https://nuget.org/packages/FSketch">installed from NuGet</a>:
      <pre>PM> Install-Package FSketch</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>

Example
-------

This example demonstrates how the DSL can be used to represent shapes.

*)
#r "FSketch.dll"
open FSketch
open FSketch.Builder
open FSketch.Dsl

let simpleShapes = shapes {
  yield rectangle (150., 50.) |> at origin |> withContour Pens.Black
  yield ellipse (140., 40.) |> at origin |> withContourAndFill (Pens.Blue, Brushes.LightBlue)
  yield rectangle (150., 50.) |> at (200., 0.) |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (200., 0.) |> withContourAndFill (Pens.Red, Brushes.LightPink)
}

(**
Samples & documentation
-----------------------

 * [Tutorial](tutorial.html) contains a further explanation of this sample library.

 * [API Reference](reference/index.html) contains automatically generated documentation for all types, modules
   and functions in the library. This includes additional brief samples on using most of the
   functions.
 
Contributing and copyright
--------------------------

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding a new public API, please also 
consider adding [samples][content] that can be turned into a documentation.

The library is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/fsprojects/FSketch/tree/master/docs/content
  [gh]: https://github.com/fsprojects/FSketch
  [issues]: https://github.com/fsprojects/FSketch/issues
  [readme]: https://github.com/fsprojects/FSketch/blob/master/README.md
  [license]: https://github.com/fsprojects/FSketch/blob/master/LICENSE.txt
*)
