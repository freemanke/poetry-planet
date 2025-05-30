using System;
using Avalonia.Media.Fonts;

namespace PoetryPlanet;

public sealed class InterFontCollection() : EmbeddedFontCollection(
    new Uri("fonts:Inter", UriKind.Absolute),
    new Uri("avares://Avalonia.Fonts.Inter/Assets", UriKind.Absolute));