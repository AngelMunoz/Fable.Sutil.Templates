module App

open Sutil
open type Feliz.length  // For CSS units like px

let app() =
    Html.div [
        Attr.style [
            Css.margin (rem 1)
        ]
        text "Hello World"
    ]

app() |> Program.mount