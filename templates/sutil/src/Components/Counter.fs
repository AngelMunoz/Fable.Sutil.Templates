module Components.Counter

open Sutil
open Sutil.DOM
open Sutil.Attr

let Counter (init: int option) =
  let count = Store.make (defaultArg init 0)

  Html.div [
    disposeOnUnmount [ count ]
    Bind.fragment count (fun counter -> Html.text $"{counter}")
    Html.br []
    Html.button [
      onClick (fun _ -> count <~= (fun count -> count + 1)) []
      Html.text "Increment"
    ]
    Html.button [
      onClick (fun _ -> count <~= (fun count -> count - 1)) []
      Html.text "Decrement"
    ]
    Html.button [
      onClick (fun _ -> count <~= (fun _ -> defaultArg init 0)) []
      Html.text "Reset"
    ]
  ]
