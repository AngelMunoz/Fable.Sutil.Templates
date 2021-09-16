[<RequireQualifiedAccess>]
module App

open Sutil
open Types
open Components.Navbar

let private app () =
  let page = Store.make Page.Home

  let onBackRequested _ = printfn "Back requested"

  let goToPage newPage _ = page <~ newPage

  let getPage page =
    match page with
    | Page.Home -> Html.custom ("flit-home", [])
    | Page.Notes -> Html.custom ("flit-notes", [])

  Html.app [
    Html.article [
      Navbar onBackRequested goToPage
      Bind.fragment page getPage
    ]
  ]

let start () =
  Program.mountElement "sutil-app" (app ())
