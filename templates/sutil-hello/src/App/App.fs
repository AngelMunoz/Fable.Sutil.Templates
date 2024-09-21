module App

open Sutil

let view() = Html.div [ text "Hello World" ]

view() |> Program.mount