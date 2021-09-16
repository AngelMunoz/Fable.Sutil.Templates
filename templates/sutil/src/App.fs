module App

open Fable.Core
open Sutil
open Types
open Pages.Home
open Pages.Notes
open Components.Navbar

JsInterop.importSideEffects "./styles.css"

let private app () =
  let page = Store.make (Page.Home)

  let onBackRequested _ = printfn "Back requested"

  let goToPage newPage _ = page <~ newPage

  let getPage page =
    match page with
    | Page.Home -> Home()
    | Page.Notes -> Notes()

  Html.app [
    Navbar onBackRequested goToPage
    Html.main [ Bind.fragment page getPage ]
  ]

Program.mountElement "sutil-app" (app ())
