#if BEHAVIOURS
namespace FSketch.Behaviours
#else
namespace FSketch
#endif

open NumericOps

module Colors =

    let Transparent = ArgbColor { Alpha = ofFloat 0.000000; R = ofFloat 1.000000; G = ofFloat 1.000000; B = ofFloat 1.000000 }
    let AliceBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.941176; G = ofFloat 0.972549; B = ofFloat 1.000000 }
    let AntiqueWhite = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.980392; G = ofFloat 0.921569; B = ofFloat 0.843137 }
    let Aqua = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 1.000000; B = ofFloat 1.000000 }
    let Aquamarine = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.498039; G = ofFloat 1.000000; B = ofFloat 0.831373 }
    let Azure = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.941176; G = ofFloat 1.000000; B = ofFloat 1.000000 }
    let Beige = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.960784; G = ofFloat 0.960784; B = ofFloat 0.862745 }
    let Bisque = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.894118; B = ofFloat 0.768627 }
    let Black = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.000000; B = ofFloat 0.000000 }
    let BlanchedAlmond = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.921569; B = ofFloat 0.803922 }
    let Blue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.000000; B = ofFloat 1.000000 }
    let BlueViolet = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.541176; G = ofFloat 0.168627; B = ofFloat 0.886275 }
    let Brown = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.647059; G = ofFloat 0.164706; B = ofFloat 0.164706 }
    let BurlyWood = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.870588; G = ofFloat 0.721569; B = ofFloat 0.529412 }
    let CadetBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.372549; G = ofFloat 0.619608; B = ofFloat 0.627451 }
    let Chartreuse = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.498039; G = ofFloat 1.000000; B = ofFloat 0.000000 }
    let Chocolate = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.823529; G = ofFloat 0.411765; B = ofFloat 0.117647 }
    let Coral = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.498039; B = ofFloat 0.313725 }
    let CornflowerBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.392157; G = ofFloat 0.584314; B = ofFloat 0.929412 }
    let Cornsilk = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.972549; B = ofFloat 0.862745 }
    let Crimson = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.862745; G = ofFloat 0.078431; B = ofFloat 0.235294 }
    let Cyan = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 1.000000; B = ofFloat 1.000000 }
    let DarkBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.000000; B = ofFloat 0.545098 }
    let DarkCyan = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.545098; B = ofFloat 0.545098 }
    let DarkGoldenrod = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.721569; G = ofFloat 0.525490; B = ofFloat 0.043137 }
    let DarkGray = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.662745; G = ofFloat 0.662745; B = ofFloat 0.662745 }
    let DarkGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.392157; B = ofFloat 0.000000 }
    let DarkKhaki = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.741176; G = ofFloat 0.717647; B = ofFloat 0.419608 }
    let DarkMagenta = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.545098; G = ofFloat 0.000000; B = ofFloat 0.545098 }
    let DarkOliveGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.333333; G = ofFloat 0.419608; B = ofFloat 0.184314 }
    let DarkOrange = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.549020; B = ofFloat 0.000000 }
    let DarkOrchid = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.600000; G = ofFloat 0.196078; B = ofFloat 0.800000 }
    let DarkRed = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.545098; G = ofFloat 0.000000; B = ofFloat 0.000000 }
    let DarkSalmon = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.913725; G = ofFloat 0.588235; B = ofFloat 0.478431 }
    let DarkSeaGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.560784; G = ofFloat 0.737255; B = ofFloat 0.545098 }
    let DarkSlateBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.282353; G = ofFloat 0.239216; B = ofFloat 0.545098 }
    let DarkSlateGray = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.184314; G = ofFloat 0.309804; B = ofFloat 0.309804 }
    let DarkTurquoise = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.807843; B = ofFloat 0.819608 }
    let DarkViolet = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.580392; G = ofFloat 0.000000; B = ofFloat 0.827451 }
    let DeepPink = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.078431; B = ofFloat 0.576471 }
    let DeepSkyBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.749020; B = ofFloat 1.000000 }
    let DimGray = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.411765; G = ofFloat 0.411765; B = ofFloat 0.411765 }
    let DodgerBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.117647; G = ofFloat 0.564706; B = ofFloat 1.000000 }
    let Firebrick = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.698039; G = ofFloat 0.133333; B = ofFloat 0.133333 }
    let FloralWhite = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.980392; B = ofFloat 0.941176 }
    let ForestGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.133333; G = ofFloat 0.545098; B = ofFloat 0.133333 }
    let Fuchsia = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.000000; B = ofFloat 1.000000 }
    let Gainsboro = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.862745; G = ofFloat 0.862745; B = ofFloat 0.862745 }
    let GhostWhite = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.972549; G = ofFloat 0.972549; B = ofFloat 1.000000 }
    let Gold = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.843137; B = ofFloat 0.000000 }
    let Goldenrod = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.854902; G = ofFloat 0.647059; B = ofFloat 0.125490 }
    let Gray = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.501961; G = ofFloat 0.501961; B = ofFloat 0.501961 }
    let Green = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.501961; B = ofFloat 0.000000 }
    let GreenYellow = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.678431; G = ofFloat 1.000000; B = ofFloat 0.184314 }
    let Honeydew = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.941176; G = ofFloat 1.000000; B = ofFloat 0.941176 }
    let HotPink = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.411765; B = ofFloat 0.705882 }
    let IndianRed = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.803922; G = ofFloat 0.360784; B = ofFloat 0.360784 }
    let Indigo = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.294118; G = ofFloat 0.000000; B = ofFloat 0.509804 }
    let Ivory = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 1.000000; B = ofFloat 0.941176 }
    let Khaki = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.941176; G = ofFloat 0.901961; B = ofFloat 0.549020 }
    let Lavender = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.901961; G = ofFloat 0.901961; B = ofFloat 0.980392 }
    let LavenderBlush = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.941176; B = ofFloat 0.960784 }
    let LawnGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.486275; G = ofFloat 0.988235; B = ofFloat 0.000000 }
    let LemonChiffon = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.980392; B = ofFloat 0.803922 }
    let LightBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.678431; G = ofFloat 0.847059; B = ofFloat 0.901961 }
    let LightCoral = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.941176; G = ofFloat 0.501961; B = ofFloat 0.501961 }
    let LightCyan = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.878431; G = ofFloat 1.000000; B = ofFloat 1.000000 }
    let LightGoldenrodYellow = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.980392; G = ofFloat 0.980392; B = ofFloat 0.823529 }
    let LightGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.564706; G = ofFloat 0.933333; B = ofFloat 0.564706 }
    let LightGray = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.827451; G = ofFloat 0.827451; B = ofFloat 0.827451 }
    let LightPink = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.713725; B = ofFloat 0.756863 }
    let LightSalmon = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.627451; B = ofFloat 0.478431 }
    let LightSeaGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.125490; G = ofFloat 0.698039; B = ofFloat 0.666667 }
    let LightSkyBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.529412; G = ofFloat 0.807843; B = ofFloat 0.980392 }
    let LightSlateGray = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.466667; G = ofFloat 0.533333; B = ofFloat 0.600000 }
    let LightSteelBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.690196; G = ofFloat 0.768627; B = ofFloat 0.870588 }
    let LightYellow = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 1.000000; B = ofFloat 0.878431 }
    let Lime = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 1.000000; B = ofFloat 0.000000 }
    let LimeGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.196078; G = ofFloat 0.803922; B = ofFloat 0.196078 }
    let Linen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.980392; G = ofFloat 0.941176; B = ofFloat 0.901961 }
    let Magenta = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.000000; B = ofFloat 1.000000 }
    let Maroon = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.501961; G = ofFloat 0.000000; B = ofFloat 0.000000 }
    let MediumAquamarine = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.400000; G = ofFloat 0.803922; B = ofFloat 0.666667 }
    let MediumBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.000000; B = ofFloat 0.803922 }
    let MediumOrchid = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.729412; G = ofFloat 0.333333; B = ofFloat 0.827451 }
    let MediumPurple = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.576471; G = ofFloat 0.439216; B = ofFloat 0.858824 }
    let MediumSeaGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.235294; G = ofFloat 0.701961; B = ofFloat 0.443137 }
    let MediumSlateBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.482353; G = ofFloat 0.407843; B = ofFloat 0.933333 }
    let MediumSpringGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.980392; B = ofFloat 0.603922 }
    let MediumTurquoise = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.282353; G = ofFloat 0.819608; B = ofFloat 0.800000 }
    let MediumVioletRed = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.780392; G = ofFloat 0.082353; B = ofFloat 0.521569 }
    let MidnightBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.098039; G = ofFloat 0.098039; B = ofFloat 0.439216 }
    let MintCream = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.960784; G = ofFloat 1.000000; B = ofFloat 0.980392 }
    let MistyRose = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.894118; B = ofFloat 0.882353 }
    let Moccasin = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.894118; B = ofFloat 0.709804 }
    let NavajoWhite = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.870588; B = ofFloat 0.678431 }
    let Navy = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.000000; B = ofFloat 0.501961 }
    let OldLace = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.992157; G = ofFloat 0.960784; B = ofFloat 0.901961 }
    let Olive = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.501961; G = ofFloat 0.501961; B = ofFloat 0.000000 }
    let OliveDrab = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.419608; G = ofFloat 0.556863; B = ofFloat 0.137255 }
    let Orange = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.647059; B = ofFloat 0.000000 }
    let OrangeRed = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.270588; B = ofFloat 0.000000 }
    let Orchid = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.854902; G = ofFloat 0.439216; B = ofFloat 0.839216 }
    let PaleGoldenrod = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.933333; G = ofFloat 0.909804; B = ofFloat 0.666667 }
    let PaleGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.596078; G = ofFloat 0.984314; B = ofFloat 0.596078 }
    let PaleTurquoise = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.686275; G = ofFloat 0.933333; B = ofFloat 0.933333 }
    let PaleVioletRed = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.858824; G = ofFloat 0.439216; B = ofFloat 0.576471 }
    let PapayaWhip = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.937255; B = ofFloat 0.835294 }
    let PeachPuff = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.854902; B = ofFloat 0.725490 }
    let Peru = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.803922; G = ofFloat 0.521569; B = ofFloat 0.247059 }
    let Pink = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.752941; B = ofFloat 0.796078 }
    let Plum = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.866667; G = ofFloat 0.627451; B = ofFloat 0.866667 }
    let PowderBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.690196; G = ofFloat 0.878431; B = ofFloat 0.901961 }
    let Purple = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.501961; G = ofFloat 0.000000; B = ofFloat 0.501961 }
    let Red = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.000000; B = ofFloat 0.000000 }
    let RosyBrown = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.737255; G = ofFloat 0.560784; B = ofFloat 0.560784 }
    let RoyalBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.254902; G = ofFloat 0.411765; B = ofFloat 0.882353 }
    let SaddleBrown = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.545098; G = ofFloat 0.270588; B = ofFloat 0.074510 }
    let Salmon = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.980392; G = ofFloat 0.501961; B = ofFloat 0.447059 }
    let SandyBrown = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.956863; G = ofFloat 0.643137; B = ofFloat 0.376471 }
    let SeaGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.180392; G = ofFloat 0.545098; B = ofFloat 0.341176 }
    let SeaShell = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.960784; B = ofFloat 0.933333 }
    let Sienna = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.627451; G = ofFloat 0.321569; B = ofFloat 0.176471 }
    let Silver = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.752941; G = ofFloat 0.752941; B = ofFloat 0.752941 }
    let SkyBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.529412; G = ofFloat 0.807843; B = ofFloat 0.921569 }
    let SlateBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.415686; G = ofFloat 0.352941; B = ofFloat 0.803922 }
    let SlateGray = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.439216; G = ofFloat 0.501961; B = ofFloat 0.564706 }
    let Snow = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.980392; B = ofFloat 0.980392 }
    let SpringGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 1.000000; B = ofFloat 0.498039 }
    let SteelBlue = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.274510; G = ofFloat 0.509804; B = ofFloat 0.705882 }
    let Tan = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.823529; G = ofFloat 0.705882; B = ofFloat 0.549020 }
    let Teal = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.000000; G = ofFloat 0.501961; B = ofFloat 0.501961 }
    let Thistle = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.847059; G = ofFloat 0.749020; B = ofFloat 0.847059 }
    let Tomato = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 0.388235; B = ofFloat 0.278431 }
    let Turquoise = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.250980; G = ofFloat 0.878431; B = ofFloat 0.815686 }
    let Violet = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.933333; G = ofFloat 0.509804; B = ofFloat 0.933333 }
    let Wheat = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.960784; G = ofFloat 0.870588; B = ofFloat 0.701961 }
    let White = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 1.000000; B = ofFloat 1.000000 }
    let WhiteSmoke = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.960784; G = ofFloat 0.960784; B = ofFloat 0.960784 }
    let Yellow = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 1.000000; G = ofFloat 1.000000; B = ofFloat 0.000000 }
    let YellowGreen = ArgbColor { Alpha = ofFloat 1.000000; R = ofFloat 0.603922; G = ofFloat 0.803922; B = ofFloat 0.196078 }

module Pens =
   let AliceBlue = { Color = Colors.AliceBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let AntiqueWhite = { Color = Colors.AntiqueWhite; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Aqua = { Color = Colors.Aqua; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Aquamarine = { Color = Colors.Aquamarine; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Azure = { Color = Colors.Azure; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Beige = { Color = Colors.Beige; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Bisque = { Color = Colors.Bisque; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Black = { Color = Colors.Black; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let BlanchedAlmond = { Color = Colors.BlanchedAlmond; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Blue = { Color = Colors.Blue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let BlueViolet = { Color = Colors.BlueViolet; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Brown = { Color = Colors.Brown; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let BurlyWood = { Color = Colors.BurlyWood; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let CadetBlue = { Color = Colors.CadetBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Chartreuse = { Color = Colors.Chartreuse; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Chocolate = { Color = Colors.Chocolate; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Coral = { Color = Colors.Coral; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let CornflowerBlue = { Color = Colors.CornflowerBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Cornsilk = { Color = Colors.Cornsilk; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Crimson = { Color = Colors.Crimson; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Cyan = { Color = Colors.Cyan; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkBlue = { Color = Colors.DarkBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkCyan = { Color = Colors.DarkCyan; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkGoldenrod = { Color = Colors.DarkGoldenrod; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkGray = { Color = Colors.DarkGray; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkGreen = { Color = Colors.DarkGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkKhaki = { Color = Colors.DarkKhaki; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkMagenta = { Color = Colors.DarkMagenta; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkOliveGreen = { Color = Colors.DarkOliveGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkOrange = { Color = Colors.DarkOrange; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkOrchid = { Color = Colors.DarkOrchid; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkRed = { Color = Colors.DarkRed; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkSalmon = { Color = Colors.DarkSalmon; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkSeaGreen = { Color = Colors.DarkSeaGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkSlateBlue = { Color = Colors.DarkSlateBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkSlateGray = { Color = Colors.DarkSlateGray; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkTurquoise = { Color = Colors.DarkTurquoise; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DarkViolet = { Color = Colors.DarkViolet; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DeepPink = { Color = Colors.DeepPink; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DeepSkyBlue = { Color = Colors.DeepSkyBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DimGray = { Color = Colors.DimGray; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let DodgerBlue = { Color = Colors.DodgerBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Firebrick = { Color = Colors.Firebrick; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let FloralWhite = { Color = Colors.FloralWhite; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let ForestGreen = { Color = Colors.ForestGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Fuchsia = { Color = Colors.Fuchsia; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Gainsboro = { Color = Colors.Gainsboro; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let GhostWhite = { Color = Colors.GhostWhite; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Gold = { Color = Colors.Gold; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Goldenrod = { Color = Colors.Goldenrod; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Gray = { Color = Colors.Gray; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Green = { Color = Colors.Green; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let GreenYellow = { Color = Colors.GreenYellow; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Honeydew = { Color = Colors.Honeydew; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let HotPink = { Color = Colors.HotPink; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let IndianRed = { Color = Colors.IndianRed; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Indigo = { Color = Colors.Indigo; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Ivory = { Color = Colors.Ivory; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Khaki = { Color = Colors.Khaki; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Lavender = { Color = Colors.Lavender; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LavenderBlush = { Color = Colors.LavenderBlush; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LawnGreen = { Color = Colors.LawnGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LemonChiffon = { Color = Colors.LemonChiffon; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightBlue = { Color = Colors.LightBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightCoral = { Color = Colors.LightCoral; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightCyan = { Color = Colors.LightCyan; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightGoldenrodYellow = { Color = Colors.LightGoldenrodYellow; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightGreen = { Color = Colors.LightGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightGray = { Color = Colors.LightGray; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightPink = { Color = Colors.LightPink; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightSalmon = { Color = Colors.LightSalmon; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightSeaGreen = { Color = Colors.LightSeaGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightSkyBlue = { Color = Colors.LightSkyBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightSlateGray = { Color = Colors.LightSlateGray; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightSteelBlue = { Color = Colors.LightSteelBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LightYellow = { Color = Colors.LightYellow; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Lime = { Color = Colors.Lime; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let LimeGreen = { Color = Colors.LimeGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Linen = { Color = Colors.Linen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Magenta = { Color = Colors.Magenta; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Maroon = { Color = Colors.Maroon; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumAquamarine = { Color = Colors.MediumAquamarine; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumBlue = { Color = Colors.MediumBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumOrchid = { Color = Colors.MediumOrchid; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumPurple = { Color = Colors.MediumPurple; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumSeaGreen = { Color = Colors.MediumSeaGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumSlateBlue = { Color = Colors.MediumSlateBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumSpringGreen = { Color = Colors.MediumSpringGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumTurquoise = { Color = Colors.MediumTurquoise; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MediumVioletRed = { Color = Colors.MediumVioletRed; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MidnightBlue = { Color = Colors.MidnightBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MintCream = { Color = Colors.MintCream; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let MistyRose = { Color = Colors.MistyRose; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Moccasin = { Color = Colors.Moccasin; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let NavajoWhite = { Color = Colors.NavajoWhite; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Navy = { Color = Colors.Navy; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let OldLace = { Color = Colors.OldLace; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Olive = { Color = Colors.Olive; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let OliveDrab = { Color = Colors.OliveDrab; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Orange = { Color = Colors.Orange; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let OrangeRed = { Color = Colors.OrangeRed; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Orchid = { Color = Colors.Orchid; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let PaleGoldenrod = { Color = Colors.PaleGoldenrod; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let PaleGreen = { Color = Colors.PaleGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let PaleTurquoise = { Color = Colors.PaleTurquoise; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let PaleVioletRed = { Color = Colors.PaleVioletRed; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let PapayaWhip = { Color = Colors.PapayaWhip; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let PeachPuff = { Color = Colors.PeachPuff; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Peru = { Color = Colors.Peru; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Pink = { Color = Colors.Pink; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Plum = { Color = Colors.Plum; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let PowderBlue = { Color = Colors.PowderBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Purple = { Color = Colors.Purple; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Red = { Color = Colors.Red; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let RosyBrown = { Color = Colors.RosyBrown; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let RoyalBlue = { Color = Colors.RoyalBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SaddleBrown = { Color = Colors.SaddleBrown; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Salmon = { Color = Colors.Salmon; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SandyBrown = { Color = Colors.SandyBrown; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SeaGreen = { Color = Colors.SeaGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SeaShell = { Color = Colors.SeaShell; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Sienna = { Color = Colors.Sienna; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Silver = { Color = Colors.Silver; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SkyBlue = { Color = Colors.SkyBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SlateBlue = { Color = Colors.SlateBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SlateGray = { Color = Colors.SlateGray; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Snow = { Color = Colors.Snow; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SpringGreen = { Color = Colors.SpringGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let SteelBlue = { Color = Colors.SteelBlue; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Tan = { Color = Colors.Tan; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Teal = { Color = Colors.Teal; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Thistle = { Color = Colors.Thistle; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Tomato = { Color = Colors.Tomato; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Turquoise = { Color = Colors.Turquoise; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Violet = { Color = Colors.Violet; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Wheat = { Color = Colors.Wheat; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let White = { Color = Colors.White; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let WhiteSmoke = { Color = Colors.WhiteSmoke; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Yellow = { Color = Colors.Yellow; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let YellowGreen = { Color = Colors.YellowGreen; Thickness = ofFloat 1.0; LineJoin = LineJoin.Round }
   let Default = Black

module Brushes =
   let Solid = { Color = Colors.Transparent }
   let AliceBlue = { Color = Colors.AliceBlue }
   let AntiqueWhite = { Color = Colors.AntiqueWhite }
   let Aqua = { Color = Colors.Aqua }
   let Aquamarine = { Color = Colors.Aquamarine }
   let Azure = { Color = Colors.Azure }
   let Beige = { Color = Colors.Beige }
   let Bisque = { Color = Colors.Bisque }
   let Black = { Color = Colors.Black }
   let BlanchedAlmond = { Color = Colors.BlanchedAlmond }
   let Blue = { Color = Colors.Blue }
   let BlueViolet = { Color = Colors.BlueViolet }
   let Brown = { Color = Colors.Brown }
   let BurlyWood = { Color = Colors.BurlyWood }
   let CadetBlue = { Color = Colors.CadetBlue }
   let Chartreuse = { Color = Colors.Chartreuse }
   let Chocolate = { Color = Colors.Chocolate }
   let Coral = { Color = Colors.Coral }
   let CornflowerBlue = { Color = Colors.CornflowerBlue }
   let Cornsilk = { Color = Colors.Cornsilk }
   let Crimson = { Color = Colors.Crimson }
   let Cyan = { Color = Colors.Cyan }
   let DarkBlue = { Color = Colors.DarkBlue }
   let DarkCyan = { Color = Colors.DarkCyan }
   let DarkGoldenrod = { Color = Colors.DarkGoldenrod }
   let DarkGray = { Color = Colors.DarkGray }
   let DarkGreen = { Color = Colors.DarkGreen }
   let DarkKhaki = { Color = Colors.DarkKhaki }
   let DarkMagenta = { Color = Colors.DarkMagenta }
   let DarkOliveGreen = { Color = Colors.DarkOliveGreen }
   let DarkOrange = { Color = Colors.DarkOrange }
   let DarkOrchid = { Color = Colors.DarkOrchid }
   let DarkRed = { Color = Colors.DarkRed }
   let DarkSalmon = { Color = Colors.DarkSalmon }
   let DarkSeaGreen = { Color = Colors.DarkSeaGreen }
   let DarkSlateBlue = { Color = Colors.DarkSlateBlue }
   let DarkSlateGray = { Color = Colors.DarkSlateGray }
   let DarkTurquoise = { Color = Colors.DarkTurquoise }
   let DarkViolet = { Color = Colors.DarkViolet }
   let DeepPink = { Color = Colors.DeepPink }
   let DeepSkyBlue = { Color = Colors.DeepSkyBlue }
   let DimGray = { Color = Colors.DimGray }
   let DodgerBlue = { Color = Colors.DodgerBlue }
   let Firebrick = { Color = Colors.Firebrick }
   let FloralWhite = { Color = Colors.FloralWhite }
   let ForestGreen = { Color = Colors.ForestGreen }
   let Fuchsia = { Color = Colors.Fuchsia }
   let Gainsboro = { Color = Colors.Gainsboro }
   let GhostWhite = { Color = Colors.GhostWhite }
   let Gold = { Color = Colors.Gold }
   let Goldenrod = { Color = Colors.Goldenrod }
   let Gray = { Color = Colors.Gray }
   let Green = { Color = Colors.Green }
   let GreenYellow = { Color = Colors.GreenYellow }
   let Honeydew = { Color = Colors.Honeydew }
   let HotPink = { Color = Colors.HotPink }
   let IndianRed = { Color = Colors.IndianRed }
   let Indigo = { Color = Colors.Indigo }
   let Ivory = { Color = Colors.Ivory }
   let Khaki = { Color = Colors.Khaki }
   let Lavender = { Color = Colors.Lavender }
   let LavenderBlush = { Color = Colors.LavenderBlush }
   let LawnGreen = { Color = Colors.LawnGreen }
   let LemonChiffon = { Color = Colors.LemonChiffon }
   let LightBlue = { Color = Colors.LightBlue }
   let LightCoral = { Color = Colors.LightCoral }
   let LightCyan = { Color = Colors.LightCyan }
   let LightGoldenrodYellow = { Color = Colors.LightGoldenrodYellow }
   let LightGreen = { Color = Colors.LightGreen }
   let LightGray = { Color = Colors.LightGray }
   let LightPink = { Color = Colors.LightPink }
   let LightSalmon = { Color = Colors.LightSalmon }
   let LightSeaGreen = { Color = Colors.LightSeaGreen }
   let LightSkyBlue = { Color = Colors.LightSkyBlue }
   let LightSlateGray = { Color = Colors.LightSlateGray }
   let LightSteelBlue = { Color = Colors.LightSteelBlue }
   let LightYellow = { Color = Colors.LightYellow }
   let Lime = { Color = Colors.Lime }
   let LimeGreen = { Color = Colors.LimeGreen }
   let Linen = { Color = Colors.Linen }
   let Magenta = { Color = Colors.Magenta }
   let Maroon = { Color = Colors.Maroon }
   let MediumAquamarine = { Color = Colors.MediumAquamarine }
   let MediumBlue = { Color = Colors.MediumBlue }
   let MediumOrchid = { Color = Colors.MediumOrchid }
   let MediumPurple = { Color = Colors.MediumPurple }
   let MediumSeaGreen = { Color = Colors.MediumSeaGreen }
   let MediumSlateBlue = { Color = Colors.MediumSlateBlue }
   let MediumSpringGreen = { Color = Colors.MediumSpringGreen }
   let MediumTurquoise = { Color = Colors.MediumTurquoise }
   let MediumVioletRed = { Color = Colors.MediumVioletRed }
   let MidnightBlue = { Color = Colors.MidnightBlue }
   let MintCream = { Color = Colors.MintCream }
   let MistyRose = { Color = Colors.MistyRose }
   let Moccasin = { Color = Colors.Moccasin }
   let NavajoWhite = { Color = Colors.NavajoWhite }
   let Navy = { Color = Colors.Navy }
   let OldLace = { Color = Colors.OldLace }
   let Olive = { Color = Colors.Olive }
   let OliveDrab = { Color = Colors.OliveDrab }
   let Orange = { Color = Colors.Orange }
   let OrangeRed = { Color = Colors.OrangeRed }
   let Orchid = { Color = Colors.Orchid }
   let PaleGoldenrod = { Color = Colors.PaleGoldenrod }
   let PaleGreen = { Color = Colors.PaleGreen }
   let PaleTurquoise = { Color = Colors.PaleTurquoise }
   let PaleVioletRed = { Color = Colors.PaleVioletRed }
   let PapayaWhip = { Color = Colors.PapayaWhip }
   let PeachPuff = { Color = Colors.PeachPuff }
   let Peru = { Color = Colors.Peru }
   let Pink = { Color = Colors.Pink }
   let Plum = { Color = Colors.Plum }
   let PowderBlue = { Color = Colors.PowderBlue }
   let Purple = { Color = Colors.Purple }
   let Red = { Color = Colors.Red }
   let RosyBrown = { Color = Colors.RosyBrown }
   let RoyalBlue = { Color = Colors.RoyalBlue }
   let SaddleBrown = { Color = Colors.SaddleBrown }
   let Salmon = { Color = Colors.Salmon }
   let SandyBrown = { Color = Colors.SandyBrown }
   let SeaGreen = { Color = Colors.SeaGreen }
   let SeaShell = { Color = Colors.SeaShell }
   let Sienna = { Color = Colors.Sienna }
   let Silver = { Color = Colors.Silver }
   let SkyBlue = { Color = Colors.SkyBlue }
   let SlateBlue = { Color = Colors.SlateBlue }
   let SlateGray = { Color = Colors.SlateGray }
   let Snow = { Color = Colors.Snow }
   let SpringGreen = { Color = Colors.SpringGreen }
   let SteelBlue = { Color = Colors.SteelBlue }
   let Tan = { Color = Colors.Tan }
   let Teal = { Color = Colors.Teal }
   let Thistle = { Color = Colors.Thistle }
   let Tomato = { Color = Colors.Tomato }
   let Turquoise = { Color = Colors.Turquoise }
   let Violet = { Color = Colors.Violet }
   let Wheat = { Color = Colors.Wheat }
   let White = { Color = Colors.White }
   let WhiteSmoke = { Color = Colors.WhiteSmoke }
   let Yellow = { Color = Colors.Yellow }
   let YellowGreen = { Color = Colors.YellowGreen }
