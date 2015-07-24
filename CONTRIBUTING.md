# How to contribute

So you'd like to contribute to EPPlusEnumerable? Awesome!

I want to make it as easy as possible to contribute to EPPlusEnumerable, 
but please review the guidelines below for a greater chance of having your
pull request merged into the main repository.

## Making Changes

* Submit pull requests of logical units.
  * Please don't make a whole slew of unrelated changes in one big pull request. If you
    want to add a feature, make a single pull request for that change, without any other
    unrelated changes included.
* Suggest any changes in an Issue before submitting the pull request
  * I have a vision for how I want the EPPlusEnumerable API to be shaped,
    and there will be a better chance for your work getting merged into the main
    repository if you discuss it first, so we can agree on naming, etc.
* Follow the style and conventions of the existing code
  * I have tried my best to comply with the [.NET Framework Guidelines and Best Practices][1]
    in the naming conventions and formatting style of the EPPlusEnumerable codebase. 
    If you use hungarian notation or some other style that is not in 
    line with the rest of the code, your pull request is less likely to get merged.
* Add Unit Tests
  * After doing all of the above, it'd be awesome if you added a few unit tests to the
    test project (which is right there in the EPPlusEnumerable solution) so I can easily
    verify that no bugs were introduced before I publish out a new version to NuGet.

[1]: https://msdn.microsoft.com/en-us/library/ms731197%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396

Thanks for contributing!
