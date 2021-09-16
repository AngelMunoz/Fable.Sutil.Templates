module Components.Navbar

open Sutil
open Sutil.Attr
open Types


let Navbar (onBackRequested: _ -> unit) (goToPage: Page -> _ -> unit) =
  Html.nav [
    Html.button [
      onClick onBackRequested []
      Html.text "Back"
    ]
    Html.button [
      onClick (goToPage Page.Home) []
      Html.text "Home"
    ]
    Html.button [
      onClick (goToPage Page.Notes) []
      Html.text "Notes"
    ]
  ]
